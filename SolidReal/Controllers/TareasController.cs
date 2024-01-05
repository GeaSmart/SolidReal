using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySolidAPI.Dtos;
using SolidReal.Factory;
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
        private readonly ILogging logging;
        private readonly RepositoryTarea repositoryTarea;
        private readonly RepositoryUsuario repositoryUsuario;        

        public TareasController(ApplicationDbContext context, ILoggingFactory logging,
            RepositoryTarea repositoryTarea, RepositoryUsuario repositoryUsuario)
        {
            this.context = context;
            this.logging = logging.GetLogger();
            this.repositoryTarea = repositoryTarea;
            this.repositoryUsuario = repositoryUsuario;
        }

        [HttpGet]
        public async Task<List<TareaConUsuarioDto>> GetAsync()
        {
            await logging.Log("Inicio de peticion: GetAsync");
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

                await logging.Log("Fin del procesamiento");
                throw new NotImplementedException();
                return Ok();
            }

            catch (Exception ex)
            {
                await logging.LogException(ex);
                return StatusCode(500);
            }
        }
    }
}