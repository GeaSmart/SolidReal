using MySolidAPI.Entities;
using Newtonsoft.Json;
using SolidReal.Factory;
using SolidReal.Logging;

namespace SolidReal.Repository
{
    public class RepositoryTarea
    {
        private readonly ILogging logging;

        public RepositoryTarea(ILoggingFactory logging)
        {
            this.logging = logging.GetLogger();
        }
        public async Task<List<Tarea>> ObtenerAsync()
        {
            logging.Log("Obteniendo tareas desde API externo");

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
