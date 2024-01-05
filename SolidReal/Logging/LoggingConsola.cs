namespace SolidReal.Logging
{
    public class LoggingConsola : ILogging
    {
        public LoggingConsola(object args)
        {
            
        }
        public async Task Log(string mensaje)
        {
            Console.WriteLine($"{DateTime.Now} : {mensaje}");
        }

        public async Task LogException(Exception ex)
        {
            SetColor(ConsoleColor.Red);
            string error = $"EXCEPCION: {ex.Message}";
            Log(error);
            SetColor(ConsoleColor.White);
        }
        private void SetColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }
}
