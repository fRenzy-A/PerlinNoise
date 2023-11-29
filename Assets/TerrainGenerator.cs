using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public MeshGenerator meshGenerator;
    public Renderer mesh;


    public float xRan;
    public float yRan;

    public float scale;
    public float octave;
    public float power;
    public float terrainWorldHeight;


    
    void Start()
    {
        meshGenerator.CreatePlane();
        
        
    }


    public void CalculateTerrainNoise(int mX, int mY)
    {
        /*float noiseX = xRan + (mX/scale);
        float noiseY =  yRan + (mY/scale);
        for (int i = 0; i < meshGenerator.vertices.Length; i++)
        {
            meshGenerator.vertices[i].y = Mathf.PerlinNoise(noiseX, noiseY) * terrainWorldHeight;
        }*/
        int Xe = 0;

        int planeX = 0;
        int planeY = 0;

        for (int x =0; x <= mX; x++)
        {
           // Xe +=1 ;

            for (int y=0; y <= mY; y++)
            {
                float noiser;
                float noiseX = xRan + x / (float)mX * scale;
                float noiseY = yRan + y / (float)mY * scale;
                noiser = Mathf.PerlinNoise(noiseX, noiseY);
                meshGenerator.vertices[Xe].y = Mathf.Pow((Mathf.PerlinNoise(Mathf.PerlinNoise(noiseX+noiser,noiseY+noiser), Mathf.PerlinNoise(noiseX + noiser, noiseY + noiser)) *terrainWorldHeight),power)/*Mathf.Pow((1* noiser + (0.5f * noiser*2) + (0.25f * noiser*4)) /(1f + 0.5f + 0.25f),power)*/;
                Xe += 1;
                planeY++;
            }
            planeX++;
        }
    }

    public void AddTerrainColor(Vector3[] vertices, Mesh mesh)
    {
        Color[] colors = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i].y < 0.1)
            {
                colors[i] = Color.green;
            }
            else if (vertices[i].y > terrainWorldHeight - terrainWorldHeight / 3)
            {
                colors[i] =Color.grey;
            }
            else colors[i] = Color.green;

        }
            

        // assign the array of colors to the Mesh.
        mesh.colors = colors;
    }
    // Update is called once per frame
    void Update()
    {

        CalculateTerrainNoise(meshGenerator.planeWidth, meshGenerator.planeHeight);
        meshGenerator.UpdateMesh();
    }
}
