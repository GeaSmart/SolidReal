using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySolidAPI.Dtos;
using MySolidAPI.Entities;
using Newtonsoft.Json;

namespace MySolidAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public TareasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<List<TareaConUsuarioDto>> GetAsync()
        {            
            Console.WriteLine("Inicio de peticion: GetAsync");
            var tareas = await context.Tareas.Include(x => x.Usuario).ToListAsync();

            return tareas.Select(x => new TareaConUsuarioDto
            {
                Id = x.Id,
                UserName = x.Usuario.Name,
                Title = x.Title
            }).ToList();
        }

        [HttpPost]
        public async Task<ActionResult> ImportFromApi()
        {
            try
            {
                Console.WriteLine("Inicio de petición: ImportFromApi");

                var cliente = new HttpClient();
                var urlTareas = "https://jsonplaceholder.typicode.com/todos";
                var responseTareas = await cliente.GetAsync(urlTareas);
                responseTareas.EnsureSuccessStatusCode();

                var bodyTareas = await responseTareas.Content.ReadAsStringAsync();
                Console.WriteLine(bodyTareas.Substring(0, 100));
                var tareas = JsonConvert.DeserializeObject<Tarea[]>(bodyTareas);
                
                var urlUsuarios = @"https://jsonplaceholder.typicode.com/users";
                var responseUsuarios = await cliente.GetAsync(urlUsuarios);
                responseUsuarios.EnsureSuccessStatusCode();

                var bodyUsuarios = await responseUsuarios.Content.ReadAsStringAsync();
                Console.WriteLine(bodyUsuarios.Substring(0, 100));
                var usuarios = JsonConvert.DeserializeObject<Usuario[]>(bodyUsuarios);

                foreach (var item in tareas)
                {
                    item.Usuario = usuarios.Where(x => x.Id == item.UserId).SingleOrDefault();
                }

                var tareasFromDb = context.Tareas.Select(x=>x.Id).ToList();
                var tareasToInsert = tareas.Where(x => !tareasFromDb.Contains(x.Id));

                await context.Tareas.AddRangeAsync(tareasToInsert);

                await context.SaveChangesAsync();

                Console.WriteLine("Fin del procesamiento");
                //throw new NotImplementedException();

                return Ok();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"ERROR:{ex.Message}");
                return StatusCode(500);
            }
        }
    }
}