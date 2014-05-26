using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MyLibrary.Collections;
using Double = MyLibrary.Types.Double;
using Int32 = MyLibrary.Types.Int32;

namespace LaplaceEquation.Editor
{
    public partial class OptimazerForm : Form
    {
        public OptimazerForm()
        {
            InitializeComponent();
            chart1.SuppressExceptions = true;
            chart2.SuppressExceptions = true;
            chart1.Series.Clear();
            chart2.Series.Clear();
            chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chart1.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chart2.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chart2.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chart1.ChartAreas[0].AxisX.IsStartedFromZero = false;
            chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;
            chart2.ChartAreas[0].AxisX.IsStartedFromZero = false;
            chart2.ChartAreas[0].AxisY.IsStartedFromZero = false;
            chart1.ChartAreas[0].AxisX2.Enabled = AxisEnabled.False;
            chart1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
            
            const int n = 2;
            const int count = 100;
            chart2.ChartAreas[0].AxisX.Minimum = n;
            chart2.ChartAreas[0].AxisX.Maximum = n + count - 1;
            var list = new StackListQueue<OptimalValues>();
            Parallel.ForEach(Enumerable.Range(n, count), i =>
            {
                var values = new OptimalValues
                {
                    Rank = i,
                    Det2nAt1 = (Math.Abs(LaplaceSolver.Det2n(1, i))),
                    Det2nAt2 = (Math.Abs(LaplaceSolver.Det2n(2, i))),
                    Det2nAt3 = (Math.Abs(LaplaceSolver.Det2n(3, i))),
                };
                lock (list) list.Add(values);
            });
            list.Sort(new OptimalValuesComparer());
            dataGridView1.DataSource = new BindingList<OptimalValues>(list);
            chart2.DataBindTable(
                new StackListQueue<OptimalValues>(
                    list.Where(
                        i => !double.IsPositiveInfinity(i.Det2nAt1) &&
                             !double.IsPositiveInfinity(i.Det2nAt2) &&
                             !double.IsPositiveInfinity(i.Det2nAt3) &&
                             !double.IsNegativeInfinity(i.Det2nAt1) &&
                             !double.IsNegativeInfinity(i.Det2nAt2) &&
                             !double.IsNegativeInfinity(i.Det2nAt3))), "Rank");
            foreach (Series series in chart2.Series) series.ChartType = SeriesChartType.FastPoint;
        }

        public double IntervalStart
        {
            get { return Double.ParseAsString(numericUpDown1.Text); }
        }

        public double IntervalEnd
        {
            get { return Double.ParseAsString(numericUpDown2.Text); }
        }

        public int MatrixRankMinimum
        {
            get { return Int32.ParseAsString(numericUpDown3.Text); }
        }

        public int MatrixRankMaximum
        {
            get { return Int32.ParseAsString(numericUpDown4.Text); }
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            for (int rank = MatrixRankMinimum; rank <= MatrixRankMaximum; rank++)
            {
                double a = IntervalStart;
                double b = IntervalEnd;
                const int count = 10000; // Количество отсчётов при рисовании графика
                chart1.ChartAreas[0].AxisX.Minimum = a;
                chart1.ChartAreas[0].AxisX.Maximum = b;
                var list = new StackListQueue<KeyValuePair<double, double>>();
                Parallel.ForEach(Enumerable.Range(0, count), i =>
                {
                    double x = (i*a + (count - i)*b)/count;
                    double y = Math.Log10(Math.Abs(LaplaceSolver.Det(x, rank)));
                    if (double.IsNegativeInfinity(y) || double.IsPositiveInfinity(y)) return;
                    lock (list) list.Add(new KeyValuePair<double, double>(x, y));
                });
                list.Sort(new KeyValuePairDoubleDoubleComparer());
                chart1.DataBindTable(list, "Key");
                chart1.Series.Last().ChartType = SeriesChartType.FastPoint;
                chart1.Series.Last().Name = string.Format("Rank = {0}", rank);
            }
        }

        private void OptimizerForm_Load(object sender, EventArgs e)
        {
            ValueChanged(sender, e);
        }
    }

    public class OptimalValues
    {
        public int Rank { get; set; }
        public double Det2nAt1 { get; set; }
        public double Det2nAt2 { get; set; }
        public double Det2nAt3 { get; set; }
    }
}