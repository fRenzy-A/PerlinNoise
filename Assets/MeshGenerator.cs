using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;


    public Vector3[] vertices;
    int[] triangles;

    public List<Vector3> indiVert = new();
    public List<int[]> indiTri = new List<int[]>();

    public int planeWidth;
    public int planeHeight;

    public TerrainGenerator terrainGenerator;


    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        //terrainGenerator = gameObject.GetComponent<TerrainGenerator>();
    }

    public void CreatePlane()
    {

        for (int x = 0; x <= planeWidth; x++)
        {
            for (int y = 0; y <= planeHeight; y++)
            {

                //theHeight = terrainGenerator.terrainWorldHeight;
                indiVert.Add(new Vector3(y, 0, x));
            }

        }
        vertices = indiVert.ToArray();


        triangles = new int[(planeWidth * planeHeight) * 6];

        int height = planeHeight;
        int tri = 0;
        for (int x = 0; x < planeHeight; x++)
        {
            
            for (int y = 0; y < planeWidth; y++)
            {

                triangles[tri] = (x*(height+1)) + y;
                triangles[tri + 1] = (x * (height + 1)) + (planeWidth + 1) + y;
                triangles[tri + 2] = (x * (height + 1)) + 1 + y;
                triangles[tri + 3] = (x * (height + 1)) + (planeWidth + 1) + y;
                triangles[tri + 4] = (x * (height + 1)) + (planeWidth + 2) + y;
                triangles[tri + 5] = (x * (height + 1)) + 1 + y;

                tri += 6;
            }

        }
    }
    public void UpdateMesh()
    {
        mesh.Clear();

        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        terrainGenerator.AddTerrainColor(vertices, mesh);
        mesh.RecalculateNormals();
     


    }

    // Update is called once per frame
    void Update()
    {

    }
}
