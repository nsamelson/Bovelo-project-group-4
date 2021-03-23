
namespace Bovelo
{
    partial class Manager_Manager_Stock
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Id_order_details = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time_to_build = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Set = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 65);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(100, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(82, 52);
            this.button4.TabIndex = 6;
            this.button4.Text = "Provider Catalog";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(12, 12);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(82, 52);
            this.button7.TabIndex = 5;
            this.button7.Text = "menu";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(617, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(158, 62);
            this.button3.TabIndex = 4;
            this.button3.Text = "Provider Orders";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(557, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 55);
            this.button1.TabIndex = 2;
            this.button1.Text = "Profile/Login";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("PMingLiU-ExtB", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(354, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bovelo";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id_order_details,
            this.Column2,
            this.Quantity,
            this.location,
            this.Column3,
            this.Column4,
            this.Time_to_build,
            this.Set});
            this.dataGridView1.Location = new System.Drawing.Point(12, 68);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(776, 367);
            this.dataGridView1.TabIndex = 17;
            // 
            // Id_order_details
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Id_order_details.DefaultCellStyle = dataGridViewCellStyle1;
            this.Id_order_details.FillWeight = 49.79281F;
            this.Id_order_details.HeaderText = "Id parts";
            this.Id_order_details.MinimumWidth = 6;
            this.Id_order_details.Name = "Id_order_details";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 249.6172F;
            this.Column2.HeaderText = "Name :";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Quantity.FillWeight = 93.3568F;
            this.Quantity.HeaderText = "Quantity in stock";
            this.Quantity.MinimumWidth = 6;
            this.Quantity.Name = "Quantity";
            // 
            // location
            // 
            this.location.HeaderText = "Location";
            this.location.Name = "location";
            // 
            // Column3
            // 
            this.Column3.FillWeight = 75.99451F;
            this.Column3.HeaderText = "Price";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.FillWeight = 148.2329F;
            this.Column4.HeaderText = "Provider";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            // 
            // Time_to_build
            // 
            this.Time_to_build.HeaderText = "Time to build";
            this.Time_to_build.Name = "Time_to_build";
            // 
            // Set
            // 
            this.Set.FillWeight = 75.99451F;
            this.Set.HeaderText = "Set Part";
            this.Set.MinimumWidth = 6;
            this.Set.Name = "Set";
            this.Set.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Set.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Set.ToolTipText = "Set";
            this.Set.UseColumnTextForButtonValue = true;
            // 
            // Manager_Manager_Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "Manager_Manager_Stock";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_order_details;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn location;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time_to_build;
        private System.Windows.Forms.DataGridViewButtonColumn Set;
    }
}