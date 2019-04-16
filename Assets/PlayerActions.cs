using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerActions : MonoBehaviour {
   
    private AudioSource audioS;
    private int playerID { get {
            return GetComponent<SimpleCharacterControl>().playerID;
        }
    }

    public int points = 0;
    public int roundsWon = 0;

    public Image pointerImage;

    public string role;
    public GameObject pickupTriggerObj;
    public GameObject pickup;
    public float throwForce = 500;
    public float throwTimerThreshold = 1;
    public float stunMultiplier = 0.3f;
    public Animator anim;

    public GameObject roundWonScreen;

    private bool justPickedUp;
    private float throwTimer;

    private SimpleCharacterControl playerControls;
    // Use this for initialization
    void Start () {
        playerControls = GetComponent<SimpleCharacterControl>();
        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
}
    public void onFallDown()
    {
        audioS.clip = GameStatics.playerSounds.fallSound;
        audioS.Play();
    }
    public void onStep()
    {
        audioS.clip = GameStatics.playerSounds.stepSound;
        audioS.Play();
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
            
            pickup.GetComponent<BoxCollider>().enabled = true;
    
            pickup = null;
        }
    }

    public int addPoint()
    {
        points = Mathf.Clamp(points + 1, 0, 3);
        if(points == 3 && roundsWon < 3)
        {
            StartCoroutine("winRound");

        }
        return points;
    }
    public IEnumerator winRound()
    {
        audioS.clip = GameStatics.playerSounds.roundWon;
        audioS.Play();
        
        yield return new WaitForSeconds(1f);

        roundsWon = Mathf.Clamp(roundsWon + 1, 0, 3);
        GameController.roundsWonDict[playerID - 1] = roundsWon;
        if (roundsWon == 3)
            win();
        else
            SceneManager.LoadScene("Attic");

    }
    public void win()
    {
        audioS.clip = GameStatics.playerSounds.gameWon;
        audioS.Play();
        GameStatics.winningPlayer = playerID;
        Destroy(GameObject.Find("GameController"));
        SceneManager.LoadScene("Win");
    }

    // Update is called once per frame
    void Update () {



        if (roundsWon == 1)
        {
            transform.parent.GetComponentsInChildren<Star>()[0].gameObject.transform.Find("star1").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            transform.parent.GetComponentsInChildren<Star>()[0].gameObject.transform.Find("star2").gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (roundsWon == 2)
        {
            transform.parent.GetComponentsInChildren<Star>()[0].gameObject.transform.Find("star1").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            transform.parent.GetComponentsInChildren<Star>()[0].gameObject.transform.Find("star2").gameObject.GetComponent<SpriteRenderer>().enabled = true;

        }
        
        else
        {
            transform.parent.GetComponentsInChildren<Star>()[0].gameObject.transform.Find("star1").gameObject.GetComponent<SpriteRenderer>().enabled = false;
            transform.parent.GetComponentsInChildren<Star>()[0].gameObject.transform.Find("star2").gameObject.GetComponent<SpriteRenderer>().enabled = false;

        }

        // if (Input.GetButtonDown("Joysticktest")) Debug.Log("Hellooooo");

        if (Input.GetButtonDown("Action" + playerID))
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
                pickup.GetComponent<BoxCollider>().enabled = true;

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
            pickup.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
