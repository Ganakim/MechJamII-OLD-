using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

namespace CustomClasses {
    [Serializable]
    public class Room : MonoBehaviour {
        public int index;
        private static gameManager gameManager = GameObject.Find("GameManager").GetComponent<gameManager>();
        private Dictionary<int, Room> level = gameManager.level;
        private static int maxX = gameManager.maxX;
        private static int maxY = gameManager.maxY;
        private static int minRooms = gameManager.minRooms;
        private static int maxRooms = gameManager.maxRooms;
        private static int density = gameManager.density;
        public List<string> tags;
        private static Dictionary<string, string> opposites = new Dictionary<string, string>{
            {"north", "south"},
            {"east",  "west"},
            {"south",  "north"},
            {"west",  "east"}
        };
        private Dictionary<string, string> _walls = new Dictionary<string, string>();

        public (Room room, Boolean wall, Boolean oob, int i) exit(string dir) {
            Dictionary<string, int> dirs = new Dictionary<string, int>{
                {"north", this.index - maxX},
                {"east", this.index + 1},
                {"south", this.index + maxX},
                {"west", this.index - 1}
            };
            if (this._walls.ContainsKey(dir)) {
                Debug.Log(this.index + ": There's a wall " + dir + " of me!" + (this._walls[dir] == "oob" ? " It's the edge of the world!!" : ""));
                return (null, this._walls[dir] == "wall" || this._walls[dir] == "oob", this._walls[dir] == "oob", dirs[dir]);
            } else {
                if (level.ContainsKey(dirs[dir])) {
                    Debug.Log(this.index + ": There's a Room " + dir + " of me!");
                    if (level[dirs[dir]].exit(opposites[dir]).wall) {
                        Debug.Log(this.index + ": But it's blocked by a wall.");
                        return (level[dirs[dir]], true, false, dirs[dir]);
                    } else {
                        Debug.Log("And the door is wide open!");
                        return (level[dirs[dir]], false, false, dirs[dir]);
                    }
                } else {
                    Debug.Log(this.index + ": No Room detected " + dir + " of me!");
                    return (null, false, false, dirs[dir]);
                }
            }
        }

        public Room(int index, string req = null) {
            this.index = index;
            if (this.index - maxX < 0) {
                this._walls.Add("north", "oob");
            } else if (level.ContainsKey(this.index - maxX) && level[this.index - maxX].exit("south").wall) {
                this._walls.Add("north", "wall");
            }
            if (Math.Floor((decimal) (this.index + 1) % maxX) <= 0) {
                this._walls.Add("east", "oob");
            } else if (level.ContainsKey(this.index + 1) && level[this.index + 1].exit("west").wall) {
                this._walls.Add("east", "wall");
            }
            if (this.index >= maxX * (maxY - 1)) {
                this._walls.Add("south", "oob");
            } else if (level.ContainsKey(this.index + maxX) && level[this.index + maxX].exit("north").wall) {
                this._walls.Add("south", "wall");
            }
            if (this.index % maxX == 0) {
                this._walls.Add("west", "oob");
            } else if (level.ContainsKey(this.index - 1) && level[this.index - 1].exit("east").wall) {
                this._walls.Add("west", "wall");
            }
            string[] d = new string[]{"north", "east", "south", "west"};
            gameManager.shuffle(d);
            foreach (string dir in d) {
                if (!this._walls.ContainsKey(dir) && this._walls.Count < 3 && Random.Range(0, 100) <= density) {
                    this._walls.Add(dir, "wall");
                }
            }
        }
    }
}