using MySolidAPI.Entities;
using Newtonsoft.Json;
using SolidReal.Logging;

namespace SolidReal.Repository
{
    public class RepositoryUsuario
    {
        private readonly LoggingConsola loggingConsola;

        public RepositoryUsuario(LoggingConsola loggingConsola)
        {
            this.loggingConsola = loggingConsola;
        }
        public async Task<List<Usuario>> ObtenerAsync()
        {
            loggingConsola.Log("Obteniendo usuarios desde API externo");

            var cliente = new HttpClient();
            var urlUsuarios = @"https://jsonplaceholder.typicode.com/users";
            var responseUsuarios = await cliente.GetAsync(urlUsuarios);
            responseUsuarios.EnsureSuccessStatusCode();

            var bodyUsuarios = await responseUsuarios.Content.ReadAsStringAsync();
            Console.WriteLine(bodyUsuarios.Substring(0, 100));
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(bodyUsuarios);
            return usuarios;
        }
    }
}
