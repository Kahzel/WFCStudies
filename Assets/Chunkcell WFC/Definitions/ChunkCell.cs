using System.Linq;
using UnityEngine;

namespace Chunkcell_WFC.Definitions
{
    //TODO Rewrite this with cardinalities and symmetries and whatnot
    public class ChunkCell : MonoBehaviour
    {
        public Chunk[] Posibilities;
        public int OffsetX;
        public int OffsetY;
        public bool IsCollapsed;

        public ChunkCell(int offsetX, int offsetY, Chunk[] intialChunks)
        {
            Posibilities = intialChunks;
            OffsetY = offsetY;
            OffsetX = offsetX;
            IsCollapsed = false;
        }

        public void OnPropagationFromDirection(Direction direction, Chunk collapsedChunk)
        {
            //TODO Proper implementation once i'm done with the propagation step in the solver
            int previousPosibilitySize = Posibilities.Length; 
            Posibilities = Posibilities.Where(chunk => chunk.GetNeighborsForDirection(direction).Contains(collapsedChunk)).ToArray();
        }

    }
}