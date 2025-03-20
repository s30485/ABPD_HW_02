using System.Threading.Tasks.Dataflow;

namespace ABPD_HW_02;

public class ConnectionException : Exception
{
    public ConnectionException() : base("Invalid network connection.") {}
}