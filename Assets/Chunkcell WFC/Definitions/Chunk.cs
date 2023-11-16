using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Chunkcell_WFC.Definitions
{
    public class Chunk : MonoBehaviour
    {
        public int[][] BasePattern;
        public SymmetryType Symmetry;
        public Dictionary<int, TileBase> tileDict;
        private int _cellSize;
        //TODO add cardinality to this
        public Dictionary<Direction, Chunk[]> Neighbors = new()
        {
            {Direction.Left, new Chunk[] {}},
            {Direction.Right, new Chunk[] {}},
            {Direction.Up, new Chunk[] {}},
            {Direction.Down, new Chunk[] {}},
        };


        public Chunk(int[][] basePattern, int cellSize = Constants.DEFAULT_CHUNK_SIZE)
        {
            BasePattern = basePattern;
            _cellSize = cellSize;
        }

        public TileBase[] GetChunkTiles()
        {
            List<TileBase> tiles = new();
            for (int row = 0; row < _cellSize; row++)
            {
                for (int col = 0; col < _cellSize; col++)
                {
                    tiles.Add(tileDict[BasePattern[row][col]]);
                }
            }
            return tiles.ToArray();
        }

        public Chunk[] GetNeighborsForDirection(Direction direction)
        {
            //TODO add cardinality to this
            return Neighbors[direction];
        }
        
    }
}