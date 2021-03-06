namespace WeightingSystem
{
    partial class EmptyCarWeighting
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblWeight = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.cbCar = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.btnCarNoSoftKeyboard = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.lblWeight);
            this.panel1.Location = new System.Drawing.Point(154, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 61);
            this.panel1.TabIndex = 2;
            // 
            // lblWeight
            // 
            this.lblWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWeight.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeight.ForeColor = System.Drawing.Color.Red;
            this.lblWeight.Location = new System.Drawing.Point(0, 0);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblWeight.Size = new System.Drawing.Size(220, 61);
            this.lblWeight.TabIndex = 0;
            this.lblWeight.Text = "0";
            this.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(26, 115);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(59, 16);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "搅拌车";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // cbCar
            // 
            this.cbCar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbCar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCar.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCar.FormattingEnabled = true;
            this.cbCar.Location = new System.Drawing.Point(91, 109);
            this.cbCar.Name = "cbCar";
            this.cbCar.Size = new System.Drawing.Size(117, 27);
            this.cbCar.TabIndex = 37;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBox1.Enabled = false;
            this.comboBox1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(295, 109);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(148, 27);
            this.comboBox1.TabIndex = 39;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(242, 115);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 16);
            this.radioButton2.TabIndex = 38;
            this.radioButton2.Text = "货车";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // btnCarNoSoftKeyboard
            // 
            this.btnCarNoSoftKeyboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCarNoSoftKeyboard.Enabled = false;
            this.btnCarNoSoftKeyboard.Location = new System.Drawing.Point(459, 110);
            this.btnCarNoSoftKeyboard.Name = "btnCarNoSoftKeyboard";
            this.btnCarNoSoftKeyboard.Size = new System.Drawing.Size(37, 27);
            this.btnCarNoSoftKeyboard.TabIndex = 40;
            this.btnCarNoSoftKeyboard.Text = "...";
            this.btnCarNoSoftKeyboard.UseVisualStyleBackColor = true;
            this.btnCarNoSoftKeyboard.Click += new System.EventHandler(this.btnCarNoSoftKeyboard_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCancle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancle.Location = new System.Drawing.Point(277, 173);
            this.btnCancle.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(93, 49);
            this.btnCancle.TabIndex = 48;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(144, 173);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(93, 49);
            this.btnOK.TabIndex = 47;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // EmptyCarWeighting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 266);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCarNoSoftKeyboard);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.cbCar);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmptyCarWeighting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "空车过磅";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.ComboBox cbCar;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button btnCarNoSoftKeyboard;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnOK;
    }
}