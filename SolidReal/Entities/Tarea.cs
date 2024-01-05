using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MySolidAPI.Entities
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set;}
        public bool Completed { get; set; }

        //Navigation properties
        public Usuario Usuario { get; set; }
    }
}
