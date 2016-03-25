namespace LukMachine
{
    partial class Report
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
      System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Report));
      this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.dataSet1 = new System.Data.DataSet();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.button3 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.radioButton3 = new System.Windows.Forms.RadioButton();
      this.radioButton2 = new System.Windows.Forms.RadioButton();
      this.radioButton1 = new System.Windows.Forms.RadioButton();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.button4 = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.textBoxDiameter = new System.Windows.Forms.TextBox();
      this.textBoxPressure = new System.Windows.Forms.TextBox();
      this.textBoxFlow = new System.Windows.Forms.TextBox();
      this.labelPermeability = new System.Windows.Forms.Label();
      this.buttonCalculate = new System.Windows.Forms.Button();
      this.textBoxThickness = new System.Windows.Forms.TextBox();
      this.label11 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.label13 = new System.Windows.Forms.Label();
      this.labelk1 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
      this.SuspendLayout();
      // 
      // chart1
      // 
      this.chart1.BorderlineColor = System.Drawing.Color.Black;
      this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
      chartArea1.Name = "ChartArea1";
      this.chart1.ChartAreas.Add(chartArea1);
      this.chart1.Location = new System.Drawing.Point(4, 13);
      this.chart1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.chart1.Name = "chart1";
      this.chart1.Size = new System.Drawing.Size(722, 381);
      this.chart1.TabIndex = 0;
      this.chart1.Text = "chart1";
      title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      title1.Name = "Title1";
      title1.Text = "Flow VS Time";
      this.chart1.Titles.Add(title1);
      // 
      // comboBox1
      // 
      this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new System.Drawing.Point(6, 21);
      this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(202, 32);
      this.comboBox1.TabIndex = 1;
      this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
      // 
      // dataSet1
      // 
      this.dataSet1.DataSetName = "NewDataSet";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.button3);
      this.groupBox1.Controls.Add(this.button2);
      this.groupBox1.Controls.Add(this.button1);
      this.groupBox1.Controls.Add(this.comboBox1);
      this.groupBox1.Location = new System.Drawing.Point(732, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(214, 106);
      this.groupBox1.TabIndex = 6;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Select data set";
      // 
      // button3
      // 
      this.button3.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button3.Image = global::LukMachine.Properties.Resources._1385_Disable_24x24_72;
      this.button3.Location = new System.Drawing.Point(146, 50);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(62, 49);
      this.button3.TabIndex = 5;
      this.button3.Text = "Clear All";
      this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // button2
      // 
      this.button2.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button2.Image = global::LukMachine.Properties.Resources._112_Minus_Orange_24x24_72;
      this.button2.Location = new System.Drawing.Point(74, 50);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(62, 49);
      this.button2.TabIndex = 4;
      this.button2.Text = "Remove";
      this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button1
      // 
      this.button1.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button1.Image = global::LukMachine.Properties.Resources._112_Plus_Green_24x24_72;
      this.button1.Location = new System.Drawing.Point(6, 50);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(62, 49);
      this.button1.TabIndex = 3;
      this.button1.Text = "Add";
      this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.radioButton3);
      this.groupBox2.Controls.Add(this.radioButton2);
      this.groupBox2.Controls.Add(this.radioButton1);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Controls.Add(this.button4);
      this.groupBox2.Location = new System.Drawing.Point(732, 117);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(214, 277);
      this.groupBox2.TabIndex = 7;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Details";
      // 
      // radioButton3
      // 
      this.radioButton3.AutoSize = true;
      this.radioButton3.Location = new System.Drawing.Point(15, 154);
      this.radioButton3.Name = "radioButton3";
      this.radioButton3.Size = new System.Drawing.Size(161, 28);
      this.radioButton3.TabIndex = 5;
      this.radioButton3.TabStop = true;
      this.radioButton3.Text = "Flow vs Pressure";
      this.radioButton3.UseVisualStyleBackColor = true;
      this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
      // 
      // radioButton2
      // 
      this.radioButton2.AutoSize = true;
      this.radioButton2.Location = new System.Drawing.Point(15, 120);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new System.Drawing.Size(191, 28);
      this.radioButton2.TabIndex = 4;
      this.radioButton2.TabStop = true;
      this.radioButton2.Text = "Flow vs Temperature";
      this.radioButton2.UseVisualStyleBackColor = true;
      this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
      // 
      // radioButton1
      // 
      this.radioButton1.AutoSize = true;
      this.radioButton1.Checked = true;
      this.radioButton1.Location = new System.Drawing.Point(15, 86);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new System.Drawing.Size(134, 28);
      this.radioButton1.TabIndex = 3;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "Flow vs Time";
      this.radioButton1.UseVisualStyleBackColor = true;
      this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(11, 32);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(40, 24);
      this.label2.TabIndex = 2;
      this.label2.Text = "File:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(11, 58);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(93, 24);
      this.label1.TabIndex = 1;
      this.label1.Text = "Sample ID:";
      // 
      // button4
      // 
      this.button4.Image = global::LukMachine.Properties.Resources._112_RightArrowShort_Green_32x32_72;
      this.button4.Location = new System.Drawing.Point(15, 200);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(141, 60);
      this.button4.TabIndex = 0;
      this.button4.Text = "Export";
      this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new System.EventHandler(this.button4_Click_1);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(569, 329);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(96, 24);
      this.label5.TabIndex = 5;
      this.label5.Text = "Burst Ratio:";
      this.label5.Visible = false;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(569, 301);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(114, 24);
      this.label4.TabIndex = 4;
      this.label4.Text = "Burst Volume:";
      this.label4.Visible = false;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(569, 271);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(122, 24);
      this.label3.TabIndex = 3;
      this.label3.Text = "Burst Pressure:";
      this.label3.Visible = false;
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.Filter = "PMI Data Files|*.pmi";
      this.openFileDialog1.Multiselect = true;
      this.openFileDialog1.Title = "Select PMI data file for processing.";
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.DefaultExt = "xlsx";
      this.saveFileDialog1.Filter = "Excel Files|*xlsx";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(424, 416);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(105, 24);
      this.label6.TabIndex = 8;
      this.label6.Text = "Permeability:";
      this.label6.Visible = false;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(23, 419);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(181, 24);
      this.label7.TabIndex = 9;
      this.label7.Text = "Sample Diameter (cm):";
      this.label7.Visible = false;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(23, 491);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(203, 24);
      this.label8.TabIndex = 10;
      this.label8.Text = "Differential Pressure (PSI):";
      this.label8.Visible = false;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(23, 527);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(119, 24);
      this.label9.TabIndex = 11;
      this.label9.Text = "Flow (mL/sec):";
      this.label9.Visible = false;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(535, 416);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(240, 24);
      this.label10.TabIndex = 12;
      this.label10.Text = "k = 14.7 * Flow/(Area*Pressure)";
      this.label10.Visible = false;
      // 
      // textBoxDiameter
      // 
      this.textBoxDiameter.Location = new System.Drawing.Point(239, 416);
      this.textBoxDiameter.Name = "textBoxDiameter";
      this.textBoxDiameter.Size = new System.Drawing.Size(92, 30);
      this.textBoxDiameter.TabIndex = 13;
      this.textBoxDiameter.Text = "2.4";
      this.textBoxDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.textBoxDiameter.Visible = false;
      // 
      // textBoxPressure
      // 
      this.textBoxPressure.Location = new System.Drawing.Point(239, 488);
      this.textBoxPressure.Name = "textBoxPressure";
      this.textBoxPressure.Size = new System.Drawing.Size(92, 30);
      this.textBoxPressure.TabIndex = 14;
      this.textBoxPressure.Text = "19.78833";
      this.textBoxPressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.textBoxPressure.Visible = false;
      // 
      // textBoxFlow
      // 
      this.textBoxFlow.Location = new System.Drawing.Point(239, 524);
      this.textBoxFlow.Name = "textBoxFlow";
      this.textBoxFlow.Size = new System.Drawing.Size(92, 30);
      this.textBoxFlow.TabIndex = 15;
      this.textBoxFlow.Text = "0.095704";
      this.textBoxFlow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.textBoxFlow.Visible = false;
      // 
      // labelPermeability
      // 
      this.labelPermeability.AutoSize = true;
      this.labelPermeability.Font = new System.Drawing.Font("Arial Narrow", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelPermeability.Location = new System.Drawing.Point(576, 477);
      this.labelPermeability.Name = "labelPermeability";
      this.labelPermeability.Size = new System.Drawing.Size(26, 40);
      this.labelPermeability.TabIndex = 16;
      this.labelPermeability.Text = "-";
      this.labelPermeability.Visible = false;
      // 
      // buttonCalculate
      // 
      this.buttonCalculate.Location = new System.Drawing.Point(354, 443);
      this.buttonCalculate.Name = "buttonCalculate";
      this.buttonCalculate.Size = new System.Drawing.Size(101, 69);
      this.buttonCalculate.TabIndex = 17;
      this.buttonCalculate.Text = "Calculate";
      this.buttonCalculate.UseVisualStyleBackColor = true;
      this.buttonCalculate.Visible = false;
      this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
      // 
      // textBoxThickness
      // 
      this.textBoxThickness.Location = new System.Drawing.Point(239, 452);
      this.textBoxThickness.Name = "textBoxThickness";
      this.textBoxThickness.Size = new System.Drawing.Size(92, 30);
      this.textBoxThickness.TabIndex = 19;
      this.textBoxThickness.Text = "0.16";
      this.textBoxThickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.textBoxThickness.Visible = false;
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(23, 455);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(186, 24);
      this.label11.TabIndex = 18;
      this.label11.Text = "Sample Thickness (cm):";
      this.label11.Visible = false;
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Font = new System.Drawing.Font("Arial Narrow", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label12.Location = new System.Drawing.Point(478, 477);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(71, 40);
      this.label12.TabIndex = 20;
      this.label12.Text = "k   =";
      this.label12.Visible = false;
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Font = new System.Drawing.Font("Arial Narrow", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label13.Location = new System.Drawing.Point(478, 444);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(71, 40);
      this.label13.TabIndex = 21;
      this.label13.Text = "k1 =";
      this.label13.Visible = false;
      // 
      // labelk1
      // 
      this.labelk1.AutoSize = true;
      this.labelk1.Font = new System.Drawing.Font("Arial Narrow", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelk1.Location = new System.Drawing.Point(576, 441);
      this.labelk1.Name = "labelk1";
      this.labelk1.Size = new System.Drawing.Size(26, 40);
      this.labelk1.TabIndex = 22;
      this.labelk1.Text = "-";
      this.labelk1.Visible = false;
      // 
      // Report
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(958, 425);
      this.Controls.Add(this.labelk1);
      this.Controls.Add(this.label13);
      this.Controls.Add(this.label12);
      this.Controls.Add(this.textBoxThickness);
      this.Controls.Add(this.label11);
      this.Controls.Add(this.buttonCalculate);
      this.Controls.Add(this.labelPermeability);
      this.Controls.Add(this.textBoxFlow);
      this.Controls.Add(this.textBoxPressure);
      this.Controls.Add(this.textBoxDiameter);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.chart1);
      this.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.MinimumSize = new System.Drawing.Size(600, 444);
      this.Name = "Report";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = " ";
      this.Load += new System.EventHandler(this.Report_Load);
      this.Resize += new System.EventHandler(this.Report_Resize);
      ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.RadioButton radioButton3;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.RadioButton radioButton1;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox textBoxDiameter;
    private System.Windows.Forms.TextBox textBoxPressure;
    private System.Windows.Forms.TextBox textBoxFlow;
    private System.Windows.Forms.Label labelPermeability;
    private System.Windows.Forms.Button buttonCalculate;
    private System.Windows.Forms.TextBox textBoxThickness;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label labelk1;
  }
}