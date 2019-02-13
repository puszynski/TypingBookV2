using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using TypingBook.Enums;

namespace TypingBook.Helpers
{
    public class CreateSelectListItemHelper
    {
        public IEnumerable<SelectListItem> GetSelectListItems<T>(List<int> selected = null) where T: Enum
        {
            yield return new SelectListItem() { Text = "Select", Value = null, Selected = false };

            foreach (var item in Enum.GetValues(typeof(T)))
            {
                yield return new SelectListItem()
                {
                    Text = item.ToString(),
                    Value = ((int)item).ToString(),
                    Selected = selected != null && selected.Contains((int)item)
                };
            }
        }

        public static IEnumerable<SelectListItem> GetApiSearchTypes(List<int> selected = null)
        {
            foreach (var item in Enum.GetValues(typeof(EBookGenre)))
            {
                yield return new SelectListItem
                {
                    Text = item.ToString(),
                    Value = ((int)item).ToString(),
                    Selected = selected != null && selected.Contains((int)item)
                };
            }
        }
    }
}
