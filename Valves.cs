using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukMachine
{
  public static class Valves
  {
    public static void CloseValve(int num)
    {
      if (num == 1)
      {
        CloseValve1();
      }
      if (num == 2)
      {
        CloseValve2();
      }
      if (num == 3)
      {
        CloseValve3();
      }
      if (num == 4)
      {
        CloseValve4();
      }
      if (num == 5)
      {
        CloseValve5();
      }
      if (num == 6)
      {
        CloseValve6();
      }
      if (num == 7)
      {
        CloseValve7();
      }
    }
    public static void OpenValve(int num)
    {
      if (num == 1)
      {
        OpenValve1();
      }
      if (num == 2)
      {
        OpenValve2();
      }
      if (num == 3)
      {
        OpenValve3();
      }
      if (num == 4)
      {
        OpenValve4();
      }
      if (num == 5)
      {
        OpenValve5();
      }
      if (num == 6)
      {
        OpenValve6();
      }
      if (num == 7)
      {
        OpenValve7();
      }
    }


    public static void CloseValve1()
    {
      COMMS.Instance.MoveValve(1, "O");
      Properties.Settings.Default.Valve1State = false;
      Properties.Settings.Default.Save();
    }
    public static void OpenValve1()
    {
      COMMS.Instance.MoveValve(1, "C");
      Properties.Settings.Default.Valve1State = true;
      Properties.Settings.Default.Save();
    }
    public static void CloseValve2()
    {
      COMMS.Instance.MoveValve(2, "O");
      Properties.Settings.Default.Valve2State = false;
      Properties.Settings.Default.Save();
    }
    public static void OpenValve2()
    {
      COMMS.Instance.MoveValve(2, "C");
      Properties.Settings.Default.Valve2State = true;
      Properties.Settings.Default.Save();
    }
    public static void CloseValve3()
    {
      COMMS.Instance.MoveValve(3, "O");
      Properties.Settings.Default.Valve3State = false;
      Properties.Settings.Default.Save();
    }
    public static void OpenValve3()
    {
      COMMS.Instance.MoveValve(3, "C");
      Properties.Settings.Default.Valve3State = true;
      Properties.Settings.Default.Save();
    }
    public static void CloseValve4()
    {
      COMMS.Instance.MoveValve(4, "O");
      Properties.Settings.Default.Valve4State = false;
      Properties.Settings.Default.Save();
    }
    public static void OpenValve4()
    {
      COMMS.Instance.MoveValve(4, "C");
      Properties.Settings.Default.Valve4State = true;
      Properties.Settings.Default.Save();
    }
    public static void CloseValve5()
    {
      COMMS.Instance.MoveValve(5, "O");
      Properties.Settings.Default.Valve5State = false;
      Properties.Settings.Default.Save();
    }
    public static void OpenValve5()
    {
      COMMS.Instance.MoveValve(5, "C");
      Properties.Settings.Default.Valve5State = true;
      Properties.Settings.Default.Save();
    }
    public static void CloseValve6()
    {
      COMMS.Instance.MoveValve(6, "O");
      Properties.Settings.Default.Valve6State = false;
      Properties.Settings.Default.Save();
    }
    public static void OpenValve6()
    {
      COMMS.Instance.MoveValve(6, "C");
      Properties.Settings.Default.Valve6State = true;
      Properties.Settings.Default.Save();
    }
    public static void CloseValve7()
    {
      COMMS.Instance.MoveValve(7, "C");
      Properties.Settings.Default.Valve7State = false;
      Properties.Settings.Default.Save();
    }
    public static void OpenValve7()
    {
      COMMS.Instance.MoveValve(7, "O");
      Properties.Settings.Default.Valve7State = true;
      Properties.Settings.Default.Save();
    }


    ///interchanging O with C
    /*
    public static void CloseValve1()
    {
      COMMS.Instance.MoveValve(1, "C");
      Properties.Settings.Default.Valve1State = false;
    }
    public static void OpenValve1()
    {
      COMMS.Instance.MoveValve(1, "O");
      Properties.Settings.Default.Valve1State = true;
    }
    public static void CloseValve2()
    {
      COMMS.Instance.MoveValve(2, "C");
      Properties.Settings.Default.Valve2State = false;
    }
    public static void OpenValve2()
    {
      COMMS.Instance.MoveValve(2, "O");
      Properties.Settings.Default.Valve2State = true;
    }
    public static void CloseValve3()
    {
      COMMS.Instance.MoveValve(3, "C");
      Properties.Settings.Default.Valve3State = false;
    }
    public static void OpenValve3()
    {
      COMMS.Instance.MoveValve(3, "O");
      Properties.Settings.Default.Valve3State = true;
    }
    public static void CloseValve4()
    {
      COMMS.Instance.MoveValve(4, "C");
      Properties.Settings.Default.Valve4State = false;
    }
    public static void OpenValve4()
    {
      COMMS.Instance.MoveValve(4, "O");
      Properties.Settings.Default.Valve4State = true;
    }
    public static void CloseValve5()
    {
      COMMS.Instance.MoveValve(5, "C");
      Properties.Settings.Default.Valve5State = false;
    }
    public static void OpenValve5()
    {
      COMMS.Instance.MoveValve(5, "O");
      Properties.Settings.Default.Valve5State = true;
    }
    public static void CloseValve6()
    {
      COMMS.Instance.MoveValve(6, "C");
      Properties.Settings.Default.Valve6State = false;
    }
    public static void OpenValve6()
    {
      COMMS.Instance.MoveValve(6, "O");
      Properties.Settings.Default.Valve6State = true;
    }
    public static void CloseValve7()
    {
      COMMS.Instance.MoveValve(7, "C");
      Properties.Settings.Default.Valve7State = false;
    }
    public static void OpenValve7()
    {
      COMMS.Instance.MoveValve(7, "O");
      Properties.Settings.Default.Valve7State = true;
    }*/

  }
}
