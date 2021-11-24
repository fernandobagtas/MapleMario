/* Level Generator V2: Endless Generator
 * 
 * NOTE: Remember to set the Y position of the prefabs so that the tiles line up at the bottom
 *       Use yPosition = (yCoordinate + ((sizeOfPrefab - 1) / 2))
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_TILE = 20f;

    [SerializeField] private Transform[] topTiles;
    [SerializeField] private Transform[] midTiles;
    [SerializeField] private Transform[] bottomTiles;
    [SerializeField] private Transform[] groundTiles;
    [SerializeField] private GameObject player;

    private float tileWidth = 1.79f;
    private float tileHeight = 1.19f;

    private float lastTileHeight;
    private float lastTileXPos;

    private void Awake()
    {
        lastTileHeight = 0f;
        lastTileXPos = tileWidth * 4;

        for (int i = 0; i < 6; i++)
        {
            spawnGroundTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = lastTileXPos - player.transform.position.x;
        Debug.Log("Tile: " + lastTileXPos + "\nPlayer: " + player.transform.position.x + "\nDistance: " + distance);
        if (distance < PLAYER_DISTANCE_SPAWN_TILE)
        {
            int lowerSpawnLimit;
            if (((int)player.transform.position.y - 6) < 0) lowerSpawnLimit = 0;
            else lowerSpawnLimit = (int)player.transform.position.y - 6;

            int upperSpawnLimit;
            if (((int)player.transform.position.y + 6) > 20) upperSpawnLimit = 20;
            else upperSpawnLimit = ((int)player.transform.position.y + 6);

            int rand = Random.Range(lowerSpawnLimit, upperSpawnLimit);
            Debug.Log("UpperSpawnLimit: " + upperSpawnLimit + "\nLowerSpawnLimit: " + lowerSpawnLimit + "\nRand: " + rand);

            if (rand > 0)
            {
                if(rand == 1)
                {
                    spawnGroundTile();
                }
                else
                {
                    spawnBottomTiles();
                    spawnMidTiles(rand - 2);
                    spawnTopTiles();
                }
            }
        }

        player = GameObject.Find("Player");
    }

    private void spawnTopTiles()
    {
        int rand = Random.Range(0, topTiles.Length);
        Instantiate(topTiles[rand], new Vector3(lastTileXPos, lastTileHeight + tileHeight), Quaternion.identity);

        lastTileHeight = 0f; //reset tile height for next set of tiles
    }

    private void spawnMidTiles(int stackSize)
    {
        for (int i = 0; i < stackSize; i++)
        {
            int rand = Random.Range(0, midTiles.Length);
            lastTileHeight += tileHeight; //increment tile height
            Instantiate(midTiles[rand], new Vector3(lastTileXPos, lastTileHeight), Quaternion.identity);
        }

    }

    private void spawnBottomTiles()
    {
        int rand = Random.Range(0, bottomTiles.Length);
        lastTileXPos += tileWidth; //increment tile x position for next set of tiles
        Instantiate(bottomTiles[rand], new Vector3(lastTileXPos, lastTileHeight), Quaternion.identity);
    }

    //Spawns ground tiles (has components of a top, middle, and bottom tile)
    private void spawnGroundTile()
    {
        int rand = Random.Range(0, groundTiles.Length);
        lastTileXPos += tileWidth;
        Instantiate(groundTiles[rand], new Vector3(lastTileXPos, groundTiles[rand].position.y), Quaternion.identity);
    }
}
