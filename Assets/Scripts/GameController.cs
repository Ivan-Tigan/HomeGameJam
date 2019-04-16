using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] panelItems;

    public GameObject loadingScreen;

    public Sprite[] pointers;

    public GameObject[] itemsToSpawn;

    public static int[] roundsWonDict = { 0, 0, 0, 0 };

    private AudioSource audioS;

    public float timerSpeed = 1f;



    public Dictionary<string, Sprite> nameToSprite {
        get {
            return new Dictionary<string, Sprite> {

                    {"Fish Bowl", (Sprite) Resources.Load<Sprite>("Icons/fishbowl")},
        {"Onesie", (Sprite) Resources.Load<Sprite>("Icons/onesie")},
        {"Backpack", (Sprite) Resources.Load<Sprite>("Icons/backpack")},
        {"Sombrero", (Sprite) Resources.Load<Sprite>("Icons/sombrero")},
        {"Boots", (Sprite) Resources.Load<Sprite>("Icons/boots")},
        {"Plastic Gun", (Sprite) Resources.Load<Sprite>("Icons/plasticgun")},
        {"Garden Hose", (Sprite) Resources.Load<Sprite>("Icons/hose")},
        {"Snowboard Jacket", (Sprite) Resources.Load<Sprite>("Icons/snowboardjacket")},
        {"Old Fireman Helmet", (Sprite) Resources.Load<Sprite>("Icons/fireman_helmet")},
        {"Bucket", (Sprite) Resources.Load<Sprite>("Icons/bucket")},
        {"Bin Lid", (Sprite) Resources.Load<Sprite>("Icons/lid")},
        {"Ruler", (Sprite) Resources.Load<Sprite>("Icons/ruler")},
        {"Eyepatch", (Sprite) Resources.Load<Sprite>("Icons/eyepatch")},
        {"Clothes Hanger", (Sprite) Resources.Load<Sprite>("Icons/hanger")},
        {"Cane", (Sprite) Resources.Load<Sprite>("Icons/cane")},
        {"Old Bridesmaid Dress", (Sprite) Resources.Load<Sprite>("Icons/dress")},
        {"Old Bracelet", (Sprite) Resources.Load<Sprite>("Icons/bracelet")},
        {"Heels", (Sprite) Resources.Load<Sprite>("Icons/heels")},
        {"Face Mask", (Sprite) Resources.Load<Sprite>("Icons/mask")},
        {"Blanket", (Sprite) Resources.Load<Sprite>("Icons/blanket")},
        {"Pyjamas", (Sprite) Resources.Load<Sprite>("Icons/pyjamas")},
        {"Traffic Cone", (Sprite) Resources.Load<Sprite>("Icons/cone")},
        {"Bathrobe", (Sprite) Resources.Load<Sprite>("Icons/bathrobe")},
        {"Pencil", (Sprite) Resources.Load<Sprite>("Icons/pencil")},
        {"White Shirt", (Sprite) Resources.Load<Sprite>("Icons/whiteshirt")},
        {"Goggles", (Sprite) Resources.Load<Sprite>("Icons/googles")},
        {"Glass Bottle", (Sprite) Resources.Load<Sprite>("Icons/bottle")},
        {"Paper Bag", (Sprite) Resources.Load<Sprite>("Icons/paperbag")},
        {"Spatula", (Sprite) Resources.Load<Sprite>("Icons/spatula")},
        {"Frying Pan", (Sprite) Resources.Load<Sprite>("Icons/pan")}


            };
        }
    }

    public string[] roles
    {
        get
        {
            return new string[] {

                 "Astronaut",
                 "Cowboy",
     //            "Fireman",
                 "Knight",
                 "Pirate",
                 "Superhero",
   //              "Princess",
                 "Wizard",
                 "Scientist",
                 "Chef"
            };
        }
    }

    public Dictionary<string, string[]> rolesToItems = new Dictionary<string, string[]>{
                                               {"Astronaut", new string[]{ "Fish Bowl", "Onesie", "Backpack" } },
                                               {"Cowboy", new string[]{ "Sombrero", "Boots", "Plastic Gun" } },
                      //                         {"Fireman", new string[]{ "Garden Hose", "Snowboard Jacket", "Old Fireman Helmet" } },
                                               {"Knight", new string[]{ "Bucket", "Bin Lid", "Ruler" } },
                                               {"Pirate", new string[]{ "Eyepatch", "Clothes Hanger", "Cane" } },
                        //                       {"Princess", new string[]{ "Old Bridesmaid Dress", "Old Bracelet", "Heels" } },
                                               {"Superhero", new string[]{ "Face Mask", "Blanket", "Pyjamas" } },
                                               {"Wizard", new string[]{ "Traffic Cone", "Bathrobe", "Pencil" } },
                                               {"Scientist", new string[]{ "White Shirt", "Goggles", "Glass Bottle" } },
                                               {"Chef", new string[]{ "Paper Bag", "Spatula", "Frying Pan" } }

    };

    public GameObject[] playerUnitsToInstantiate;
    private List<GameObject> playerUnitsReferences = new List<GameObject>();

    private Dictionary<int, Vector3[]> spawnPositions
    {
        get
        {
            return new Dictionary<int, Vector3[]> {
                {
                    2, new Vector3[]
                    {
                        new Vector3(-1.5f, -0.4f, -0.2f),
                        new Vector3(1.5f, -0.4f, -0.2f)
                    }
                },
                {
                    3, new Vector3[]
                    {
                        new Vector3(-2f, -0.4f, -0.2f),
                        new Vector3(0f, -0.4f, -0.2f),
                        new Vector3(2f, -0.4f, -0.2f)
                    }
                },
                {
                    4, new Vector3[]
                    {
                        new Vector3(-3f, -0.4f, -0.2f),
                        new Vector3(-1f, -0.4f, -0.2f),
                        new Vector3(1f, -0.4f, -0.2f),
                        new Vector3(3f, -0.4f, -0.2f)
                    }
                }

            };
        }
    }
    
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
        audioS = gameObject.AddComponent<AudioSource>(); 
        StartCoroutine("InitializeGame");
    }
    public IEnumerator InitializeGame()
    {
        StartCoroutine("spawnItems");
        yield return new WaitForSeconds(0);

    }
    public IEnumerator spawnItems()
    {
        Time.timeScale = 100;
        for (int i = 0; i < itemsToSpawn.Length; i++)
        {
            GameObject go = itemsToSpawn[i];
            yield return new WaitForSeconds(0.1f);
            Instantiate(go, new Vector3(Random.Range(-0.5f, 0.5f), 1, Random.Range(-0.5f, 0.5f)), new Quaternion());
        }
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.5f);

        StartCoroutine("disableLoadingScreen");



    }

    public IEnumerator disableLoadingScreen()
    {
        for (float i = 1; i >= 0; i -= 0.01f)
        {
            loadingScreen.GetComponent<CanvasGroup>().alpha = i;
            yield return new WaitForSeconds(0.001f);
        }
        StartCoroutine("spawnPlayers");
    }

    public IEnumerator spawnPlayers()
    {
        for (int i = 0; i < GameStatics.numberOfPlayers; i++)
        {

            Vector3 spawnAtTemp = spawnPositions[GameStatics.numberOfPlayers][i];


            GameObject temp = Instantiate(playerUnitsToInstantiate[i], spawnAtTemp, new Quaternion());


            playerUnitsReferences.Add(temp);
        }

        panelItems[GameStatics.numberOfPlayers-2].SetActive(true);
        for (int i = 0; i < GameStatics.numberOfPlayers; i++)
        {
            playerUnitsReferences[i].GetComponentInChildren<SimpleCharacterControl>().playerID = i + 1;
            string randomRole = roles[Random.Range(0, roles.Length)];
            playerUnitsReferences[i].GetComponentInChildren<PlayerActions>().role = randomRole;
            playerUnitsReferences[i].GetComponentInChildren<Text>().text = randomRole;
            //playerUnitsReferences[i].GetComponentInChildren<Text>().alignment = TextAnchor.MiddleCenter;

            PlayerActions actionsTemp = playerUnitsReferences[i].GetComponentInChildren<PlayerActions>();
            actionsTemp.roundsWon = roundsWonDict[i];

            playerUnitsReferences[i].GetComponentInChildren<CollectionPlace>().itemsToBeCollected = rolesToItems[randomRole];
            playerUnitsReferences[i].GetComponentInChildren<PlayerActions>().pointerImage.sprite = pointers[i];


            Transform panelPlayerI = panelItems[GameStatics.numberOfPlayers - 2].transform.GetChild(i);

            Image[] imagesTemp = panelPlayerI.gameObject.GetComponentsInChildren<Image>();
           // if(imagesTemp == null) Debug.Log("WOHOOOOOOo2");
           // else Debug.Log("ahaaa2");
            for (int j = 0; j<3; j++)
            {
                Debug.Log(rolesToItems[randomRole][j]);
                Debug.Log(nameToSprite[rolesToItems[randomRole][j]]);

                imagesTemp[j].sprite = nameToSprite[rolesToItems[randomRole][j]];
                imagesTemp[j].gameObject.name = rolesToItems[randomRole][j];
            }
            


        }

        //      for (int i = 0; i < playerUnitsReferences.Capacity; i++)
        //      {
        //          playerUnitsReferences[i].GetComponentInChildren<SimpleCharacterControl>().m_animator.SetFloat("Vertical", 1f);
        //
        //    }


        yield return new WaitForSeconds(1f);
        for (int i = 0; i < GameStatics.numberOfPlayers; i++)
        {
            playerUnitsReferences[i].GetComponentInChildren<SimpleCharacterControl>().m_controlMode = SimpleCharacterControl.ControlMode.Stop;


        }
        GameObject.Find("TimerStartGame").GetComponentInChildren<Text>().text = "3";
        audioS.clip = GameStatics.playerSounds.timerTick;
        audioS.Play();

        yield return new WaitForSeconds(timerSpeed);
        GameObject.Find("TimerStartGame").GetComponentInChildren<Text>().text = "2";
        audioS.Play();

        yield return new WaitForSeconds(timerSpeed);
        GameObject.Find("TimerStartGame").GetComponentInChildren<Text>().text = "1";
        audioS.Play();

        yield return new WaitForSeconds(timerSpeed);
        GameObject.Find("TimerStartGame").GetComponentInChildren<Text>().text = "GO!";
        audioS.clip = GameStatics.playerSounds.timerGO;
        audioS.Play();

        yield return new WaitForSeconds(0.5f);
        GameObject.Find("TimerStartGame").GetComponentInChildren<Text>().text = "";

        for (int i = 0; i < GameStatics.numberOfPlayers; i++)
        {
            playerUnitsReferences[i].GetComponentInChildren<SimpleCharacterControl>().m_controlMode = SimpleCharacterControl.ControlMode.Direct;
            playerUnitsReferences[i].GetComponentInChildren<Rigidbody>().useGravity = true;
            playerUnitsReferences[i].GetComponentInChildren<Collider>().enabled = true;

        }


    }



}
