﻿namespace LukMachine
{
    partial class AutoScrn
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
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoScrn));
      this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.TimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.PressureColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.button3 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.listBox1 = new System.Windows.Forms.ListBox();
      this.label12 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.SuspendLayout();
      // 
      // chart1
      // 
      this.chart1.BorderlineColor = System.Drawing.Color.Black;
      this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
      chartArea1.Name = "ChartArea1";
      this.chart1.ChartAreas.Add(chartArea1);
      this.chart1.Location = new System.Drawing.Point(12, 223);
      this.chart1.Name = "chart1";
      series1.ChartArea = "ChartArea1";
      series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
      series1.IsVisibleInLegend = false;
      series1.Name = "Series1";
      this.chart1.Series.Add(series1);
      this.chart1.Size = new System.Drawing.Size(664, 308);
      this.chart1.TabIndex = 0;
      this.chart1.Text = "chart1";
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.AllowUserToResizeColumns = false;
      this.dataGridView1.AllowUserToResizeRows = false;
      this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
      this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TimeColumn,
            this.PressureColumn});
      this.dataGridView1.Location = new System.Drawing.Point(682, 223);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      this.dataGridView1.RowHeadersVisible = false;
      this.dataGridView1.Size = new System.Drawing.Size(213, 217);
      this.dataGridView1.TabIndex = 1;
      // 
      // TimeColumn
      // 
      this.TimeColumn.HeaderText = "Time(sec)";
      this.TimeColumn.Name = "TimeColumn";
      this.TimeColumn.ReadOnly = true;
      // 
      // PressureColumn
      // 
      this.PressureColumn.HeaderText = "Pressure";
      this.PressureColumn.Name = "PressureColumn";
      this.PressureColumn.ReadOnly = true;
      // 
      // button3
      // 
      this.button3.Image = global::LukMachine.Properties.Resources.base_cog_32;
      this.button3.Location = new System.Drawing.Point(828, 446);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(67, 85);
      this.button3.TabIndex = 5;
      this.button3.Text = "Manual Control";
      this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // button2
      // 
      this.button2.Enabled = false;
      this.button2.Image = global::LukMachine.Properties.Resources._109_AllAnnotations_Error_32x32_72;
      this.button2.Location = new System.Drawing.Point(755, 446);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(67, 85);
      this.button2.TabIndex = 3;
      this.button2.Text = "Abort Test";
      this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button1
      // 
      this.button1.Image = global::LukMachine.Properties.Resources._1427928241_Play;
      this.button1.Location = new System.Drawing.Point(682, 446);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(67, 85);
      this.button1.TabIndex = 2;
      this.button1.Text = "Start Test";
      this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 534);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(92, 19);
      this.label1.TabIndex = 6;
      this.label1.Text = "Test Status:";
      // 
      // listBox1
      // 
      this.listBox1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.listBox1.FormattingEnabled = true;
      this.listBox1.ItemHeight = 16;
      this.listBox1.Location = new System.Drawing.Point(15, 553);
      this.listBox1.Name = "listBox1";
      this.listBox1.Size = new System.Drawing.Size(880, 68);
      this.listBox1.TabIndex = 7;
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(485, 180);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(154, 19);
      this.label12.TabIndex = 39;
      this.label12.Text = "Select Temperature:";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(485, 117);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(135, 19);
      this.label8.TabIndex = 38;
      this.label8.Text = "Select Flow Rate:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(485, 98);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(190, 19);
      this.label5.TabIndex = 37;
      this.label5.Text = "Select Sample Chamber:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(485, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(90, 19);
      this.label2.TabIndex = 34;
      this.label2.Text = "Sample ID:";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(485, 79);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(79, 19);
      this.label6.TabIndex = 36;
      this.label6.Text = "Data File:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(485, 47);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(98, 19);
      this.label3.TabIndex = 35;
      this.label3.Text = "Lot Number:";
      // 
      // AutoScrn
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.ClientSize = new System.Drawing.Size(905, 633);
      this.Controls.Add(this.label12);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.listBox1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.dataGridView1);
      this.Controls.Add(this.chart1);
      this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.MinimumSize = new System.Drawing.Size(923, 468);
      this.Name = "AutoScrn";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.AutoScrn_Load);
      ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PressureColumn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label3;
  }
}