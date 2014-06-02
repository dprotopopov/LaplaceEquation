using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MyCudafy.Collections;
using Double = MyLibrary.Types.Double;
using Int32 = MyLibrary.Types.Int32;

namespace LaplaceEquation
{
    public class NativeLaplaceSolver : LaplaceSolver
    {
        /// <summary>
        ///     На входе многомерный куб с размерностями сторон sizes и шагом сетки lengths
        ///     Пересчитываются только значения внутренних точек
        ///     На каждом итерации внутренней точке присваивается взвешенное среднее соседних по осям точек
        ///     Коэффициенты расчитываются на основании шага сетки и параметра a
        ///     Итерации продолжаются до тех пор пока максимальное изменение не будет меньше epsilon
        ///     Параметр a определяет метод вычисления
        ///     При a == 1 это метод Якоби (диагональные элементы равны нулю)
        ///     При a == 2 это метод Гаусса-Зенделя
        ///     При a == 3 достигается оптимальная верхняя оценка сходимости алгоритма
        ///     (определитель матрицы принимает минимальное значение)
        /// </summary>
        /// <param name="sizes"></param>
        /// <param name="lengths"></param>
        /// <param name="src">Исходный массив</param>
        /// <param name="dest">Итоговый массив</param>
        /// <param name="epsilon">Точность вычислений</param>
        /// <param name="a">Параметр алгоритма для вычисления весовых коэффициентов</param>
        /// <param name="relax">
        ///     При использовании метода релаксации задействовано в два раза меньше памяти и вычисления производятся
        ///     на-месте. Для устанения коллизий с совместным доступом производится раскраска точек красное-чёрное для обработки
        ///     их по-очереди
        /// </param>
        public override IEnumerable<double> Solve(int[] sizes, double[] lengths, double[] src, double[] dest,
            double epsilon, double a, bool relax)
        {
            Debug.Assert(sizes.Length == lengths.Length);
            Debug.Assert(sizes.Aggregate(Int32.Mul) > 0);
            Debug.Assert(lengths.Aggregate(Double.Mul) > 0.0);
            Debug.Assert(sizes.Aggregate(Int32.Mul) == src.Length);
            Debug.Assert(sizes.Aggregate(Int32.Mul) == dest.Length);

            if (AppendLineCallback != null) AppendLineCallback("Размер массива:");
            for (int i = 0; i < sizes.Length; i++)
                if (AppendLineCallback != null)
                    AppendLineCallback(string.Format("Размер массива по оси № {0}:\t{1}", i, sizes[i]));

            // Расчёт коэффициентов слагаемых
            if (AppendLineCallback != null) AppendLineCallback("Расчёт коэффициентов слагаемых");
            var w = new double[sizes.Length + 1];
            double sum2 = 0;
            for (int i = 0; i < sizes.Length; i++) sum2 += (sizes[i] - 1)*(sizes[i] - 1)/(lengths[i]*lengths[i]);
            for (int i = 0; i < sizes.Length; i++)
                w[i] = (sizes[i] - 1)*(sizes[i] - 1)/(lengths[i]*lengths[i])/sum2/(1.0 + a);
            w[sizes.Length] = (a - 1.0)/(1.0 + a);

            if (AppendLineCallback != null) AppendLineCallback("Коэффициенты:");
            for (int i = 0; i < sizes.Length; i++)
                if (AppendLineCallback != null)
                    AppendLineCallback(string.Format("Коэффициенты по оси № {0} (у двух точек):\t{1}", i, w[i]));
            if (AppendLineCallback != null)
                AppendLineCallback(string.Format("Коэффициент у средней точки:\t{0}", w[sizes.Length]));

            if (AppendLineCallback != null && relax)
                AppendLineCallback("Используется релаксация");

            // Степень дифференциального оператора
            // Реализовано только для оператора Лапласа
            // Для больших степеней надо использовать соответствующие полиномы большей степени
            // Для дифференциального оператора степени 2 (оператора Лапласа) полином имеет степень 1
            // Для дифференциального оператора степени rank полином имеет степень rank-1
            const int rank = 2;

            var extV = new int[sizes.Length + 1]; // Координатная база точек в кубе
            var intV = new int[sizes.Length + 1]; // Координатная база внутренних точек
            intV[0] = extV[0] = 1;
            for (int i = 1; i <= sizes.Length; i++)
            {
                extV[i] = extV[i - 1]*sizes[i - 1];
                intV[i] = intV[i - 1]*(sizes[i - 1] - rank);
            }

            int extTotal = extV[sizes.Length]; // Количество точек в кубе
            int intTotal = intV[sizes.Length]; // Количество внутренних точек

            double[][] workspace = relax
                ? new[] {new double[extTotal]}
                : new[] {new double[extTotal], new double[extTotal]};

            // Копируем исходный массив в рабочую область
            if (AppendLineCallback != null) AppendLineCallback("Копируем исходный массив в рабочую область");
            Buffer.BlockCopy(src, 0, workspace[0], 0, extTotal*sizeof (double));
            if (!relax) Buffer.BlockCopy(src, 0, workspace[1], 0, extTotal*sizeof (double));

            var read = new object();
            var write = new object();

            var queue = new StackListQueue<double>();
            for (int step = 0;; step++)
            {
                //if (AppendLineCallback != null) AppendLineCallback(string.Format("Шаг итерации № {0}", step));

                //  При использовании метода релаксации задействовано в два раза меньше памяти и вычисления производятся
                //  на-месте. Для устанения коллизий с совместным доступом производится раскраска точек красное-чёрное для обработки
                //  их по-очереди
                double[] prev = relax ? workspace[0] : workspace[step & 1]; // Исходные значения
                double[] next = relax ? workspace[0] : workspace[1 - (step & 1)]; // Вычисляемые значения

                // Перебор по индексам внутренних точек
                //if (AppendLineCallback != null) AppendLineCallback("Перебор по индексам внутренних точек");
                //if (AppendLineCallback != null)
                //    AppendLineCallback("Преобразуем индекс внутренней точки в координаты в кубе");
                //if (AppendLineCallback != null) AppendLineCallback("Преобразуем координаты в кубе в индекс точки");
                //if (AppendLineCallback != null)
                //    AppendLineCallback("Вычисляем среднее взвешенное соседних точек для всех внутренних точек куба");

                double deltaSum = 0;
                double squareSum = 0;

                if (!relax)
                    Parallel.ForEach(Enumerable.Range(0, intTotal), intId =>
                    {
                        // Преобразуем индекс внутренней точки в координаты в кубе
                        // Преобразуем координаты в кубе в индекс точки
                        int id = 0;
                        for (int i = 0, v = intId; i < sizes.Length; i++)
                        {
                            int index = (rank >> 1) + (v%(sizes[i] - rank));
                            id += index*extV[i];
                            v = v/(sizes[i] - rank);
                        }

                        // Вычисляем среднее взвешенное соседних точек
                        // для всех внутренних точек куба
                        // и одновременно подсчитываем амплитуду изменений
                        double x = prev[id];
                        double y = x*w[sizes.Length];
                        for (int i = 0; i < sizes.Length; i++)
                            y += (prev[id - extV[i]] + prev[id + extV[i]])*w[i];
                        lock (write) next[id] = y;
                        double delta = x*(x - y);
                        double square = y;
                        delta = delta*delta;
                        square = square*square;
                        lock (write)
                        {
                            deltaSum += delta;
                            squareSum += square;
                        }
                    });
                else
                    for (int p = 0; p < 2; p++)
                        Parallel.ForEach(Enumerable.Range(0, intTotal), intId =>
                        {
                            // Преобразуем индекс внутренней точки в координаты в кубе
                            // Преобразуем координаты в кубе в индекс точки
                            // и подсчитваем чётность точки
                            // Чётность точки равна сумме координат
                            int id = 0;
                            int parity = 0; // Чётность точки
                            for (int i = 0, v = intId; i < sizes.Length; i++)
                            {
                                int index = (rank >> 1) + (v%(sizes[i] - rank));
                                parity += index;
                                id += index*extV[i];
                                v = v/(sizes[i] - rank);
                            }

                            if (parity%2 != p) return;

                            // Вычисляем среднее взвешенное соседних точек
                            // для всех внутренних точек куба
                            // и одновременно подсчитываем амплитуду изменений
                            double x = prev[id];
                            double y = x*w[sizes.Length];
                            for (int i = 0; i < sizes.Length; i++)
                                y += (prev[id - extV[i]] + prev[id + extV[i]])*w[i];
                            lock (write) next[id] = y;
                            double delta = x*(x - y);
                            double square = y;
                            delta = delta*delta;
                            square = square*square;
                            lock (write)
                            {
                                deltaSum += delta;
                                squareSum += square;
                            }
                        });

                //if (AppendLineCallback != null)
                //    AppendLineCallback(string.Format("Амплитуда изменений = {0}/{1}", deltaSum, squareSum));

                queue.Enqueue(deltaSum/squareSum);

                if (deltaSum > epsilon*squareSum) continue;

                if (AppendLineCallback != null)
                    AppendLineCallback(string.Format("Потребовалось {0} итераций", step + 1));

                // Копируем рабочую область в итоговый массив
                if (AppendLineCallback != null) AppendLineCallback("Копируем рабочую область в итоговый массив");
                Buffer.BlockCopy(next, 0, dest, 0, extTotal*sizeof (double));
                break;
            }
            return queue;
        }
    }
}