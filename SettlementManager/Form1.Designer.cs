namespace SettlementManager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dataGridView1 = new DataGridView();
            button2 = new Button();
            button1 = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToFileToolStripMenuItem = new ToolStripMenuItem();
            loadFromFileToolStripMenuItem = new ToolStripMenuItem();
            sortToolStripMenuItem = new ToolStripMenuItem();
            byNameToolStripMenuItem = new ToolStripMenuItem();
            byPopulationToolStripMenuItem = new ToolStripMenuItem();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 40);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 82;
            dataGridView1.Size = new Size(1121, 452);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // button2
            // 
            button2.Location = new Point(960, 519);
            button2.Name = "button2";
            button2.Size = new Size(75, 74);
            button2.TabIndex = 1;
            button2.Text = "-";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(879, 519);
            button1.Name = "button1";
            button1.Size = new Size(75, 74);
            button1.TabIndex = 2;
            button1.Text = "+";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button2_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(32, 32);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, sortToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1145, 40);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToFileToolStripMenuItem, loadFromFileToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(71, 38);
            fileToolStripMenuItem.Text = "File";
            fileToolStripMenuItem.Click += fileToolStripMenuItem_Click;
            // 
            // saveToFileToolStripMenuItem
            // 
            saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
            saveToFileToolStripMenuItem.Size = new Size(296, 44);
            saveToFileToolStripMenuItem.Text = "Save to file";
            saveToFileToolStripMenuItem.Click += saveToFileToolStripMenuItem_Click;
            // 
            // loadFromFileToolStripMenuItem
            // 
            loadFromFileToolStripMenuItem.Name = "loadFromFileToolStripMenuItem";
            loadFromFileToolStripMenuItem.Size = new Size(296, 44);
            loadFromFileToolStripMenuItem.Text = "Load from file";
            loadFromFileToolStripMenuItem.Click += loadFromFileToolStripMenuItem_Click;
            // 
            // sortToolStripMenuItem
            // 
            sortToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { byNameToolStripMenuItem, byPopulationToolStripMenuItem });
            sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            sortToolStripMenuItem.Size = new Size(77, 38);
            sortToolStripMenuItem.Text = "Sort";
            sortToolStripMenuItem.Click += sortToolStripMenuItem_Click;
            // 
            // byNameToolStripMenuItem
            // 
            byNameToolStripMenuItem.Name = "byNameToolStripMenuItem";
            byNameToolStripMenuItem.Size = new Size(359, 44);
            byNameToolStripMenuItem.Text = "by name";
            byNameToolStripMenuItem.Click += byNameToolStripMenuItem_Click;
            // 
            // byPopulationToolStripMenuItem
            // 
            byPopulationToolStripMenuItem.Name = "byPopulationToolStripMenuItem";
            byPopulationToolStripMenuItem.Size = new Size(359, 44);
            byPopulationToolStripMenuItem.Text = "by population";
            byPopulationToolStripMenuItem.Click += byPopulationToolStripMenuItem_Click;
            // 
            // button3
            // 
            button3.Location = new Point(723, 547);
            button3.Name = "button3";
            button3.Size = new Size(150, 46);
            button3.TabIndex = 5;
            button3.Text = "Search";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1145, 634);
            Controls.Add(button3);
            Controls.Add(menuStrip1);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(dataGridView1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button2;
        private Button button1;
        private ContextMenuStrip contextMenuStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToFileToolStripMenuItem;
        private ToolStripMenuItem loadFromFileToolStripMenuItem;
        private ToolStripMenuItem sortToolStripMenuItem;
        private ToolStripMenuItem byNameToolStripMenuItem;
        private ToolStripMenuItem byPopulationToolStripMenuItem;
        private Button button3;
    }
}
