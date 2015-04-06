namespace OfisUpdater
{
    using System.Globalization;
    using System.Linq;

    public static class Culture
    {
        /// <remarks>
        /// http://stackoverflow.com/a/9841533/11963
        /// </remarks>
        public static CultureInfo FromISOName(string name)
        {
            return CultureInfo
                .GetCultures(CultureTypes.NeutralCultures)
                .FirstOrDefault(c => c.TwoLetterISOLanguageName == name);
        }
    }
}
