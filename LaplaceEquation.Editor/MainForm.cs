using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MiniMax.Forms;
using MyFormula;
using Int32 = MyLibrary.Types.Int32;

namespace LaplaceEquation.Editor
{
    public partial class MainForm : Form
    {
        private static readonly BuildChooseDialog CudaBuildChooseDialog =
            new BuildChooseDialog(typeof (MyCudaFormula));

        private static readonly ExecuteSolverDialog ExecuteSolverDialog = new ExecuteSolverDialog
        {
            CudaBuildChooseDialog = CudaBuildChooseDialog
        };

        private static readonly RandomDialog RandomDailog = new RandomDialog();

        public MainForm()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new CreateMatrixDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return;
            var child = new MatrixForm(dialog.Sizes, dialog.Lengths, dialog.Data)
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
            if (ExecuteSolverDialog.ShowDialog() != DialogResult.OK) return;
            LaplaceSolver solver;
            if (ExecuteSolverDialog.NativeMethod)
                solver = new NativeLaplaceSolver
                {
                    AppendLineCallback = child.AppendLineCallback,
                };
            else if (ExecuteSolverDialog.CudafyMethod)
                solver = new CudafyLaplaceSolver
                {
                    AppendLineCallback = child.AppendLineCallback,
                    GridSize = ExecuteSolverDialog.GridSize,
                    BlockSize = ExecuteSolverDialog.BlockSize,
                };

            else
            {
                MessageBox.Show("Не выбран метод");
                return;
            }
            int[] sizes = child.Sizes.ToArray();
            double[] lengths = child.Lengths.ToArray();
            double[] src = child.SrcData.ToArray();
            var dest = new double[sizes.Aggregate(Int32.Mul)];
            bool relax = ExecuteSolverDialog.UseRelax;
            double a = ExecuteSolverDialog.AlgorithmParameter;
            double epsilon = ExecuteSolverDialog.Epsilon;
            DateTime startDateTime = DateTime.Now;
            IEnumerable<double> queue = solver.Solve(sizes, lengths, src, dest, epsilon, a, relax);
            DateTime endDateTime = DateTime.Now;
            child.DestData = new BindingList<double>(dest);

            var timeSpan = new TimeSpan(endDateTime.Ticks - startDateTime.Ticks);

            double srcE = src.Average();
            double srcS2 = src.Select(x => x*x).Average() - srcE*srcE;
            double destE = dest.Average();
            double destS2 = dest.Select(x => x*x).Average() - destE*destE;
            var experiment = new Experiment
            {
                LaplaceMethod = ExecuteSolverDialog.LaplaceMethod,
                Rows = sizes[0],
                Columns = sizes[1],
                SrcE = srcE,
                SrcS2 = srcS2,
                DestE = destE,
                DestS2 = destS2,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime,
                RunTimeSpan = timeSpan,
                Steps = queue.Count(),
                AlgorithmParameter = a,
                Epsilon = epsilon,
                GridSize = ExecuteSolverDialog.GridSize,
                BlockSize = ExecuteSolverDialog.BlockSize,
                Relax = relax
            };
            child.Experiments.Add(experiment);
            child.chart1.DataBindTable(queue);
            child.chart1.Series.Last().Name = experiment.ToString();
            child.chart1.ChartAreas.Last().AxisY.IsLogarithmic = true;
            child.chart1.Series.Last().ChartType = SeriesChartType.FastPoint;
            MessageBox.Show(timeSpan.ToString());
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void optimizerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var optimazer = new OptimazerForm
            {
                MdiParent = this
            };
            optimazer.Show();
        }

        private void randomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var child = ActiveMdiChild as MatrixForm;
            if (child == null) return;
            if (!(child is MatrixForm)) return;
            if (RandomDailog.ShowDialog() != DialogResult.OK) return;
            child.Random(RandomDailog.Minimum, RandomDailog.Maximum);
        }
    }
}