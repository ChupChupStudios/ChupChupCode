using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel : MonoBehaviour
{
    public Vector3 position;
    public float speed = 1.0f;
    private Material material;
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    //public void UpdatePosition(float time)
    //{
    //    float waveHeight = Mathf.Sin(position.x + position.z + time);
    //    transform.position = new Vector3(position.x, waveHeight, position.z);
    //}

    public void UpdatePosition(float time)
    {
        float amplitudeX = 1.2f;
        float periodX = 5.5f;
        float speedX = 1.2f;
        float verticalOffsetX = 0.0f;
        float dispersionX = 0.8f;
        float verticalAmplitudeX = 0.3f;

        float amplitudeZ = 1.5f;
        float periodZ = 8.0f;
        float speedZ = 1.0f;
        float verticalOffsetZ = 0.0f;
        float dispersionZ = 1.2f;
        float verticalAmplitudeZ = 0.4f;

        float waveHeightX = amplitudeX * Mathf.Sin((2 * Mathf.PI / periodX) * (position.x - speedX * time))
            * Mathf.Exp(-Mathf.Pow(position.y - verticalOffsetX, 2) / (2 * dispersionX * dispersionX)) * verticalAmplitudeX;

        float waveHeightZ = amplitudeZ * Mathf.Sin((2 * Mathf.PI / periodZ) * (position.z - speedZ * time))
            * Mathf.Exp(-Mathf.Pow(position.y - verticalOffsetZ, 2) / (2 * dispersionZ * dispersionZ)) * verticalAmplitudeZ;

        transform.position = new Vector3(position.x, waveHeightX + waveHeightZ, position.z);

        // Calcula el color según la altura del voxel
        float normalizedHeight = Mathf.InverseLerp(-1.0f, 1.0f, transform.position.y);
        Color startColor = new Color(0.176f, 0.569f, 0.972f, 1.0f); // #2d91f8
        //Color endColor = new Color(0.184f, 0.325f, 0.937f, 1.0f);   // #2f53ef
        Color endColor = new Color(21f / 255f, 46f / 255f, 154f / 255f, 1.0f);   // #152e9a

        Color color = Color.Lerp(endColor, startColor, normalizedHeight);

        // Actualiza el color del material
        material.color = color;
    }

}
