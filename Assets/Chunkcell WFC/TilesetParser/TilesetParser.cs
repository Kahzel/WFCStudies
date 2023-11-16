using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Chunkcell_WFC.TilesetParser
{
    public class TilesetParser
    {
        public static void ParseTileset(string tileset)
        {
            Dictionary<string, dynamic> tilesetDict = new();
            FileStream stream = new FileStream("/Assets/Chunkcell WFC/Tilesets"+tileset+".xml", FileMode.Open);
            XDocument xmlDoc = XDocument.Load(stream);

            XElement tilesetElem = xmlDoc.Element("Tileset");
            
            //TODO Finish this: build chunks and its neighbors, and build Tileset

        }
    }
}