using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public List<Vector3> indiVert = new();
    public List<int> indiTri = new List<int>();



    int planeWidth = 5;
    int planeHeight = 5;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreatePlane();
        UpdateMesh();

    }

    void CreateQuad()
    {
        //really weird because unity operates on tris so technically i have to make quads manually


        
    }

    void CreatePlane()
    {
        for (int x = 0; x <= planeWidth; x++)
        {
            for (int y = 0; y <= planeHeight; y++)
            {

                indiVert.Add(new Vector3(x, 0, y));
                indiVert.Add(new Vector3(x + 1, 0, y));
            }
        }
        triangles = new int[]
                {
                    0, 1, 2,
                    1, 3, 2,
                };

        /*vertices = new Vector3[]
                {
                    new Vector3(0, 0, 0),
                    new Vector3(0, 0, 1),
                    new Vector3(1, 0, 0),
                    new Vector3(1, 0, 1),
                    new Vector3(2, 0, 0),
                    new Vector3(2, 0, 1),

                };
        triangles = new int[]
                {
                    0, 1, 2,
                    1, 3, 2,
                    2,3,4,
                    3,5,4
                }; */
    }
    void UpdateMesh()
    {
        mesh.Clear();


        /*foreach(Vector3[] v in plane)
        {
            mesh.vertices = v;
           
        }
        mesh.triangles = triangles;*/

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
