using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

[ExecuteInEditMode]
public class LineRenderer3D : MonoBehaviour
{
    GameObject[] target;
    List<Vector3> path;
    LineRenderer renderer;
    public void Awake()
    {
        path = new List<Vector3>();
        renderer = this.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        target = GameObject.FindGameObjectsWithTag("target");
        renderer.positionCount = target.Length * 2;
        foreach(GameObject obj in target)
        {
            if (obj.transform.hasChanged)
            {
                for (int i = 0; i < target.Length; i++)
                {
                    if (i < target.Length - 1)
                    {
                        drawLine(target[i].transform.position, target[i + 1].transform.position);
                    }
                    else
                    {
                        drawLine(target[i].transform.position, target[0].transform.position);
                    }
                }
                path.Clear();
            }
        }
    }

    public void drawLine(Vector3 from, Vector3 to)
    {
        path.Add(from);
        path.Add(to);
        renderer.SetPositions(path.ToArray());
    }
}
