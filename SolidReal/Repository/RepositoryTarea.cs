using MySolidAPI.Entities;
using Newtonsoft.Json;
using SolidReal.Logging;

namespace SolidReal.Repository
{
    public class RepositoryTarea
    {
        private readonly LoggingConsola loggingConsola;

        public RepositoryTarea(LoggingConsola loggingConsola)
        {
            this.loggingConsola = loggingConsola;
        }
        public async Task<List<Tarea>> ObtenerAsync()
        {
            loggingConsola.Log("Obteniendo tareas desde API externo");

            var cliente = new HttpClient();
            var urlTareas = "https://jsonplaceholder.typicode.com/todos";
            var responseTareas = await cliente.GetAsync(urlTareas);
            responseTareas.EnsureSuccessStatusCode();

            var bodyTareas = await responseTareas.Content.ReadAsStringAsync();
            Console.WriteLine(bodyTareas.Substring(0, 100));
            var tareas = JsonConvert.DeserializeObject<List<Tarea>>(bodyTareas);
            return tareas;
        }
    }
}
