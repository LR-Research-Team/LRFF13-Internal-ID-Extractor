namespace LRFF13_ID_ZTR_Extractor
{
    internal class Program
    {
        static void Main(string[] args)
        {
//            if (args.Length == 0)
//            {
//#if DEBUG
//                args = new string[2];
//                args[0] = @"C:\Program Files (x86)\Steam\steamapps\common\LIGHTNING RETURNS FINAL FANTASY XIII\weiss_data\db\resident\item.json";
//                args[1] = @"C:\Program Files (x86)\Steam\steamapps\common\LIGHTNING RETURNS FINAL FANTASY XIII\weiss_data\txtres\resident\system\txtres_us.txt";
//#endif
//            }

            if (args.Length != 3)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine(@"Parameter 1: Path to item.json (exported from weiss_data\db\resident\item.wdb");
                Console.WriteLine(@"Parameter 2: Path to txtres_us.txt (exported from weiss_data\txtres\resident\system\txtres_us.ztr");
                Console.WriteLine(@"Parameter 3: Output folder path");
                Console.WriteLine("Example: " + "LRFF13_ID_ZTR_Extractor.exe " +
                                    "\"C:\\Program Files (x86)\\Steam\\steamapps\\common\\LIGHTNING RETURNS FINAL FANTASY XIII\\weiss_data\\db\\resident\\item.json\" " +
                                    "\"C:\\Program Files (x86)\\Steam\\steamapps\\common\\LIGHTNING RETURNS FINAL FANTASY XIII\\weiss_data\\txtres\\resident\\system\\txtres_us.txt\" " +
                                    "\"c:\\temp\\IdOutput\"");
                return;
            }

            if(!File.Exists(args[0]))
            {
                Console.WriteLine($"Item file does not exist: {args[0]}");
                return;
            }

            if (!File.Exists(args[1]))
            {
                Console.WriteLine($"Txtres file does not exist: {args[1]}");
                return;
            }

            string outputFolder = args[2];
            if(!Directory.Exists(outputFolder))
            {
                Console.WriteLine($"Output folder does not exist: {outputFolder}");
                return;
            }

            if(!outputFolder.EndsWith("\\"))
                outputFolder += "\\";

            var parser = new ItemWdbParser(args[0], args[1]);
            var markdownParser = new MarkdownParser(parser);
            var csvParser = new CsvParser(parser);
            markdownParser.Parse("cos", $"{outputFolder}item_costumes.md");
            csvParser.Parse("cos", $"{outputFolder}item_costumes.csv");
            markdownParser.Parse("wea", $"{outputFolder}item_weapons.md");
            csvParser.Parse("wea", $"{outputFolder}item_weapons.csv");
            markdownParser.Parse("shi", $"{outputFolder}item_shields.md");
            csvParser.Parse("shi", $"{outputFolder}item_shields.csv");
            markdownParser.Parse("acc", $"{outputFolder}item_accesories.md");
            csvParser.Parse("acc", $"{outputFolder}item_accesories.csv");
            markdownParser.Parse("it", $"{outputFolder}item_potions.md");
            csvParser.Parse("it", $"{outputFolder}item_potions.csv");
            markdownParser.Parse("mg", $"{outputFolder}item_debuffs.md");
            csvParser.Parse("mg", $"{outputFolder}item_debuffs.csv");
            markdownParser.Parse("abi", $"{outputFolder}item_garb_abilities.md");
            csvParser.Parse("abi", $"{outputFolder}item_garb_abilities.csv");
            markdownParser.Parse("at", $"{outputFolder}item_attacks.md");
            csvParser.Parse("at", $"{outputFolder}item_attacks.csv");
            markdownParser.Parse("e", $"{outputFolder}item_cosmetics.md");
            csvParser.Parse("e", $"{outputFolder}item_cosmetics.csv");
            markdownParser.Parse("gd", $"{outputFolder}item_guarding.md");
            csvParser.Parse("gd", $"{outputFolder}item_guarding.csv");
            markdownParser.Parse("gil", $"{outputFolder}item_gil.md");
            csvParser.Parse("gil", $"{outputFolder}item_gil.csv");
            markdownParser.Parse("key", $"{outputFolder}item_key_items.md");
            csvParser.Parse("key", $"{outputFolder}item_key_items.csv");
            markdownParser.Parse("libra", $"{outputFolder}item_libra.md");
            csvParser.Parse("libra", $"{outputFolder}item_libra.csv");
            markdownParser.Parse("mat", $"{outputFolder}item_materials.md");
            csvParser.Parse("mat", $"{outputFolder}item_materials.csv");
            markdownParser.Parse("mb", $"{outputFolder}item_magics.md");
            csvParser.Parse("mb", $"{outputFolder}item_magics.csv");
            markdownParser.Parse("ac", $"{outputFolder}item_atb.md");
            csvParser.Parse("ac", $"{outputFolder}item_atb.csv");
            markdownParser.Parse("av", $"{outputFolder}item_evade.md");
            csvParser.Parse("av", $"{outputFolder}item_evade.csv");
            markdownParser.Parse("ga", $"{outputFolder}item_counters.md");
            csvParser.Parse("ga", $"{outputFolder}item_counters.csv");
            markdownParser.Parse("ma", $"{outputFolder}item_ruin.md");
            csvParser.Parse("ma", $"{outputFolder}item_ruin.csv");
            markdownParser.Parse("mc", $"{outputFolder}item_elemental.md");
            csvParser.Parse("mc", $"{outputFolder}item_elemental.csv");
            markdownParser.Parse("ms", $"{outputFolder}item_ultimates.md");
            csvParser.Parse("ms", $"{outputFolder}item_ultimates.csv");
        }
    }
}
