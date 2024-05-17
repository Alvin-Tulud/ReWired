using GridObjects.Components.CompTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class wallCollisions : MonoBehaviour
{
    Tilemap tilemap; 
    List<Vector3> tileWorldLocations; // Use this for initialization


    void Start () 
    { 
        tileWorldLocations = new List<Vector3>(); 

        tilemap = GetComponent<Tilemap>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin) 
        { 
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z); 
            Vector3 place = tilemap.CellToWorld(localPlace); 


            if (tilemap.HasTile(localPlace)) 
            { 
                tileWorldLocations.Add(place); 
            } 
        } 
        //print(tileWorldLocations); 


        //for each wall we get in the tilemap set the editor tile under it to not walkable
        foreach(Vector3 wall in tileWorldLocations)
        {
            Vector3 boardConversion = new Vector3(wall.x + 0.5f, wall.y + 0.5f, wall.z);

            RaycastHit2D ray;
            ray = Physics2D.Raycast(boardConversion, Vector2.right,0.1f);

            //Debug.Log("wall || " + wall + " || " + "boardConversion || " + boardConversion + " || " + ray.transform.name + " || " + ray.transform.position);

            ray.transform.GetComponent<CanWalk>().setWalkable(false);
            ray.transform.GetComponent<CanWalk>().setWall(true);

        }
    }
}
