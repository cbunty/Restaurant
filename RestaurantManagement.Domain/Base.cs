using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain
{
    public class Base : Audit
    {
        public byte? StatusId { get; set; } = (byte)Enumerations.StatusEnum.Active;
        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }
    }
}
