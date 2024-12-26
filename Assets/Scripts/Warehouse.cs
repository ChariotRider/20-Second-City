using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Warehouse : Building
{
    [SerializeField]
    private int range;

    [SerializeField]
    private TileDataObject[] tilesInRange;

    [SerializeField]
    private Building[] typesOfBuildingToScore;

    [SerializeField]
    private List<Building> buildingsInRange = new List<Building>();
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onCalculateScoring += CalculateScore;

        foreach (TileDataObject baseTiles in occupiedTiles)
        {
            tilesInRange = gameManager.tileDataObjectManager.findTilesInRange(baseTiles.GetComponent<TileDataObject>(), range);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculateScore()
    {
        int totalScore = 0;
        Debug.Log("calculating score");
        foreach (TileDataObject tile in tilesInRange)
        {
            if (tile.buildingOnTile != null)
            {
                if (tile.buildingOnTile.BuildingName == "Warehouse")
                {
                    Debug.Log("Warehouse in range");
                    totalScore = totalScore - 5;
                } else
                {
                    if (tile.buildingOnTile.BuildingName == typesOfBuildingToScore[0].name || tile.buildingOnTile.BuildingName == typesOfBuildingToScore[1].name)
                    {
                        buildingsInRange.Add(tile.buildingOnTile);
                        Debug.Log("there's a building in range");
                    }
                }
            }
        }

        foreach (Building building in buildingsInRange)
        {
                totalScore = totalScore + 3;
        }

        totalScore = totalScore + 5;
        gameManager.score = gameManager.score + totalScore;
        gameManager.scoreList.Add(totalScore);
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
}
