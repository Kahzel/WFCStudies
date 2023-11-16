using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public class ValuesManager<T>
    {
        private int[][] _grid;
        private Dictionary<int, IValue<T>> valueIndexDict = new Dictionary<int, IValue<T>>();
        private int index = 0;

        public ValuesManager(IValue<T>[][] grid)
        {
            CreateGridIndices(grid);
        }

        private void CreateGridIndices(IValue<T>[][] valuesGrid)
        {
            _grid = JaggedArrayFactory.CreateJaggedArray<int[][]>(valuesGrid.Length, valuesGrid[0].Length);
            for (int row = 0; row < valuesGrid.Length; row++)
            {
                for (int col = 0; col < valuesGrid[0].Length; col++)
                {
                    SetGridPosIndex(valuesGrid, row, col);
                }
            }
        }

        private void SetGridPosIndex(IValue<T>[][] grid, int row, int col)
        {
            if (valueIndexDict.ContainsValue(grid[row][col]))
            {
                var key = valueIndexDict.FirstOrDefault(x => x.Value.Equals(grid[row][col]));
                _grid[row][col] = key.Key;
            }
            else
            {
                _grid[row][col] = index;
                valueIndexDict.Add(_grid[row][col], grid[row][col]);
                index++;
            }
        }

        public int GetGridValue(int x, int y)
        {
            if (x >= _grid[0].Length || y >= _grid.Length || x < 0 || y < 0)
            {
                throw new IndexOutOfRangeException(String.Format("[WFC] ({0},{1}) is out of bounds", x, y));
            }

            return _grid[y][x];
        }

        public IValue<T> GetValueFromIndex(int index)
        {
            if (valueIndexDict.ContainsKey(index))
            {
                return valueIndexDict[index];
            }
            else
            {
                throw new IndexOutOfRangeException(String.Format("[WFC] Index {0} not in ValuesManager dictionary",
                    index));
            }
        }

        public int GetGridValueWithOffset(int x, int y)
        {
            int yMax = _grid.Length;
            int xMax = _grid[0].Length;
            if (x < 0 && y < 0)
            {
                return GetGridValue(xMax + x, yMax + y);
            }
            if (x <0  && y >= yMax)
            {
                return GetGridValue(xMax + x, y - yMax);
            }
            if (x >= xMax && y < 0)
            {
                return GetGridValue(x - xMax, yMax + y);
            }
            if (x >= xMax && y >= yMax)
            {
                return GetGridValue(x - xMax, y - yMax);
            }

            if (x < 0)
            {
                return GetGridValue(xMax + x, y);
            }
            if (x >= xMax)
            {
                return GetGridValue(x - xMax, y);
            }
            if (y < 0)
            {
                return GetGridValue(x, yMax + y);
            }

            if (y >= yMax)
            {
                return GetGridValue(x, y - yMax);
            }
            return GetGridValue(x, y);
        }

        public int[][] GetPatternValuesAtPosition(int x, int y, int size)
        {
            int[][] patternArray = JaggedArrayFactory.CreateJaggedArray<int[][]>(size, size);
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    patternArray[row][col] = GetGridValueWithOffset(x + col, y + row);
                }
            }

            return patternArray;
        }

        public Vector2 GetGridSize()
        {
            if (_grid == null)
            {
                return Vector2.zero;
            }

            return new Vector2(_grid[0].Length, _grid.Length);
        }
    }
}
