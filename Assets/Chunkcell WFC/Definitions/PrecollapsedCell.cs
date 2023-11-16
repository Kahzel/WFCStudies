using System.Linq;

namespace Chunkcell_WFC.Definitions
{
    //TODO Rewrite this with cardinalities and symmetries and whatnot
    public class PrecollapsedCell : ChunkCell
    {
        public PrecollapsedCell(int offsetX, int offsetY, Chunk[] intialChunks) : base(offsetX, offsetY, intialChunks)
        {
            IsCollapsed = true;
            if (Posibilities.Length > 1)
            {
                Posibilities = Posibilities.Take(1).ToArray(); // Force posibilities to be 1, since it's precollapsed
            }
        }
    }
}