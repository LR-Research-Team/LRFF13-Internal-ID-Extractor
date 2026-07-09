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

            ItemWdbParser parser = new ItemWdbParser(args[0], args[1]);
            parser.Parse("cos", $"{outputFolder}item_costumes.md");
            parser.Parse("wea", $"{outputFolder}item_weapons.md");
            parser.Parse("shi", $"{outputFolder}item_shields.md");
            parser.Parse("acc", $"{outputFolder}item_accesories.md");
            parser.Parse("it", $"{outputFolder}item_potions.md");
            parser.Parse("mg", $"{outputFolder}item_debuffs.md");
            parser.Parse("abi", $"{outputFolder}item_garb_abilities.md");
            parser.Parse("at", $"{outputFolder}item_attacks.md");
            parser.Parse("e", $"{outputFolder}item_cosmetics.md");
            parser.Parse("gd", $"{outputFolder}item_guarding.md");
            parser.Parse("gil", $"{outputFolder}item_gil.md");
            parser.Parse("key", $"{outputFolder}item_key_items.md");
            parser.Parse("libra", $"{outputFolder}item_libra.md");
            parser.Parse("mat", $"{outputFolder}item_materials.md");
            parser.Parse("mb", $"{outputFolder}item_magics.md");
            parser.Parse("ac", $"{outputFolder}item_atb.md");
            parser.Parse("av", $"{outputFolder}item_evade.md");
            parser.Parse("ga", $"{outputFolder}item_counters.md");
            parser.Parse("ma", $"{outputFolder}item_ruin.md");
            parser.Parse("mc", $"{outputFolder}item_elemental.md");
            parser.Parse("ms", $"{outputFolder}item_ultimates.md");
        }
    }
}
