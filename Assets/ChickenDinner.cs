using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenDinner : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = "Player " + GameStatics.winningPlayer + " wins!";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
