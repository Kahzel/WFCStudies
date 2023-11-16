using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public class PatternNeighbours
    {
        public Dictionary<Direction, HashSet<int>> neighboursInDirection = new Dictionary<Direction, HashSet<int>>();

        public void AddPatternToDictionary(Direction dir, int index)
        {
            if (neighboursInDirection.ContainsKey(dir))
            {
                neighboursInDirection[dir].Add(index);
            }
            else
            {
                neighboursInDirection.Add(dir, new HashSet<int>() { index });
            }
        }

        public HashSet<int> AtDirection(Direction dir)
        {
            if (neighboursInDirection.ContainsKey(dir))
            {
                return neighboursInDirection[dir];
            }

            return new HashSet<int>();
        }

        public void AddNeighbour(PatternNeighbours neighbours)
        {
            foreach (var neighbour in neighbours.neighboursInDirection)
            {
                if (!neighboursInDirection.ContainsKey(neighbour.Key))
                {
                    neighboursInDirection.Add(neighbour.Key, new HashSet<int>());
                }
                neighboursInDirection[neighbour.Key].UnionWith(neighbour.Value);
            }
        }
    }
}
