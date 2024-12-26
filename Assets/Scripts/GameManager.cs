using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    //Grid Generation variables
    public Tilemap tilemap;
    public TileBase groundTile;

    public GameObject townCenterObject;

    public Tile[] typedTiles;

    public Vector2Int size;
    public int width;
    public int height;

    public Camera cam;
    private Grid grid;

    public bool currentlyBuilding;

    public List<GameObject> ConstructedBuildings;
    public List<GameObject> gridTileList;
    public GameObject[,] gridTileArray;
    public int[,] tileGenerationArray;
    public GameObject newestGridTileObject;

    public GameObject tileDataObjectTemplate;

    public TileDataObjectManager tileDataObjectManager;
    public UIManager uiManager;
    public ResourceManager resourceManager;

    public int score;
    public List<int> scoreList;

    public GridBuildingSystem gridBuildingSystem;

    public enum TileTypes
    {
        Grass,
        Forest
    }

    enum gameState
    {
        resting,
        building
    }

    // Start is called before the first frame update
    void Start()
    {
        gridBuildingSystem = FindObjectOfType<GridBuildingSystem>();
        gridTileArray = new GameObject[width, height];
        cam = FindObjectOfType<Camera>();
        Debug.Log(Camera.current);
        createGridNumberArray();
        generateGrid();
        placeTownCenter();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(getTileFromMouseClick(cam.ScreenToWorldPoint(Input.mousePosition)));
            Debug.Log(isTileBuildable(tilemap.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition))));
        }
    }

    private void generateGrid()
    {
        Vector3Int[,] positions = new Vector3Int[height, width];
        TileBase[,] tileArray = new TileBase[height, width];

        for (int i = 0;  i < height; i++)
        {

            for (int j = 0; j < width; j++)
            {
                Debug.Log(positions.Length);
                positions[i, j] = new Vector3Int(i, j);

                tileArray[i, j] = typedTiles[tileGenerationArray[i, j]];
                tilemap.SetTile(positions[i, j], tileArray[i, j]);

                Vector3 newSpawnPosition = tilemap.CellToWorld(new Vector3Int(i, j, 0));
                Vector3 anchorShiftedSpawnPosition = new Vector3(newSpawnPosition.x + tilemap.tileAnchor.x, newSpawnPosition.y + tilemap.tileAnchor.y, 0f);
                newestGridTileObject = Instantiate(tileDataObjectTemplate, anchorShiftedSpawnPosition, Quaternion.identity);
                newestGridTileObject.transform.parent = tileDataObjectManager.transform;

                newestGridTileObject.GetComponent<TileDataObject>().xPosition = i;
                newestGridTileObject.GetComponent<TileDataObject>().yPosition = j;
                gridTileArray[i, j] = newestGridTileObject;
                Debug.Log(gridTileArray[i, j].transform.position);


                Debug.Log("Array position " + i + ", " + j + " is set to " + positions[i, j]);

                
                
            }
        }


        

        foreach (GameObject tileObject in gridTileList)
        {
            CustomTile cTile = (getTileFromMouseClick(tileObject.transform.position) as CustomTile);
        }

        cam.transform.position = new Vector3(size.x / 2, size.y / 2, -10);

    }

    public void newgenerateGrid()
    {
        Vector3Int[,] positions = new Vector3Int[tileGenerationArray.GetLength(0), tileGenerationArray.GetLength(1)];
        TileBase[,] tileArray = new TileBase[positions.GetLength(0), positions.GetLength(1)];

        Debug.Log("New Tile Generation is happening with lengths of " + tileGenerationArray.GetLength(0) + ", " + tileGenerationArray.GetLength(1));
        for (int i = 0; i < positions.GetLength(0); i++)
        {
            for (int j = 0; j < positions.GetLength(1); j++)
            {
                tileArray[i, j] = typedTiles[tileGenerationArray[i, j]];
                tilemap.SetTile(positions[i, j], tileArray[i, j]);

                Vector3 newSpawnPosition = tilemap.CellToWorld(new Vector3Int(i, j, 0));
                Vector3 anchorShiftedSpawnPosition = new Vector3(newSpawnPosition.x + tilemap.tileAnchor.x, newSpawnPosition.y + tilemap.tileAnchor.y, 0f);
                newestGridTileObject = Instantiate(tileDataObjectTemplate, anchorShiftedSpawnPosition, Quaternion.identity);
                newestGridTileObject.transform.parent = tileDataObjectManager.transform;

                newestGridTileObject.GetComponent<TileDataObject>().xPosition = i;
                newestGridTileObject.GetComponent<TileDataObject>().yPosition = j;
                gridTileArray[i, j] = newestGridTileObject;
            }
        }


    }

    public void SetCurrentlyBuilding(bool x)
    {
        currentlyBuilding = x;
        Debug.Log(currentlyBuilding);
    }

    public TileBase getTileFromMouseClick(Vector3 position)
    {
        Vector3Int clickedPosition = tilemap.WorldToCell(position);
        TileBase selectedTile = tilemap.GetTile(clickedPosition);
        Debug.Log(tilemap.GetTile(clickedPosition));
        Debug.Log(tilemap.GetTile(clickedPosition));
        return tilemap.GetTile(clickedPosition);
    }

    //methods for interacting with TileDataObjectManager
    public bool isTileBuildable(Vector3Int tileLocation)
    {
        Debug.Log(tileLocation.x + ", " + tileLocation.y);
        TileDataObject checkedTile = gridTileArray[tileLocation.x, tileLocation.y].GetComponent<TileDataObject>();
        return checkedTile.isBuildable;
    }

    public TileDataObject returnTileDataObjectFromPosition(Vector3Int position)
    {
        TileDataObject grabbedTile = gridTileArray[position.x, position.y].GetComponent<TileDataObject>();
        return grabbedTile;
    }

    public void updateTileBuildability(Vector3Int tileLocation, bool newBuildState)
    {
        TileDataObject updatedTile = gridTileArray[tileLocation.x, tileLocation.y].GetComponent<TileDataObject>();
        updatedTile.isBuildable = newBuildState;
    }

    public void createGridNumberArray()
    {
        tileGenerationArray = new int[size.x, size.y];

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                if (Random.value <= .75f)
                {
                    if (Random.value >= .1f)
                    {
                        Debug.Log("Template Grid " + i + ", " + j);
                        tileGenerationArray[i, j] = 0;
                    } else
                    {
                        Debug.Log("Template Grid " + i + ", " + j);
                        tileGenerationArray[i, j] = 2;
                    }
                } else
                {
                    Debug.Log("Template Grid " + i + ", " + j);
                    tileGenerationArray[i, j] = 1;
                }
            }

        }
    }

    private void placeTownCenter()
    {
        bool TCplaced = false;
        while (!TCplaced)
        {
            TileDataObject checkedTile = gridTileArray[Random.Range(0, size.x), Random.Range(0, size.y)].GetComponent<TileDataObject>();
            Vector3Int tilePosition = new Vector3Int(checkedTile.xPosition, checkedTile.yPosition, 0);
            Vector3 cellLocation = tilemap.GetCellCenterWorld(tilePosition);

            if (checkedTile.isBuildable)
            {
                GameObject newTC = Instantiate(townCenterObject, cellLocation, Quaternion.identity);
                newTC.GetComponent<Building>().ConstructBuilding(tilePosition.x, tilePosition.y);
                updateTileBuildability(tilePosition, false);
               TCplaced = true;
            }
        }

    }
}
