namespace Core.Utils
{
    public class ConverterData
    {
        public static string databr(string aa)
        {
            string d = aa.Substring(8, 2), m = aa.Substring(5, 2), a = aa.Substring(0, 4);
            aa = d + "/" + m + "/" + a;
            return aa;
        }
        public static string databr2(string aa)
        {
            string d = aa.Substring(8, 2), m = aa.Substring(5, 2), a = aa.Substring(0, 4);
            aa = d + m + a;
            return aa;
        }
    }
}
