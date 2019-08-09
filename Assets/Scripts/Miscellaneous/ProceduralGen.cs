using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProceduralGen : MonoBehaviour
{
    public static ProceduralGen proceduralGen;

    private  Random.State seedGenerator;

    public int genSeed = 10; //Default value

    private  bool seedGenInit = false;

    public List<GameObject> roomPrefabs;
    public List<GameObject> enemyPrefabs;
    public GameObject[,] gridSize;

    public ScanWayPoints scan;

    public int numCols;
    public int numRows;

    public float tileWidth = 50.0f; // These values might need to be changed later
    public float tileHeight = 50.0f;

    //Map of the day
    public bool mapOfTheDay;

    // Start is called before the first frame update
    void Start()
    {
        proceduralGen = this;
        Generate();
    }

    void Generate()
    {
        //Seed the random generator
        
        Random.InitState(GenerateSeed());

        if (genSeed.ToString().Length > 2)
        {
            //Create 2d array
            gridSize = new GameObject[numCols, numRows];

            //For loop
            for (int currentCol = 0; currentCol < numCols; currentCol++)
            {
                for (int currentRow = 0; currentRow < numRows; currentRow++)
                {
                    //Instantiate a room
                    GameObject tempRoom = Instantiate(RandomRoom());

                    //Instanciate an enemy along with the room
                    GameObject tempEnemy = Instantiate(RandomEnemy());

                    //Ad to grid array
                    gridSize[currentCol, currentRow] = tempRoom;

                    //Move into position
                    tempRoom.transform.position = new Vector3(currentCol * tileWidth, 0, -currentRow * tileHeight);
                    tempEnemy.transform.position = new Vector3(currentCol * tileWidth, 1, -currentRow * tileHeight);

                    //Room Name
                    tempRoom.name = "Room (" + currentCol + ", " + currentRow + ")";

                    //Make it a child of this object
                    tempRoom.GetComponent<Transform>().parent = this.gameObject.GetComponent<Transform>();

                    
                }
            }

            //After field is generated, add in player
            GameManager.instance.SpawnOnSpot(GameManager.instance.players[0], new Vector3(-0.082f, -12.60907f, 0.356f));
            Camera_Follow.camerafollow.ScanForPlayer();

            //Scan for waypoints
            scan.Scan();

        } else
        {
            Debug.LogError("Field wasn't generated.");
        }
    }

    public GameObject RandomRoom()
    {
        int roomIndex = Random.Range(0, roomPrefabs.Count);

        return roomPrefabs[roomIndex];
        throw new NotImplementedException();
    }
    public GameObject RandomEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Count);

        return enemyPrefabs[enemyIndex];

        throw new NotImplementedException();
    }

    public int GenerateSeed()
    {
        switch (mapOfTheDay)
        {
            case true:
                GenerateMapOfTheDay();
                break;
        }
        var temp = Random.state;

        if (!seedGenInit)
        {

            Random.InitState(genSeed);
            seedGenerator = Random.state;
            seedGenInit = true;

        }

        Random.state = seedGenerator;

        var generatedSeed = Random.Range(int.MinValue, int.MaxValue);

        seedGenerator = Random.state;

        Random.state = temp;

        return generatedSeed;
    }

    public void GenerateMapOfTheDay()
    {
        genSeed = GetCurrentDay();
    }

    public int GetCurrentDay()
    {
        string day = DateTime.Now.Day.ToString();
        string month = DateTime.Now.Month.ToString();
        string year = DateTime.Now.Year.ToString();
        string seed = day + month + year;

        int seedVal = Convert.ToInt32(seed);

        return seedVal;
    }
}
