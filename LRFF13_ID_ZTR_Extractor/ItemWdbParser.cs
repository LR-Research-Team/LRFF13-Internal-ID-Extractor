using System.Text.Json;

namespace LRFF13_ID_ZTR_Extractor
{
    internal sealed record ItemRecord(string Id, string Name);

    internal class ItemWdbParser
    {
        private readonly Dictionary<string, string> PrefixLookup = new() {
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

        internal IReadOnlyList<ItemRecord> GetItems(string prefix)
        {
            var itemJson = JsonDocument.Parse(File.ReadAllText(ItemWdbPath));

            var ztrLookup = new Dictionary<string, string>();
            foreach (var line in File.ReadLines(TxtresPath))
            {
                if (!line.Contains("|:|"))
                    continue;

                var parts = line.Split("|:|", 2, StringSplitOptions.TrimEntries);
                if (parts.Length != 2 || parts[0].EndsWith("h"))
                    continue;

                ztrLookup[parts[0]] = parts[1];
            }

            var items = new List<ItemRecord>();
            foreach (var item in itemJson.RootElement.GetProperty("records").EnumerateArray())
            {
                string record = item.GetProperty("record").GetString() ?? "";
                if (!record.StartsWith(prefix) ||
                    (prefix == "ac" && record.StartsWith("acc")) ||
                    (prefix == "ma" && record.StartsWith("mat")))
                {
                    continue;
                }

                string name = "N/A";
                string nameId = item.GetProperty("sItemNameStringId").GetString() ?? "";
                if (!string.IsNullOrWhiteSpace(nameId) && ztrLookup.TryGetValue(nameId, out string? txtName))
                {
                    int descriptionStart = txtName.IndexOf('{');
                    name = descriptionStart >= 0 ? txtName[..descriptionStart] : txtName;
                }

                string level = record.Length >= 3
                    ? record[^3..] switch
                    {
                        "_00" => " Lv. 1",
                        "_10" => " Lv. 2",
                        "_20" => " Lv. 3",
                        "_30" => " Lv. 4",
                        "_40" => " Lv. 5",
                        _ => ""
                    }
                    : "";

                items.Add(new ItemRecord(record, name + level));
            }

            return items;
        }

        internal string GetCategoryName(string prefix) => PrefixLookup[prefix];
    }
}
