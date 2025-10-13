using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    bool playerAttacked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player is colliding with the enemy, check for the player attacking (use the spawner manager, which takes in the playerAttacking property for this step)
    }
}
