using System.Collections.Generic;

namespace LaplaceEquation.Editor
{
    internal class KeyValuePairDoubleDoubleComparer : IComparer<KeyValuePair<double, double>>
    {
        public int Compare(KeyValuePair<double, double> x, KeyValuePair<double, double> y)
        {
            if (x.Key < y.Key) return -1;
            if (x.Key > y.Key) return 1;
            return 0;
        }
    }
}