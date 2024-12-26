using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCenter : Building
{
    [SerializeField]
    private int range;

    [SerializeField]
    private TileDataObject[] tilesInRange;
    // Start is called before the first frame update
    [SerializeField]
    private List<Building> buildingsInRange = new List<Building>();
    void Start()
    {
        GameEvents.current.onCalculateScoring += CalculateScore;

        foreach (TileDataObject baseTiles in occupiedTiles)
        {
            tilesInRange = gameManager.tileDataObjectManager.findTilesInRange(baseTiles.GetComponent<TileDataObject>(), range);
        }
    }
    private void OnMouseEnter()
    {
        foreach (TileDataObject tile in tilesInRange)
        {
            tile.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.970f, 0.660f, 0.660f, .7f);
        }
    }

    private void OnMouseExit()
    {
        foreach (TileDataObject tile in tilesInRange)
        {
            tile.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.970f, 0.660f, 0.660f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculateScore()
    {
        int totalScore = 0;
        foreach (TileDataObject tile in tilesInRange)
        {
            if (tile.buildingOnTile != null)
            {
                if (tile.buildingOnTile.BuildingName != this.BuildingName)
                {
                    buildingsInRange.Add(tile.buildingOnTile);
                }
            }
        }

        foreach (Building building in buildingsInRange)
        {
            totalScore = totalScore + 2;
        }

        gameManager.score = gameManager.score + totalScore;
        gameManager.scoreList.Add(totalScore);
    }
}
