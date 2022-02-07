using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracowniaProgramowania.Models
{
    public class Companies
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int NIP { get; set; }

        [ForeignKey("Industry")]
        public int IndustryId { get; set; }
        public virtual Industries Industry { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual Users User { get; set; }
        public bool IsDeleted { get; set; }

    }
}
