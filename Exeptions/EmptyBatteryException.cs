using System.Threading.Tasks.Dataflow;

namespace ABPD_HW_02;

public class EmptyBatteryException : Exception 
{
    public EmptyBatteryException() : base("Battery too low to turn on device.") {}
}