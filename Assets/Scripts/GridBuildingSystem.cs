using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GridBuildingSystem : MonoBehaviour
{

    [SerializeField]
    GameObject mouseIndicator;

    private GameManager gameManager;

    public GameObject BuildingToConstruct;
    private Building GhostAssignedBuilding;
    private Building BuildingToConstructScript;

    public Tilemap tilemap;

    public AudioSource placementSound;




    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        updateGhostPosition();
        if (Input.GetMouseButtonDown(1))
        {
            cancelConstruction();
        }

        if (Input.GetMouseButtonDown(0))
        {
            constructBuilding();
        }
    }

    void updateGhostPosition()
    {
        if (gameManager.currentlyBuilding)
        {
            Vector3Int clickedPosition = tilemap.WorldToCell(gameManager.cam.ScreenToWorldPoint(Input.mousePosition));
            Vector3 cellLocation = tilemap.GetCellCenterWorld(clickedPosition);
            if (BuildingToConstruct != null)
            {
                BuildingToConstruct.transform.position = cellLocation;
            }
        }


    }

    public void SetBuildingToConstruct(GameObject newBuilding)
    {
        if (newBuilding != null)
        {
            GameObject spawnedBuiling = Instantiate(newBuilding);
            BuildingToConstruct = spawnedBuiling;
            GhostAssignedBuilding = spawnedBuiling.GetComponent<BuildingGhost>().assignedBuilding.GetComponent<Building>();
        } 
        else
        {
            BuildingToConstruct = null;
        }

    }

    void constructBuilding()
    {
        if (gameManager.currentlyBuilding)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3Int clickedPosition = tilemap.WorldToCell(gameManager.cam.ScreenToWorldPoint(Input.mousePosition));
                Vector3 cellLocation = tilemap.GetCellCenterWorld(clickedPosition);

                if (gameManager.getTileFromMouseClick(clickedPosition) != null)
                {
                    if (gameManager.isTileBuildable(clickedPosition))
                    {
                        if (gameManager.resourceManager.doYouHaveEnoughResources(GhostAssignedBuilding.woodCost, GhostAssignedBuilding.stoneCost))
                        {
                            GameObject spawnedBuilding = Instantiate(BuildingToConstruct.GetComponent<BuildingGhost>().assignedBuilding, cellLocation, Quaternion.identity);
                            gameManager.resourceManager.payResources(spawnedBuilding.GetComponent<Building>());
                            spawnedBuilding.GetComponent<Building>().ConstructBuilding(tilemap.WorldToCell(cellLocation).x, tilemap.WorldToCell(cellLocation).y);
                            gameManager.updateTileBuildability(clickedPosition, false);
                            placementSound.Play();
                        }

                    }
                }
            }
        }
    }
    void cancelConstruction()
    {
        gameManager.SetCurrentlyBuilding(false);
        Destroy(BuildingToConstruct);
        SetBuildingToConstruct(null);
        GhostAssignedBuilding = null;
    }
}
