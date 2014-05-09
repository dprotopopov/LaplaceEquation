using System;
using System.Windows.Forms;
using Double = MyLibrary.Types.Double;
using Int32 = MyLibrary.Types.Int32;

namespace LaplaceEquation.Editor
{
    public partial class OptimizerForm : Form
    {
        public OptimizerForm()
        {
            InitializeComponent();
        }

        public double IntervalStart
        {
            get { return Double.ParseAsString(numericUpDown1.Text); }
        }

        public double IntervalEnd
        {
            get { return Double.ParseAsString(numericUpDown2.Text); }
        }

        public int IntervalCount
        {
            get { return Int32.ParseAsString(numericUpDown3.Text); }
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            for (int i = 0; i <= IntervalCount; i++)
                try
                {
                    double x = (i*IntervalStart + (IntervalCount - i)*IntervalEnd)/IntervalCount;
                    double y = Math.Log10(Math.Abs(LaplaceSolver.Det(x, IntervalCount + 1)));
                    chart1.Series[0].Points.AddXY(x, y);
                }
                catch (Exception)
                {
                }
        }

        private void OptimizerForm_Load(object sender, EventArgs e)
        {
            ValueChanged(sender, e);
        }
    }
}