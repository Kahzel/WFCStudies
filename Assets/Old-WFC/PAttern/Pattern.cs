using System;
using System.Collections.Generic;

namespace WaveFunctionCollapse
{
    public class Pattern
    {
        private int _index;
        private int[][] _grid;
        
        public int Index
        {
            get => _index;
        }

        public string Hash
        {
            get;
            set;
        }

        public Pattern(int[][] grid, string hash, int index)
        {
            _grid = grid;
            _index = index;
            Hash = hash;
        }

        public void SetGridValue(int x, int y, int value)
        {
            _grid[y][x] = value;
        }

        public int GetGridVaue(int x, int y)
        {
            return _grid[y][x];
        }

        public bool CheckGridValue(int x, int y, int value)
        {
            return value.Equals(_grid[y][x]);
        }

        internal bool ComparePatterns(Direction dir, Pattern pattern)
        {
            int[][] ownDirectionGrid = GetGridValuesAtDirection(dir);
            int[][] otherDirectionGrid = pattern.GetGridValuesAtDirection(dir.GetOpposite());

            for (int row = 0; row < ownDirectionGrid.Length; row++)
            {
                for (int col = 0; col < ownDirectionGrid[0].Length; col++)
                {
                    if (ownDirectionGrid[row][col] != otherDirectionGrid[row][col])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private int[][] GetGridValuesAtDirection(Direction dir)
        {
            int[][] gridValues;

            switch (dir)
            {
                case Direction.Up:
                    gridValues = JaggedArrayFactory.CreateJaggedArray<int[][]>(_grid.Length - 1, _grid.Length);
                    PopulateDirectionGrid(0, _grid.Length, 1, _grid.Length, gridValues);
                    break;
                case Direction.Down:
                    gridValues = JaggedArrayFactory.CreateJaggedArray<int[][]>(_grid.Length - 1, _grid.Length);
                    PopulateDirectionGrid(0, _grid.Length, 0, _grid.Length - 1, gridValues);
                    break;
                case Direction.Left:
                    gridValues = JaggedArrayFactory.CreateJaggedArray<int[][]>(_grid.Length, _grid.Length - 1);
                    PopulateDirectionGrid(0, _grid.Length - 1, 0, _grid.Length, gridValues);
                    break;
                case Direction.Right:
                    gridValues = JaggedArrayFactory.CreateJaggedArray<int[][]>(_grid.Length, _grid.Length - 1);
                    PopulateDirectionGrid(1, _grid.Length, 0, _grid.Length, gridValues);
                    break;
                default:
                    return _grid;
            }

            return gridValues;
        }

        private void PopulateDirectionGrid(int xmin, int xmax, int ymin, int ymax, int[][] gridValues)
        {
            List<int> tempList = new List<int>();
            for (int row = ymin; row < ymax; row++)
            {
                for (int col = xmin; col < xmax; col++)
                {
                    tempList.Add(_grid[row][col]);
                }
            }

            for (int i = 0; i < tempList.Count; i++)
            {
                int x = i % gridValues.Length;
                int y = i / gridValues.Length;
                gridValues[x][y] = tempList[i];
            }
        }
    }
}
