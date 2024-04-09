using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public Terrain terrain;
    public float FindHeight(Vector3 pos) 
    {
        float normalizedX = pos.x / terrain.terrainData.size.x; 
        float normalizedZ = pos.z / terrain.terrainData.size.z;
        int rowIndex = Mathf.FloorToInt(normalizedZ * terrain.terrainData.heightmapResolution);
        int columnIndex = Mathf.FloorToInt(normalizedX * terrain.terrainData.heightmapResolution);

        return terrain.terrainData.GetHeight(rowIndex, columnIndex);
    }
}
