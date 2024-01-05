using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySolidAPI.Dtos;
using MySolidAPI.Entities;
using Newtonsoft.Json;
using SolidReal.Logging;
using SolidReal.Mapping;
using SolidReal.Repository;

namespace MySolidAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly LoggingConsola loggingConsola;
        private readonly RepositoryTarea repositoryTarea;
        private readonly RepositoryUsuario repositoryUsuario;

        public TareasController(ApplicationDbContext context, LoggingConsola loggingConsola,
            RepositoryTarea repositoryTarea, RepositoryUsuario repositoryUsuario)
        {
            this.context = context;
            this.loggingConsola = loggingConsola;
            this.repositoryTarea = repositoryTarea;
            this.repositoryUsuario = repositoryUsuario;
        }

        [HttpGet]
        public async Task<List<TareaConUsuarioDto>> GetAsync()
        {
            loggingConsola.Log("Inicio de peticion: GetAsync");
            var tareas = await context.Tareas.Include(x => x.Usuario).ToListAsync();
            var tareasConUsuario = tareas.Select(x => x.AsTareaConUsuarioDto(x.Usuario)).ToList();

            return tareasConUsuario;
        }

        [HttpPost]
        public async Task<ActionResult> ImportFromApi()
        {
            try
            {
                var tareas = await repositoryTarea.ObtenerAsync();
                var usuarios = await repositoryUsuario.ObtenerAsync();

                foreach (var item in tareas)
                {
                    item.Usuario = usuarios.Where(x => x.Id == item.UserId).SingleOrDefault();
                }
                var tareasFromDb = context.Tareas.Select(x => x.Id).ToList();
                var tareasToInsert = tareas.Where(x => !tareasFromDb.Contains(x.Id));

                await context.Tareas.AddRangeAsync(tareasToInsert);
                await context.SaveChangesAsync();

                loggingConsola.Log("Fin del procesamiento");
                //throw new NotImplementedException();
                return Ok();
            }

            catch (Exception ex)
            {
                loggingConsola.LogException(ex);
                return StatusCode(500);
            }
        }
    }
}