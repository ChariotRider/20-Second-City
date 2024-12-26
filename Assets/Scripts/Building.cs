using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string BuildingName;

    public int height;
    public int width;

    public int woodCost;
    public int stoneCost;

    public bool isProductionBuilding;

    public int[] resourceCosts;



    public int spawnX;
    public int spawnY;
    public Sprite sprite;

    public int id;

    public GameManager gameManager;

    public List<TileDataObject> occupiedTiles;


    public void ConstructBuilding(int gridX, int gridY)
    {
        gameManager = FindObjectOfType<GameManager>();
        Debug.Log("this building is constructed at " + gridX + "X and " + gridY + "Y");
        id = gameManager.ConstructedBuildings.Count;
        gameManager.ConstructedBuildings.Add(this.gameObject);
        Vector3Int tilePosition = new Vector3Int(gridX, gridY, 0);
        Debug.Log(tilePosition);
        TileDataObject builtTile = gameManager.returnTileDataObjectFromPosition(tilePosition);
        Debug.Log(builtTile.name);
        occupiedTiles.Add(gameManager.returnTileDataObjectFromPosition(tilePosition));
        builtTile.buildingOnTile = this.GetComponent<Building>();

        if (width > 1 || height > 1)
        {

        }
        else
        {

        }

    }

    private void Start()
    {
        
    }
}
