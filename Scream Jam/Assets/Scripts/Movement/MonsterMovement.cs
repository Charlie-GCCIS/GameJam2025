using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum StartPosition
{
    // Which spawn point the monster has
    Up,
    Down,
    Left,
    Right
}

public class MonsterMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    [SerializeField]
    Vector2 velocity;

    [SerializeField]
    Vector2 moveDirection;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    Vector2 position;

    [SerializeField]
    StartPosition spawnPosition;

    [SerializeField]
    SpawnMonster spawner;

    [SerializeField]
    GameObject playerObject;

    bool roaming = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int random = spawner.GetDifferentRandom();
        spawnPosition = (StartPosition)random;

        switch (spawnPosition)
        {
            case StartPosition.Up:
                moveDirection = new Vector2(0, 1);
                transform.rotation = Quaternion.Euler(0,0,0);
                rb.transform.position = new Vector3(0,-10,transform.position.z);
                break;
            case StartPosition.Down:
                moveDirection = new Vector2(0, -1);
                transform.rotation = Quaternion.Euler(0, 0, 180);
                rb.transform.position = new Vector3(0, 10, transform.position.z);
                break;
            case StartPosition.Left:
                moveDirection = new Vector2(1, 0);
                transform.rotation = Quaternion.Euler(0, 0, 270);
                rb.transform.position = new Vector3(-10, 0, transform.position.z);
                break;
            case StartPosition.Right:
                moveDirection = new Vector2(-1, 0);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                rb.transform.position = new Vector3(10, 0, transform.position.z);
                break;
        }
        Debug.Log(moveDirection + " " + random);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (roaming)
        {
            velocity = moveDirection * speed * Time.fixedDeltaTime;
            Vector2 newPos = (Vector2)transform.position + velocity;
            rb.MovePosition(newPos);
        }
        else
        {
            Vector3 targetPos = playerObject.transform.position - transform.position;
            float targetSpin = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            Quaternion turnRotation = Quaternion.Euler(0, 0, targetSpin + transform.rotation.z);
            transform.rotation = turnRotation;
            Debug.Log(playerObject.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            roaming = false;
            Debug.Log("Player");
        }
        Debug.Log("Not Player");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            roaming = true;
            Debug.Log("Player");
        }
        Debug.Log("Not Player");
    }
}
