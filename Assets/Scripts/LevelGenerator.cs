/* Level Generator V2: Endless Generator
 * 
 * NOTE: Remember to set the Y position of the prefabs so that the tiles line up at the bottom
 *       Use yPosition = (yCoordinate + ((sizeOfPrefab - 1) / 2))
 *       
 *       
 *       
 * TODO: Add death condition when player falls beyond the platforms (beond a certain y position maybe)
 *       Main menu and pause menu 
 *          Doesnt have to be complex. Just a play and quit for the main menu and quit and reset for the pause menu is fine
 *       Increasing difficulty as the player goes on (increase space between tile spawns and descrease length of tile spawns maybe?)
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
    [SerializeField] private Transform invisWall;

    private float tileWidth = 1.79f;    //Width that is shared with all the tiles
    private float tileHeight = 1.19f;   //Height of the mid tiles

    //stores the position of the latest tile placed (for height, it also gets reset to 0f when the program is done spawning the pillar of tiles) 
    private float lastTileHeight;       
    private float lastTileXPos;

    private Queue<List<Transform>> tileQueue = new Queue<List<Transform>>(); //store game objects spawned here to I can destroy them later once enough has spawned

    private void Awake()
    {
        lastTileHeight = 0f;
        lastTileXPos = tileWidth * 4; //preset this because I have 4 tiles preset already before the game even starts (look at the scene view in Unity before you start it)

        for (int i = 0; i < 6; i++)
        {
            //spawnGroundTile();

            int rand = Random.Range(0, groundTiles.Length);
            lastTileXPos += tileWidth;
            Instantiate(groundTiles[rand], new Vector3(lastTileXPos, groundTiles[rand].position.y), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = lastTileXPos - player.transform.position.x; //get distance between the player and the last tile spawned
        Debug.Log("Tile: " + lastTileXPos + "\nPlayer: " + player.transform.position.x + "\nDistance: " + distance + "\nQueueCount: " + tileQueue.Count + "\nWall XPos: " + invisWall.position.x);

        if (distance < PLAYER_DISTANCE_SPAWN_TILE) //spawn more tiles if the player gets close enough to the last spawned tile
        {
            //This should delete old tiles to make room for the new tiles 
            if (tileQueue.Count > 10)
            {
                List<Transform> tilesToDelete = tileQueue.Dequeue();//remove old tiles from the queue and...
                destoryTiles(tilesToDelete);                        //...destroy them

                invisWall.position = new Vector3(tileQueue.Peek()[0].position.x, 0); //move invisible wall to the next tile to be destroyed
            }

            //(int Random.Range is minInclusive, maxExclusive)
            //(float Random.Range is minInclusive, maxInclusive)

            int styleRand = Random.Range(0, 2);     //select by random if is will spawn a pillar of tiles or a platform of tiles (0 for pillar, 1 for tiles)
            Debug.Log("styleRand: " + styleRand);

            int spaceRand = Random.Range(0, 9);     //how many spaces should there be before the next set of tiles can spawn (0 - 8)
            Debug.Log("spaceRand: " + spaceRand);
            lastTileXPos += (tileWidth * spaceRand);

            int lengthRand = Random.Range(1, 10);   //how long will the next set of tiles be (1 - 9)
            Debug.Log("lengthRand: " + lengthRand);


            //how high will the next set of tiles be
            //limit the height of the next tiles to spawn so it's still possible for the player to traverse them vertically
            int lowerSpawnLimit;
            if (((int)player.transform.position.y - 6) < 1) lowerSpawnLimit = 1;
            else lowerSpawnLimit = (int)player.transform.position.y - 6;

            int upperSpawnLimit;
            if (((int)player.transform.position.y + 6) > 15) upperSpawnLimit = 15;
            else upperSpawnLimit = ((int)player.transform.position.y + 6);

            int heightRand = Random.Range(lowerSpawnLimit, upperSpawnLimit);
            Debug.Log("UpperSpawnLimit: " + upperSpawnLimit + "\nLowerSpawnLimit: " + lowerSpawnLimit + "\nRand: " + heightRand);

            List<Transform> spawnedTiles = new List<Transform>();

            if(styleRand == 0)
            {
                for(int i = 0; i < lengthRand; i++)
                {
                    if (heightRand == 1)
                    {
                        spawnGroundTile(spawnedTiles);
                    }
                    else
                    {
                        spawnBottomTiles(spawnedTiles);
                        spawnMidTiles(heightRand - 2, spawnedTiles);
                        spawnTopTiles(spawnedTiles);
                    }
                }
            }
            else if(styleRand == 1)
            {
                for (int i = 0; i < lengthRand; i++)
                {
                    spawnPlatformTiles(heightRand, spawnedTiles);
                }
            }

            tileQueue.Enqueue(spawnedTiles); //put list of tiles spawned into a queue for destroying later. 
            /*
            if (heightRand > 0)
            {
                if(heightRand == 1)
                {
                    spawnGroundTile();
                }
                else
                {
                    spawnBottomTiles();
                    spawnMidTiles(heightRand - 2);
                    spawnTopTiles();
                }
            }*/
        }

        player = GameObject.Find("Player");
    }

    private void spawnTopTiles(List<Transform> tileList)
    {
        int rand = Random.Range(0, topTiles.Length);

        Transform tile = Instantiate(topTiles[rand], new Vector3(lastTileXPos, lastTileHeight + tileHeight), Quaternion.identity); //init tile to game 
        tileList.Add(tile);     //shove tile to list for deletion later
        lastTileHeight = 0f;    //reset tile height for next set of tiles
    }

    private void spawnMidTiles(int stackSize, List<Transform> tileList)
    {
        for (int i = 0; i < stackSize; i++)
        {
            int rand = Random.Range(0, midTiles.Length);

            lastTileHeight += tileHeight; //increment tile height
            Transform tile = Instantiate(midTiles[rand], new Vector3(lastTileXPos, lastTileHeight), Quaternion.identity);
            tileList.Add(tile);
        }

    }

    private void spawnBottomTiles(List<Transform> tileList)
    {
        int rand = Random.Range(0, bottomTiles.Length);

        lastTileXPos += tileWidth; //increment tile x position for next set of tiles
        Transform tile = Instantiate(bottomTiles[rand], new Vector3(lastTileXPos, lastTileHeight), Quaternion.identity);
        tileList.Add(tile);
    }

    //Spawns ground tiles (has components of a top, middle, and bottom tile)
    private void spawnGroundTile(List<Transform> tileList)
    {
        int rand = Random.Range(0, groundTiles.Length);

        lastTileXPos += tileWidth;
        Transform tile = Instantiate(groundTiles[rand], new Vector3(lastTileXPos, groundTiles[rand].position.y), Quaternion.identity);
        tileList.Add(tile);
    }

    private void spawnPlatformTiles(float height, List<Transform> tileList)
    {
        int rand = Random.Range(0, groundTiles.Length);

        lastTileXPos += tileWidth;
        Transform tile = Instantiate(groundTiles[rand], new Vector3(lastTileXPos, height), Quaternion.identity);
        tileList.Add(tile);
    }

    private void destoryTiles(List<Transform> tilesToDelete)
    {
        for (int i = 0; i < tilesToDelete.Count; i++)
        {
            Destroy(tilesToDelete[i].gameObject);
        }
    }
}
