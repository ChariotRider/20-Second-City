using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TileDataObject : MonoBehaviour
{
    public GameManager gameManager;
    
    public CustomTile parentTile;
    public string tileName;
    public bool isBuildable;

    public int xPosition;
    public int yPosition;

    public int tempRange;

    public Building buildingOnTile;

    public bool isProductionBuilding;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        parentTile = findParent();
        fillTileData();
        addObjectToTileArray();
    }

    private CustomTile findParent()
    {
        return gameManager.getTileFromMouseClick(this.transform.position) as CustomTile;
    }

    private void fillTileData()
    {
        tileName = parentTile.tileName;
        isBuildable = parentTile.isBuildable;
    }

    private void addObjectToTileArray()
    {
        gameManager.gridTileList.Add(this.gameObject);
        gameManager.newestGridTileObject = this.gameObject;
    }

    public void updateConstructedBuilding(GameObject newBuilding)
    {
        buildingOnTile = newBuilding.GetComponent<Building>();
    }
}
