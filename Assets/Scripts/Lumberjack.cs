using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lumberjack : Building
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
    private int numberOfForestsInRange = 0;
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
            if (tilesInRange[i].tileName == "Forest")
            {
                numberOfForestsInRange++;
            }
        }
    }

    private void OnSecondPassed()
    {
        timeUntilNextCollection--;
        if (timeUntilNextCollection <= 0)
        {
            if (forestInRange())
            {
                timeUntilNextCollection = secondsBetweenCollection;
                CollectWood();
            }
        }
    }
    public void CollectWood()
    {
        Debug.Log(numberOfForestsInRange);
        int amountToCollect = numberOfForestsInRange * collectionAmount;
        Debug.Log("I am collecting " + amountToCollect + " wood");
        GameObject resourceIcon = Instantiate(resourceCollectedObject, this.gameObject.transform);
        resourceIcon.GetComponent<ResourceCollectedIcon>().updateResourceText(amountToCollect);
        resourceManager.collectWood(amountToCollect);
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

    public bool forestInRange()
    {
        bool isForestInRange = false;
        for (int i = 0; i < tilesInRange.Length; i++)
        {
            if (tilesInRange[i].tileName == "Forest")
            {
                isForestInRange = true;
            }
        }

        return isForestInRange;
    }

    public void CalculateScore()
    {
        gameManager.score = gameManager.score + basePoints;
        gameManager.scoreList.Add(basePoints);
    }

 }

