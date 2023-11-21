using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PerlinNoiseTest : MonoBehaviour
{
    // Start is called before the first frame update
    
    Vector3 cubePos;
    float posY;
    public List<GameObject> cubeList;

    public float noiseHeight = 1.0f;

    public int width;
    public int height;
    void Start()
    {

        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                float noise = Mathf.PerlinNoise1D(noiseHeight);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubeList.Add(cube);
            }
        }
    }

    // Update is called once per frame
    public void CalculateHeight()
    {

        float noise = Mathf.PerlinNoise1D(Time.deltaTime);

        while (width < 15)
        {
            foreach (GameObject _cube in cubeList)
            {
                _cube.transform.position = new Vector3(width += 1, noise, height += 1);
            }
        }
        

    }
    void Update()
    {

        CalculateHeight();

    }

}
