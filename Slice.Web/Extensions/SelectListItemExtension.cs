namespace Slice.Web.Extensions;
public static class SelectListItemExtension
{
    public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items)
    {
        var list = new List<SelectListItem>();
        var selectListItem = new SelectListItem
        {
            Text = "--Select--",
            Value = "0"
        };

        list.Add(selectListItem);

        foreach (var item in items)
        {
            selectListItem = new SelectListItem
            {
                Text = item.GetPropertyValue("Name"),
                Value = item.GetPropertyValue("Id")
            };

            list.Add(selectListItem);
        }

        return list;
    }
}