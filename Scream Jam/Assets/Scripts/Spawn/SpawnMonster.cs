using UnityEngine;

public class SpawnMonster : MonoBehaviour
{

    [SerializeField]
    GameObject monster;

    float timer;
    int currentRandom;

    [SerializeField]
    GameObject playerObject;

    public int CurrentRandom { get { return currentRandom; } }


    // The static instance of the GameManager.
    public static SpawnMonster Instance { get; private set; }

    // A public property to store the player's position.
    public Vector3 PlayerPosition { get; set; }

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
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2)
        {
            Instantiate(monster, monster.transform.position, Quaternion.identity);
            timer = 0;
        }
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
