using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public string name;
    public float weight;
    public float stunDuration = 0.5f;
    public float hitForceMultiplier;
    public float notConsiderAsThrownThreshold;
    public bool hasBeenThrown;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(GetComponent<Rigidbody>().velocity.magnitude <= notConsiderAsThrownThreshold)
        {
            hasBeenThrown = false;

        }
        else
        {
            hasBeenThrown = true;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (hasBeenThrown)
            {
                collision.gameObject.GetComponent<PlayerActions>().getHit(this);
                collision.gameObject.GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity * hitForceMultiplier);
            }
        }
    }
    
}
