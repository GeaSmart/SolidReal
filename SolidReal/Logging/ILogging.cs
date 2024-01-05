namespace SolidReal.Logging
{
    public interface ILogging
    {
        Task Log(string message);
        Task LogException(Exception exception);
    }
}
