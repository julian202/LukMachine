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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.label4 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.labelDurations = new System.Windows.Forms.Label();
      this.labelStepsTotal = new System.Windows.Forms.Label();
      this.labelTargetPressure = new System.Windows.Forms.Label();
      this.groupBoxPressure = new System.Windows.Forms.GroupBox();
      this.labelTotalTime = new System.Windows.Forms.Label();
      this.labelStepCurrent = new System.Windows.Forms.Label();
      this.labelPumpState = new System.Windows.Forms.Label();
      this.labelPressure = new System.Windows.Forms.Label();
      this.labelDuration = new System.Windows.Forms.Label();
      this.groupBoxCollectedVolume = new System.Windows.Forms.GroupBox();
      this.verticalProgressBar2 = new LukMachine.VerticalProgressBar();
      this.groupBoxReservoir = new System.Windows.Forms.GroupBox();
      this.verticalProgressBar1 = new LukMachine.VerticalProgressBar();
      this.button4 = new System.Windows.Forms.Button();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.buttonSkipSettingTemp = new System.Windows.Forms.Button();
      this.buttonSkipPressure = new System.Windows.Forms.Button();
      this.labelChamber = new System.Windows.Forms.Label();
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
      // TimeColumn
      // 
      this.TimeColumn.HeaderText = "Time(sec)";
      this.TimeColumn.Name = "TimeColumn";
      this.TimeColumn.ReadOnly = true;
      // 
      // PressureColumn
      // 
      this.PressureColumn.HeaderText = "Flow(mL/min)";
      this.PressureColumn.Name = "PressureColumn";
      this.PressureColumn.ReadOnly = true;
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
      // panel1
      // 
      this.panel1.Controls.Add(this.buttonSkipPressure);
      this.panel1.Controls.Add(this.buttonSkipSettingTemp);
      this.panel1.Controls.Add(this.label4);
      this.panel1.Location = new System.Drawing.Point(30, 272);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(614, 178);
      this.panel1.TabIndex = 13;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(41, 61);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(527, 23);
      this.label4.TabIndex = 0;
      this.label4.Text = "Please wait...  (setting reservoirs / temperature / pressure)";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(20, 93);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(132, 21);
      this.label6.TabIndex = 16;
      this.label6.Text = "Temperature  =";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(24, 93);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(201, 21);
      this.label8.TabIndex = 14;
      this.label8.Text = "Temperature  =   not set";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.labelChamber);
      this.groupBox1.Controls.Add(this.labelDurations);
      this.groupBox1.Controls.Add(this.labelStepsTotal);
      this.groupBox1.Controls.Add(this.label8);
      this.groupBox1.Controls.Add(this.labelTargetPressure);
      this.groupBox1.Location = new System.Drawing.Point(467, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(210, 190);
      this.groupBox1.TabIndex = 18;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Programmed Parameters";
      // 
      // labelDurations
      // 
      this.labelDurations.AutoSize = true;
      this.labelDurations.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelDurations.Location = new System.Drawing.Point(24, 63);
      this.labelDurations.Name = "labelDurations";
      this.labelDurations.Size = new System.Drawing.Size(99, 21);
      this.labelDurations.TabIndex = 21;
      this.labelDurations.Text = "Duration  =";
      // 
      // labelStepsTotal
      // 
      this.labelStepsTotal.AutoSize = true;
      this.labelStepsTotal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelStepsTotal.Location = new System.Drawing.Point(24, 33);
      this.labelStepsTotal.Name = "labelStepsTotal";
      this.labelStepsTotal.Size = new System.Drawing.Size(121, 21);
      this.labelStepsTotal.TabIndex = 17;
      this.labelStepsTotal.Text = "Total Steps  =";
      // 
      // labelTargetPressure
      // 
      this.labelTargetPressure.AutoSize = true;
      this.labelTargetPressure.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelTargetPressure.Location = new System.Drawing.Point(24, 123);
      this.labelTargetPressure.Name = "labelTargetPressure";
      this.labelTargetPressure.Size = new System.Drawing.Size(177, 21);
      this.labelTargetPressure.TabIndex = 17;
      this.labelTargetPressure.Text = "Pressure   =   not set";
      // 
      // groupBoxPressure
      // 
      this.groupBoxPressure.Controls.Add(this.labelTotalTime);
      this.groupBoxPressure.Controls.Add(this.labelStepCurrent);
      this.groupBoxPressure.Controls.Add(this.labelPumpState);
      this.groupBoxPressure.Controls.Add(this.label6);
      this.groupBoxPressure.Controls.Add(this.labelPressure);
      this.groupBoxPressure.Location = new System.Drawing.Point(683, 12);
      this.groupBoxPressure.Name = "groupBoxPressure";
      this.groupBoxPressure.Size = new System.Drawing.Size(210, 190);
      this.groupBoxPressure.TabIndex = 19;
      this.groupBoxPressure.TabStop = false;
      this.groupBoxPressure.Text = "Current State";
      // 
      // labelTotalTime
      // 
      this.labelTotalTime.AutoSize = true;
      this.labelTotalTime.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelTotalTime.Location = new System.Drawing.Point(20, 63);
      this.labelTotalTime.Name = "labelTotalTime";
      this.labelTotalTime.Size = new System.Drawing.Size(103, 21);
      this.labelTotalTime.TabIndex = 21;
      this.labelTotalTime.Text = "Total time =";
      // 
      // labelStepCurrent
      // 
      this.labelStepCurrent.AutoSize = true;
      this.labelStepCurrent.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelStepCurrent.Location = new System.Drawing.Point(20, 33);
      this.labelStepCurrent.Name = "labelStepCurrent";
      this.labelStepCurrent.Size = new System.Drawing.Size(68, 21);
      this.labelStepCurrent.TabIndex = 18;
      this.labelStepCurrent.Text = "Step  =";
      // 
      // labelPumpState
      // 
      this.labelPumpState.AutoSize = true;
      this.labelPumpState.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelPumpState.Location = new System.Drawing.Point(20, 153);
      this.labelPumpState.Name = "labelPumpState";
      this.labelPumpState.Size = new System.Drawing.Size(133, 21);
      this.labelPumpState.TabIndex = 19;
      this.labelPumpState.Text = "Pump Power  =";
      // 
      // labelPressure
      // 
      this.labelPressure.AutoSize = true;
      this.labelPressure.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelPressure.Location = new System.Drawing.Point(20, 123);
      this.labelPressure.Name = "labelPressure";
      this.labelPressure.Size = new System.Drawing.Size(98, 21);
      this.labelPressure.TabIndex = 15;
      this.labelPressure.Text = "Pressure =";
      // 
      // labelDuration
      // 
      this.labelDuration.AutoSize = true;
      this.labelDuration.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelDuration.Location = new System.Drawing.Point(532, 218);
      this.labelDuration.Name = "labelDuration";
      this.labelDuration.Size = new System.Drawing.Size(106, 21);
      this.labelDuration.TabIndex = 20;
      this.labelDuration.Text = "Step time  =";
      this.labelDuration.Visible = false;
      // 
      // groupBoxCollectedVolume
      // 
      this.groupBoxCollectedVolume.Controls.Add(this.verticalProgressBar2);
      this.groupBoxCollectedVolume.Location = new System.Drawing.Point(236, 12);
      this.groupBoxCollectedVolume.Name = "groupBoxCollectedVolume";
      this.groupBoxCollectedVolume.Size = new System.Drawing.Size(225, 190);
      this.groupBoxCollectedVolume.TabIndex = 20;
      this.groupBoxCollectedVolume.TabStop = false;
      this.groupBoxCollectedVolume.Text = "Collected Volume";
      // 
      // verticalProgressBar2
      // 
      this.verticalProgressBar2.Location = new System.Drawing.Point(65, 42);
      this.verticalProgressBar2.Name = "verticalProgressBar2";
      this.verticalProgressBar2.Size = new System.Drawing.Size(84, 124);
      this.verticalProgressBar2.TabIndex = 10;
      this.verticalProgressBar2.Value = 50;
      // 
      // groupBoxReservoir
      // 
      this.groupBoxReservoir.Controls.Add(this.verticalProgressBar1);
      this.groupBoxReservoir.Location = new System.Drawing.Point(13, 12);
      this.groupBoxReservoir.Name = "groupBoxReservoir";
      this.groupBoxReservoir.Size = new System.Drawing.Size(217, 190);
      this.groupBoxReservoir.TabIndex = 21;
      this.groupBoxReservoir.TabStop = false;
      this.groupBoxReservoir.Text = "Reservoir";
      // 
      // verticalProgressBar1
      // 
      this.verticalProgressBar1.Location = new System.Drawing.Point(68, 42);
      this.verticalProgressBar1.Name = "verticalProgressBar1";
      this.verticalProgressBar1.Size = new System.Drawing.Size(84, 124);
      this.verticalProgressBar1.TabIndex = 9;
      this.verticalProgressBar1.Value = 50;
      // 
      // button4
      // 
      this.button4.AutoSize = true;
      this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button4.ForeColor = System.Drawing.Color.Green;
      this.button4.Location = new System.Drawing.Point(665, 528);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(229, 86);
      this.button4.TabIndex = 23;
      this.button4.Text = "Open Report Window";
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new System.EventHandler(this.button4_Click);
      // 
      // linkLabel1
      // 
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.linkLabel1.Location = new System.Drawing.Point(528, 525);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(95, 18);
      this.linkLabel1.TabIndex = 24;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "Open Folder";
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      // 
      // buttonSkipSettingTemp
      // 
      this.buttonSkipSettingTemp.Location = new System.Drawing.Point(171, 121);
      this.buttonSkipSettingTemp.Name = "buttonSkipSettingTemp";
      this.buttonSkipSettingTemp.Size = new System.Drawing.Size(282, 36);
      this.buttonSkipSettingTemp.TabIndex = 1;
      this.buttonSkipSettingTemp.Text = "Skip Setting Temperature";
      this.buttonSkipSettingTemp.UseVisualStyleBackColor = true;
      this.buttonSkipSettingTemp.Visible = false;
      this.buttonSkipSettingTemp.Click += new System.EventHandler(this.buttonSkipSettingTemp_Click);
      // 
      // buttonSkipPressure
      // 
      this.buttonSkipPressure.Location = new System.Drawing.Point(171, 118);
      this.buttonSkipPressure.Name = "buttonSkipPressure";
      this.buttonSkipPressure.Size = new System.Drawing.Size(282, 36);
      this.buttonSkipPressure.TabIndex = 2;
      this.buttonSkipPressure.Text = "Skip Setting Pressure";
      this.buttonSkipPressure.UseVisualStyleBackColor = true;
      this.buttonSkipPressure.Visible = false;
      this.buttonSkipPressure.Click += new System.EventHandler(this.buttonSkipPressure_Click);
      // 
      // labelChamber
      // 
      this.labelChamber.AutoSize = true;
      this.labelChamber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelChamber.Location = new System.Drawing.Point(24, 153);
      this.labelChamber.Name = "labelChamber";
      this.labelChamber.Size = new System.Drawing.Size(104, 21);
      this.labelChamber.TabIndex = 22;
      this.labelChamber.Text = "Chamber  =";
      // 
      // AutoScrn
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.ClientSize = new System.Drawing.Size(905, 629);
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.button4);
      this.Controls.Add(this.labelDuration);
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
    private System.Windows.Forms.Label labelTargetPressure;
    private System.Windows.Forms.Label labelPumpState;
    private System.Windows.Forms.Label labelDuration;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Label labelDurations;
    private System.Windows.Forms.Label labelStepsTotal;
    private System.Windows.Forms.Label labelStepCurrent;
    private System.Windows.Forms.Label labelTotalTime;
    private System.Windows.Forms.DataGridViewTextBoxColumn TimeColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn PressureColumn;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.Button buttonSkipSettingTemp;
    private System.Windows.Forms.Button buttonSkipPressure;
    private System.Windows.Forms.Label labelChamber;
    // private System.Windows.Controls.ProgressBar progbar;


  }
}