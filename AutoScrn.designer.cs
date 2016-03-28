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
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoScrn));
      this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.button3 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.listBox1 = new System.Windows.Forms.ListBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.buttonSkip = new System.Windows.Forms.Button();
      this.buttonSkipPressure = new System.Windows.Forms.Button();
      this.buttonSkipSettingTemp = new System.Windows.Forms.Button();
      this.labelPanel = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.labelChamber = new System.Windows.Forms.Label();
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
      this.groupBoxReservoir = new System.Windows.Forms.GroupBox();
      this.buttonReport = new System.Windows.Forms.Button();
      this.linkLabelOpenFolder = new System.Windows.Forms.LinkLabel();
      this.backgroundWorkerMainLoop = new System.ComponentModel.BackgroundWorker();
      this.backgroundWorkerReadAndDisplay = new System.ComponentModel.BackgroundWorker();
      this.labelStepTime = new System.Windows.Forms.Label();
      this.TimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.PressureColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.timerForStopWatch = new System.Windows.Forms.Timer(this.components);
      this.dataGridView2 = new System.Windows.Forms.DataGridView();
      this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.DurationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.TemperatureColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.verticalProgressBar1 = new LukMachine.VerticalProgressBar();
      this.verticalProgressBar2 = new LukMachine.VerticalProgressBar();
      ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.panel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBoxPressure.SuspendLayout();
      this.groupBoxCollectedVolume.SuspendLayout();
      this.groupBoxReservoir.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
      this.groupBox2.SuspendLayout();
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
      // 
      // button2
      // 
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
      this.panel1.Controls.Add(this.buttonSkip);
      this.panel1.Controls.Add(this.buttonSkipPressure);
      this.panel1.Controls.Add(this.buttonSkipSettingTemp);
      this.panel1.Controls.Add(this.labelPanel);
      this.panel1.Location = new System.Drawing.Point(30, 272);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(614, 178);
      this.panel1.TabIndex = 13;
      // 
      // buttonSkip
      // 
      this.buttonSkip.Location = new System.Drawing.Point(252, 124);
      this.buttonSkip.Name = "buttonSkip";
      this.buttonSkip.Size = new System.Drawing.Size(143, 36);
      this.buttonSkip.TabIndex = 3;
      this.buttonSkip.Text = "Skip";
      this.buttonSkip.UseVisualStyleBackColor = true;
      this.buttonSkip.Click += new System.EventHandler(this.buttonSkip_Click);
      // 
      // buttonSkipPressure
      // 
      this.buttonSkipPressure.Location = new System.Drawing.Point(300, 22);
      this.buttonSkipPressure.Name = "buttonSkipPressure";
      this.buttonSkipPressure.Size = new System.Drawing.Size(282, 36);
      this.buttonSkipPressure.TabIndex = 2;
      this.buttonSkipPressure.Text = "Skip Setting Pressure";
      this.buttonSkipPressure.UseVisualStyleBackColor = true;
      this.buttonSkipPressure.Visible = false;
      this.buttonSkipPressure.Click += new System.EventHandler(this.buttonSkipPressure_Click);
      // 
      // buttonSkipSettingTemp
      // 
      this.buttonSkipSettingTemp.Location = new System.Drawing.Point(45, 38);
      this.buttonSkipSettingTemp.Name = "buttonSkipSettingTemp";
      this.buttonSkipSettingTemp.Size = new System.Drawing.Size(282, 36);
      this.buttonSkipSettingTemp.TabIndex = 1;
      this.buttonSkipSettingTemp.Text = "Skip Setting Temperature";
      this.buttonSkipSettingTemp.UseVisualStyleBackColor = true;
      this.buttonSkipSettingTemp.Visible = false;
      this.buttonSkipSettingTemp.Click += new System.EventHandler(this.buttonSkipSettingTemp_Click);
      // 
      // labelPanel
      // 
      this.labelPanel.AutoSize = true;
      this.labelPanel.Location = new System.Drawing.Point(41, 61);
      this.labelPanel.Name = "labelPanel";
      this.labelPanel.Size = new System.Drawing.Size(527, 23);
      this.labelPanel.TabIndex = 0;
      this.labelPanel.Text = "Please wait...  (setting reservoirs / temperature / pressure)";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(20, 108);
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
      this.groupBox1.Location = new System.Drawing.Point(664, 333);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(210, 190);
      this.groupBox1.TabIndex = 18;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Programmed Parameters";
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
      this.groupBoxPressure.Controls.Add(this.labelStepTime);
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
      this.groupBoxPressure.Text = "Current State:";
      // 
      // labelTotalTime
      // 
      this.labelTotalTime.AutoSize = true;
      this.labelTotalTime.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelTotalTime.Location = new System.Drawing.Point(20, 83);
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
      this.labelPumpState.Location = new System.Drawing.Point(20, 158);
      this.labelPumpState.Name = "labelPumpState";
      this.labelPumpState.Size = new System.Drawing.Size(133, 21);
      this.labelPumpState.TabIndex = 19;
      this.labelPumpState.Text = "Pump Power  =";
      // 
      // labelPressure
      // 
      this.labelPressure.AutoSize = true;
      this.labelPressure.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelPressure.Location = new System.Drawing.Point(20, 133);
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
      this.groupBoxCollectedVolume.Location = new System.Drawing.Point(336, 12);
      this.groupBoxCollectedVolume.Name = "groupBoxCollectedVolume";
      this.groupBoxCollectedVolume.Size = new System.Drawing.Size(181, 190);
      this.groupBoxCollectedVolume.TabIndex = 20;
      this.groupBoxCollectedVolume.TabStop = false;
      this.groupBoxCollectedVolume.Text = "Collected Volume";
      // 
      // groupBoxReservoir
      // 
      this.groupBoxReservoir.Controls.Add(this.verticalProgressBar1);
      this.groupBoxReservoir.Location = new System.Drawing.Point(523, 12);
      this.groupBoxReservoir.Name = "groupBoxReservoir";
      this.groupBoxReservoir.Size = new System.Drawing.Size(154, 190);
      this.groupBoxReservoir.TabIndex = 21;
      this.groupBoxReservoir.TabStop = false;
      this.groupBoxReservoir.Text = "Reservoir";
      // 
      // buttonReport
      // 
      this.buttonReport.AutoSize = true;
      this.buttonReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.buttonReport.ForeColor = System.Drawing.Color.Green;
      this.buttonReport.Location = new System.Drawing.Point(664, 529);
      this.buttonReport.Name = "buttonReport";
      this.buttonReport.Size = new System.Drawing.Size(229, 86);
      this.buttonReport.TabIndex = 25;
      this.buttonReport.Text = "Open Report Window";
      this.buttonReport.UseVisualStyleBackColor = true;
      this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
      // 
      // linkLabelOpenFolder
      // 
      this.linkLabelOpenFolder.AutoSize = true;
      this.linkLabelOpenFolder.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.linkLabelOpenFolder.Location = new System.Drawing.Point(533, 528);
      this.linkLabelOpenFolder.Name = "linkLabelOpenFolder";
      this.linkLabelOpenFolder.Size = new System.Drawing.Size(95, 18);
      this.linkLabelOpenFolder.TabIndex = 26;
      this.linkLabelOpenFolder.TabStop = true;
      this.linkLabelOpenFolder.Text = "Open Folder";
      this.linkLabelOpenFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelOpenFolder_LinkClicked);
      // 
      // backgroundWorkerMainLoop
      // 
      this.backgroundWorkerMainLoop.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMainLoop_DoWork);
      this.backgroundWorkerMainLoop.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerMainLoop_ProgressChanged);
      // 
      // backgroundWorkerReadAndDisplay
      // 
      this.backgroundWorkerReadAndDisplay.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerReadAndDisplay_DoWork);
      this.backgroundWorkerReadAndDisplay.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerReadAndDisplay_ProgressChanged);
      // 
      // labelStepTime
      // 
      this.labelStepTime.AutoSize = true;
      this.labelStepTime.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelStepTime.Location = new System.Drawing.Point(20, 58);
      this.labelStepTime.Name = "labelStepTime";
      this.labelStepTime.Size = new System.Drawing.Size(101, 21);
      this.labelStepTime.TabIndex = 22;
      this.labelStepTime.Text = "Step time =";
      // 
      // TimeColumn
      // 
      this.TimeColumn.HeaderText = "Time(m:s)";
      this.TimeColumn.Name = "TimeColumn";
      this.TimeColumn.ReadOnly = true;
      // 
      // PressureColumn
      // 
      this.PressureColumn.HeaderText = "Flow(mL/min)";
      this.PressureColumn.Name = "PressureColumn";
      this.PressureColumn.ReadOnly = true;
      // 
      // timerForStopWatch
      // 
      this.timerForStopWatch.Interval = 1000;
      this.timerForStopWatch.Tick += new System.EventHandler(this.timerForStopWatch_Tick);
      // 
      // dataGridView2
      // 
      this.dataGridView2.AllowUserToAddRows = false;
      this.dataGridView2.AllowUserToDeleteRows = false;
      this.dataGridView2.AllowUserToResizeColumns = false;
      this.dataGridView2.AllowUserToResizeRows = false;
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
      this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
      this.dataGridView2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
      this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.DurationColumn,
            this.TemperatureColumn});
      this.dataGridView2.Location = new System.Drawing.Point(12, 28);
      this.dataGridView2.Name = "dataGridView2";
      this.dataGridView2.ReadOnly = true;
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
      this.dataGridView2.RowHeadersVisible = false;
      dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle7;
      this.dataGridView2.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      this.dataGridView2.Size = new System.Drawing.Size(294, 149);
      this.dataGridView2.TabIndex = 46;
      // 
      // dataGridViewTextBoxColumn1
      // 
      this.dataGridViewTextBoxColumn1.DividerWidth = 1;
      this.dataGridViewTextBoxColumn1.HeaderText = "Pressure (PSI)";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      // 
      // DurationColumn
      // 
      this.DurationColumn.DividerWidth = 1;
      this.DurationColumn.HeaderText = "Duration (Mins)";
      this.DurationColumn.Name = "DurationColumn";
      this.DurationColumn.ReadOnly = true;
      // 
      // TemperatureColumn
      // 
      this.TemperatureColumn.DividerWidth = 1;
      this.TemperatureColumn.HeaderText = "Temperature (C)";
      this.TemperatureColumn.Name = "TemperatureColumn";
      this.TemperatureColumn.ReadOnly = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.dataGridView2);
      this.groupBox2.Location = new System.Drawing.Point(13, 12);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(317, 190);
      this.groupBox2.TabIndex = 47;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Program:";
      // 
      // verticalProgressBar1
      // 
      this.verticalProgressBar1.Location = new System.Drawing.Point(37, 42);
      this.verticalProgressBar1.Name = "verticalProgressBar1";
      this.verticalProgressBar1.Size = new System.Drawing.Size(84, 124);
      this.verticalProgressBar1.TabIndex = 9;
      this.verticalProgressBar1.Value = 50;
      // 
      // verticalProgressBar2
      // 
      this.verticalProgressBar2.Location = new System.Drawing.Point(52, 42);
      this.verticalProgressBar2.Name = "verticalProgressBar2";
      this.verticalProgressBar2.Size = new System.Drawing.Size(84, 124);
      this.verticalProgressBar2.TabIndex = 10;
      this.verticalProgressBar2.Value = 50;
      // 
      // AutoScrn
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.ClientSize = new System.Drawing.Size(905, 629);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.linkLabelOpenFolder);
      this.Controls.Add(this.buttonReport);
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
      this.Text = "Auto test";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoScrn_FormClosing);
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
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
      this.groupBox2.ResumeLayout(false);
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
    private System.Windows.Forms.Label labelPanel;
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
    private System.Windows.Forms.Label labelDurations;
    private System.Windows.Forms.Label labelStepsTotal;
    private System.Windows.Forms.Label labelStepCurrent;
    private System.Windows.Forms.Label labelTotalTime;
    private System.Windows.Forms.Button buttonSkipSettingTemp;
    private System.Windows.Forms.Button buttonSkipPressure;
    private System.Windows.Forms.Label labelChamber;
    private System.Windows.Forms.Button buttonReport;
    private System.Windows.Forms.LinkLabel linkLabelOpenFolder;
    private System.ComponentModel.BackgroundWorker backgroundWorkerMainLoop;
    private System.ComponentModel.BackgroundWorker backgroundWorkerReadAndDisplay;
    private System.Windows.Forms.Button buttonSkip;
    private System.Windows.Forms.Label labelStepTime;
    private System.Windows.Forms.DataGridViewTextBoxColumn TimeColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn PressureColumn;
    private System.Windows.Forms.Timer timerForStopWatch;
    private System.Windows.Forms.DataGridView dataGridView2;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn DurationColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn TemperatureColumn;
    private System.Windows.Forms.GroupBox groupBox2;
    // private System.Windows.Controls.ProgressBar progbar;


  }
}