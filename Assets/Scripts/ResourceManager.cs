using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int woodCount;
    public int stoneCount;

    public void collectWood(int newWood)
    {
        woodCount = woodCount + newWood;
    }

    public void collectStone(int newStone)
    {
        stoneCount = stoneCount + newStone;
    }

    public BuildingCost returnBankedResourcesAsCost()
    {
        BuildingCost bankedResources = new BuildingCost();
        bankedResources.woodCost = woodCount;

        return bankedResources;
    }

    public bool doYouHaveEnoughResources(int woodCost, int stoneCost)
    {
        bool enoughResources = true;
        if (woodCost > woodCount)
        {
            enoughResources = false;
        }
        if (stoneCost > stoneCount)
        {
            enoughResources = false;
        }

        return enoughResources;
    }

    public void payResources(Building constructedBuilding)
    {
        collectWood(-constructedBuilding.woodCost);
        collectStone(-constructedBuilding.stoneCost);
    }

    enum resourceIndex
    {
        Wood,
        Stone
    }
}
