using System.Runtime.Serialization;

namespace RestaurantManagement.Domain.Enumerations
{
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum StatusEnum
    {
        [EnumMember(Value = "Active")]
        Active = 1,
        [EnumMember(Value = "InActive")]
        InActive
    }
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum OrderStatusEnum
    {
        [EnumMember(Value = "Pending")]
        Pending = 1,
        [EnumMember(Value = "Rejected")]
        Rejected,
        [EnumMember(Value = "Completed")]
        Completed,
        [EnumMember(Value = "Failed")]
        Failed,
        [EnumMember(Value = "Paid")]
        Paid,
        [EnumMember(Value = "ProcessingPayment")]
        ProcessingPayment,
    }
}
