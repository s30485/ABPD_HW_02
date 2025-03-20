using System.Threading.Tasks.Dataflow;

namespace ABPD_HW_02;

public class EmptySystemException : Exception
{
    public EmptySystemException() : base("No operating system installed.") {}
}