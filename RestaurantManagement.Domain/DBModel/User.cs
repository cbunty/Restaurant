using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.DBModel
{
    public class User : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "varchar(500)")]
        public string UserName { get; set; }
        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Password { get; set; }
    }
}
