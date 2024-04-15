using System.Runtime.Serialization;

namespace LogsByBrady.Enums
{
    public enum BradysFormatProvider
    {
        [EnumMember(Value = "txt")]
        Text,
        [EnumMember(Value = "json")]
        Json,
        All
    }

}