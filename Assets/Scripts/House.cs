using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
{
    [SerializeField]
    private int range;

    [SerializeField]
    private TileDataObject[] tilesInRange;

    [SerializeField]
    private List<Building> buildingsInRange = new List<Building>();

    [SerializeField]
    private int[] scorearray;

    [SerializeField]
    private List<string> buildingNameListForScoring = new List<string>();

    void Start()
    {
        GameEvents.current.onCalculateScoring += CalculateScore;

        foreach (TileDataObject baseTiles in occupiedTiles)
        {
            tilesInRange = gameManager.tileDataObjectManager.findTilesInRange(baseTiles.GetComponent<TileDataObject>(), range);
        }
    }



    public void CalculateScore()
    {
        int totalScore = 0;
        Debug.Log("calculating house score");
        foreach (TileDataObject tile in tilesInRange)
        {
            if (tile == this.occupiedTiles[0])
            {

            } else {
                if (tile.buildingOnTile != null)
                {
                    Debug.Log("there is a building on the tile");
                    if (!buildingNameListForScoring.Contains(tile.buildingOnTile.BuildingName))
                    {
                        buildingNameListForScoring.Add(tile.buildingOnTile.BuildingName);
                        Debug.Log(tile.buildingOnTile.BuildingName + " is next to house");
                    }
                    else
                    {
                        Debug.Log("building already in collection");
                    }


                }
            }
            
        }

        for (int i = 0; i < buildingNameListForScoring.Count; i++)
        {
            Debug.Log(buildingNameListForScoring[i]);
        }

        totalScore = scorearray[buildingNameListForScoring.Count - 1];


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
