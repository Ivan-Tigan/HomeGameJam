using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {
    private int playerID;
    public string role;
    public GameObject pickupTriggerObj;
    public GameObject pickup;
    public float throwForce = 500;
    public float throwTimerThreshold = 1;
    public float stunMultiplier = 0.3f;
    public Animator anim;

    private bool justPickedUp;
    private float throwTimer;

    private SimpleCharacterControl playerControls;
    // Use this for initialization
    void Start () {
        playerControls = GetComponent<SimpleCharacterControl>();
        playerID = GetComponent<SimpleCharacterControl>().playerID;
        anim = GetComponent<Animator>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    public void getHit(Pickup pUp)
    {
        IEnumerator stun = playerControls.getStunned(pUp.stunDuration);
        playerControls.StartCoroutine(stun);
        if(pickup != null)
        {
            pickup = null;
        }
    }

    // Update is called once per frame
    void Update () {
        playerID = GetComponent<SimpleCharacterControl>().playerID;

       // if (Input.GetButtonDown("Joysticktest")) Debug.Log("Hellooooo");

        if(Input.GetButtonDown("Action" + playerID))
        {
            if (pickup == null && pickupTriggerObj.GetComponent<PickupTrigger>().collided != null)
            {
                pickup = pickupTriggerObj.GetComponent<PickupTrigger>().collided;
                justPickedUp = true;
            }

        }

        if (Input.GetButton("Action" + playerID) && pickup != null)
        {
            throwTimer += Time.deltaTime;
        }
        

        if (Input.GetButtonUp("Action" + playerID))
        {
            
           
                if (pickup != null && !justPickedUp)
                {
                    
                    if (throwTimer >= throwTimerThreshold)
                    {
                        pickup.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce / pickup.GetComponent<Pickup>().weight);
                    }
                    
                    pickup = null;
                }
                else
                {
                    justPickedUp = false;
                }
                
            
            throwTimer = 0;
        }

        if (pickup != null)
        {
            pickup.transform.position = pickupTriggerObj.transform.position;
        }
    }
}
