using System;
using System.Windows.Forms;

namespace LaplaceEquation.Editor
{
    public partial class ExecuteSolverDailog : Form
    {
        public ExecuteSolverDailog()
        {
            InitializeComponent();
        }

        public double AlgorithmParameter
        {
            get { return Convert.ToDouble(numericUpDown1.Value); }
            set { numericUpDown1.Value = Convert.ToDecimal(value); }
        }

        public double Epsilon
        {
            get { return Convert.ToDouble(numericUpDown2.Value); }
            set { numericUpDown2.Value = Convert.ToDecimal(value); }
        }

        public bool NativeMethod
        {
            get { return radioButton1.Checked; }
            set { radioButton1.Checked = value; }
        }

        public bool CudafyMethod
        {
            get { return radioButton4.Checked; }
            set { radioButton4.Checked = value; }
        }
    }
}