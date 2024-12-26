using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileDataObjectManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public TileDataObject[] findTilesInRange(TileDataObject startTile, int range)
    {
        TileDataObject[] tempflatArray = new TileDataObject[gameManager.gridTileArray.GetLength(0) * gameManager.gridTileArray.GetLength(1)];
        int arrayPosition = 0;
        for (int i = 0; i < gameManager.gridTileArray.GetLength(0); i++)
        {
            for (int j = 0; j < gameManager.gridTileArray.GetLength(1); j++)
            {
                tempflatArray[arrayPosition] = gameManager.gridTileArray[i, j].GetComponent<TileDataObject>();
                arrayPosition++;
            }
        }

        var tilesWithinRange = tempflatArray.Where(t => (Mathf.Abs(startTile.xPosition - t.xPosition) + Mathf.Abs(startTile.yPosition - t.yPosition) <= range));

        foreach (TileDataObject tile in tilesWithinRange)
        {
            tile.tempRange = Mathf.Abs(startTile.xPosition - tile.xPosition) + Mathf.Abs(startTile.yPosition - tile.yPosition);
        }

        var sortedTilesWithinRange = tilesWithinRange.OrderBy(t => t.tempRange);

        foreach (TileDataObject tile in sortedTilesWithinRange)
        {
            tile.tempRange = 0;
        }

        return sortedTilesWithinRange.ToArray();
    }
}
