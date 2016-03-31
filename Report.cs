using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LukMachine
{
  public partial class Report : Form
  {
    //array list to store header data to, all data from each file will be stored as a CSV in single array item.
    ArrayList peep = new ArrayList();
    ArrayList distensionML = new ArrayList();
    ArrayList distensionCM = new ArrayList();

    bool openSaved;
    string lastPUnit = "";
    int myBorderWidth = Convert.ToInt32(Properties.Settings.Default.myBorderWidth);

    public Report()
    {
      InitializeComponent();
      openSaved = false;

    }
    public Report(bool openSavedParam)
    {
      InitializeComponent();
      openSaved = openSavedParam;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      OpenDataFiles(false);
    }

    public void OpenDataFiles(bool knowName)
    {

      string[] selectedFiles;
      if (!knowName)
      {
        openFileDialog1.ShowDialog();
        selectedFiles = openFileDialog1.FileNames;

        if (selectedFiles.Length > 1 || comboBox1.Items.Count > 0)
        {
          if (comboBox1.FindStringExact("Show All") == -1)
          {
            comboBox1.Items.Insert(0, "Show All");
          }
        }
      }
      else
      {
        selectedFiles = new string[1];
        selectedFiles[0] = Properties.Settings.Default.TestData;
        //MessageBox.Show(Properties.Settings.Default.TestData);
      }

      foreach (string s in selectedFiles)
      {
        string sampleInfoCSV = null;
        string fileName;
        fileName = Path.GetFileNameWithoutExtension(s);     
        if (comboBox1.FindStringExact(fileName) != -1)
        {
          //if item is already listed
          //MessageBox.Show("A file with the same name is already open. This file will be skipped.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          continue;
        }
        StreamReader SR = new StreamReader(s);
        string RL = SR.ReadLine();
        if (RL != "Liquid Permeability Test")
        {
          MessageBox.Show(s + " is not a liquid permeability data file!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
          continue;
        }
        string[] splitStuff;
        RL = SR.ReadLine(); //blank
        RL = SR.ReadLine(); //Sample Info
        RL = SR.ReadLine(); //blank
        RL = SR.ReadLine(); //Sample ID
        splitStuff = RL.Split('=');
        string sampleID = splitStuff[1];
        sampleInfoCSV = sampleID + ",";
        RL = SR.ReadLine(); //Lot Number
        splitStuff = RL.Split('=');
        string lotNumber = splitStuff[1];
        sampleInfoCSV += lotNumber + ",";
        /*
        RL = SR.ReadLine();//Paper sample
        splitStuff = RL.Split('=');
        string paper = splitStuff[1];
        sampleInfoCSV += paper + ",";
        RL = SR.ReadLine();//number of sheets
        splitStuff = RL.Split('=');
        string sheets = splitStuff[1];
        sampleInfoCSV += sheets + ",";
        RL = SR.ReadLine(); //grammage of sample
        splitStuff = RL.Split('=');
        string grammage = splitStuff[1];
        sampleInfoCSV +=  grammage + ",";*/

        RL = SR.ReadLine(); //blank
        RL = SR.ReadLine(); //Test Details
        RL = SR.ReadLine(); //blank
        RL = SR.ReadLine(); //Date
        splitStuff = RL.Split('=');
        string date = splitStuff[1];
        sampleInfoCSV += date + ",";
        RL = SR.ReadLine(); //Duration Units
        splitStuff = RL.Split('=');
        sampleInfoCSV += splitStuff[1] + ",";
        RL = SR.ReadLine(); //Flow Units
        splitStuff = RL.Split('=');
        sampleInfoCSV += splitStuff[1] + ",";
        RL = SR.ReadLine(); //Temperature Units
        splitStuff = RL.Split('=');
        sampleInfoCSV += splitStuff[1] + ",";
        RL = SR.ReadLine(); //Pressure Units
        splitStuff = RL.Split('=');
        string pressureUnits = splitStuff[1];
        if (pressureUnits != lastPUnit)
        {
          if (lastPUnit != "" && lastPUnit != null)
          {
            MessageBox.Show("When loading multiple data files, all data must be in the same pressure units. The software cannot graph files with different pressure units together.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            continue;
          }
        }
        lastPUnit = pressureUnits;
        sampleInfoCSV += pressureUnits + ",";
        RL = SR.ReadLine(); //Steps
        splitStuff = RL.Split('=');
        sampleInfoCSV += splitStuff[1] + ",";
        RL = SR.ReadLine(); //Pressures
        splitStuff = RL.Split('=');
        sampleInfoCSV += splitStuff[1] + ",";
        RL = SR.ReadLine(); //Durations
        splitStuff = RL.Split('=');
        sampleInfoCSV += splitStuff[1] + ",";
        RL = SR.ReadLine(); //Temperatures
        splitStuff = RL.Split('=');
        sampleInfoCSV += splitStuff[1] + ",";
        RL = SR.ReadLine(); //blank
        RL = SR.ReadLine(); //Data:
        RL = SR.ReadLine(); //blank
        RL = SR.ReadLine(); //Time, Volume.
        splitStuff = RL.Split('\t');
        sampleInfoCSV += splitStuff[0] + ",";
        sampleInfoCSV += splitStuff[1] + ",";
        sampleInfoCSV += splitStuff[2] + ",";
        sampleInfoCSV += splitStuff[3] + ",";
        RL = SR.ReadLine(); //blank

        //setup data table for current sample
        dataSet1.Tables.Add(fileName);
        dataSet1.Tables[fileName].Columns.Add("Volume");
        dataSet1.Tables[fileName].Columns.Add("Time");
        dataSet1.Tables[fileName].Columns.Add("Temperature");
        dataSet1.Tables[fileName].Columns.Add("Pressure");
        dataSet1.Tables[fileName].Columns.Add("Permeability");

        //add sample name to combobox
        comboBox1.Items.Add(fileName);
        while (!SR.EndOfStream)
        {
          //read/split each line and convert to double type.
          RL = SR.ReadLine();
          if (RL.Contains('\t'))
          {
            string[] splitString = RL.Split('\t');
            try
            {
              double time = Convert.ToDouble(splitString[0]);
              double volume = Convert.ToDouble(splitString[1]);
              double temperature = Convert.ToDouble(splitString[2]);
              double pressure = Convert.ToDouble(splitString[3]);
              double permeability = 0;
              try
              {
                permeability = Convert.ToDouble(splitString[4]);
              }
              catch (Exception)
              {
                MessageBox.Show("The file does not have a permeability column!");
                return;
              }
              
              //add doubles to data table for current sample
              dataSet1.Tables[fileName].Rows.Add(time, volume, temperature, pressure, permeability);
            }
            catch (FormatException ex)
            {
              MessageBox.Show(s + " is a valid data file, however the program has encountered an error while reading the time/pressure data!" + Environment.NewLine + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
              //go to the end of file so while loop will end
              SR.ReadToEnd();
              //delete this table
              //dataSet1.Tables[sampleID].Dispose();
              dataSet1.Tables.Remove(fileName);
              //remove sample name from combobox
              comboBox1.Items.RemoveAt(comboBox1.FindStringExact(fileName));
              //peep.RemoveAt(peep.Count - 1);
              //return to while that should end; (endofstream) should be reached.
              continue;
            }
          }
          //find burst pressure (if it exists, if not sample didn't burst.) 
          /*
          if (RL.Contains("Burst Pressure="))
          {
            string[] getBurst = RL.Split('=');
            string burstPressure = getBurst[1];
            sampleInfoCSV += burstPressure;
            RL = SR.ReadLine();
            string[] getVol = RL.Split('=');
            string burstVolume = getVol[1];
            sampleInfoCSV += "," + fileName;
            sampleInfoCSV += "," + burstVolume;
          }*/
        }
        //sampleInfoCSV += "," + fileName;
        sampleInfoCSV += fileName;
        peep.Add(sampleInfoCSV);
        SR.Close();
      }


      //show the first item, what ever it is.
      if (comboBox1.Items.Count == 1)
      {
        comboBox1.SelectedIndex = 0;
      }
      else
      {
        comboBox1.SelectedIndex = comboBox1.FindStringExact("Show All");
      }
    }

    private void readData()
    {

    }


    private void Report_Load(object sender, EventArgs e)
    {
      Pumps.SetPump2(0); //for some reason the pump doesn't always stop
      LoadDistensionTable();

      if (Properties.Settings.Default.mustRunReport)
      {
        Properties.Settings.Default.mustRunReport = false;
        Properties.Settings.Default.Save();
        OpenDataFiles(true);
      }

      if (openSaved)
      {
        OpenDataFiles(true);
      }
      //open last data:
      try
      {
        OpenDataFiles(true);
      }
      catch (Exception)
      {
      }    
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      //clear legends
      chart1.Legends.Clear();
      //if they want to see all files graphed together
      if (comboBox1.Text == "Show All")
      {
        //clear current series
        chart1.Series.Clear();
        //loop though each data file
        foreach (string s in comboBox1.Items)
        {
          //skip 'Show All' because.
          if (s == "Show All") continue;
          //add a series named for the sample id
          chart1.Series.Add(s);
          //set chart type
          chart1.Series[s].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
          chart1.Series[s].BorderWidth = myBorderWidth;
      

          //loop through the data table named for the sample id and add it to the series.
          foreach (DataRow asdf in dataSet1.Tables[s].Rows)
          {
            chart1.Series[s].Points.AddXY(Convert.ToDouble(asdf[0]), Convert.ToDouble(asdf[1]));
          }
          label2.Text = "Data: Multiple";
          label1.Text = "Sample ID: N/A";
          double averageBurst = 0;
          foreach (string st in peep)
          {
            string[] bust = st.Split(',');
            double bp = Convert.ToDouble(bust[7]);
            averageBurst += bp;
          }
          averageBurst = averageBurst / peep.Count;
          label3.Text = "Avg. Burst Pressure: " + averageBurst.ToString("###.00");
          label4.Text = "Burst Volume: N/A";
          label5.Text = "Burst Ratio: N/A";
        }
      }
      else
      {
        //show single selected data set in chart1
        chart1.Series.Clear();
        chart1.Series.Add(comboBox1.Text);
        chart1.Series[comboBox1.Text].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        chart1.Series[comboBox1.Text].BorderWidth = myBorderWidth;
        foreach (DataRow asdf in dataSet1.Tables[comboBox1.Text].Rows)
        {
          chart1.Series[comboBox1.Text].Points.AddXY(Convert.ToDouble(asdf[0]), Convert.ToDouble(asdf[1]));
        }
        string header = null;
        if (comboBox1.SelectedIndex == 0)
        {
          header = peep[comboBox1.SelectedIndex].ToString();
        }
        else
        {
          header = peep[comboBox1.SelectedIndex - 1].ToString();
        }

        string[] busted = header.Split(',');
        //lets convert it to a double just to format it back to TEXT.


        //julian commented out this
        //double bustedUP = Convert.ToDouble(busted[8]);

        label2.Text = "Data: " + comboBox1.Text;
        label1.Text = "Sample ID: " + busted[0];
        //label3.Text = "Burst Pressure: " + bustedUP.ToString("###.00");
        //label4.Text = "Burst Volume: " + busted[10];
        if (busted[2] == "N")
        {
          label5.Text = "Burst Ratio: N/A";
        }
        else
        {
          //double bRatio =  GetBurstRatio(Convert.ToDouble(busted[4]),Convert.ToDouble(bustedUP.ToString("###.00")));
          //label5.Text = "Burst Ratio: " + bRatio.ToString("#.0000");
        }


      }
      //add a legend and axis titles.
      chart1.Legends.Add("s");

      chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 12F);
      chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 12F);
      chart1.ChartAreas[0].AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot;
      chart1.ChartAreas[0].AxisY.Title = "Flow (mL/min)";
      chart1.ChartAreas[0].AxisX.Title = "Time (Mins)";

    }

    private double GetBurstRatio(double grammage, double burstPressure)
    {
      //throw new NotImplementedException();
      double burstRatio = burstPressure / grammage;
      return burstRatio;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      //remove data set, combo item, array item.
      if (comboBox1.Items.Count > 0)
      {
        if (comboBox1.Text != "Show All")
        {
          if (comboBox1.Items.Count == 1)
          {
            dataSet1.Tables.Clear();
            peep.Clear();
            lastPUnit = "";
            comboBox1.Items.Clear();
            chart1.Series.Clear();
            lastPUnit = null;
          }
          else
          {
            int selectedItem = comboBox1.FindStringExact(comboBox1.Text);
            peep.RemoveAt(selectedItem - 1);
            dataSet1.Tables.RemoveAt(selectedItem - 1);
            comboBox1.Items.RemoveAt(selectedItem);
            comboBox1.SelectedIndex = selectedItem - 1;
          }
        }

        if (comboBox1.FindStringExact("Show All") != -1 && comboBox1.Items.Count == 2)
        {
          comboBox1.Items.RemoveAt(comboBox1.FindStringExact("Show All"));
          comboBox1.SelectedIndex = 0;
        }
      }
    }

    private void button3_Click(object sender, EventArgs e)
    {
      //kill everything.
      dataSet1.Tables.Clear();
      peep.Clear();
      lastPUnit = "";
      comboBox1.Items.Clear();
      chart1.Series.Clear();
      lastPUnit = null;
    }

    private void button4_Click(object sender, EventArgs e)
    {
      MessageBox.Show(lastPUnit);
    }

    private void button4_Click_1(object sender, EventArgs e)
    {

      if (comboBox1.Items.Count > 0)
      {
        export exp = new export();
        DialogResult expResult = exp.ShowDialog();
        if (expResult == DialogResult.OK)
        {
          if (Properties.Settings.Default.ExportOption == 0)
          {
            if (comboBox1.Text == "Show All")
            {
              MessageBox.Show("To export a single data set, you must select a single data set to export.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
              return;
            }
            //get correct header
            string header = null;
            if (comboBox1.Items.Count > 1)
            {
              header = peep[comboBox1.SelectedIndex - 1].ToString();
            }
            else
            {
              header = peep[comboBox1.SelectedIndex].ToString();
            }

            ExportExcelFile(header, Properties.Settings.Default.ExportOption, Properties.Settings.Default.ExportPath);

          }
          if (Properties.Settings.Default.ExportOption == 1)
          {
            if (comboBox1.Items.Count == 1)
            {
              //selected export multiple, but only has one. Thanks, rocket surgeon.
              string header = peep[comboBox1.SelectedIndex].ToString();
              ExportExcelFile(header, Properties.Settings.Default.ExportOption, Properties.Settings.Default.ExportPath);
            }
            if (comboBox1.Items.Count > 1)
            {
              //normal
              foreach (string s in comboBox1.Items)
              {
                if (s == "Show All") continue;
                string header = peep[comboBox1.FindStringExact(s) - 1].ToString();
                ExportExcelFile(header, Properties.Settings.Default.ExportOption, Properties.Settings.Default.ExportPath);
              }
            }
            MessageBox.Show("All files exported successfully!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
        }
      }
    }

    private void ExportExcelFile(string headerInfo, int multi, string path)
    {
      //parse header data
      string[] splitStuff = headerInfo.Split(',');

      int row = 0;
      //create an excel file object
      var excel = new OfficeOpenXml.ExcelPackage();
      //create excel worksheet for text data
      var ws = excel.Workbook.Worksheets.Add(splitStuff[0]);
      //create excel worksheet for graph
      var gs = excel.Workbook.Worksheets.Add(splitStuff[0] + " graph");
      //output header stuff
      ws.Cells[1, 1].Value = "Liquid Permeability Test";
      ws.Cells[2, 1].Value = " ";
      ws.Cells[3, 1].Value = "Sample Info:";
      ws.Cells[4, 1].Value = " ";
      ws.Cells[5, 1].Value = "Sample ID";
      ws.Cells[5, 2].Value = splitStuff[0];
      ws.Cells[6, 1].Value = "Lot Number";
      ws.Cells[6, 2].Value = splitStuff[1];
      ws.Cells[7, 1].Value = " ";
      ws.Cells[8, 1].Value = "Test Details:";
      ws.Cells[9, 1].Value = " ";
      ws.Cells[10, 1].Value = "Date";
      ws.Cells[10, 2].Value = splitStuff[2];
      ws.Cells[11, 1].Value = "Time Units";
      ws.Cells[11, 2].Value = splitStuff[3];
      ws.Cells[12, 1].Value = "Flow Units";
      ws.Cells[12, 2].Value = splitStuff[4];
      ws.Cells[13, 1].Value = "Temperature Units";
      ws.Cells[13, 2].Value = splitStuff[5];
      ws.Cells[14, 1].Value = "Pressure Units";
      ws.Cells[14, 2].Value = splitStuff[6];
      ws.Cells[15, 1].Value = "Steps";
      ws.Cells[15, 2].Value = splitStuff[7];
      ws.Cells[16, 1].Value = "Pressure";
      ws.Cells[16, 2].Value = splitStuff[8];
      ws.Cells[17, 1].Value = "Duration";
      ws.Cells[17, 2].Value = splitStuff[9];
      ws.Cells[18, 1].Value = "Temperature";
      ws.Cells[18, 2].Value = splitStuff[10];
      ws.Cells[19, 1].Value = " ";
      ws.Cells[20, 1].Value = "Data:";
      ws.Cells[21, 1].Value = " ";
      ws.Cells[22, 1].Value = splitStuff[11];
      ws.Cells[22, 2].Value = splitStuff[12];
      ws.Cells[22, 3].Value = splitStuff[13];
      ws.Cells[22, 4].Value = splitStuff[14];
      row = 23;

      //output test data
      string dataname = splitStuff[15];
      foreach (DataRow asdf in dataSet1.Tables[dataname].Rows)
      {
        row++;
        ws.Cells[row, 1].Value = Convert.ToDouble(asdf[0]);
        ws.Cells[row, 2].Value = Convert.ToDouble(asdf[1]);
        ws.Cells[row, 3].Value = Convert.ToDouble(asdf[2]);
        ws.Cells[row, 4].Value = Convert.ToDouble(asdf[3]);
      }

      //output graph
      OfficeOpenXml.ExcelRange r1, r2;
      var chart = (OfficeOpenXml.Drawing.Chart.ExcelLineChart)gs.Drawings.AddChart("some_name", OfficeOpenXml.Drawing.Chart.eChartType.Line);
      chart.Legend.Position = OfficeOpenXml.Drawing.Chart.eLegendPosition.Right;
      chart.Legend.Add();
      chart.SetPosition(1, 0, 1, 0);
      chart.SetSize(1000, 600);

      try
      {
        r1 = ws.Cells["A26:A" + row.ToString()];
        r2 = ws.Cells["B26:B" + row.ToString()];
        chart.Series.Add(r2, r1);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        MessageBox.Show("Argument our of range!" + Environment.NewLine + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      chart.Title.Text = "Volume VS Time";

      chart.YAxis.Title.Text = "Differential Pressure(" + splitStuff[3] + ")";
      chart.XAxis.Title.Text = "Time(Mins)";
      ws.Cells[ws.Dimension.Address.ToString()].AutoFitColumns();
      saveFileDialog1.FileName = splitStuff[0];
      if (multi == 0)
      {
        if (Properties.Settings.Default.AutoDataFile)
        {
          using (var file = File.Create(Properties.Settings.Default.ExportPath + @"\" + dataname + ".xlsx"))
            excel.SaveAs(file);
          MessageBox.Show(Properties.Settings.Default.ExportPath + @"\" + dataname + ".xlsx" + " has been saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
          saveFileDialog1.FileName = dataname;
          DialogResult saveFile = saveFileDialog1.ShowDialog();
          if (saveFile != DialogResult.Cancel)
          {

            try
            {


              using (var file = File.Create(saveFileDialog1.FileName))
                excel.SaveAs(file);
              MessageBox.Show(saveFileDialog1.FileName + " has been saved.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
              MessageBox.Show(ex.Message);

            }


          }
        }
      }
      if (multi == 1)
      {
        using (var file = File.Create(path + @"\" + dataname + ".xlsx"))
          excel.SaveAs(file);
      }

    }
    private void button5_Click(object sender, EventArgs e)
    {
      foreach (DataTable asdf in dataSet1.Tables)
      {
        chart1.Series.Add(asdf.TableName);
        chart1.Series[asdf.TableName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        chart1.Series[comboBox1.Text].BorderWidth = myBorderWidth;
        chart1.Series[asdf.TableName].Points.DataBindXY(asdf.Columns["Time"].ToString(), asdf.Columns["Pressure"].ToString());
      }
    }

    private void button5_Click_1(object sender, EventArgs e)
    {
      MessageBox.Show(comboBox1.FindStringExact("PIE").ToString());
    }

    private void Report_Resize(object sender, EventArgs e)
    {
      //resize and reposition things when window resizes.
      if (WindowState != FormWindowState.Minimized)
      {
        chart1.Size = new System.Drawing.Size(this.Width - 206, this.Height - 63);
        groupBox1.Location = new System.Drawing.Point(chart1.Width + 10, chart1.Top);
        groupBox2.Location = new System.Drawing.Point(chart1.Width + 10, chart1.Top + 105);
      }
    }

    private void LoadDistensionTable()
    {
      distensionML.Add(0);
      distensionML.Add(0.19205);
      distensionML.Add(0.38295);
      distensionML.Add(0.0575);
      distensionML.Add(0.76705);
      distensionML.Add(0.95795);
      distensionML.Add(1.15);
      distensionML.Add(1.34205);
      distensionML.Add(1.53295);
      distensionML.Add(1.725);
      distensionML.Add(1.91705);
      distensionML.Add(2.10795);
      distensionML.Add(2.3);
      distensionML.Add(2.49205);
      distensionML.Add(2.68295);
      distensionML.Add(2.875);
      distensionML.Add(3.06705);
      distensionML.Add(3.25795);
      distensionML.Add(3.45);
      distensionML.Add(3.64205);
      distensionML.Add(3.83295);
      distensionML.Add(4.025);
      distensionML.Add(4.21705);
      distensionML.Add(4.40795);
      distensionML.Add(4.6);
      distensionML.Add(4.79205);
      distensionML.Add(4.98295);
      distensionML.Add(5.175);
      distensionML.Add(5.36705);
      distensionML.Add(5.55795);
      distensionML.Add(5.75);
      distensionML.Add(5.94205);
      distensionML.Add(6.13295);
      distensionML.Add(6.325);
      distensionML.Add(6.51705);
      distensionML.Add(6.70795);
      distensionML.Add(6.9);
      distensionML.Add(7.09205);
      distensionML.Add(7.28295);
      distensionML.Add(7.475);
      distensionML.Add(7.66705);
      distensionML.Add(7.85795);
      distensionML.Add(8.05);
      distensionML.Add(8.24205);
      distensionML.Add(8.43295);
      distensionML.Add(8.625);
      distensionML.Add(8.8205);
      distensionML.Add(9.00795);
      distensionML.Add(9.2);
      distensionML.Add(9.39205);
      distensionML.Add(9.58295);
      distensionML.Add(9.775);
      distensionML.Add(9.96705);
      distensionML.Add(10.35);
      distensionML.Add(10.54205);
      distensionML.Add(10.73295);
      distensionML.Add(10.925);
      distensionML.Add(11.11705);
      distensionML.Add(11.30795);
      distensionML.Add(11.5);
      distensionML.Add(11.69205);
      distensionML.Add(11.88295);
      distensionML.Add(12.075);
      distensionML.Add(12.26705);
      distensionML.Add(12.45795);
      distensionML.Add(12.65);
      distensionML.Add(12.84205);
      distensionML.Add(13.03295);
      distensionML.Add(13.225);
      distensionML.Add(13.41705);
      distensionML.Add(13.60795);
      distensionML.Add(13.8);
      distensionML.Add(13.99205);
      distensionML.Add(14.18295);
      distensionML.Add(14.375);
      distensionML.Add(14.56705);
      distensionML.Add(14.75795);
      distensionML.Add(14.95);
      distensionML.Add(15.14205);
      distensionML.Add(15.33295);
      distensionML.Add(15.525);
      distensionML.Add(15.71705);
      distensionML.Add(15.90795);
      distensionML.Add(16.1);
      distensionML.Add(16.1);
      distensionML.Add(16.29205);
      distensionML.Add(16.48295);
      distensionML.Add(16.675);
      distensionML.Add(16.86705);
      distensionML.Add(17.05795);
      distensionML.Add(17.25);

      distensionCM.Add(0);
      distensionCM.Add(0);
      distensionCM.Add(0.254);
      distensionCM.Add(0.508);
      distensionCM.Add(0.9652);
      distensionCM.Add(1.2446);
      distensionCM.Add(1.524);
      distensionCM.Add(1.8034);
      distensionCM.Add(2.0574);
      distensionCM.Add(2.3114);
      distensionCM.Add(2.5908);
      distensionCM.Add(2.8448);
      distensionCM.Add(3.0734);
      distensionCM.Add(3.302);
      distensionCM.Add(3.5306);
      distensionCM.Add(3.7846);
      distensionCM.Add(4.064);
      distensionCM.Add(4.318);
      distensionCM.Add(4.572);
      distensionCM.Add(4.8768);
      distensionCM.Add(5.1816);
      distensionCM.Add(5.461);
      distensionCM.Add(5.7658);
      distensionCM.Add(6.0706);
      distensionCM.Add(6.3754);
      distensionCM.Add(6.7056);
      distensionCM.Add(7.0104);
      distensionCM.Add(7.3152);
      distensionCM.Add(7.62);
      distensionCM.Add(7.8994);
      distensionCM.Add(8.2042);
      distensionCM.Add(8.509);
      distensionCM.Add(8.8138);
      distensionCM.Add(9.0678);
      distensionCM.Add(9.3472);
      distensionCM.Add(9.6012);
      distensionCM.Add(9.8806);
      distensionCM.Add(10.16);
      distensionCM.Add(10.3886);
      distensionCM.Add(10.6426);
      distensionCM.Add(10.8712);
      distensionCM.Add(11.0998);
      distensionCM.Add(11.3284);
      distensionCM.Add(11.557);
      distensionCM.Add(11.7856);
      distensionCM.Add(11.9888);
      distensionCM.Add(12.2174);
      distensionCM.Add(12.4206);
      distensionCM.Add(12.6492);
      distensionCM.Add(12.8524);
      distensionCM.Add(13.0302);
      distensionCM.Add(13.2334);
      distensionCM.Add(13.4366);
      distensionCM.Add(13.6398);
      distensionCM.Add(13.7922);
      distensionCM.Add(13.9954);
      distensionCM.Add(14.1732);
      distensionCM.Add(14.351);
      distensionCM.Add(14.5034);
      distensionCM.Add(14.6812);
      distensionCM.Add(14.859);
      distensionCM.Add(15.0368);
      distensionCM.Add(15.2146);
      distensionCM.Add(15.367);
      distensionCM.Add(15.5448);
      distensionCM.Add(15.6972);
      distensionCM.Add(15.8496);
      distensionCM.Add(16.0274);
      distensionCM.Add(16.1798);
      distensionCM.Add(16.3576);
      distensionCM.Add(16.4846);
      distensionCM.Add(16.637);
      distensionCM.Add(16.7894);
      distensionCM.Add(16.9418);
      distensionCM.Add(17.0688);
      distensionCM.Add(17.2212);
      distensionCM.Add(17.3736);
      distensionCM.Add(17.5006);
      distensionCM.Add(17.653);
      distensionCM.Add(17.78);
      distensionCM.Add(17.907);
      distensionCM.Add(18.0594);
      distensionCM.Add(18.2118);
      distensionCM.Add(18.3642);
      distensionCM.Add(18.4658);
      distensionCM.Add(18.6182);
      distensionCM.Add(18.7452);
      distensionCM.Add(18.8976);
      distensionCM.Add(19.0246);
      distensionCM.Add(19.1516);
      distensionCM.Add(19.2786);
    }

    private double CalculateDistension(double burstVolume)
    {
      //burstVolume is total mL of fluid pushed by the pump during a test.

      for (int i = 0; i <= distensionML.Count; i++)
      {
        if (burstVolume < Convert.ToDouble(distensionML[i]))
        {
          double bigPointML = Convert.ToDouble(distensionML[i]);
          double bigPointCM = Convert.ToDouble(distensionCM[i]);
          double smallPointML = Convert.ToDouble(distensionML[i - 1]);
          double smallPointCM = Convert.ToDouble(distensionCM[i - 1]);
          double mlDiff = bigPointML - smallPointML;
          double cmDiff = bigPointCM - smallPointCM;
          double myDiff = burstVolume - smallPointML;

          double thisThing = myDiff / mlDiff;

          double thisThingNow = cmDiff * thisThing;

          double thisThingHere = smallPointCM + thisThingNow;
          string format = thisThingHere.ToString("#.0000");
          return Convert.ToDouble(format);
        }
      }

      return 0;
    }

    private void button5_Click_2(object sender, EventArgs e)
    {
      MessageBox.Show(CalculateDistension(4.7).ToString());
    }

    private void radioButton2_CheckedChanged(object sender, EventArgs e)
    {
      if (radioButton2.Checked)
      {
        try
        {
          //MessageBox.Show("y");
          chart1.Series.Clear();
          chart1.Series.Add(comboBox1.Text);
          chart1.Series[comboBox1.Text].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
          chart1.Series[comboBox1.Text].BorderWidth = myBorderWidth;
          foreach (DataRow asdf in dataSet1.Tables[comboBox1.Text].Rows)
          {
            double x = Convert.ToDouble(asdf[2]);
            double y = Convert.ToDouble(asdf[1]);
            chart1.Series[comboBox1.Text].Points.AddXY(x, y);
            Refresh();
          }
          chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 12F);
          chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 12F);
          chart1.ChartAreas[0].AxisY.Title = "Flow (mL/min)";
          chart1.ChartAreas[0].AxisX.Title = "Temperature (C)";

          chart1.Titles.Clear();
          System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
          title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          title1.Text = "Flow VS Temperature";
          chart1.Titles.Add(title1);
        }
        catch (Exception)
        {}
      }
    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
      if (radioButton1.Checked)
      {
        try
        {
          chart1.Series.Clear();
          chart1.Series.Add(comboBox1.Text);
          chart1.Series[comboBox1.Text].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
          chart1.Series[comboBox1.Text].BorderWidth = myBorderWidth;
          foreach (DataRow asdf in dataSet1.Tables[comboBox1.Text].Rows)
          {
            chart1.Series[comboBox1.Text].Points.AddXY(Convert.ToDouble(asdf[0]), Convert.ToDouble(asdf[1]));
          }
          chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 12F);
          chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 12F);
          chart1.ChartAreas[0].AxisY.Title = "Flow (mL/min)";
          chart1.ChartAreas[0].AxisX.Title = "Time (Mins)";

          chart1.Titles.Clear();
          System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
          title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          title1.Text = "Flow VS Time";
          chart1.Titles.Add(title1);
        }
        catch (Exception)
        {}
      }
    }

    private void radioButton3_CheckedChanged(object sender, EventArgs e)
    {
      if (radioButton3.Checked)
      {
        try
        {
          chart1.Series.Clear();
          chart1.Series.Add(comboBox1.Text);
          chart1.Series[comboBox1.Text].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
          chart1.Series[comboBox1.Text].BorderWidth = myBorderWidth;
          foreach (DataRow asdf in dataSet1.Tables[comboBox1.Text].Rows)
          {
            chart1.Series[comboBox1.Text].Points.AddXY(Convert.ToDouble(asdf[3]), Convert.ToDouble(asdf[1]));
          }
          chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 12F);
          chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 12F);
          chart1.ChartAreas[0].AxisY.Title = "Flow (mL/min)";
          chart1.ChartAreas[0].AxisX.Title = "Pressure (PSI)";

          chart1.Titles.Clear();
          System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
          title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          title1.Text = "Flow VS Pressure";
          chart1.Titles.Add(title1);
        }
        catch (Exception)
        {
        }
      }
    }

    private void buttonCalculate_Click(object sender, EventArgs e)
    {
      double perm;
      double flow = Convert.ToDouble(textBoxFlow.Text);
      double thickness = Convert.ToDouble(textBoxThickness.Text);
      double k1 = flow * thickness * 14.7;
      labelk1.Text = k1.ToString();   
      double area = 3.1415926 * (Convert.ToDouble(textBoxDiameter.Text) * Convert.ToDouble(textBoxDiameter.Text)) / 4;    
      //perm = k1  / (60 * area * Convert.ToDouble(textBoxPressure.Text));
      perm = k1 / (area * Convert.ToDouble(textBoxPressure.Text));
      labelPermeability.Text = "= " + perm.ToString("#.0000000");
      /*double i = 10 / 5 / 2;
      MessageBox.Show(i.ToString());*/
    }

    private void radioButton4_CheckedChanged(object sender, EventArgs e)
    {
      if (radioButton4.Checked)
      {
        try
        {
          chart1.Series.Clear();
          chart1.Series.Add(comboBox1.Text);
          chart1.Series[comboBox1.Text].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
          chart1.Series[comboBox1.Text].BorderWidth = myBorderWidth;
          foreach (DataRow asdf in dataSet1.Tables[comboBox1.Text].Rows)
          {
            chart1.Series[comboBox1.Text].Points.AddXY(Convert.ToDouble(asdf[0]), Convert.ToDouble(asdf[3]));
          }
          chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 12F);
          chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 12F);
          chart1.ChartAreas[0].AxisY.Title = "Pressure (PSI)";
          chart1.ChartAreas[0].AxisX.Title = "Time (Mins)";

          chart1.Titles.Clear();
          System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
          title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          title1.Text = "Pressure VS Time";
          chart1.Titles.Add(title1);
        }
        catch (Exception)
        { }
      }
    }

    private void radioButton5_CheckedChanged(object sender, EventArgs e)
    {
      if (radioButton5.Checked)
      {
        try
        {
          chart1.Series.Clear();
          chart1.Series.Add(comboBox1.Text);
          chart1.Series[comboBox1.Text].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
          chart1.Series[comboBox1.Text].BorderWidth = myBorderWidth;
          foreach (DataRow asdf in dataSet1.Tables[comboBox1.Text].Rows)
          {
            chart1.Series[comboBox1.Text].Points.AddXY(Convert.ToDouble(asdf[0]), Convert.ToDouble(asdf[2]));
          }
          chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 12F);
          chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 12F);
          chart1.ChartAreas[0].AxisY.Title = "Temperature (C)";
          chart1.ChartAreas[0].AxisX.Title = "Time (Mins)";

          chart1.Titles.Clear();
          System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
          title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          title1.Text = "Temperature VS Time";
          chart1.Titles.Add(title1);
        }
        catch (Exception)
        { }
      }
    }

    private void radioButton6_CheckedChanged(object sender, EventArgs e)
    {
      if (radioButton6.Checked)
      {
        try
        {
          chart1.Series.Clear();
          chart1.Series.Add(comboBox1.Text);
          chart1.Series[comboBox1.Text].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
          chart1.Series[comboBox1.Text].BorderWidth = myBorderWidth;
          foreach (DataRow asdf in dataSet1.Tables[comboBox1.Text].Rows)
          {
            chart1.Series[comboBox1.Text].Points.AddXY(Convert.ToDouble(asdf[0]), Convert.ToDouble(asdf[4]));
          }
          chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 12F);
          chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 12F);
          chart1.ChartAreas[0].AxisY.Title = "Permeability (Darcys)";
          chart1.ChartAreas[0].AxisX.Title = "Time (Mins)";

          chart1.Titles.Clear();
          System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
          title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          title1.Text = "Permeability VS Time";
          chart1.Titles.Add(title1);
        }
        catch (Exception)
        { }
      }
    }

    private void linkLabelOpenFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("explorer.exe", System.IO.Directory.GetParent(Properties.Settings.Default.TestData).ToString());
    }

  }
}
