using System.ComponentModel;

namespace Web3MusicStore.API.Models;

public static class EnumExtensions
{
  public static string GetDescriptionAttributeValue(this Enum enumValue)
  {
    var enumType = enumValue.GetType();
    var valueAttributes = enumType
    .GetMember(enumValue.ToString())
    .FirstOrDefault()?
    .GetCustomAttributes(typeof(DescriptionAttribute), false).SingleOrDefault() as DescriptionAttribute;

    return (valueAttributes == null) ? enumValue.ToString() : valueAttributes.Description;
  }
}
