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
    float speed = 0.5f;

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

    GameObject playerObject;

    int random = -1;

    bool roaming = true;

    SpawnMonster spawner;

    public int Direction { get { return random; } set { random = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SpawnMonster.Instance != null)
        {
            random = SpawnMonster.Instance.GetDifferentRandom();
        }

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

    private void FixedUpdate()
    {

        if (roaming)
        {
            //move towards opposite wall
            velocity = moveDirection * speed * Time.fixedDeltaTime;
            Vector2 newPos = (Vector2)transform.position + velocity;
            rb.MovePosition(newPos);
        }
        else
        {
            //find the coorect rotation to get to the player
            Vector3 targetPos = SpawnMonster.Instance.PlayerPosition - transform.position;
            float targetSpin = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            Quaternion turnRotation = Quaternion.Euler(0, 0, targetSpin - 90 + transform.rotation.z);
            transform.rotation = turnRotation;

            //move towards player
            Vector2 targetDirection = (Vector2)targetPos;
            velocity = targetDirection * speed * Time.fixedDeltaTime;
            Vector2 newPos = (Vector2)transform.position + velocity;
            rb.MovePosition(newPos);


            Debug.Log(SpawnMonster.Instance.PlayerPosition);
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
        switch (spawnPosition)
        {
            case StartPosition.Up:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case StartPosition.Down:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case StartPosition.Left:
                transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            case StartPosition.Right:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }
    }
}
