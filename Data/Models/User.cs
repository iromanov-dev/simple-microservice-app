using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class User : Entity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Surname { get; set; }
        [MaxLength(50)]
        [Required]
        public string Patronymic { get; set; }
        [MaxLength(50)]
        [Required]
        public string Phone { get; set; }
        [MaxLength(255)]
        [Required]
        public string Email { get; set; }
        public long? OrganizationId { get; set; }
        public virtual Organization? Organization { get; set; }
    }
}
