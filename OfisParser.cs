namespace OfisUpdater
{
    using CsvHelper;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class OfisParser
    {
        public IEnumerable<OfisEntry> Parse(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(stream, true))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.IgnoreHeaderWhiteSpace = true;
                return csv.GetRecords<OfisEntry>().ToList();
            }
        }
    }
}
