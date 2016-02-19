namespace LukMachine
{
  partial class Temperature
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
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(86, 68);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(501, 19);
      this.label1.TabIndex = 0;
      this.label1.Text = "Please wait... the chamber is being heated to the target temperature";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(191, 131);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(175, 19);
      this.label2.TabIndex = 1;
      this.label2.Text = "Target temperature    =";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(373, 131);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(51, 19);
      this.label3.TabIndex = 2;
      this.label3.Text = "label3";
      // 
      // timer1
      // 
      this.timer1.Interval = 700;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(373, 177);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(15, 19);
      this.label4.TabIndex = 4;
      this.label4.Text = "-";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(191, 177);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(176, 19);
      this.label5.TabIndex = 3;
      this.label5.Text = "Current temperature  =";
      // 
      // Temperature
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(670, 300);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "Temperature";
      this.Text = "Temperature";
      this.Load += new System.EventHandler(this.Temperature_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
  }
}