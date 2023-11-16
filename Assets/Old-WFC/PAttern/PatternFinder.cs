using System;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public static class PatternFinder
    {
        public static PatternDataResults GetPatternDataFromGrid<T>(ValuesManager<T> valuesManager, int patternSize, bool equalWeights)
        {
            Dictionary<string, PatternData> patternHashDatas = new Dictionary<string, PatternData>();
            Dictionary<int, PatternData> patternIndexDatas = new Dictionary<int, PatternData>();
            Vector2 gridSize = valuesManager.GetGridSize();
            int patternGridSizeX = 0;
            int patternGridSizeY = 0;
            int rowMin = -1, colMin = -1, rowMax =-1, colMax =-1;
            
            if (patternSize < 3)
            {
                patternGridSizeX = (int)gridSize.x + 3 - patternSize;
                patternGridSizeY = (int)gridSize.y + 3 - patternSize;
                rowMax = patternGridSizeY - 1;
                colMax = patternGridSizeX - 1;
            }
            else
            {
                patternGridSizeX = (int)gridSize.x + patternSize - 1;
                patternGridSizeY = (int)gridSize.y + patternSize - 1;
                rowMin = 1 - patternSize;
                colMin = 1 - patternSize;
                rowMax = (int)gridSize.y;
                colMax = (int)gridSize.x;
            }

            int[][] patternIndices = JaggedArrayFactory.CreateJaggedArray<int[][]>(patternGridSizeY, patternGridSizeX);
            int totalFrequency = 0;
            int patternIndex = 0;

            for (int row = rowMin; row < rowMax; row++)
            {
                for (int col = colMin; col < colMax; col++)
                {
                    int[][] gridValues = valuesManager.GetPatternValuesAtPosition(col, row, patternSize);
                    string hashValue = HashCodeCalculator.CalculateHashCode(gridValues);

                    if (!patternHashDatas.ContainsKey(hashValue))
                    {
                        Pattern pattern = new Pattern(gridValues, hashValue, patternIndex);
                        patternIndex++;
                        AddNewPattern(patternHashDatas, patternIndexDatas, hashValue, pattern);
                    }
                    else
                    {
                        if (!equalWeights)
                        {
                            patternIndexDatas[patternHashDatas[hashValue].pattern.Index].AddToFrequency();
                        }
                    }
                    totalFrequency++;
                    if (patternSize < 3)
                    {
                        patternIndices[row + 1][col + 1] = patternHashDatas[hashValue].pattern.Index;
                    }
                    else
                    {
                        patternIndices[row + patternSize - 1][col + patternSize - 1] = patternHashDatas[hashValue].pattern.Index;
                    }
                }
            }

            CalculateRelativeFrequency(patternIndexDatas, totalFrequency);
            return new PatternDataResults(patternIndices, patternIndexDatas);
        }

        private static void CalculateRelativeFrequency(Dictionary<int, PatternData> patternIndexDatas, int totalFrequency)
        {
            foreach (var pattern in patternIndexDatas.Values)
            {
                pattern.CalculateRelativeFrequency(totalFrequency);                
            }
        }

        private static void AddNewPattern(Dictionary<string, PatternData> patternHashDatas, Dictionary<int, PatternData> patternIndexDatas, string hashValue, Pattern pattern)
        {
            PatternData data = new PatternData(pattern);
            patternHashDatas.Add(hashValue, data);
            patternIndexDatas.Add(pattern.Index, data);
        }

        public static Dictionary<int, PatternNeighbours> FindNeighbours(IFindNeighbourStrategy findNeighbourStrategy, PatternDataResults finderResult)
        {
            return findNeighbourStrategy.FindNeighbours(finderResult);
        }
        
        public static PatternNeighbours CheckNeighboursInEachDirection(int x, int y, PatternDataResults patterndataResults)
        {
            PatternNeighbours neighbours = new PatternNeighbours();
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                int possiblePatternIndex = patterndataResults.GetNeighbourAtDirection(x, y, dir);
                if (possiblePatternIndex >= 0)
                {
                    neighbours.AddPatternToDictionary(dir, possiblePatternIndex);
                }
            }
            return neighbours;
        }
        
        public static void AddNeighboursToDictionary(Dictionary<int, PatternNeighbours> dictionary, int patternIndex, PatternNeighbours neighbours)
        {
            if (dictionary.ContainsKey(patternIndex) == false)
            {

                dictionary.Add(patternIndex, neighbours);

            }
            dictionary[patternIndex].AddNeighbour(neighbours);

        }
    }
}