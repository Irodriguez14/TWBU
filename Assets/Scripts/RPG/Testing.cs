using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{

    GridMap<PathNode> grid;

    private Pathfinding pathfinding;

    // Start is called before the first frame update
    void Start() {
        grid = new GridMap<PathNode>(4, 2, 10f, new Vector3(10,0),(GridMap < PathNode > g, int x, int y) => new PathNode(g, x, y));

        pathfinding = new Pathfinding(20,10);
    }

    private void Update(){
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);

            if (path != null) {
                for (int i = 0; i < path.Count - 1; i++) {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f,Color.green);
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x,y).isWalkable);
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }

    }

}


