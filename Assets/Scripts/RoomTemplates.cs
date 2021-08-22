using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedKey;
    public GameObject key;

    void Update()
    {
        if(waitTime <= 0 && spawnedKey == false)
        {
            for (int i = 0; i< rooms.Count; i++)
            {
                if(i == rooms.Count - 1)
                {
                    Instantiate(key, rooms[i].transform.position, Quaternion.identity);
                    spawnedKey = true;
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
