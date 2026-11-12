# LRFF13_ID_ZTR_Extractor

**LRFF13_Internal_ID_Extractor** is a C# utility for extracting and mapping internal IDs to display names for Lightning Returns: Final Fantasy XIII. 
It parses game data (wtb and ztr files, exported to json and txt) and exports the results as Markdown or CSV tables for easy reference and modding.

## Features

- Parses `item.json` (export of `item.wdb`) for item and ability records.
- Maps internal IDs to display names using `txtres_us.txt` (export of `txtres_us.ztr`).
- Outputs clean, readable Markdown or CSV tables.
- Handles special cases for item prefixes and ability levels.

## Usage

1. **Build the project** in Visual Studio (.NET 8 required).
2. **Prepare the following files** (you can export these files using `Nova Chrystallia` from your game installation files):
   - `item.json` located in `weiss_data\db\resident\` (exported from `item.wdb`).
   - `txtres_us.txt` located in `weiss_data\txtres\resident\system\` (exported from `txtres_us.ztr`).
3. **Run the application** with the required arguments:

   LRFF13_ID_ZTR_Extractor.exe <item.json> <txtres_us.txt> <outputpath>
   LRFF13_ID_ZTR_Extractor.exe "C:\Path\item.json" "C:\Path\txtres_us.txt" "C:\path\output\"

   Output files will be generated in the specified locations (see `Program.Main` for details).

   **Example:**
   `LRFF13_ID_ZTR_Extractor.exe "C:\Program Files (x86)\Steam\steamapps\common\LIGHTNING RETURNS FINAL FANTASY XIII\weiss_data\db\resident\item.json" "C:\Program Files (x86)\Steam\steamapps\common\LIGHTNING RETURNS FINAL FANTASY XIII\weiss_data\txtres\resident\system\txtres_us.txt" "c:\temp\IdOutput"`

## Output

- Markdown and CSV tables mapping:
  - Internal record IDs
  - ZTR string IDs
  - Human-readable names (with ability levels where applicable)

## Project Structure

- `Program.cs`: Entry point; sets up file paths and runs parsers.
- `ItemWdbParser.cs`: Parses item data into reusable item records.
- `MarkdownParser.cs`: Exports item records as Markdown tables.
- `CsvParser.cs`: Exports the same item data as escaped CSV tables.

## Requirements

- .NET 8 SDK
- Visual Studio 2022 or later