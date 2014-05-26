using System.Collections.Generic;

namespace LaplaceEquation.Editor
{
    public class OptimalValuesComparer : IComparer<OptimalValues>
    {
        public int Compare(OptimalValues x, OptimalValues y)
        {
            return x.Rank - y.Rank;
        }
    }
}