﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse
{
    public class TileMapOutput : IOutputCreator<Tilemap>
    {
        private Tilemap outputImage;
        private ValuesManager<TileBase> valueManager;
        public Tilemap OutputImage => outputImage;

        public TileMapOutput(ValuesManager<TileBase> valueManager, Tilemap outputImage)
        {
            this.outputImage = outputImage;
            this.valueManager = valueManager;
        }

        public void CreateOutput(PatternManager manager, int[][] outputvalues, int width, int height)
        {
            if(outputvalues.Length == 0)
            {
                return;
            }
            outputImage.ClearAllTiles();

            int[][] valueGrid;
            valueGrid = manager.ConvertPatternToValues<TileBase>(outputvalues);

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    TileBase tile = valueManager.GetValueFromIndex(valueGrid[row][col]).value;
                    outputImage.SetTile(new Vector3Int(col, row, 0), tile);
                }
            }
        }
    }

}