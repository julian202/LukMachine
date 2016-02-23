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
    }
    public static void StopPump1()
    {
      COMMS.Instance.MoveMotorValve(1, "S");
    }
    public static void SetPump2(int percent)
    {
      COMMS.Instance.SetRegulator(1, percent * 4000 / 100);
    }

    
  }



}
