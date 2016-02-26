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
      COMMS.Instance.MoveMotorValve(1, "O");
      Properties.Settings.Default.RefillPumpState = true;
    }
    public static void StopPump1()
    {
      COMMS.Instance.MoveMotorValve(1, "S");
      Properties.Settings.Default.RefillPumpState = false;
    }
    public static void SetPump2(int percent)
    {
      COMMS.Instance.SetRegulator(1, percent * 4000 / 100);
      Properties.Settings.Default.MainPumpStatePercent = percent;
    }

    public static void IncreaseMainPump(int percent) //Increases MainPump by percent %
    {
      if ((Properties.Settings.Default.MainPumpStatePercent + percent)<=100) //becuase pump can't be less than 0.
      {
        SetPump2(Properties.Settings.Default.MainPumpStatePercent + percent);
      }
      else
      {
        System.Diagnostics.Debug.WriteLine("can't make pump 2 higher than 100%");
      }
    }

    public static void DecreaseMainPump(int percent) //Increases MainPump by percent %
    {
      if (Properties.Settings.Default.MainPumpStatePercent>= percent) //becuase pump can't be less than 0.
      {
        SetPump2(Properties.Settings.Default.MainPumpStatePercent - percent);
      }
      else
      {
        System.Diagnostics.Debug.WriteLine("can't make pump 2 lower than 0%");
      }
      
    }

  }



}
