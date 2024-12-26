using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : Building
{
    [SerializeField]
    private GameObject resourceCollectedObject;
    private ResourceManager resourceManager;

    public int secondsBetweenCollection;
    public int timeUntilNextCollection;

    public int collectionAmount;

    public int basePoints;

    private int pointsToAward = 0;
    void Start()
    {
        timeUntilNextCollection = secondsBetweenCollection;
        resourceManager = gameManager.resourceManager;
        GameEvents.current.onSecondPassed += OnSecondPassed;
        GameEvents.current.onCalculateScoring += CalculateScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSecondPassed()
    {
        timeUntilNextCollection--;
        if (timeUntilNextCollection <= 0)
        {
            pointsToAward++;
            timeUntilNextCollection = secondsBetweenCollection;
            GameObject resourceIcon = Instantiate(resourceCollectedObject, this.gameObject.transform);
        }
    }

    private void CalculateScore()
    {
        gameManager.score = gameManager.score + basePoints + pointsToAward;
        gameManager.scoreList.Add(basePoints + pointsToAward);
    }
}
