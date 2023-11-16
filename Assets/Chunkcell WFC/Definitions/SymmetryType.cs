using Chunkcell_WFC.Definitions;
using UnityEngine;

namespace Chunkcell_WFC.Definitions
{
    public abstract class SymmetryType
    {
        public int Cardinality;
        protected int CellSize;
        
        public SymmetryType(int cellSize)
        {
            CellSize = cellSize;
        }
        public abstract int[][] GetCardinal(int[][] pattern, int cardinal);
        public abstract Vector3Int GetTransformVector(int cardinal);
    }
}


public class XSymmetry : SymmetryType
{
    public new const int Cardinality = 1;
    
    public XSymmetry(int cellSize) : base(cellSize) {}

    public override int[][] GetCardinal(int[][] pattern, int cardinal)
    {
        return pattern;
    }

    public override Vector3Int GetTransformVector(int cardinal)
    {
        return new Vector3Int(0, 0, 0);
    }
}


public class TSymmetry: SymmetryType
{
    public new const int Cardinality = 8;

    public TSymmetry(int cellSize) : base(cellSize) {}
    
    public override int[][] GetCardinal(int[][] pattern, int cardinal)
    {
        if (cardinal is < Cardinality and > 0)
        {
            int[][] newPattern = JaggedArrayFactory.CreateJaggedArray<int[][]>(CellSize, CellSize);
            switch (cardinal)
            {
                case 1:
                    newPattern[0][0] = pattern[0][1];
                    newPattern[0][1] = pattern[1][1];
                    newPattern[1][0] = pattern[0][0];
                    newPattern[1][1] = pattern[1][0];
                    break;
                case var n when n is 2 or 5:
                    newPattern[0][0] = pattern[1][1];
                    newPattern[0][1] = pattern[1][0];
                    newPattern[1][0] = pattern[0][1];
                    newPattern[1][1] = pattern[0][0];
                    break;
                case 3:
                    newPattern[0][0] = pattern[1][0];
                    newPattern[0][1] = pattern[0][0];
                    newPattern[1][0] = pattern[1][1];
                    newPattern[1][1] = pattern[0][1];
                    break;
                case 4:
                    return pattern;
                case 6:
                    newPattern[0][0] = pattern[1][1];
                    newPattern[0][1] = pattern[0][1];
                    newPattern[1][0] = pattern[1][0];
                    newPattern[1][1] = pattern[0][0];
                    break;
                case 7:
                    newPattern[0][0] = pattern[0][0];
                    newPattern[0][1] = pattern[1][0];
                    newPattern[1][0] = pattern[0][1];
                    newPattern[1][1] = pattern[1][1];
                    break;
            }
            return newPattern;
        }
        return pattern;
    }

    public override Vector3Int GetTransformVector(int cardinal)
    {
        if (cardinal is < Cardinality and > 0)
        {
            switch (cardinal)
            {
                case 1:
                    return new Vector3Int(0, 0, 90);
                case var n when n is 2 or 5:
                    return new Vector3Int(0, 0, 180);
                case 3:
                    return new Vector3Int(0, 0, 270);
                case 4:
                    return new Vector3Int(0, 0, 0);
                case 6:
                    return new Vector3Int(0, 180, 90);
                case 7:
                    return new Vector3Int(0, 180, 270);
            }
        }

        return new Vector3Int(0, 0, 0);
    }
}

public class ISymmetry : SymmetryType
{
    public new const int Cardinality = 2;
    
    public ISymmetry(int cellSize) : base(cellSize) {}
    
    public override int[][] GetCardinal(int[][] pattern, int cardinal)
    {
        switch (cardinal)
        {
            case int n when n % 2 == 0:
                return pattern;
            default:
                int[][] newPattern = JaggedArrayFactory.CreateJaggedArray<int[][]>(CellSize, CellSize);
                newPattern[0][0] = newPattern[1][0];
                newPattern[0][1] = newPattern[1][1];
                newPattern[1][0] = newPattern[0][0];
                newPattern[1][1] = newPattern[0][1];
                return newPattern;
        }
    }

    public override Vector3Int GetTransformVector(int cardinal)
    {
        return cardinal % 2 == 0 ? new Vector3Int(0, 0, 0) : new Vector3Int(0, 0, 90);
    }
}

public class SlashSymmetry : SymmetryType
{
    public new const int Cardinality = 8;
    
    public SlashSymmetry(int cellSize) : base(cellSize) {}

    public override int[][] GetCardinal(int[][] pattern, int cardinal)
    {
        if (cardinal is 0 or 2 or 5 or 7)
        {
            return pattern;
        }
        int[][] newPattern = JaggedArrayFactory.CreateJaggedArray<int[][]>(CellSize, CellSize);
        newPattern[0][0] = pattern[0][1];
        newPattern[0][1] = pattern[0][0];
        newPattern[1][0] = pattern[1][1];
        newPattern[1][1] = pattern[1][0];
        
        return newPattern;
    }

    public override Vector3Int GetTransformVector(int cardinal)
    {
        return cardinal is 0 or 2 or 5 or 7 ? new Vector3Int(0, 0, 0) : new Vector3Int(0, 0, 90);
    }
}
//TODO L and F symmetries zzzz