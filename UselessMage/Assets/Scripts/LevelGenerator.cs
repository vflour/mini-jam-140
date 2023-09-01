using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    public float roomSize = 10;

    public int minSize = 50;
    public int maxSize = 300;
    public Dictionary<string,GameObject> Rooms;

    public UnityEvent OnRoomsGenerated;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
        OnRoomsGenerated.Invoke();
    }

    void GenerateLevel(){
        //UP = 1
        //RIGHT = 2
        //DOWN = 3
        //LEFT = 4
        roomPrefabs = Resources.LoadAll<GameObject>("Rooms/");
        
        GameObject currentRoom = Instantiate(roomPrefabs[0],this.transform);
        currentRoom.transform.position = new Vector3(0,0,0);
        Rooms = new Dictionary<string,GameObject>();
        Rooms.Add(GetKey(currentRoom.transform),currentRoom);
        SpawnRoom(currentRoom);
        Debug.Log(Rooms.Count);
    }
    string GetKey(Transform t){
        int x = (int)((t.position.x*32)/roomSize);
        int y = (int)((t.position.y*32)/roomSize);
        string key = x + "_" + y;
        return key;
    }
    GameObject GetRoom(int Dir){
        GameObject newRoom = roomPrefabs[Random.Range(0,roomPrefabs.Length)];
        Transform parent = newRoom.transform;
        bool correctRoom = false;
        foreach (Transform t in parent){
            if(t.tag == "SpawnPoint"){
                int OppositeDir = t.GetComponent<DirID>().Dir;
                if (Dir == 1 && OppositeDir == 3) {
                    correctRoom = true;
                } 
                else if (Dir == 2 && OppositeDir == 4){
                    correctRoom = true;
                }
                else if (Dir == 3 && OppositeDir == 1){
                    correctRoom = true;
                }
                else if (Dir == 4 && OppositeDir == 2){
                    correctRoom = true;
                }
            }
        }
        if (correctRoom){
            return newRoom;
        } else {
            return GetRoom(Dir);
        } 
    }
    void SpawnRoom(GameObject PreviousRoom){
        Transform parent = PreviousRoom.transform;
        foreach (Transform t in parent){
            if(t.tag == "SpawnPoint"){
                string key = GetKey(t);
                if(!Rooms.ContainsKey(key) && Rooms.Count < maxSize)
                {
                    GameObject currentRoom = Instantiate(GetRoom(t.GetComponent<DirID>().Dir),this.transform);
                    currentRoom.transform.position = new Vector3(t.position.x,t.position.y,t.position.y);
                    Rooms.Add(key,currentRoom);
                    SpawnRoom(currentRoom); 
                }
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
