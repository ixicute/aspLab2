using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SkolprojektLab2.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [Required]
        public string CourseName { get; set; }

        public virtual ICollection<RelationshipTable>? Relationships { get; set; }
    }
}
