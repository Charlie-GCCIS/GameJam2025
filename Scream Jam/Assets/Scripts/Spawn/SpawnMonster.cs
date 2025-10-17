using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnMonster : MonoBehaviour
{

    int score = 0;

    [SerializeField]
    GameObject monster;

    [SerializeField]
    GameObject child;

    float timerMonster;
    float timerChild;
    int currentRandom;

    Vehicle player;

    [SerializeField]
    TMP_Text scoreText;

    public int CurrentRandom { get { return currentRandom; } }


    // The static instance of the GameManager.
    public static SpawnMonster Instance { get; private set; }

    // A public property to store the player's position.
    public Vector3 PlayerPosition { 
        get 
        { return Vehicle.Instance.transform.position; } 
        set { Vehicle.Instance.transform.position = value; } }
    public bool PlayerAttacked { get { return Vehicle.Instance.PlayerAttacked; } }

    public int Score { get { return score; } set { score = value; } }

    // Ensures only one instance of the GameManager exists.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Prevents the manager from being destroyed when loading new scenes.
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerMonster = 0;
        timerChild = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            timerMonster += Time.deltaTime;
            timerChild += Time.deltaTime;
            if (timerMonster >= 4)
            {
                Instantiate(monster, monster.transform.position, Quaternion.identity);
                timerMonster = -4;
            }

            timerMonster += Time.deltaTime;
            if (timerChild >= 5)
            {
                Instantiate(child, child.transform.position, Quaternion.identity);
                timerChild = -3;
            }
        }

        scoreText.text = $"Score: {score}";
    }

    public int GetDifferentRandom()
    {
        int newRandom = currentRandom;
        while (newRandom == currentRandom)
        {
            newRandom = Random.Range(0, 4);
        }
        currentRandom = newRandom;
        return newRandom;
    }
}
