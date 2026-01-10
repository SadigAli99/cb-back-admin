

using System.ComponentModel.DataAnnotations;

namespace CB.Shared.Helpers
{
    public static class EnumHelper
    {
        public static Dictionary<int, string> GetEnumValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
            .Cast<T>()
            .ToDictionary(
                k => Convert.ToInt32(k),
                v =>
                {
                    var memInfo = typeof(T).GetMember(v.ToString());
                    var displayAttr = memInfo[0]
                        .GetCustomAttributes(typeof(DisplayAttribute), false)
                        .FirstOrDefault() as DisplayAttribute;

                    return displayAttr?.Name ?? v.ToString();
                });
        }
    }
}
