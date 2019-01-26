using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    [Range(2,4)]
    public static int numberOfPlayers;
    public GameObject[] playerUnitsToInstantiate;
    private GameObject[] playerUnits; 

	// Use this for initialization
	void Start () {
		
	}
	
    void startRound()
    {
     //   for(int i = 0; i < numberOfPlayers; i++)
       // {
            //Instantiate(playerUnitToInstantiate, )
       // }
    } 

	// Update is called once per frame
	void Update () {
		
	}
}
