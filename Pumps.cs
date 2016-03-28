using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukMachine
{
  public static class Pumps
  {
    public static void StartPump1()
    {
      COMMS.Instance.MoveMotorValve(3, "O");
      Properties.Settings.Default.RefillPumpState = true;
      Properties.Settings.Default.Save();
    }
    public static void StopPump1()
    {
      COMMS.Instance.MoveMotorValve(3, "S");
      Properties.Settings.Default.RefillPumpState = false;
      Properties.Settings.Default.Save();
    }
    public static void SetPump2(double percent)
    {
      int num= Convert.ToInt32(percent * 4000 / 100);
      COMMS.Instance.SetRegulator(1, num);
      Properties.Settings.Default.MainPumpStatePercent = percent;
      Properties.Settings.Default.Save();
    }

    public static void IncreaseMainPump(double percent) //Increases MainPump by percent %
    {
      if ((Properties.Settings.Default.MainPumpStatePercent + percent)<=100) //becuase pump can't be less than 0.
      {
        SetPump2(Properties.Settings.Default.MainPumpStatePercent + percent);
      }
      else
      {
        System.Diagnostics.Debug.WriteLine("can't make pump 2 higher than 100%");
      }
      Properties.Settings.Default.Save();
    }

    public static void DecreaseMainPump(double percent) //Increases MainPump by percent %
    {
      if (Properties.Settings.Default.MainPumpStatePercent>= percent) //becuase pump can't be less than 0.
      {
        SetPump2(Properties.Settings.Default.MainPumpStatePercent - percent);
      }
      else
      {
        System.Diagnostics.Debug.WriteLine("can't make pump 2 lower than 0%");
      }
      Properties.Settings.Default.Save();
    }

  }



}
