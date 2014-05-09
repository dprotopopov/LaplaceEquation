using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MyCudafy.Collections;
using Double = MyLibrary.Types.Double;
using Int32 = MyLibrary.Types.Int32;

namespace LaplaceEquation.Editor
{
    public partial class MatrixForm : Form
    {
        private readonly MatrixIO _dataGridViewManual = new MatrixIO
        {
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
            Dock = DockStyle.Fill,
            Name = "dataGridViewManual",
            RowTemplate = {Height = 20},
            TabIndex = 0
        };

        public MatrixForm(IEnumerable<int> sizes, IEnumerable<double> lengths, IEnumerable<double> data)
        {
            InitializeComponent();
            Data = new BindingList<double>(new StackListQueue<double>(data));
            Sizes = new BindingList<int>(new StackListQueue<int>(sizes));
            Lengths = new BindingList<double>(new StackListQueue<double>(lengths));
            FileName = "";
            groupBox2.Controls.Add(_dataGridViewManual);
            _dataGridViewManual.TheData = new string[VertSize, HorzSize];
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

        public BindingList<int> Sizes
        {
            get { return new BindingList<int>(new StackListQueue<int> {VertSize, HorzSize}); }
            set
            {
                HorzSize = value[0];
                VertSize = value[1];
                Data =
                    new BindingList<double>(new MyLibrary.Collections.StackListQueue<double>(Data).GetRange(0,
                        Math.Min(Data.Count(), Sizes.Aggregate(Int32.Mul))));
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

        public BindingList<double> Data
        {
            get
            {
                string[,] data = _dataGridViewManual.TheData;
                var list = new StackListQueue<double>();
                for (int i = 0; i < data.GetLength(0); i++)
                    for (int j = 0; j < data.GetLength(1); j++)
                        list.Add(Double.ParseAsString(data[i, j].Replace('.',',')));
                return new BindingList<double>(list);
            }
            set
            {
                if (Sizes.Aggregate(Int32.Mul) == 0) return;
                var data = new string[VertSize, HorzSize];
                var queue = new StackListQueue<double>(value);
                for (int i = 0; i < VertSize; i++)
                    for (int j = 0; j < HorzSize; j++)
                        data[i, j] = queue.Dequeue().ToString().Replace(',', '.');
                _dataGridViewManual.TheData = data;
            }
        }

        public void SaveMatrix(string fileName)
        {
            using (StreamWriter writer = File.CreateText(fileName))
            {
                writer.WriteLine(string.Join("\t", Sizes));
                writer.WriteLine(string.Join("\t", Lengths));
                writer.WriteLine(string.Join(Environment.NewLine, Data));
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
            string[,] old = _dataGridViewManual.TheData;
            var nova = new string[VertSize, HorzSize];
            for (int i = 0; i < old.GetLength(0) && i < nova.GetLength(0); i++)
                for (int j = 0; j < old.GetLength(1) && j < nova.GetLength(1); j++)
                    nova[i, j] = old[i, j];
            _dataGridViewManual.TheData = nova;
        }
    }
}