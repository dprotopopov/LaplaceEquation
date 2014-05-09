using System;

namespace LaplaceEquation
{
    public abstract class LaplaceSolver
    {
        public enum LaplaceSolverAlgoriphm
        {
            Jacobi = 1,
            Gauss = 2,
            Optimal = 3
        }

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
        public abstract void Solve(int[] sizes, double[] lengths, double[] src, double[] dest, double epsilon, double a);

        /// <summary>
        ///     Вычисление определителя матрицы ранга k, у которой на главной диагонали указано число y,
        ///     а на диагоналях выше и ниже главной диагонали указано число x
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static double Det(double x, double y, int k)
        {
            double z = Math.Sqrt(Math.Abs(y*y - 4*x*x));
            double c = y - z;
            double d = y + z;
            var cc = new double[k + 1];
            var dd = new double[k + 1];
            dd[0] = cc[0] = 1;
            for (int i = 1; i <= k; i++) cc[i] = cc[i - 1]*c/2;
            for (int i = 1; i <= k; i++) dd[i] = dd[i - 1]*d/2;
            double v = 0;
            for (int i = 0; i <= k; i++) v += cc[i]*dd[k - i];
            return v;
        }

        public static double Det(double a, int n)
        {
            return Det(1.0/(a + 1.0), (a - 1.0)/(a + 1.0), n - 2);
        }
    }
}