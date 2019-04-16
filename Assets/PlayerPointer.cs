using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointer : MonoBehaviour {
    public Transform player;
    public float height = 5;
	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {
        Vector3 pos = player.position;
        pos.y = player.position.y + height;
        transform.position = pos;
    }
}
