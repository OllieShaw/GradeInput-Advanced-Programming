using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Text.Json;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace GradeInput_Advanced_Programming.Helper
{
    public class UtilityHelper
    {
        public static SelectList AddEmptyOptionToSelectList(SelectList originalSelectList)
        {
            var items = originalSelectList.ToList();
            items.Insert(0, CreateEmptyOption());
            return new SelectList(items, "Value", "Text");
        }

        public static SelectList AddEmptyOptionToSelectList(IEnumerable<SelectListItem> originalSelectList)
        {
            var items = originalSelectList.ToList();

            items.Insert(0, CreateEmptyOption());

            return new SelectList(items, "Value", "Text");
        }
        public static SelectListItem CreateEmptyOption()
        {
            return new SelectListItem
            {
                Selected = true,
                Disabled = true,
                Text = "Select an option",
                Value = ""
            };
        }
        public static SelectList ConvertToSelectList<T>(IEnumerable<T> items, string valueField, string textField)
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in items)
            {
                var valueProperty = item?.GetType().GetProperty(valueField);
                var textProperty = item?.GetType().GetProperty(textField);

                if (valueProperty != null && textProperty != null)
                {
                    var value = valueProperty.GetValue(item, null)?.ToString();
                    var text = textProperty.GetValue(item, null)?.ToString();

                    selectListItems.Add(new SelectListItem
                    {
                        Value = value,
                        Text = text
                    });
                }
            }

            var orderedSelectListItems = selectListItems.OrderBy(item => item.Text).ToList();

            return new SelectList(orderedSelectListItems, "Value", "Text");
        }

    }


}