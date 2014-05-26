using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MyCudafy;
using MyCudafy.Collections;
using Double = MyLibrary.Types.Double;
using Int32 = MyLibrary.Types.Int32;

namespace LaplaceEquation
{
    public class CudafyLaplaceSolver : LaplaceSolver
    {
        public int GridSize { get; set; }

        public int BlockSize { get; set; }

        public override IEnumerable<double> Solve(int[] sizes, double[] lengths, double[] src, double[] dest, double epsilon, double a)
        {
            Debug.Assert(sizes.Length == lengths.Length);
            Debug.Assert(sizes.Aggregate(Int32.Mul) > 0);
            Debug.Assert(lengths.Aggregate(Double.Mul) > 0.0);
            Debug.Assert(sizes.Aggregate(Int32.Mul) == src.Length);
            Debug.Assert(sizes.Aggregate(Int32.Mul) == dest.Length);

            int length = sizes.Aggregate(Int32.Mul);
            var workspace = new double[length];
            var queue = new StackListQueue<double>();
            // Копируем исходный массив в рабочую область
            Buffer.BlockCopy(src, 0, workspace, 0, length*sizeof (double));
            lock (CudafyMultiDimentionalArray.Semaphore)
            {
                CudafyMultiDimentionalArray.SetArray(sizes, lengths, workspace);
                queue.Enqueue(CudafyMultiDimentionalArray.ExecuteLaplaceSolver(epsilon, a, GridSize, BlockSize,
                    AppendLineCallback));
                workspace = CudafyMultiDimentionalArray.GetArray();
            }
            // Копируем рабочую область в итоговый массив
            Buffer.BlockCopy(workspace, 0, dest, 0, length*sizeof (double));
            return queue;
        }
    }
}