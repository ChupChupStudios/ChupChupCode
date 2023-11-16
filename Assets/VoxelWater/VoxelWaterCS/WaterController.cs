using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public int gridSizeX = 100;
    public int gridSizeY = 100;
    public GameObject voxelPrefab;
    public float waveSpeed = 1.0f;

    private Voxel[,] voxels;

    void Start()
    {
        GenerateWaterGrid();
    }

    void Update()
    {
        UpdateWaterSimulation();
    }

    void GenerateWaterGrid()
    {
        voxels = new Voxel[gridSizeX, gridSizeY];

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                GameObject voxelObj = Instantiate(voxelPrefab, new Vector3(x, 0, y), Quaternion.identity);
                Voxel voxel = voxelObj.GetComponent<Voxel>();
                voxel.position = new Vector3(x, 0, y);
                voxels[x, y] = voxel;
            }
        }
    }

    void UpdateWaterSimulation()
    {
        float time = Time.time * waveSpeed;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                voxels[x, y].UpdatePosition(time);
            }
        }
    }
}
