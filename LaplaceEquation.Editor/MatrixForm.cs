using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MyCudafy.Collections;
using MyLibrary.Trace;
using Double = MyLibrary.Types.Double;
using Int32 = MyLibrary.Types.Int32;

namespace LaplaceEquation.Editor
{
    public partial class MatrixForm : Form, ITrace
    {
        private static readonly Random Rnd = new Random();

        private readonly MatrixIO _dataGridViewDestMatrix = new MatrixIO
        {
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
            Dock = DockStyle.Fill,
            Name = "dataGridViewDestMatrix",
            RowTemplate = {Height = 20},
            TabIndex = 0
        };

        private readonly MatrixIO _dataGridViewSrcMatrix = new MatrixIO
        {
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
            Dock = DockStyle.Fill,
            Name = "dataGridViewSrcMatrix",
            RowTemplate = {Height = 20},
            TabIndex = 0
        };

        public MatrixForm(IEnumerable<int> sizes, IEnumerable<double> lengths, IEnumerable<double> data)
        {
            InitializeComponent();
            SrcData = new BindingList<double>(new StackListQueue<double>(data));
            DestData = new BindingList<double>(new StackListQueue<double>(data));
            Sizes = new BindingList<int>(new StackListQueue<int>(sizes));
            Lengths = new BindingList<double>(new StackListQueue<double>(lengths));
            FileName = "";
            tabPageSrc.Controls.Add(_dataGridViewSrcMatrix);
            tabPageDest.Controls.Add(_dataGridViewDestMatrix);
            AppendLineCallback = AppendLine;
            Experiments = new BindingList<Experiment>();
            dataGridView1.DataSource = Experiments;
            chart1.Series.Clear();
        }

        public int HorzSize
        {
            get { return Convert.ToInt32(numericUpDownHorzSize.Value); }
            set { numericUpDownHorzSize.Value = value; }
        }

        public int VertSize
        {
            get { return Convert.ToInt32(numericUpDownVertSize.Value); }
            set { numericUpDownVertSize.Value = value; }
        }

        public double HorzLength
        {
            get { return Convert.ToDouble(numericUpDownHorzLength.Value); }
            set { numericUpDownHorzLength.Value = Convert.ToDecimal(value); }
        }

        public double VertLength
        {
            get { return Convert.ToDouble(numericUpDownVertLength.Value); }
            set { numericUpDownVertLength.Value = Convert.ToDecimal(value); }
        }

        public string FileName { get; set; }
        public BindingList<Experiment> Experiments { get; set; }

        public BindingList<int> Sizes
        {
            get { return new BindingList<int>(new StackListQueue<int> {VertSize, HorzSize}); }
            set
            {
                HorzSize = value[0];
                VertSize = value[1];
                SrcData =
                    new BindingList<double>(new MyLibrary.Collections.StackListQueue<double>(SrcData).GetRange(0,
                        Math.Min(SrcData.Count(), Sizes.Aggregate(Int32.Mul))));
                DestData =
                    new BindingList<double>(new MyLibrary.Collections.StackListQueue<double>(DestData).GetRange(0,
                        Math.Min(DestData.Count(), Sizes.Aggregate(Int32.Mul))));
            }
        }

        public BindingList<double> Lengths
        {
            get { return new BindingList<double>(new StackListQueue<double> {HorzLength, VertLength}); }
            set
            {
                HorzLength = value[0];
                VertLength = value[1];
            }
        }

        public BindingList<double> SrcData
        {
            get
            {
                string[,] data = _dataGridViewSrcMatrix.TheData;
                var list = new StackListQueue<double>();
                for (int i = 0; i < data.GetLength(0); i++)
                    for (int j = 0; j < data.GetLength(1); j++)
                        list.Enqueue(Double.ParseAsString(data[i, j].Replace('.', ',')));
                return new BindingList<double>(list);
            }
            set
            {
                if (Sizes.Aggregate(Int32.Mul) == 0) return;
                var data = new string[VertSize, HorzSize];
                var queue = new StackListQueue<double>(value);
                for (int i = 0; i < VertSize && queue.Any(); i++)
                    for (int j = 0; j < HorzSize && queue.Any(); j++)
                        data[i, j] = queue.Dequeue().ToString().Replace(',', '.');
                _dataGridViewSrcMatrix.TheData = data;
            }
        }

