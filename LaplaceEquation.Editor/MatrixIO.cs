using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace LaplaceEquation.Editor
{
    public class MatrixIO : DataGridView
    {
        #region Private fields

        private const int DimentionSize = 10; //default row and col size
        //array for the data in the grid
        private string[,] _mData;

        #endregion

        #region Constructors

        public MatrixIO()
        {
            InitializeDataGridView(DimentionSize, DimentionSize);
        }

        public MatrixIO(int nRows, int nCols)
        {
            InitializeDataGridView(nRows, nCols);
        }

        public MatrixIO(Point location, int nRows, int nCols)
        {
            InitializeDataGridView(nRows, nCols);
            Location = location;
        }

        #endregion

        #region Initialisation of the DataGridView

        private void InitializeDataGridView(int rows, int columns)
        {
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            EnableHeadersVisualStyles = false;
            SelectionMode = DataGridViewSelectionMode.CellSelect;
            EditMode = DataGridViewEditMode.EditOnKeystroke;
            Name = "dataGridViewMatrix";
            TabIndex = 0;
            RowHeadersWidth = 60;
            //used to attach event-handlers to the events of the editing control(nice name!)
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            for (int i = 0; i < columns; i++)
            {
                AddAColumn(i);
            }
            RowHeadersDefaultCellStyle.Padding = new Padding(3); //helps to get rid of the selection triangle?
            for (int i = 0; i < rows; i++)
            {
                AddARow(i);
            }
            ColumnHeadersDefaultCellStyle.Font = new Font("Verdana", 6F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;
            RowHeadersDefaultCellStyle.Font = new Font("Verdana", 6F, FontStyle.Bold, GraphicsUnit.Point, 0);
            RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            RowHeadersDefaultCellStyle.BackColor = Color.Gainsboro;
            ShowEditingIcon = false;
            SelectionMode = DataGridViewSelectionMode.CellSelect;
            CellLeave += MatrixIO_CellChanged;
            CurrentCellDirtyStateChanged += MatrixIO_CellChanged;
            EditingControlShowing += MatrixIO_EditingControlShowing;
        }

        private void MatrixIO_CellChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void AddARow(int i)
        {
            var arow = new DataGridViewRow
            {
                HeaderCell =
                {
                    Value = i.ToString(CultureInfo.InvariantCulture)
                }
            };
            Rows.Add(arow);
        }

        private void AddAColumn(int i)
        {
            var acolumn = new DataGridViewTextBoxColumn
            {
                HeaderText = i.ToString(CultureInfo.InvariantCulture),
                Name = "Column" + i,
                Width = 60,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            //make a Style template to be used in the grid
            using (var acell = new DataGridViewTextBoxCell
            {
                ValueType = typeof (string),
                Style =
                {
                    BackColor = Color.LightCyan,
                    SelectionBackColor = Color.FromArgb(128, 255, 255),
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            })
                acolumn.CellTemplate = acell;
            Columns.Add(acolumn);
        }

        public void MakeMatrixTitle(string title)
        {
            TopLeftHeaderCell.Value = title;
            TopLeftHeaderCell.Style.BackColor = Color.AliceBlue;
        }

        #endregion

        #region Properties and property utility functions

        public string[,] TheData
        {
            get
            {
                _mData = new string[RowCount, ColumnCount];
                ExtractTextBoxes();
                return _mData;
            }
            set
            {
                int rows = value.GetLength(0);
                int cols = value.GetLength(1);
                _mData = new string[rows, cols];
                _mData = value;
                ResizeOurself(rows, cols);
                FillTextBoxes();
            }
        }

        private void ResizeOurself(int r, int c)
        {
            //adjust rows and cols, do nothing if they equal 
            //
            while (c < ColumnCount)
            {
                Columns.RemoveAt(ColumnCount - 1);
            }
            while (c > ColumnCount)
            {
                AddAColumn(ColumnCount);
            }
            while (r < RowCount)
            {
                Rows.RemoveAt(RowCount - 1);
            }
            while (r > RowCount)
            {
                AddARow(RowCount);
            }
        }

        private void FillTextBoxes() //fill the textboxes
        {
            for (int r = 0; r < RowCount; r++)
            {
                for (int c = 0; c < ColumnCount; c++)
                {
                    Rows[r].Cells[c].Value = _mData[r, c]; //notice r, c
                }
            }
        }

        private void ExtractTextBoxes()
        {
            for (int r = 0; r < RowCount; r++)
            {
                for (int c = 0; c < ColumnCount; c++)
                {
                    _mData[r, c] = (string) Rows[r].Cells[c].EditedFormattedValue; //notice r, c 
                }
            }
        }

        #endregion

// Boolean flag used to determine when a character other than a number is entered.
        private bool NumberEntered;

        #region Key and keyboard processing

        //Check if key entered is "numeric"
        private bool CheckKey(Keys K, bool isDecimalPoint, bool isMinus)
        {
            if (K == Keys.Back) //backspace?
                return true;
            if (K == Keys.OemPeriod || K == Keys.Decimal) //decimal point?
                return isDecimalPoint ? false : true; //or: return !isDecimalPoint
            if (K == Keys.OemMinus || K == Keys.Subtract)
                return !isMinus;
            if ((K >= Keys.D0) && (K <= Keys.D9)) //digit from top of keyboard?
                return true;
            if ((K >= Keys.NumPad0) && (K <= Keys.NumPad9)) //digit from keypad?
                return true;
            return false; //no "numeric" key
        }

        // Handle the KeyDown event to determine the type of character entered into the control.
        // The method here should be registered as KeyEventHandler to the EditingControl 
        // of the DataGridView in order for it to work (took me some time to figure that out...)
        private void MatrixIO_KeyDown(object sender, KeyEventArgs e)
        {
            //we know we have columns of type DataGridViewTextBoxColumn so :
            var Tbx = (TextBox) sender;
            bool decimalTyped = Tbx.Text.Contains(".");
            bool minusTyped = Tbx.Text.Contains("-");
            // Initialize the flag.
            NumberEntered = CheckKey(e.KeyCode, decimalTyped, minusTyped);
        }

        // This event occurs after the KeyDown event and can be used to prevent
        // characters from entering the control.
        private void MatrixIO_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (NumberEntered == false)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        private void MatrixIO_KeyUp(object sender, KeyEventArgs e)
        {
            if (ModifierKeys == Keys.Shift)
            {
                NumberEntered = false;
            }
        }

        private void MatrixIO_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //Unsubscribe from the event in case it is subscribed
            EditingControl.KeyPress -= MatrixIO_KeyPress;
            EditingControl.KeyDown -= MatrixIO_KeyDown;
            EditingControl.KeyPress += MatrixIO_KeyPress;
            EditingControl.KeyDown += MatrixIO_KeyDown;
        }

        #endregion
    }
}