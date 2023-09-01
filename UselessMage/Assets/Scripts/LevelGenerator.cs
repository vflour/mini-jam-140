using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    public int roomSize = 10;

    public int minSize = 50;
    public int maxSize = 300;
    Dictionary<string,GameObject> Rooms;
    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
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
        //if(!(Rooms.Count > minSize && Rooms.Count < maxSize)){
        //    foreach (Transform t in this.transform){
        //        Destroy(t.gameObject);
        //    }
        //    Debug.Log("Not within bounds, restarting.");
        //    GenerateLevel();
        //}
    }
    string GetKey(Transform t){
        int x = (int)(t.position.x/roomSize);
        int y = (int)(t.position.y/roomSize);
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
                if(!Rooms.ContainsKey(key))
                {
                    GameObject currentRoom = Instantiate(GetRoom(t.GetComponent<DirID>().Dir),this.transform);
                    currentRoom.transform.position = t.position;
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
