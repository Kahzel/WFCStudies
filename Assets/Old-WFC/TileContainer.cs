using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse
{
    public class TileContainer
    {
        public TileBase tile;
        public int x, y;

        public TileContainer(TileBase tile, int x, int y)
        {
            this.tile = tile;
            this.x = x;
            this.y = y;
        }
    }
}