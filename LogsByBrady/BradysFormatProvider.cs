using System.Runtime.Serialization;

namespace LogsByBrady
{
    public enum BradysFormatProvider
    {
        [EnumMember(Value = "txt")]
        Text,
        [EnumMember(Value = "json")]
        Json
    }

}