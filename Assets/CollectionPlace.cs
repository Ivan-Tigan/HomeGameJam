using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionPlace : MonoBehaviour {

    public string[] itemsToBeCollected;
    private AudioSource audioS;
    public Sprite img;
	// Use this for initialization
	void Start () {
       audioS = gameObject.AddComponent<AudioSource>();
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "pickup")
        {
            //collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            string itemName = collision.gameObject.GetComponent<Pickup>().name;
            if(Array.IndexOf(itemsToBeCollected, itemName) > -1)
            {
                audioS.clip = collision.gameObject.GetComponent<Pickup>().soundWhenAcquired;
                audioS.Play();
                transform.parent.GetComponentInChildren<PlayerActions>().addPoint();
                SimpleCharacterControl charTemp = transform.parent.GetComponentInChildren<SimpleCharacterControl>();
                PlayerActions actionsTemp = transform.parent.GetComponentInChildren<PlayerActions>();
                
                GameObject.Find("Panel" + GameStatics.numberOfPlayers + "Players").transform.Find("PanelPlayer"+ charTemp.playerID).Find(itemName).GetComponent<Image>().sprite = img;
                Destroy(collision.gameObject);
            }
        }
    }
}
