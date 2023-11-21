using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SalesReach.Domain.Enums.Extensions
{
    public static class EnumExtensions
    {
        public static string DisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }

        public static T ConvertToEnum<T>(string value)
        {
            if (Enum.TryParse<T>(value, out T result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException($"O valor '{value}' não é válido para o enum {typeof(T).Name}.");
            }
        }
    }         
}
