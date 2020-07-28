using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class showVertex : MonoBehaviour
{
    Mesh mesh;
    List<GameObject> handles;
    Vector3 position;
    void Start()
    {
        mesh = this.GetComponent<MeshFilter>().mesh;
        handles = new List<GameObject>();
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            GameObject handle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            handle.transform.position = mesh.vertices[i];
            handle.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            handles.Add(handle);
        }
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!transform.position.Equals(position))
        {
            updateHandles();
            position = transform.position;
        }
    }

    private void updateHandles()
    {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            handles[i].transform.position = mesh.vertices[i];
        }
    }

    private void OnDestroy()
    {
        foreach(GameObject handle in handles)
        {
            Destroy(handle);
        }
    }
}
