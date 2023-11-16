//using UnityEngine;

//public class WaterVoxelManager : MonoBehaviour
//{
//    public GameObject cubePrefab;
//    public int gridWidth = 10;
//    public int gridHeight = 10;
//    public float spacing = 1.5f;

//    void Start()
//    {
//        GenerateOcean();
//    }

//    void GenerateOcean()
//    {
//        for (int i = 0; i < gridWidth; i++)
//        {
//            for (int j = 0; j < gridHeight; j++)
//            {
//                Vector3 position = new Vector3(i * spacing, 0f, j * spacing);
//                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
//                cube.transform.parent = transform;
//            }
//        }
//    }
//}


using UnityEngine;

public class WaterVoxelManager : MonoBehaviour
{
    public GameObject cubePrefab;
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float spacing = 1.5f;
    public float globalSpeed = 1.0f;
    public float globalAmplitude = 1.0f;

    void Start()
    {
        GenerateOcean();
    }

    void GenerateOcean()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                Vector3 position = new Vector3(i * spacing, 0f, j * spacing);
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
                cube.transform.parent = transform;

                // Asigna valores únicos de velocidad y amplitud a cada cubo
                MeshRenderer meshRenderer = cube.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    Material material = meshRenderer.material;
                    if (material != null)
                    {
                        material.SetFloat("_GlobalSpeed", globalSpeed);
                        material.SetFloat("_GlobalAmplitude", globalAmplitude);
                        material.SetFloat("_ID", Random.Range(2.0f, 3.0f));
                    }
                }
            }
        }
    }
}


