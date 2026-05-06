using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GradeInput_Advanced_Programming.Models
{
    public static class EnumExtensions
    {
        public static SelectList ToSelectList<TEnum>(
        TEnum? selectedValue = null
    ) where TEnum : struct, Enum
        {
            var values = Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new
                {
                    Value = (int)(object)e,
                    Text = e.ToString()
                });

            return new SelectList(values, "Value", "Text", selectedValue?.ToString());
        }

        public static T GetEnumFromId<T>(int id) where T : struct, Enum
        {
            foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var enumValue = (T)field.GetValue(null)!;
                if (Convert.ToInt32(enumValue) == id)
                    return enumValue;
            }

            throw new ArgumentException($"No {typeof(T).Name} with Id or value '{id}' found.");
        }
    }
}
