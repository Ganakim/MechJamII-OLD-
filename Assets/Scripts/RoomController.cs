using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomClasses;

public class RoomController : MonoBehaviour {
    private Dictionary<string, GameObject> walls = new Dictionary<string, GameObject>();
    private string roomFolder = "";

    void Start() {
        walls.Add("north", transform.Find("nWall").gameObject);
        walls.Add("east", transform.Find("eWall").gameObject);
        walls.Add("south", transform.Find("sWall").gameObject);
        walls.Add("west", transform.Find("wWall").gameObject);
    }
    
    void Update() {
        
    }

    void DrawRoom(Room room, bool isMini) {
        string[] dirs = new string[]{"north", "east", "south", "west"};
        foreach (string d in dirs) {
            roomFolder += d[0];
            if (room.exit(d).wall) {
                walls[d].GetComponent<Collider>().enabled = false;
                walls[d].GetComponent<Renderer>().enabled = true;
            } else {
                walls[d].GetComponent<Collider>().isTrigger = false;
            }
        }
        GameObject floor = transform.Find("floor").gameObject;
        if (isMini) {
            floor.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/rectBase.png") as Sprite;
            floor.GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, 100/255);
        } else {
            Sprite[] floors = Resources.LoadAll<Sprite>("Sprites/rooms/" + roomFolder);
            floor.GetComponent<SpriteRenderer>().sprite = floors[Random.Range(0, floors.Length-1)];
        }
    }
}
