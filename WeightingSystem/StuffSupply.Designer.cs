namespace WeightingSystem
{
    partial class StuffSupply
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StuffSupply));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.StuffGrid = new System.Windows.Forms.DataGridView();
            this.Stuffid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StuffName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StoreNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.SupplyGrid = new System.Windows.Forms.DataGridView();
            this.chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Supply = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.b_delete = new System.Windows.Forms.Button();
            this.b_edit = new System.Windows.Forms.Button();
            this.b_add = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StuffGrid)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SupplyGrid)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 527);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(872, 88);
            this.panel1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(169, 23);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(131, 49);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnExit
            // 
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(729, 23);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(131, 49);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdate.Location = new System.Drawing.Point(13, 23);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(131, 49);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "修改";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.EnabledChanged += new System.EventHandler(this.btnUpdate_EnabledChanged);
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(547, 527);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "材料信息";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.StuffGrid);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(4, 54);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(539, 469);
            this.panel3.TabIndex = 2;
            // 
            // StuffGrid
            // 
            this.StuffGrid.AllowUserToAddRows = false;
            this.StuffGrid.AllowUserToDeleteRows = false;
            this.StuffGrid.AllowUserToResizeRows = false;
            this.StuffGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.StuffGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StuffGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StuffGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Stuffid,
            this.StuffName,
            this.Spec,
            this.PductName,
            this.StoreNum});
            this.StuffGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StuffGrid.Location = new System.Drawing.Point(0, 0);
            this.StuffGrid.Margin = new System.Windows.Forms.Padding(4);
            this.StuffGrid.MultiSelect = false;
            this.StuffGrid.Name = "StuffGrid";
            this.StuffGrid.ReadOnly = true;
            this.StuffGrid.RowHeadersVisible = false;
            this.StuffGrid.RowTemplate.Height = 23;
            this.StuffGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.StuffGrid.Size = new System.Drawing.Size(539, 469);
            this.StuffGrid.TabIndex = 0;
            this.StuffGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StuffGrid_CellContentClick);
            this.StuffGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.StuffGrid_CellValueChanged);
            this.StuffGrid.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.StuffGrid_RowStateChanged);
            // 
            // Stuffid
            // 
            this.Stuffid.DataPropertyName = "StuffID";
            this.Stuffid.HeaderText = "";
            this.Stuffid.Name = "Stuffid";
            this.Stuffid.ReadOnly = true;
            this.Stuffid.Visible = false;
            // 
            // StuffName
            // 
            this.StuffName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.StuffName.DataPropertyName = "StuffName";
            this.StuffName.HeaderText = "材料名称";
            this.StuffName.Name = "StuffName";
            this.StuffName.ReadOnly = true;
            // 
            // Spec
            // 
            this.Spec.DataPropertyName = "Spec";
            this.Spec.HeaderText = "规格";
            this.Spec.Name = "Spec";
            this.Spec.ReadOnly = true;
            // 
            // PductName
            // 
            this.PductName.DataPropertyName = "SupplyName";
            this.PductName.HeaderText = "厂家";
            this.PductName.Name = "PductName";
            this.PductName.ReadOnly = true;
            // 
            // StoreNum
            // 
            this.StoreNum.DataPropertyName = "Inventory";
            this.StoreNum.HeaderText = "库存(T)";
            this.StoreNum.Name = "StoreNum";
            this.StoreNum.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(539, 31);
            this.panel2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(398, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 26);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(383, 26);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel5);
            this.groupBox2.Controls.Add(this.panel4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(547, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(325, 527);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "供应商信息";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.SupplyGrid);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(4, 94);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(317, 429);
            this.panel5.TabIndex = 3;
            // 
            // SupplyGrid
            // 
            this.SupplyGrid.AllowUserToAddRows = false;
            this.SupplyGrid.AllowUserToDeleteRows = false;
            this.SupplyGrid.AllowUserToResizeRows = false;
            this.SupplyGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.SupplyGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SupplyGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SupplyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SupplyGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chk,
            this.Supply,
            this.ID});
            this.SupplyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SupplyGrid.Location = new System.Drawing.Point(0, 0);
            this.SupplyGrid.Margin = new System.Windows.Forms.Padding(4);
            this.SupplyGrid.MultiSelect = false;
            this.SupplyGrid.Name = "SupplyGrid";
            this.SupplyGrid.RowHeadersVisible = false;
            this.SupplyGrid.RowTemplate.Height = 23;
            this.SupplyGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SupplyGrid.Size = new System.Drawing.Size(317, 429);
            this.SupplyGrid.TabIndex = 0;
            // 
            // chk
            // 
            this.chk.FillWeight = 40.60914F;
            this.chk.HeaderText = "";
            this.chk.Name = "chk";
            this.chk.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Supply
            // 
            this.Supply.DataPropertyName = "text";
            this.Supply.FillWeight = 159.3909F;
            this.Supply.HeaderText = "供应商";
            this.Supply.Name = "Supply";
            this.Supply.ReadOnly = true;
            this.Supply.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Supply.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "value";
            this.ID.HeaderText = "";
            this.ID.Name = "ID";
            this.ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ID.Visible = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.b_delete);
            this.panel4.Controls.Add(this.b_edit);
            this.panel4.Controls.Add(this.b_add);
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.textBox2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(4, 23);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(317, 71);
            this.panel4.TabIndex = 2;
            // 
            // b_delete
            // 
            this.b_delete.Location = new System.Drawing.Point(221, 34);
            this.b_delete.Name = "b_delete";
            this.b_delete.Size = new System.Drawing.Size(75, 23);
            this.b_delete.TabIndex = 5;
            this.b_delete.Text = "停用";
            this.b_delete.UseVisualStyleBackColor = true;
            this.b_delete.Visible = false;
            this.b_delete.Click += new System.EventHandler(this.b_delete_Click);
            // 
            // b_edit
            // 
            this.b_edit.Location = new System.Drawing.Point(125, 34);
            this.b_edit.Name = "b_edit";
            this.b_edit.Size = new System.Drawing.Size(75, 23);
            this.b_edit.TabIndex = 4;
            this.b_edit.Text = "修改";
            this.b_edit.UseVisualStyleBackColor = true;
            this.b_edit.Visible = false;
            this.b_edit.Click += new System.EventHandler(this.b_edit_Click);
            // 
            // b_add
            // 
            this.b_add.Location = new System.Drawing.Point(25, 34);
            this.b_add.Name = "b_add";
            this.b_add.Size = new System.Drawing.Size(75, 23);
            this.b_add.TabIndex = 3;
            this.b_add.Text = "新增";
            this.b_add.UseVisualStyleBackColor = true;
            this.b_add.Visible = false;
            this.b_add.Click += new System.EventHandler(this.b_add_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(260, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(49, 26);
            this.button2.TabIndex = 2;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(3, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(251, 26);
            this.textBox2.TabIndex = 1;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // StuffSupply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 615);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = global::WeightingSystem.Properties.Resources.LOGO;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StuffSupply";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "原料供应商管理";
            this.Load += new System.EventHandler(this.StuffSupply_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StuffGrid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SupplyGrid)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView SupplyGrid;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView StuffGrid;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button b_delete;
        private System.Windows.Forms.Button b_edit;
        private System.Windows.Forms.Button b_add;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chk;
        private System.Windows.Forms.DataGridViewTextBoxColumn Supply;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stuffid;
        private System.Windows.Forms.DataGridViewTextBoxColumn StuffName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn PductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoreNum;
    }
}