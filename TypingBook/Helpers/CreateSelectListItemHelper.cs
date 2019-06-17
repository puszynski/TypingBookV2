using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using TypingBook.Enums;

namespace TypingBook.Helpers
{
    public class CreateSelectListItemHelper
    {
        #region Singleton
        private static CreateSelectListItemHelper _createSelectListItemHelper;
        private CreateSelectListItemHelper()
        {                
        }
        public static CreateSelectListItemHelper GetInstance()
        {
            if (_createSelectListItemHelper == null)
                _createSelectListItemHelper = new CreateSelectListItemHelper();
            return _createSelectListItemHelper;
        }
        #endregion

        // eFitness - zaimplementuj
        public static List<SelectListItem> SelectListForEnum<T>(Predicate<T> selectedItemPredicate, Func<T, string> textMappingFunction) where T : Enum
        {
            var enumValues = Enum
                    .GetValues(typeof(T))
                    .Cast<T>();

            return enumValues.Select(x => new SelectListItem()
            {
                Value = Convert.ToInt32(x).ToString(),
                Text = textMappingFunction(x),
                Selected = selectedItemPredicate(x),
            })
            .ToList();
        }

        public IEnumerable<SelectListItem> GetSelectListItems<T>(List<int> selected = null) where T: Enum
        {
            //yield return new SelectListItem() { Text = "Select", Value = null, Selected = false };

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

        // Non generic example
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
