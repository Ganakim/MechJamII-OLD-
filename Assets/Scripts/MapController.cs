using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomClasses;

public class MapController : MonoBehaviour {
    private bool isActive = false;
    Dictionary<string, GameObject> map = new Dictionary<string, GameObject>();

    void Start() {
        gameObject.SetActive(false);
    }

    void Update() {
        if (gameObject.activeSelf && !isActive) {
            isActive = true;
            DrawMap();
        }
        if (!gameObject.activeSelf && isActive) {
            isActive = false;
            ClearMap();
        }
    }

    public void DrawMap() {
        // Dictionary<string, Room> rooms = GameObject.Find("GameManager").GetComponent("GameManager").level;

    }

    public void ClearMap() {
        // Dictionary<string, Room> rooms = GameObject.Find("GameManager").GetComponent("GameManager").level;

    }
}
