namespace SolidReal.Logging
{
    public class LoggingConsola
    {
        public void Log(string mensaje)
        {
            Console.WriteLine($"{DateTime.Now} : {mensaje}");
        }

        public void LogException(Exception ex)
        {
            string error = $"EXCEPCION: {ex.Message}";
            Log(error);            
        }
    }
}
