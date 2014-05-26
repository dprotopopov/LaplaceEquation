using System;
using System.Windows.Forms;

namespace LaplaceEquation.Editor
{
    public partial class RandomDialog : Form
    {
        public RandomDialog()
        {
            InitializeComponent();
        }

        public double Maximum
        {
            get { return Convert.ToDouble(numericUpDownMaximum.Value); }
            set { numericUpDownMaximum.Value = Convert.ToDecimal(value); }
        }
        public double Minimum
        {
            get { return Convert.ToDouble(numericUpDownMinimum.Value); }
            set { numericUpDownMinimum.Value = Convert.ToDecimal(value); }
        }
    }
}