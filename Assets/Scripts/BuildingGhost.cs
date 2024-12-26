using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    public int assignedBuildingWidth;
    public int assignedBuildingHeight;

    public int assignedBuildingWoodCost;
    public int assignedBuildingStoneCost; 

    public GameObject assignedBuilding;

    public GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.gridBuildingSystem.BuildingToConstruct != this.gameObject)
        {
            Destroy(this.gameObject);
        }
    }
}
