using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse{
    public class WFCCore
    {
        OutputGrid outputGrid;
        PatternManager patternManager;

        private int maxIterations = 0;

        public WFCCore(int outputWidth, int outputHeight, int maxIterations, PatternManager patternManager)
        {
            outputGrid = new OutputGrid(outputWidth, outputHeight, patternManager.GetPatternCount());
            this.patternManager = patternManager;
            this.maxIterations = maxIterations;
        }

        public int[][] CreateOutputGrid()
        {
            int iteration = 0;
            while(iteration < this.maxIterations)
            {
                Debug.Log("Trying iteration " + iteration);
                CoreSolver solver = new CoreSolver(this.outputGrid, this.patternManager);
                int innerIteration = 1000;
                while(!solver.CheckForConflicts() && !solver.CheckIfSolved())
                {
                    Vector2Int position = solver.GetLowestEntropyCell();
                    solver.CollapseCell(position);
                    solver.Propagate();
                    innerIteration--;
                    if(innerIteration <= 0)
                    {
                        Debug.Log("Propagation is taking too long, cancelling WFC.");
                        return new int[0][];
                    }
                }
                if (solver.CheckForConflicts())
                {
                    Debug.Log("\n Conflict occured. Iteration: " + iteration);
                    iteration++;
                    outputGrid.ResetAllPossibilities();
                    solver = new CoreSolver(this.outputGrid, this.patternManager);
                }
                else
                {
                    Debug.Log("Solved on: " + iteration);
                    this.outputGrid.PrintResultsToConsole();
                    break;
                }
            }
            if(iteration>= this.maxIterations)
            {
                Debug.Log("Could not solve the tilemap");
            }
            return outputGrid.GetSolvedOutputGrid();
        }
    }

}