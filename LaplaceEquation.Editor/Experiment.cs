using System;
using System.Linq;
using MyLibrary;
using MyLibrary.Attributes;
using String = MyLibrary.Types.String;

namespace LaplaceEquation.Editor
{
    public class Experiment : IValueable
    {
        [Value]
        public int Rows { get; set; }

        [Value]
        public int Columns { get; set; }

        [Value]
        public double Epsilon { get; set; }

        [Value]
        public double AlgorithmParameter { get; set; }

        [Value]
        public double SrcE { get; set; }

        [Value]
        public double SrcS2 { get; set; }

        [Value]
        public double DestE { get; set; }

        [Value]
        public double DestS2 { get; set; }

        [Value]
        public DateTime StartDateTime { get; set; }

        [Value]
        public DateTime EndDateTime { get; set; }

        [Value]
        public TimeSpan RunTimeSpan { get; set; }

        [Value]
        public int Steps { get; set; }

        [Value]
        public LaplaceMethod LaplaceMethod { get; set; }

        [Value]
        public int BlockSize { get; set; }

        [Value]
        public int GridSize { get; set; }

        public Values ToValues()
        {
            return new Values(this);
        }

        public override string ToString()
        {
            Values values = ToValues();
            return
                String.Parse(
                    new Transformation().ParseTemplate(new Values(values.Keys,
                        values.Values.Select(v => v.FirstOrDefault()))));
        }
    }
}