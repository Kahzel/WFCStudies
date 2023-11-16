using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;


namespace WaveFunctionCollapse
{
    public class PatternManager
    {
        private Dictionary<int, PatternData> dataDict;
        private Dictionary<int, PatternNeighbours> neighboursDict;

        private int _patternSize = -1;
        private IFindNeighbourStrategy strategy;

        public PatternManager(int size)
        {
            _patternSize = size;
        }

        public void ProcessGrid<T>(ValuesManager<T> valuesManager, bool equalWeights, string strategyName = null)
        {
            NeighbourStrategyFactory strategyFactory = new NeighbourStrategyFactory();
            strategy = strategyFactory.CreateInstance(strategyName == null ? _patternSize + "" : strategyName);
            CreatePatterns(valuesManager, strategy, equalWeights);
        }

        private void CreatePatterns<T>(ValuesManager<T> valuesManager, IFindNeighbourStrategy findNeighbourStrategy, bool equalWeights)
        {
            var finderResult = PatternFinder.GetPatternDataFromGrid(valuesManager, _patternSize, equalWeights);
            dataDict = finderResult.PatternIndexDictionary;
            GetNeighbours(finderResult, strategy);
        }

        private void GetNeighbours(PatternDataResults finderResult, IFindNeighbourStrategy findNeighbourStrategy)
        {
            neighboursDict = PatternFinder.FindNeighbours(findNeighbourStrategy, finderResult);
        }

        public PatternData GetPatternDataAt(int index)
        {
            return dataDict[index];
        }

        public HashSet<int> GetNeighboursForPatternAtDirection(int index, Direction dir)
        {
            return neighboursDict[index].AtDirection(dir);
        }

        public float GetPatternFrequency(int index)
        {
            return GetPatternDataAt(index).FrequencyRelative;
        }
        
        public float GetPatternFrequencyLog2(int index)
        {
            return GetPatternDataAt(index).FrequencyRelativeLog2;
        }

        public int GetPatternCount()
        {
            return dataDict.Count;
        }

        public int[][] ConvertPatternToValues<T>(int[][] outputvalues)
        {
            int patternOutputWidth = outputvalues[0].Length;
            int patternOutputHeight = outputvalues.Length;
            int valueGridWidth = patternOutputWidth + _patternSize - 1;
            int valueGridHeight = patternOutputHeight + _patternSize - 1;
            int[][] valueGrid = JaggedArrayFactory.CreateJaggedArray<int[][]>(valueGridHeight, valueGridWidth);

            for (int row = 0; row < patternOutputHeight; row++)
            {
                for (int col = 0; col < patternOutputWidth; col++)
                {
                    Pattern pattern = GetPatternDataAt(outputvalues[row][col]).pattern;

                    GetPatternValues(patternOutputWidth, patternOutputHeight, valueGrid, row, col, pattern);
                }
            }
            return valueGrid;
        }

        private void GetPatternValues(int patternOutputWidth, int patternOutputHeight, int[][] valueGrid, int row, int col, Pattern pattern)
        {
            if (row == patternOutputHeight - 1 && col == patternOutputWidth - 1)
            {
                for (int row_1 = 0; row_1 < _patternSize; row_1++)
                {
                    for (int col_1 = 0; col_1 < _patternSize; col_1++)
                    {
                        valueGrid[row + row_1][col + col_1] = pattern.GetGridVaue(col_1, row_1);
                    }
                }
            }
            else if (row == patternOutputHeight - 1)
            {
                for (int row_1 = 0; row_1 < _patternSize; row_1++)
                {
                    valueGrid[row + row_1][col] = pattern.GetGridVaue(0, row_1);
                }
            }
            else if (col == patternOutputWidth - 1)
            {
                for (int col_1 = 0; col_1 < _patternSize; col_1++)
                {
                    valueGrid[row][col + col_1] = pattern.GetGridVaue(col_1, 0);
                } 
            }
            else
            {
                valueGrid[row][col] = pattern.GetGridVaue(0, 0);
            }
        }
    }

}
