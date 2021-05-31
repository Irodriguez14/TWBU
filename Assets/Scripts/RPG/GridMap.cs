using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class GridMap<TGridObject> { 

    private int width;
    private int height;
    private float cellSize;
    private TGridObject[,] gridArray;
    private Vector3 originPosition;
    private TextMesh[,] debugTextArray;

    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs {
        public int x;
        public int y;
    }

    public GridMap(int width, int height, float cellSize, Vector3 origin, Func<GridMap<TGridObject>, int, int, TGridObject> holo) {

        this.height = height;
        this.width = width;
        this.originPosition = origin;
        this.cellSize = cellSize;

        gridArray = new TGridObject[width,height];
        debugTextArray = new TextMesh[width,height];

        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++) {
                debugTextArray[x,y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 40, Color.black, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x,y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width,height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        
    }

    public int GetWidth() {
        return width;
    }

    public int GetHeight() {
        return height;
    }


    private Vector3 GetWorldPosition(int x, int y) { return new Vector3(x, y) * cellSize + originPosition; }

    public void GetXY(Vector3 position, out int x, out int y) {
        x = Mathf.FloorToInt((position - originPosition).x / cellSize);
        y = Mathf.FloorToInt((position - originPosition).y / cellSize);
    }

    public void SetValue(int x, int y, TGridObject value) {
        if (x >= 0 && y >= 0 && x < width && y < height) {
            gridArray[x, y] = value;
            debugTextArray[x,y].text = gridArray[x, y].ToString();
        }
        
    }

    public void SetValue(Vector3 position, TGridObject value) {
        int x, y;
        GetXY(position, out x, out y);
        SetValue(x, y, value);
    }

    public TGridObject GetValue(int x, int y) {
        if (x >= 0 && y >= 0 && x < width && y < height) {
            return gridArray[x, y];
        }
        else {
            return default(TGridObject);
        }
    }

    public void TriggerGridObjectChanged(int x, int y){
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
    }

    public TGridObject GetGridObject(int x, int y) {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition) {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }

    public TGridObject GetValue(Vector3 position)
    {
        int x, y;
        GetXY(position, out x, out y);
        return GetValue(x, y);
    }
}

