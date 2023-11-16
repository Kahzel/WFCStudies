using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse
{
    public class InputImageParameters
    {
        private Vector2Int? bottomRightCoords = null;
        private Vector2Int? topLeftCoords = null;
        private BoundsInt tilemapBounds;
        private TileBase[] tiles;
        public Queue<TileContainer> tileQueue = new Queue<TileContainer>();
        private int width = 0, height = 0;
        private Tilemap tilemap;
        
        public int Height
        {
            get => height;
        }
        public int Width
        {
            get => width;
        }

        public InputImageParameters(Tilemap inputTilemap)
        {
            this.tilemap = inputTilemap;
            this.tilemapBounds = this.tilemap.cellBounds;
            this.tiles = this.tilemap.GetTilesBlock(this.tilemapBounds);
            ExtractTiles();
            VerifyTiles();

        }

        private void VerifyTiles()
        {
            if (topLeftCoords == null || bottomRightCoords == null)
            {
                throw new InvalidDataException(String.Format("[WFC] Tried to generate from an empty Tilemap {0}", tilemap.name));
            }

            var minX = bottomRightCoords.Value.x;
            var maxX = topLeftCoords.Value.x;
            var minY = bottomRightCoords.Value.y;
            var maxY = topLeftCoords.Value.y;

            width = Math.Abs(maxX - minX) + 1;
            height = Math.Abs(maxY - minY) + 1;

            var expectedTiles = width * height;
            if (tileQueue.Count != expectedTiles)
            {
                throw new InvalidDataException(String.Format("[WFC] Tilemap {0} contains empty tiles", tilemap.name));
            }

            if (tileQueue.Any(tile => tile.x < minX || tile.x > maxX || tile.y < minY || tile.y > maxY))
            {
                throw new InvalidDataException(String.Format("[WFC] Tilemap {0} contains out of bounds tiles", tilemap.name));
            }
        }

        private void ExtractTiles()
        {
            for (int row = 0; row < tilemapBounds.size.y; row++)
            {
                for (int col = 0; col < tilemapBounds.size.x; col++)
                {
                    int index = col + (row * tilemapBounds.size.x);
                    TileBase tile = tiles[index];
                    if (bottomRightCoords == null && tile != null)
                    {
                        bottomRightCoords = new Vector2Int(col, row);
                    }

                    if (tile != null) // Remove this condition if i need to include empty tiles into the patterns later on
                    {
                        tileQueue.Enqueue(new TileContainer(tile, col, row));
                        topLeftCoords = new Vector2Int(col, row);
                    }
                }
            }
        }
    }
}