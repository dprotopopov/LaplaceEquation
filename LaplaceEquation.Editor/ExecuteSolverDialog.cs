using System;
using System.Globalization;
using System.Windows.Forms;

namespace LaplaceEquation.Editor
{
    public partial class ExecuteSolverDialog : Form
    {
        public ExecuteSolverDialog()
        {
            InitializeComponent();
        }

        public double AlgorithmParameter
        {
            get { return Convert.ToDouble(numericUpDownAlgorithmParameter.Value); }
            set { numericUpDownAlgorithmParameter.Value = Convert.ToDecimal(value); }
        }

        public double Epsilon
        {
            get { return Convert.ToDouble(numericUpDownEpsilon.Value); }
            set { numericUpDownEpsilon.Value = Convert.ToDecimal(value); }
        }

        public bool NativeMethod
        {
            get { return radioButtonNativeMethod.Checked; }
            set { radioButtonNativeMethod.Checked = value; }
        }

        public bool CudafyMethod
        {
            get { return radioButtonCudafyMethod.Checked; }
            set { radioButtonCudafyMethod.Checked = value; }
        }

        public int GridSize
        {
            get { return Convert.ToInt32(numericUpDownGridSize.Value); }
            set { numericUpDownGridSize.Value = value; }
        }

        public int BlockSize
        {
            get { return Convert.ToInt32(numericUpDownBlockSize.Value); }
            set { numericUpDownBlockSize.Value = value; }
        }

        public LaplaceMethod LaplaceMethod
        {
            get
            {
                if (CudafyMethod) return LaplaceMethod.Cudafy;
                if (NativeMethod) return LaplaceMethod.Native;
                throw new NotImplementedException();
            }
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            try
            {
                textBox2.Text = (GridSize*BlockSize).ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
            }
        }
    }
}