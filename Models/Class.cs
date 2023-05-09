using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkolprojektLab2.Models
{
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassId { get; set; }

        [Required]
        public string ClassName { get; set; }

        public virtual ICollection<RelationshipTable> Relationships { get; set; }
    }
}