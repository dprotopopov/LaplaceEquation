using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MyCudafy.Collections;
using Int32 = MyLibrary.Types.Int32;

namespace LaplaceEquation.Editor
{
    public partial class CreateMatrixDialog : Form
    {
        public CreateMatrixDialog()
        {
            InitializeComponent();
        }

        public IEnumerable<int> Sizes
        {
            get
            {
                return new StackListQueue<int>
                {
                    Convert.ToInt32(numericUpDown1.Value),
                    Convert.ToInt32(numericUpDown2.Value),
                };
            }
        }

        public IEnumerable<double> Lenghts
        {
            get
            {
                return new StackListQueue<double>
                {
                    Convert.ToDouble(numericUpDown3.Value),
                    Convert.ToDouble(numericUpDown4.Value),
                };
            }
        }

        public IEnumerable<double> Data
        {
            get { return new double[Sizes.Aggregate(Int32.Mul)]; }
        }
    }
}