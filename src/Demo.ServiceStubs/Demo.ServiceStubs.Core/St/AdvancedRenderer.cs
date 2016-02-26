using Antlr4.StringTemplate;

namespace Demo.ServiceStubs.Core.St
{
    public class AdvancedRenderer : IAttributeRenderer
    {
        public string ToString(object obj, string formatString, System.Globalization.CultureInfo culture)
        {
            if (obj == null)
                return null;

            if (string.IsNullOrEmpty(formatString))
                return obj.ToString();

            return string.Format("{0:" + formatString + "}", obj);
        }
    }
}
