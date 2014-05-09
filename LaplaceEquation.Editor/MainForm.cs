using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Int32 = MyLibrary.Types.Int32;

namespace LaplaceEquation.Editor
{
    public partial class MainForm : Form
    {
        private readonly ExecuteSolverDailog _executeSolverDailog = new ExecuteSolverDailog();
        private readonly OptimizerForm _optimizer = new OptimizerForm();

        public MainForm()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new CreateMatrixDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return;
            var child = new MatrixForm(dialog.Sizes, dialog.Lenghts, dialog.Data)
            {
                MdiParent = this
            };
            child.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            MatrixForm child = MatrixForm.OpenMatrix(openFileDialog1.FileName);
            child.MdiParent = this;
            child.Show();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var child = ActiveMdiChild as MatrixForm;
            if (child == null) return;
            if (!(child is MatrixForm)) return;
            saveFileDialog1.FileName = child.FileName;
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            child.SaveMatrix(saveFileDialog1.FileName);
        }

        private void executeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var child = ActiveMdiChild as MatrixForm;
            if (child == null) return;
            if (!(child is MatrixForm)) return;
            if (_executeSolverDailog.ShowDialog() != DialogResult.OK) return;
            LaplaceSolver solver;
            if (_executeSolverDailog.NativeMethod)
                solver = new SharpLaplaceSolver();
            else if (_executeSolverDailog.CudafyMethod)
                solver = new CudafyLaplaceSolver();
            else
            {
                MessageBox.Show("Не выбран метод");
                return;
            }
            int[] sizes = child.Sizes.ToArray();
            double[] lengths = child.Lengths.ToArray();
            double[] src = child.Data.ToArray();
            var dest = new double[sizes.Aggregate(Int32.Mul)];
            double a = _executeSolverDailog.AlgorithmParameter;
            double epsilon = _executeSolverDailog.Epsilon;
            solver.Solve(sizes, lengths, src, dest, epsilon, a);
            child.Data = new BindingList<double>(dest);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void optimizerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _optimizer.Show();
        }
    }
}