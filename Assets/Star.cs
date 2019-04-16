using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {
    public Transform player;
    public float height = 5;
    // Use this for initialization
    void Start()
    {
        transform.rotation = new Quaternion(30, 0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.position;
        pos.y = player.position.y + height;
        transform.position = pos;
    }
}
