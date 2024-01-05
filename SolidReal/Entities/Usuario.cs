using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MySolidAPI.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
