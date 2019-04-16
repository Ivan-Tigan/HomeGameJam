using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseNumberOfPlayers : MonoBehaviour {

    public Button[] buttons;
    public int highlighted = 0;


	// Use this for initialization
	void Start () {
        


    }
   
    // Update is called once per frame
    void Update () {
		if(Input.GetButtonDown("SelectionRight"))
        {
            highlighted = mod(highlighted + 1, 3);

        }
        else if(Input.GetButtonDown("SelectionLeft"))
        {
            highlighted = mod(highlighted - 1, 3);

        }
        buttons[highlighted].Select();
        if (Input.GetButtonDown("JumpAny"))
        {
            GameStatics.numberOfPlayers = highlighted + 2;
            SceneManager.LoadScene("Attic");
        }
	}

    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}
