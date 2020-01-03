using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public string path;
    
    // Start is called before the first frame update
    void Start()
    {
        ReadCsv();
    }

    void ReadCsv()
    {
        string fileData = System.IO.File.ReadAllText(path);
        string[] lines = fileData.Split("/"[0]);
        
        float[] vectorData = new float[3];
        List<Vector3> vectors = new List<Vector3>();
        int ct;

        foreach (string line in lines)
        {
            if (line.Length != 0)
            {
                Debug.Log("Line " + line);
                string[] lineData = (line.Trim()).Split(","[0]);

                for (int i = 0; i < 3; i++)
                {
                    float.TryParse(lineData[i], out float x);
                    vectorData[i] = x;
                }

                vectors.Add(new Vector3(vectorData[0], vectorData[1], vectorData[2]));
            }
        }

        GenerateMesh(vectors);
    }

    void GenerateMesh(List<Vector3> vectors)
    {
        Mesh mesh = new Mesh(); // GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        int n = 0;

        foreach (Vector3 vector in vectors)
        {
            verts.Add(vector);
            tris.Add(n);
            n++;
            Debug.Log("vect " + vector);
            //mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) };
            //mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) };
            //mesh.triangles = new int[] { 0, 1, 2 };
        }

        Debug.Log("tris: " + tris.Count);
        Debug.Log("verts: " + verts.Count);

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();

        List<Vector2> uvs = new List<Vector2>();
        for (int i = 0; i < verts.Count; i++)
        {
            if ((i + 1) % 3 == 1)
            {
                uvs.Add(new Vector2(0, 0));
                Debug.Log("added uv 0,0 for " + i + "th vert");
            }
            else if ((i + 1) % 3 == 2)
            {
                uvs.Add(new Vector2(0, 1));
                Debug.Log("added uv 0,1 for " + i + "th vert");
            }
            else
            {
                uvs.Add(new Vector2(1, 1));
                Debug.Log("added uv 1,1 for " + i + "th vert");
            }
        }

        mesh.uv = uvs.ToArray();
        GetComponent<MeshFilter>().mesh = mesh;
    }
}

/*
 * 
 *         Mesh mesh = new Mesh(); // GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) };
        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
        mesh.triangles = new int[] { 0, 1, 2 };
        GetComponent<MeshFilter>().mesh = mesh;
*/