using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using WaveFunctionCollapse;

public class Test : MonoBehaviour
{
    public Tilemap input;
    public Tilemap output;
    public int patternSize;
    public int maxIterations = 500;
    public int outputWidth = 9;
    public int outputHeight = 9;
    public bool equalWeights = false;
    private ValuesManager<TileBase> valuesManager;
    private WFCCore core;
    private PatternManager manager;
    private TileMapOutput tileMapOutput;

    // Start is called before the first frame update
    void Start()
    {
        CreateWFC();
        CreateTilemap();
    }

    public void CreateWFC()
    {
        TileBaseInputReader reader = new TileBaseInputReader(input);
        var grid = reader.ReadInputToGrid();
        valuesManager = new ValuesManager<TileBase>(grid);
        for (int row = 0; row < grid.Length; row++)
        {
            for (int col = 0; col < grid[0].Length; col++)
            {
                Debug.Log(string.Format("Index {0}: {1}",grid[row][col].value, valuesManager.GetGridValue(col, row)));
            }
        }
        List<String> list = new List<String>();
        manager = new PatternManager(2);
        manager.ProcessGrid(valuesManager,equalWeights);
        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            Debug.Log(dir.ToString() + ": " + string.Join(" ", manager.GetNeighboursForPatternAtDirection(0, dir).ToArray()));
        }
        Debug.Log("SOLVER TIME!");
        core = new WFCCore(outputWidth, outputHeight, maxIterations, manager);
    }

    public void CreateTilemap()
    {
        var result = core.CreateOutputGrid();
        tileMapOutput = new TileMapOutput(valuesManager, output);
        tileMapOutput.CreateOutput(manager, result, outputWidth, outputWidth);
    }

    public void SaveTilemap()
    {
        if (tileMapOutput.OutputImage != null)
        {
            output = tileMapOutput.OutputImage;
            GameObject newTilemap = output.gameObject;

            PrefabUtility.SaveAsPrefabAsset(newTilemap, "Assets/WFC/Exports/Tilemap.prefab");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
