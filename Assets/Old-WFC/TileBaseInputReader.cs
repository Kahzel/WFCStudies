using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse
{
    public class TileBaseInputReader : IInputReader<TileBase>
    {
        private Tilemap _inputTilemap;

        public TileBaseInputReader(Tilemap input)
        {
            _inputTilemap = input;
        }

        public TileBase[][] ReadInputTileMap()
        {
            InputImageParameters imageParameters = new InputImageParameters(_inputTilemap);
            return CreateTileBaseGrid(imageParameters);
        }

        private TileBase[][] CreateTileBaseGrid(InputImageParameters imageParameters)
        {
            TileBase[][] grid =
                JaggedArrayFactory.CreateJaggedArray<TileBase[][]>(imageParameters.Height, imageParameters.Width);

            for (int row = 0; row < imageParameters.Height; row++)
            {
                for (int col = 0; col < imageParameters.Width; col++)
                {
                    grid[row][col] = imageParameters.tileQueue.Dequeue().tile;
                }
            }

            return grid;
        }

        public IValue<TileBase>[][] ReadInputToGrid()
        {
            var grid = ReadInputTileMap();
            TileBaseValue[][] valueGrid = null;
            if (grid != null)
            {
                valueGrid = JaggedArrayFactory.CreateJaggedArray<TileBaseValue[][]>(grid.Length, grid[0].Length);
                for (int row = 0; row < grid.Length; row++)
                {
                    for (int col = 0; col < grid[0].Length; col++)
                    {
                        valueGrid[row][col] = new TileBaseValue(grid[row][col]);
                    }
                }
            }
            return valueGrid;
        }
    }
}
