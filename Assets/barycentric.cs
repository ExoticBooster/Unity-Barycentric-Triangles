using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.ExceptionServices;
using UnityEditor.PackageManager;
using UnityEngine;

[ExecuteInEditMode]
public class barycentric : MonoBehaviour
{
    public GameObject checkPoint;
    private GameObject[] targets;
    private List<Vector2> positions;
    // Update is called once per frame

   
    void Update()
    {
        targets = GameObject.FindGameObjectsWithTag("target");
        positions = new List<Vector2>();
        foreach (GameObject target in targets)
        {
            Vector3 position = target.transform.position;
            positions.Add(new Vector2(position.x, position.z));
        }
        Vector2 ballPosition = new Vector2(checkPoint.transform.position.x, checkPoint.transform.position.z);
        bool currently = checkIfInField(positions.ToArray(), ballPosition);
        UnityEngine.Color currentColor = currently ? UnityEngine.Color.green : UnityEngine.Color.red;
        checkPoint.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", currentColor);
    }


    public static  Boolean checkIfInField(Vector2[] points, Vector2 position)
    {
        Boolean is_inside = false;
        float xMean = 0, zMean = 0;
        //calculate mean
        for (int i = 0; i < points.Length; i++)
        {
            xMean += points[i].x;
            zMean += points[i].y;
        }
        xMean = xMean / points.Length;
        zMean = zMean / points.Length;

        Vector2 mean = new Vector2(xMean, zMean);

        // laufe nun über alle Punkte und bilde für zwei Benachbarte Punkte mit dem Mittelpunkt ein dreieck.
        // falls der angegebene Punkt sich in einem dieser Dreiecke befindet, ist er inerhalb der grenze, sonst nicht.

        for(int i = 0; i < points.Length; i++)
        {
            //betrachte alle Punkte mit seinem rechten Nachbar.
            //Da der letzte Punkt keinen rechten Nachbar Besitzt, bilden wir ein dreieck zwischen ihm und dem 0 Punkt
            Vector2 p1 = points[i];
            Vector2 p2 = (i < points.Length - 1) ? points[i + 1] : points[0];

            float alpha =
                ((p1.y - p2.y) * (position.x - p2.x) + (p2.x - p1.x)*(position.y - p2.y)) /
                ((p1.y - p2.y) * (mean.x - p2.x) + (p2.x - p1.x) * (mean.y - p2.y));

            float beta =
                ((p2.y - mean.y) * (position.x - p2.x) + (mean.x - p2.x) * (position.y - p2.y)) /
                ((p1.y - p2.y) * (mean.x - p2.x) + (p2.x - p1.x) * (mean.y - p2.y));

            float gamma = 1.0f - alpha - beta;

            if( alpha >= 0 && beta >= 0 && gamma >= 0)
            {
                is_inside = true;
                break;
            }
        }
        return is_inside;
    }
}
