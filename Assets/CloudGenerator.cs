using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{

    
    public float scale;
    public float power;
    public float freq1;
    public float freq2;

    public float noisee;

    public int cloudGridSize;
    public float cloudFloatHeight;
    public float cloudHeight;

    public Vector3[] cloudPos;
    public GameObject[] cloudGO;

    public float cloudPower;
    // Start is called before the first frame update
    void Start()
    {       
        GenerateCloudParts();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateCloudNoise(cloudGridSize, cloudGridSize);
        UpdateCloudParts();
    }

    public void CalculateCloudNoise(int mX, int mY)
    {
        int Xe = 0;


        for (int x = 0; x < mX; x++)
        {
            for (int y = 0; y < mY; y++)
            {
                float noiser;
                float noiseX = (x / (float)mX) * scale;
                float noiseY = (y / (float)mY) * scale;
                noiser = (freq1 * Mathf.PerlinNoise(noiseX, noiseY))  // i am not going to attempt to automate this process
                    + (freq2 * Mathf.PerlinNoise(2 * noiseX, 2 * noiseY));
                noiser = Mathf.Pow(noiser, power);
                cloudPos[Xe].y = noiser * cloudFloatHeight;
                Xe += 1;
            }
        }
    }

    public void GenerateCloudParts()
    {
        int Xe = 0;
        cloudPos = new Vector3[cloudGridSize*cloudGridSize];
        cloudGO = new GameObject[cloudGridSize*cloudGridSize];
        for (int x = 0; x < cloudGridSize; x++)
        {
            for (int y = 0; y < cloudGridSize; y++)
            {
                cloudPos[Xe] = new Vector3(x*4, 0, y*4);
                cloudGO[Xe] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                cloudGO[Xe].transform.position = cloudPos[Xe];
                Xe+=1;
            }
        }
        
    }

    public void UpdateCloudParts()
    {
        for (int x = 0;x < cloudGridSize*cloudGridSize; x++)
        {
            
            if (cloudPos[x].y > cloudHeight)
            {
                cloudGO[x].SetActive(true);
                cloudGO[x].transform.position = cloudPos[x];
                cloudGO[x].transform.localScale = new Vector3((cloudPos[x].y * cloudPower), (cloudPos[x].y * cloudPower),(cloudPos[x].y * cloudPower));
            }
            else
            {
                cloudGO[x].SetActive(false);
                cloudGO[x].transform.position = cloudPos[x];
                cloudGO[x].transform.localScale = Vector3.zero;
            }
        }
        
    }

}
