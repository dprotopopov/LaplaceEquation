namespace LaplaceEquation.Editor
{
    partial class MatrixForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownVertLength = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHorzLength = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownVertSize = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHorzSize = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVertLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHorzLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVertSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHorzSize)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(948, 571);
            this.splitContainer1.SplitterDistance = 391;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownVertLength);
            this.groupBox1.Controls.Add(this.numericUpDownHorzLength);
            this.groupBox1.Controls.Add(this.numericUpDownVertSize);
            this.groupBox1.Controls.Add(this.numericUpDownHorzSize);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(391, 571);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // numericUpDownVertLength
            // 
            this.numericUpDownVertLength.DecimalPlaces = 4;
            this.numericUpDownVertLength.Location = new System.Drawing.Point(219, 86);
            this.numericUpDownVertLength.Name = "numericUpDownVertLength";
            this.numericUpDownVertLength.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownVertLength.TabIndex = 9;
            // 
            // numericUpDownHorzLength
            // 
            this.numericUpDownHorzLength.DecimalPlaces = 4;
            this.numericUpDownHorzLength.Location = new System.Drawing.Point(92, 86);
            this.numericUpDownHorzLength.Name = "numericUpDownHorzLength";
            this.numericUpDownHorzLength.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownHorzLength.TabIndex = 8;
            // 
            // numericUpDownVertSize
            // 
            this.numericUpDownVertSize.Location = new System.Drawing.Point(219, 57);
            this.numericUpDownVertSize.Name = "numericUpDownVertSize";
            this.numericUpDownVertSize.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownVertSize.TabIndex = 7;
            this.numericUpDownVertSize.ValueChanged += new System.EventHandler(this.SizesValueChanged);
            // 
            // numericUpDownHorzSize
            // 
            this.numericUpDownHorzSize.Location = new System.Drawing.Point(92, 57);
            this.numericUpDownHorzSize.Name = "numericUpDownHorzSize";
            this.numericUpDownHorzSize.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownHorzSize.TabIndex = 6;
            this.numericUpDownHorzSize.ValueChanged += new System.EventHandler(this.SizesValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(216, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Вертикально";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Горизонтально";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Длина";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Размер";
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(553, 571);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // MatrixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 571);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MatrixForm";
            this.Text = "MatrixForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVertLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHorzLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVertSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHorzSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDownVertSize;
        private System.Windows.Forms.NumericUpDown numericUpDownHorzSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownVertLength;
        private System.Windows.Forms.NumericUpDown numericUpDownHorzLength;

    }
}