using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracowniaProgramowania.Models
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