        public BindingList<double> DestData
        {
            get
            {
                string[,] data = _dataGridViewDestMatrix.TheData;
                var list = new StackListQueue<double>();
                for (int i = 0; i < data.GetLength(0); i++)
                    for (int j = 0; j < data.GetLength(1); j++)
                        list.Enqueue(Double.ParseAsString(data[i, j].Replace('.', ',')));
                return new BindingList<double>(list);
            }
            set
            {
                if (Sizes.Aggregate(Int32.Mul) == 0) return;
                var data = new string[VertSize, HorzSize];
                var queue = new StackListQueue<double>(value);
                for (int i = 0; i < VertSize && queue.Any(); i++)
                    for (int j = 0; j < HorzSize && queue.Any(); j++)
                        data[i, j] = queue.Dequeue().ToString(CultureInfo.InvariantCulture).Replace(',', '.');
                _dataGridViewDestMatrix.TheData = data;
            }
        }

        public ProgressCallback ProgressCallback { get; set; }
        public AppendLineCallback AppendLineCallback { get; set; }
        public CompliteCallback CompliteCallback { get; set; }

        public void SaveMatrix(string fileName)
        {
            using (StreamWriter writer = File.CreateText(fileName))
            {
                writer.WriteLine(string.Join("\t", Sizes));
                writer.WriteLine(string.Join("\t", Lengths));
                writer.WriteLine(string.Join(Environment.NewLine, SrcData));
                FileName = fileName;
            }
        }

        public static MatrixForm OpenMatrix(string fileName)
        {
            using (StreamReader reader = File.OpenText(fileName))
            {
                IEnumerable<int> sizes =
                    (from Match match in new Regex(@"\d+").Matches(reader.ReadLine())
                        select Int32.ParseAsString(match.Value));
                IEnumerable<double> lengths =
                    (from Match match in new Regex(@"\d+([\.,]\d*)?").Matches(reader.ReadLine())
                        select Double.ParseAsString(match.Value.Replace('.', ',')));
                var data = new StackListQueue<double>();
                for (string line = reader.ReadLine(); !string.IsNullOrEmpty(line); line = reader.ReadLine())
                    data.Add(from Match match in new Regex(@"-?\d+([\.,]\d*)?").Matches(line)
                        select Double.ParseAsString(match.Value.Replace('.', ',')));
                return new MatrixForm(sizes, lengths, data)
                {
                    FileName = fileName
                };
            }
        }

        private void SizesValueChanged(object sender, EventArgs e)
        {
            if (Sizes.Aggregate(Int32.Mul) == 0) return;
            string[,] old = _dataGridViewSrcMatrix.TheData;
            var nova = new string[VertSize, HorzSize];
            for (int i = 0; i < old.GetLength(0) && i < nova.GetLength(0); i++)
                for (int j = 0; j < old.GetLength(1) && j < nova.GetLength(1); j++)
                    nova[i, j] = old[i, j];
            _dataGridViewSrcMatrix.TheData = nova;
        }

        private void AppendLine(string line)
        {
            textBox1.AppendText(string.Format("{0}\t{1}{2}", DateTime.Now, line, Environment.NewLine));
        }

        public void Random(double minimum, double maximum)
        {
            var nova = new string[VertSize, HorzSize];
            for (int i = 0; i < nova.GetLength(0); i++)
                for (int j = 0; j < nova.GetLength(1); j++)
                    nova[i, j] = (minimum + (maximum - minimum)*Rnd.NextDouble()).ToString(CultureInfo.InvariantCulture);
            _dataGridViewSrcMatrix.TheData = nova;
            _dataGridViewDestMatrix.TheData = nova;
        }
    }
}