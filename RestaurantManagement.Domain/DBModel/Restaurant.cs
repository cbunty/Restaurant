using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.DBModel
{
    public class Restaurant : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Name { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [StringLength(2000)]
        public string Address { get; set; }
        [Column(TypeName = "varchar(13)")]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string WebsiteUrl { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
