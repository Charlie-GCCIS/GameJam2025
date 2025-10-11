using UnityEngine;

public class SpawnMonster : MonoBehaviour
{

    [SerializeField]
    GameObject monster;

    float timer;
    int currentRandom;

    public int CurrentRandom { get { return currentRandom; } }


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
