using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public MeshGenerator meshGenerator;
    public Renderer mesh;

    public float scale;
    public float power;
    public float freq1;
    public float freq2;
    public float freq3;
    public float freq4;
    public float terrainWorldHeight;

    public float waterLevel;
    public float grassLevel;
    public float mountainPeakLevel;


    
    void Start()
    {
        meshGenerator.CreatePlane();
        
        
    }


    public void CalculateTerrainNoise(int mX, int mY)
    {
        int Xe = 0;


        for (int x =0; x <= mX; x++)
        {
            for (int y=0; y <= mY; y++)
            {
                float noiser;
                float noiseX = (x / (float)mX) * scale;
                float noiseY = (y / (float)mY) * scale;
                noiser = (freq1 * Mathf.PerlinNoise(noiseX, noiseY))  // i am not going to attempt to automate this process
                    + (freq2 * Mathf.PerlinNoise(2 * noiseX, 2 * noiseY))
                    + (freq3 * Mathf.PerlinNoise(4 * noiseX, 4 * noiseY))
                    + (freq4 * Mathf.PerlinNoise(8 * noiseX, 8 * noiseY));
                noiser = Mathf.Pow(noiser, power);
                meshGenerator.vertices[Xe].y = noiser * terrainWorldHeight;
                Xe += 1;
            }
        }
    }

    public void AddTerrainColor(Vector3[] vertices, Mesh mesh)
    {
        Color[] colors = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            
            if (vertices[i].y < waterLevel)
            {
                colors[i] = Color.blue;
            }
            else if (vertices[i].y > mountainPeakLevel)
            {
                colors[i] =Color.white;
            }
            else if (vertices[i].y > mountainPeakLevel - 7f)
            {
                colors[i] = new Color(0.14f, 0.56f, 0.01f, 1);
            }
            else colors[i] = Color.green;

            if (vertices[i].y < waterLevel && vertices[i].y > waterLevel - 0.1f)
            {
                colors[i] = new Color(0.56f, 0.24f, 0, 1);
            }
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
