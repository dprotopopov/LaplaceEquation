namespace LaplaceEquation.Editor
{
    partial class ExecuteSolverDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExecuteSolverDialog));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.numericUpDownAlgorithmParameter = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownBlockSize = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownGridSize = new System.Windows.Forms.NumericUpDown();
            this.radioButtonCudafyMethod = new System.Windows.Forms.RadioButton();
            this.radioButtonNativeMethod = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownEpsilon = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAlgorithmParameter)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlockSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEpsilon)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.numericUpDownAlgorithmParameter);
            this.groupBox3.Location = new System.Drawing.Point(30, 22);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(648, 192);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Параметр";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(20, 21);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(596, 98);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // numericUpDownAlgorithmParameter
            // 
            this.numericUpDownAlgorithmParameter.DecimalPlaces = 4;
            this.numericUpDownAlgorithmParameter.Location = new System.Drawing.Point(261, 125);
            this.numericUpDownAlgorithmParameter.Name = "numericUpDownAlgorithmParameter";
            this.numericUpDownAlgorithmParameter.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownAlgorithmParameter.TabIndex = 0;
            this.numericUpDownAlgorithmParameter.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.numericUpDownBlockSize);
            this.groupBox4.Controls.Add(this.numericUpDownGridSize);
            this.groupBox4.Controls.Add(this.radioButtonCudafyMethod);
            this.groupBox4.Controls.Add(this.radioButtonNativeMethod);
            this.groupBox4.Location = new System.Drawing.Point(30, 322);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(648, 87);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Реализация";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(511, 48);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Опции";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.buttonCudaChoose_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(380, 49);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 22);
            this.textBox2.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(357, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "=";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(258, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "x";
            // 
            // numericUpDownBlockSize
            // 
            this.numericUpDownBlockSize.Location = new System.Drawing.Point(278, 49);
            this.numericUpDownBlockSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDownBlockSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBlockSize.Name = "numericUpDownBlockSize";
            this.numericUpDownBlockSize.Size = new System.Drawing.Size(72, 22);
            this.numericUpDownBlockSize.TabIndex = 3;
            this.numericUpDownBlockSize.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDownBlockSize.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // numericUpDownGridSize
            // 
            this.numericUpDownGridSize.Location = new System.Drawing.Point(173, 49);
            this.numericUpDownGridSize.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownGridSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownGridSize.Name = "numericUpDownGridSize";
            this.numericUpDownGridSize.Size = new System.Drawing.Size(77, 22);
            this.numericUpDownGridSize.TabIndex = 2;
            this.numericUpDownGridSize.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDownGridSize.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // radioButtonCudafyMethod
            // 
            this.radioButtonCudafyMethod.AutoSize = true;
            this.radioButtonCudafyMethod.Location = new System.Drawing.Point(40, 49);
            this.radioButtonCudafyMethod.Name = "radioButtonCudafyMethod";
            this.radioButtonCudafyMethod.Size = new System.Drawing.Size(73, 21);
            this.radioButtonCudafyMethod.TabIndex = 1;
            this.radioButtonCudafyMethod.TabStop = true;
            this.radioButtonCudafyMethod.Text = "Cudafy";
            this.radioButtonCudafyMethod.UseVisualStyleBackColor = true;
            // 
            // radioButtonNativeMethod
            // 
            this.radioButtonNativeMethod.AutoSize = true;
            this.radioButtonNativeMethod.Location = new System.Drawing.Point(40, 22);
            this.radioButtonNativeMethod.Name = "radioButtonNativeMethod";
            this.radioButtonNativeMethod.Size = new System.Drawing.Size(69, 21);
            this.radioButtonNativeMethod.TabIndex = 0;
            this.radioButtonNativeMethod.TabStop = true;
            this.radioButtonNativeMethod.Text = "Native";
            this.radioButtonNativeMethod.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(571, 459);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(490, 459);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Ok";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownEpsilon);
            this.groupBox1.Location = new System.Drawing.Point(30, 238);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(648, 65);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Погрешность";
            // 
            // numericUpDownEpsilon
            // 
            this.numericUpDownEpsilon.DecimalPlaces = 10;
            this.numericUpDownEpsilon.Location = new System.Drawing.Point(261, 21);
            this.numericUpDownEpsilon.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            917504});
            this.numericUpDownEpsilon.Name = "numericUpDownEpsilon";
            this.numericUpDownEpsilon.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownEpsilon.TabIndex = 0;
            this.numericUpDownEpsilon.Value = new decimal(new int[] {
            1,
            0,
            0,
            655360});
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(163, 153);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(327, 21);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Использовать релаксацию (красное-чёрное)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ExecuteSolverDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 547);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Name = "ExecuteSolverDialog";
            this.Text = "ExecuteSolverDialog";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAlgorithmParameter)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlockSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGridSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEpsilon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButtonCudafyMethod;
        private System.Windows.Forms.RadioButton radioButtonNativeMethod;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown numericUpDownAlgorithmParameter;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownEpsilon;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownBlockSize;
        private System.Windows.Forms.NumericUpDown numericUpDownGridSize;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}