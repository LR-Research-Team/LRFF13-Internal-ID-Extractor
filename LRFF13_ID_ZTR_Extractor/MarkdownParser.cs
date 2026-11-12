namespace LRFF13_ID_ZTR_Extractor
{
    internal sealed class MarkdownParser
    {
        private readonly ItemWdbParser ItemParser;

        public MarkdownParser(ItemWdbParser itemParser)
        {
            ItemParser = itemParser;
        }

        public void Parse(string prefix, string outputMdPath)
        {
            var categoryName = ItemParser.GetCategoryName(prefix);
            Console.WriteLine($"Parsing {categoryName}");

            using var writer = new StreamWriter(outputMdPath);
            writer.WriteLine($"### {categoryName}");
            writer.WriteLine("| ID | Name |");
            writer.WriteLine("| -- | -- |");

            foreach (var item in ItemParser.GetItems(prefix))
                writer.WriteLine($"| {item.Id} | {item.Name} |");

            Console.WriteLine($"Done parsing prefix '{categoryName}', written to {outputMdPath}");
        }
    }
}