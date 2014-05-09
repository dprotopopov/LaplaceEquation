using System;
using System.Diagnostics;
using System.Linq;
using MyCudafy;
using Double = MyLibrary.Types.Double;
using Int32 = MyLibrary.Types.Int32;

namespace LaplaceEquation
{
    public class CudafyLaplaceSolver : LaplaceSolver
    {
        public override void Solve(int[] sizes, double[] lengths, double[] src, double[] dest, double epsilon, double a)
        {
            Debug.Assert(sizes.Length == lengths.Length);
            Debug.Assert(sizes.Aggregate(Int32.Mul) > 0);
            Debug.Assert(lengths.Aggregate(Double.Mul) > 0.0);
            Debug.Assert(sizes.Aggregate(Int32.Mul) == src.Length);
            Debug.Assert(sizes.Aggregate(Int32.Mul) == dest.Length);

            int length = sizes.Aggregate(Int32.Mul);
            var workspace = new double[length];
            // Копируем исходный массив в рабочую область
            Buffer.BlockCopy(src, 0, workspace, 0, length*sizeof (double));
            lock (CudafyMultiDimentionalArray.Semaphore)
            {
                CudafyMultiDimentionalArray.SetArray(sizes, lengths, workspace);
                CudafyMultiDimentionalArray.ExecuteLaplaceSolver(epsilon, a);
                workspace = CudafyMultiDimentionalArray.GetArray();
            }
            // Копируем рабочую область в итоговый массив
            Buffer.BlockCopy(workspace, 0, dest, 0, length*sizeof (double));
        }
    }

    public class SharpLaplaceSolver : LaplaceSolver
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
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <param name="epsilon"></param>
        /// <param name="a"></param>
        public override void Solve(int[] sizes, double[] lengths, double[] src, double[] dest, double epsilon, double a)
        {
            Debug.Assert(sizes.Length == lengths.Length);
            Debug.Assert(sizes.Aggregate(Int32.Mul) > 0);
            Debug.Assert(lengths.Aggregate(Double.Mul) > 0.0);
            Debug.Assert(sizes.Aggregate(Int32.Mul) == src.Length);
            Debug.Assert(sizes.Aggregate(Int32.Mul) == dest.Length);

            // Расчёт коэффициентов слагаемых
            var w = new double[sizes.Length + 1];
            double sum2 = 0;
            for (int i = 0; i < sizes.Length; i++) sum2 += (sizes[i] - 1) * (sizes[i] - 1) / (lengths[i] * lengths[i]);
            for (int i = 0; i < sizes.Length; i++) w[i] = (sizes[i] - 1) * (sizes[i] - 1) / (lengths[i] * lengths[i]) / sum2 / (1.0 + a);
            w[sizes.Length] = (1.0 - a)/(1.0 + a);

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

            double[][] workspace = {new double[extTotal], new double[extTotal]};

            // Копируем исходный массив в рабочую область
            Buffer.BlockCopy(src, 0, workspace[0], 0, extTotal*sizeof (double));
            Buffer.BlockCopy(src, 0, workspace[1], 0, extTotal*sizeof (double));

            for (int step = 0;; step++)
            {
                double[] prev = workspace[step & 1]; // Исходные значения
                double[] next = workspace[1 - (step & 1)]; // Вычисляемые значения

                // Перебор по индексам внутренних точек
                for (int intId = 0; intId < intTotal; intId++)
                {
                    // Преобразуем индекс внутренней точки в координаты в кубе
                    // Преобразуем координаты в кубе в индекс точки
                    int id = 0;
                    for (int i = 0, v = intId; i < sizes.Length; i++)
                    {
                        id += ((rank >> 1) + (v%(sizes[i] - rank)))*extV[i];
                        v = v/(sizes[i] - rank);
                    }
                    // Вычисляем среднее арифметическое соседних точек
                    // для всех внутренних точек куба
                    double s = 0;
                    for (int i = 0; i < sizes.Length; i++)
                        s += (prev[id - extV[i]] + prev[id + extV[i]])*w[i];
                    next[id] = s - prev[id]*w[sizes.Length];
                }

                // Рассчитываем амплитуду изменений
                var delta = new double[extTotal];
                for (int i = 0; i < extTotal; i++) delta[i] = Math.Abs(next[i] - prev[i]);

                if (delta.Max() > epsilon) continue;

                // Копируем рабочую область в итоговый массив
                Buffer.BlockCopy(next, 0, dest, 0, extTotal*sizeof (double));
                break;
            }
        }
    }
}