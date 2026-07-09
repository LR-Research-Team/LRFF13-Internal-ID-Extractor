using System.Text.Json;

namespace LRFF13_ID_ZTR_Extractor
{
    internal class ItemWdbParser
    {
        private readonly Dictionary<string, string> PrefixLookup = new Dictionary<string, string>() {
            {"wea", "weapons" },
            {"shi", "shields" },
            {"acc", "accessories" },
            {"cos", "costumes (garbs)" },
            {"it", "Potions" },
            {"mg", "Debuffs" },
            {"abi", "Garb Abilities" },
            {"at", "Attacks" },
            {"e", "Cosmetics" },
            {"gd", "Guarding" },
            {"gil", "Pickups" },
            {"key", "Key Items" },
            {"libra", "Notes" },
            {"mat", "Materials" },
            {"mb", "Magics" },
            {"ac", "ATB" },
            {"av", "Evade" },
            {"ga", "Counters" },
            {"ma", "Ruin" },
            {"mc", "Elemental" },
            {"ms", "Ultimates" },
        };

        private readonly string ItemWdbPath;
        private readonly string TxtresPath;

        public ItemWdbParser(string itemPath, string txtPath)
        {
            ItemWdbPath = itemPath;
            TxtresPath = txtPath;
        }

        /// <summary>
        /// Parse the item.json file looking for the "record" and "sItemNameStringId" values.
        /// Set the "record" value to the ID
        /// Use the "sItemNameStringId" ID as a lookup into txtres_us, set its value to Name
        /// Export as a markdown table to <paramref name="outputMdPath"/>
        /// </summary>
        /// <param name="prefix">The prefix to search for in the item file</param>
        /// <param name="outputMdPath">The path to export to</param>
        public void Parse(string prefix, string outputMdPath)
        {
            Console.WriteLine($"Parsing {PrefixLookup[prefix]}");

            var itemJson = JsonDocument.Parse(File.ReadAllText(ItemWdbPath));

            // Load txtres_us.txt
            var ZTRLookup = new Dictionary<string, string>();

            foreach (var line in File.ReadLines(TxtresPath))
            {
                if (!line.Contains("|:|"))
                    continue;

                var parts = line.Split("|:|", 2, StringSplitOptions.TrimEntries);
                if (parts.Length != 2)
                    continue;

                string key = parts[0];
                string value = parts[1];

                // Make sure we skip over "h" post-fixed entries which are descriptions
                if (key.EndsWith("h"))
                    continue;

                ZTRLookup[key] = value;
            }

            using var writer = new StreamWriter(outputMdPath);
            //Create the header for the .md table
            writer.WriteLine($"### {PrefixLookup[prefix]}");
            writer.WriteLine("| ID | Name |");
            writer.WriteLine("| -- | -- |");

            // Iterate all the entries in the main "records" array
            foreach (var item in itemJson.RootElement.GetProperty("records").EnumerateArray())
            {
                // Get the record (internal ID) and sItemNameStringId (ZTR lookup ID) values
                string record = item.GetProperty("record").GetString() ?? "";
                string nameId = item.GetProperty("sItemNameStringId").GetString() ?? "";

                // Apply prefix filter
                if (!record.StartsWith(prefix))
                    continue;

                // Special exceptions for "ma" and "ac" prefixes, which would otherwise also catch "mat" and "acc"
                bool match = false;

                if (prefix == "ac")
                {
                    // match "ac*" but NOT "acc*"
                    if (record.StartsWith("ac") && !record.StartsWith("acc"))
                        match = true;
                }
                else if (prefix == "ma")
                {
                    // match "ma*" but NOT "mat*"
                    if (record.StartsWith("ma") && !record.StartsWith("mat"))
                        match = true;
                }
                else
                {
                    // normal prefix match
                    if (record.StartsWith(prefix))
                        match = true;
                }

                if (!match)
                    continue;

                string name = "N/A";

                // Lookup name
                if (!string.IsNullOrWhiteSpace(nameId) &&
                    ZTRLookup.TryGetValue(nameId, out string txtName))
                {
                    if (txtName.Contains('{'))
                        txtName = txtName[..txtName.IndexOf('{')];

                    name = txtName;
                }

                // Abilities are post-fixed with a "level" indicator, map it to the level displayed in-game
                string level = "";
                switch (record.Substring(record.Length - 3, 3))
                {
                    case "_00":
                        level = " Lv. 1";
                        break;

                    case "_10":
                        level = " Lv. 2";
                        break;

                    case "_20":
                        level = " Lv. 3";
                        break;

                    case "_30":
                        level = " Lv. 4";
                        break;

                    case "_40":
                        level = " Lv. 5";//☆?
                        break;

                    default:
                        break;
                }

                writer.WriteLine($"| {record} | {name}{level} |");
            }

            Console.WriteLine($"Done parsing prefix '{PrefixLookup[prefix]}', written to {outputMdPath}");
        }
    }
}
