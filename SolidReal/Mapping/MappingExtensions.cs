using MySolidAPI.Dtos;
using MySolidAPI.Entities;

namespace SolidReal.Mapping
{
    public static class MappingExtensions
    {
        public static TareaConUsuarioDto AsTareaConUsuarioDto(this Tarea item, Usuario usuario)
        {
            return new TareaConUsuarioDto(item.Id, usuario.Name, item.Title);
        }
    }
}
