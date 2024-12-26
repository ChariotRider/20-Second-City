using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using TMPro;

[CreateAssetMenu(fileName = "New Tile", menuName = "Tile")]
public class CustomTile : Tile
{
    public GameObject dataTile;

    public Sprite tileSprite;

    public string tileName;
    public bool isBuildable;

}
