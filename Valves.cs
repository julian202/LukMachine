using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukMachine
{
  public static class Valves
  {
    public static void CloseValve1()
    {
      COMMS.Instance.MoveValve(1, "C");
    }
    public static void OpenValve1()
    {
      COMMS.Instance.MoveValve(1, "O");
    }
    public static void CloseValve2()
    {
      COMMS.Instance.MoveValve(2, "C");
    }
    public static void OpenValve2()
    {
      COMMS.Instance.MoveValve(2, "O");
    }
    public static void CloseValve3()
    {
      COMMS.Instance.MoveValve(3, "C");
    }
    public static void OpenValve3()
    {
      COMMS.Instance.MoveValve(3, "O");
    }
    public static void CloseValve4()
    {
      COMMS.Instance.MoveValve(4, "C");
    }
    public static void OpenValve4()
    {
      COMMS.Instance.MoveValve(4, "O");
    }
    public static void CloseValve5()
    {
      COMMS.Instance.MoveValve(5, "C");
    }
    public static void OpenValve5()
    {
      COMMS.Instance.MoveValve(5, "O");
    }
    public static void CloseValve6()
    {
      COMMS.Instance.MoveValve(6, "C");
    }
    public static void OpenValve6()
    {
      COMMS.Instance.MoveValve(6, "O");
    }
    public static void CloseValve7()
    {
      COMMS.Instance.MoveValve(7, "C");
    }
    public static void OpenValve7()
    {
      COMMS.Instance.MoveValve(7, "O");
    }
  }
}
