using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarry : Building
{
    [SerializeField]
    private GameObject resourceCollectedObject;
    private ResourceManager resourceManager;

    [SerializeField]
    private int range;

    [SerializeField]
    private TileDataObject[] tilesInRange;

    public int secondsBetweenCollection;
    public int timeUntilNextCollection;

    public int collectionAmount;

    public int basePoints;

    [SerializeField]
    private int numberOfMountainsInRange = 0;
    private void Start()
    {
        timeUntilNextCollection = secondsBetweenCollection;
        resourceManager = gameManager.resourceManager;
        GameEvents.current.onSecondPassed += OnSecondPassed;
        GameEvents.current.onCalculateScoring += CalculateScore;
        foreach (TileDataObject baseTiles in occupiedTiles)
        {
            tilesInRange = gameManager.tileDataObjectManager.findTilesInRange(baseTiles.GetComponent<TileDataObject>(), range);
        }

        for (int i = 0; i < tilesInRange.Length; i++)
        {
            if (tilesInRange[i].tileName == "Mountain")
            {
                numberOfMountainsInRange++;
            }
        }
    }

    private void OnSecondPassed()
    {
        timeUntilNextCollection--;
        if (timeUntilNextCollection <= 0)
        {
            if (mountainInRange())
            {
                timeUntilNextCollection = secondsBetweenCollection;
                CollectStone();
            }
        }
    }
    public void CollectStone()
    {
        Debug.Log("Mining Rocks");
        int amountToCollect = numberOfMountainsInRange * collectionAmount;
        GameObject resourceIcon = Instantiate(resourceCollectedObject, this.gameObject.transform);
        resourceIcon.GetComponent<ResourceCollectedIcon>().updateResourceText(amountToCollect);
        resourceManager.collectStone(amountToCollect);
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

    public bool mountainInRange()
    {
        bool isMountainInRange = false;
        for (int i = 0; i < tilesInRange.Length; i++)
        {
            if (tilesInRange[i].tileName == "Mountain")
            {
                isMountainInRange = true;
            }
        }

        return isMountainInRange;
    }

    public void CalculateScore()
    {
        gameManager.score = gameManager.score + basePoints;
        gameManager.scoreList.Add(basePoints);
    }
}
