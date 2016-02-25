namespace LukMachine
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoScrn));
      this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.button3 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.listBox1 = new System.Windows.Forms.ListBox();
      this.verticalProgressBar2 = new LukMachine.VerticalProgressBar();
      this.verticalProgressBar1 = new LukMachine.VerticalProgressBar();
      this.panel1 = new System.Windows.Forms.Panel();
      this.label4 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.groupBoxPressure = new System.Windows.Forms.GroupBox();
      this.labelPressure = new System.Windows.Forms.Label();
      this.groupBoxCollectedVolume = new System.Windows.Forms.GroupBox();
      this.groupBoxReservoir = new System.Windows.Forms.GroupBox();
      this.TimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.PressureColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.panel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBoxPressure.SuspendLayout();
      this.groupBoxCollectedVolume.SuspendLayout();
      this.groupBoxReservoir.SuspendLayout();
      this.SuspendLayout();
      // 
      // chart1
      // 
      this.chart1.BorderlineColor = System.Drawing.Color.Black;
      this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
      chartArea1.Name = "ChartArea1";
      this.chart1.ChartAreas.Add(chartArea1);
      this.chart1.Location = new System.Drawing.Point(12, 208);
      this.chart1.Name = "chart1";
      series1.ChartArea = "ChartArea1";
      series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
      series1.IsVisibleInLegend = false;
      series1.Name = "Series1";
      this.chart1.Series.Add(series1);
      this.chart1.Size = new System.Drawing.Size(646, 308);
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
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TimeColumn,
            this.PressureColumn});
      this.dataGridView1.Location = new System.Drawing.Point(664, 208);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
      this.dataGridView1.RowHeadersVisible = false;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
      this.dataGridView1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.dataGridView1.Size = new System.Drawing.Size(231, 308);
      this.dataGridView1.TabIndex = 1;
      // 
      // button3
      // 
      this.button3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button3.Image = global::LukMachine.Properties.Resources.base_cog_32;
      this.button3.Location = new System.Drawing.Point(591, 419);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(67, 85);
      this.button3.TabIndex = 5;
      this.button3.Text = "Manual Control";
      this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Visible = false;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // button2
      // 
      this.button2.Enabled = false;
      this.button2.Image = global::LukMachine.Properties.Resources._109_AllAnnotations_Error_32x32_72;
      this.button2.Location = new System.Drawing.Point(664, 528);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(229, 86);
      this.button2.TabIndex = 3;
      this.button2.Text = "Stop Test";
      this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button1
      // 
      this.button1.Image = global::LukMachine.Properties.Resources._1427928241_Play;
      this.button1.Location = new System.Drawing.Point(495, 418);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(67, 85);
      this.button1.TabIndex = 2;
      this.button1.Text = "Start Test";
      this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Visible = false;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 528);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(115, 23);
      this.label1.TabIndex = 6;
      this.label1.Text = "Test Status:";
      // 
      // listBox1
      // 
      this.listBox1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.listBox1.FormattingEnabled = true;
      this.listBox1.ItemHeight = 19;
      this.listBox1.Location = new System.Drawing.Point(12, 553);
      this.listBox1.Name = "listBox1";
      this.listBox1.Size = new System.Drawing.Size(646, 61);
      this.listBox1.TabIndex = 7;
      // 
      // verticalProgressBar2
      // 
      this.verticalProgressBar2.Location = new System.Drawing.Point(64, 42);
      this.verticalProgressBar2.Name = "verticalProgressBar2";
      this.verticalProgressBar2.Size = new System.Drawing.Size(84, 124);
      this.verticalProgressBar2.TabIndex = 10;
      this.verticalProgressBar2.Value = 50;
      // 
      // verticalProgressBar1
      // 
      this.verticalProgressBar1.Location = new System.Drawing.Point(63, 42);
      this.verticalProgressBar1.Name = "verticalProgressBar1";
      this.verticalProgressBar1.Size = new System.Drawing.Size(84, 124);
      this.verticalProgressBar1.TabIndex = 9;
      this.verticalProgressBar1.Value = 50;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.label4);
      this.panel1.Location = new System.Drawing.Point(111, 272);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(547, 178);
      this.panel1.TabIndex = 13;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(51, 81);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(432, 23);
      this.label4.TabIndex = 0;
      this.label4.Text = "Please wait...  (setting reservoirs / temperature)";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(37, 110);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(103, 24);
      this.label6.TabIndex = 16;
      this.label6.Text = "Current  =";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(36, 59);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(186, 24);
      this.label8.TabIndex = 14;
      this.label8.Text = "Target    =   not set";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label8);
      this.groupBox1.Controls.Add(this.label6);
      this.groupBox1.Location = new System.Drawing.Point(449, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(248, 190);
      this.groupBox1.TabIndex = 18;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Temperature";
      // 
      // groupBoxPressure
      // 
      this.groupBoxPressure.Controls.Add(this.labelPressure);
      this.groupBoxPressure.Location = new System.Drawing.Point(703, 12);
      this.groupBoxPressure.Name = "groupBoxPressure";
      this.groupBoxPressure.Size = new System.Drawing.Size(190, 190);
      this.groupBoxPressure.TabIndex = 19;
      this.groupBoxPressure.TabStop = false;
      this.groupBoxPressure.Text = "Pressure";
      // 
      // labelPressure
      // 
      this.labelPressure.AutoSize = true;
      this.labelPressure.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelPressure.Location = new System.Drawing.Point(61, 65);
      this.labelPressure.Name = "labelPressure";
      this.labelPressure.Size = new System.Drawing.Size(44, 24);
      this.labelPressure.TabIndex = 15;
      this.labelPressure.Text = "PSI";
      // 
      // groupBoxCollectedVolume
      // 
      this.groupBoxCollectedVolume.Controls.Add(this.verticalProgressBar2);
      this.groupBoxCollectedVolume.Location = new System.Drawing.Point(224, 12);
      this.groupBoxCollectedVolume.Name = "groupBoxCollectedVolume";
      this.groupBoxCollectedVolume.Size = new System.Drawing.Size(219, 190);
      this.groupBoxCollectedVolume.TabIndex = 20;
      this.groupBoxCollectedVolume.TabStop = false;
      this.groupBoxCollectedVolume.Text = "Collected Volume";
      // 
      // groupBoxReservoir
      // 
      this.groupBoxReservoir.Controls.Add(this.verticalProgressBar1);
      this.groupBoxReservoir.Location = new System.Drawing.Point(13, 12);
      this.groupBoxReservoir.Name = "groupBoxReservoir";
      this.groupBoxReservoir.Size = new System.Drawing.Size(205, 190);
      this.groupBoxReservoir.TabIndex = 21;
      this.groupBoxReservoir.TabStop = false;
      this.groupBoxReservoir.Text = "Reservoir";
      // 
      // TimeColumn
      // 
      this.TimeColumn.HeaderText = "Time(sec)";
      this.TimeColumn.Name = "TimeColumn";
      this.TimeColumn.ReadOnly = true;
      // 
      // PressureColumn
      // 
      this.PressureColumn.HeaderText = "Volume(ml)";
      this.PressureColumn.Name = "PressureColumn";
      this.PressureColumn.ReadOnly = true;
      // 
      // AutoScrn
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.ClientSize = new System.Drawing.Size(905, 629);
      this.Controls.Add(this.groupBoxReservoir);
      this.Controls.Add(this.groupBoxCollectedVolume);
      this.Controls.Add(this.groupBoxPressure);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.panel1);
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
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBoxPressure.ResumeLayout(false);
      this.groupBoxPressure.PerformLayout();
      this.groupBoxCollectedVolume.ResumeLayout(false);
      this.groupBoxReservoir.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ListBox listBox1;
    private VerticalProgressBar verticalProgressBar1;
    private VerticalProgressBar verticalProgressBar2;
    private System.Windows.Forms.Label label4;
    public System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBoxPressure;
    private System.Windows.Forms.Label labelPressure;
    private System.Windows.Forms.GroupBox groupBoxCollectedVolume;
    private System.Windows.Forms.GroupBox groupBoxReservoir;
    private System.Windows.Forms.DataGridViewTextBoxColumn TimeColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn PressureColumn;
    // private System.Windows.Controls.ProgressBar progbar;


  }
}