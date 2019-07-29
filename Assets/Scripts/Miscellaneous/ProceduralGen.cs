using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class ProceduralGen : MonoBehaviour
{
    public static ProceduralGen proceduralGen;

    private  Random.State seedGenerator;

    public int genSeed = 55; //Default value

    private  bool seedGenInit = false;

    public List<GameObject> roomPrefabs;
    public List<GameObject> enemyPrefabs;
    public GameObject[,] gridSize;

    public int numCols;
    public int numRows;

    public float tileWidth = 50.0f; // These values might need to be changed later
    public float tileHeight = 50.0f;

    //Map of the day
    public bool mapOfTheDay = false;

    //Dates
    private DateTime _lastMOTD = DateTime.MinValue;

    // Start is called before the first frame update
    void Start()
    {
        if (proceduralGen == null)
        {
            proceduralGen = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        Generate();
        Debug.Log(_lastMOTD);
    }

    void Generate()
    {
        //Seed the random generator
        Random.seed = GenerateSeed();

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
                tempEnemy.transform.position = new Vector3(currentCol * tileWidth, 0, -currentRow * tileHeight);

                //Room Name
                tempRoom.name = "Room (" + currentCol + ", " + currentRow + ")";

                //Make it a child of this object
                tempRoom.GetComponent<Transform>().parent = this.gameObject.GetComponent<Transform>();
            }
        }

        //After field is generated, add in player
        GameManager.instance.SpawnOnSpot(GameManager.instance.players[0], new Vector3(-0.082f, -12.60907f, 0.356f));
        Camera_Follow.camerafollow.ScanForPlayer();

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
        //store current time;
        if (_lastMOTD.AddDays(1) > DateTime.Now)
        {
            genSeed = DateTime.Now.DayOfYear;
            _lastMOTD = DateTime.Now;
            Debug.Log("It's a new day");
        }
    }
}
