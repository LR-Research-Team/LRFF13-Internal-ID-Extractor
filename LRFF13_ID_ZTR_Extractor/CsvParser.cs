namespace LRFF13_ID_ZTR_Extractor
{
    internal sealed class CsvParser
    {
        private readonly ItemWdbParser ItemParser;

        public CsvParser(ItemWdbParser itemParser)
        {
            ItemParser = itemParser;
        }

        public void Parse(string prefix, string outputCsvPath)
        {
            Console.WriteLine($"Parsing CSV for {prefix}");

            using var writer = new StreamWriter(outputCsvPath);
            writer.WriteLine("ID,Name");

            foreach (var item in ItemParser.GetItems(prefix))
                writer.WriteLine($"{Escape(item.Id)},{Escape(item.Name)}");

            Console.WriteLine($"Done parsing CSV prefix '{prefix}', written to {outputCsvPath}");
        }

        private static string Escape(string value)
        {
            if (!value.Contains(',') && !value.Contains('"') && !value.Contains('\r') && !value.Contains('\n'))
                return value;

            return $"\"{value.Replace("\"", "\"\"")}\"";
        }
    }
}