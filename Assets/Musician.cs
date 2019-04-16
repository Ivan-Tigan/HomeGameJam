using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct PlayerSounds
{
    public AudioClip fallSound;
    public AudioClip stepSound;
    public AudioClip roundWon;
    public AudioClip gameWon;
    public AudioClip timerTick;
    public AudioClip timerGO;
}

public class Musician : MonoBehaviour {
    public AudioClip introMusic;
    public AudioClip gameplayMusic;

    public PlayerSounds playerSounds;

    
    
    private AudioSource audio;
    private bool switchedMusic = false;
    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);
        audio = GetComponent<AudioSource>();

        GameStatics.playerSounds = playerSounds;
        audio.Play();

    }
	

	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name.Equals("Attic") && !switchedMusic)
        {
            audio.Stop();
            audio.clip = gameplayMusic;
            audio.Play();
            switchedMusic = true;
        }
    }
}
