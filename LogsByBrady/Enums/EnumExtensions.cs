using Microsoft.VisualBasic.FileIO;
using System.Reflection;
using System.Runtime.Serialization;

namespace LogsByBrady.Enums
{
    public static class EnumExtensions
    {
        internal static string? ToEnumMember<T>(this T value) where T : Enum
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())?
                .GetCustomAttribute<EnumMemberAttribute>(false)?
                .Value;
        }
        internal static BradysFormatProvider ToEnum<T>(string valueToFind)
        {
            valueToFind = valueToFind.StartsWith('.') ? valueToFind.Substring(1) : valueToFind;
            BradysFormatProvider fileTypeToReturn = BradysFormatProvider.Text;
            foreach (BradysFormatProvider fileType in Enum.GetValues(typeof(BradysFormatProvider)))
            {
                var memberInfo = typeof(BradysFormatProvider).GetMember(fileType.ToString());
                var enumMemberAttribute = (EnumMemberAttribute)Attribute.GetCustomAttribute(memberInfo[0], typeof(EnumMemberAttribute));
                if (enumMemberAttribute != null && enumMemberAttribute.Value == valueToFind)
                {
                    fileTypeToReturn = fileType;
                }
            }
            return fileTypeToReturn;
        }
    }

}