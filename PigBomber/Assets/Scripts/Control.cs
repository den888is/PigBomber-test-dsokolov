using UnityEngine;
using UnityEngine.Tilemaps;

public class Control : MonoBehaviour
{
    private GameObject tilemapBackground, tilemapUnderground, tilemapBefore;
    void Start()
    {
        tilemapBackground = GameObject.Find("TilemapBackground");
        tilemapUnderground = GameObject.Find("TilemapUnderground");
        tilemapBefore =GameObject.Find("TilemapBefore");
        tilemapBackground.GetComponent<Tilemap>().CompressBounds();
        tilemapUnderground.GetComponent<Tilemap>().CompressBounds();
        tilemapBefore.GetComponent<Tilemap>().CompressBounds();
    }
}
