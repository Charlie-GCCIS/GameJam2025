using Unity.Jobs;
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

    public Vector3 path;

    [SerializeField]
    Vector2 moveDirection;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    Vector2 position;

    [SerializeField]
    StartPosition spawnPosition;

    

    int random = -1;
    public bool roaming = true;
    public bool onPath = true;

    GameObject playerObject;
    SpawnMonster spawner;

    public int Direction { get { return random; } set { random = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SpawnMonster.Instance != null)
        {
            random = SpawnMonster.Instance.GetDifferentRandom();
        }

        // the spawn point of the monster (Up, Down, Left, or Right)
        spawnPosition = (StartPosition)random;

        //random position throughout the row or column the monster is on
        if (spawnPosition == StartPosition.Up || spawnPosition == StartPosition.Down)
        {
            random = Random.Range(-7, 8);
        }
        else
        {
            random = Random.Range(-4, 5);
        }

        switch (spawnPosition)
        {
            case StartPosition.Up:
                moveDirection = new Vector2(0, 1);
                transform.rotation = Quaternion.Euler(0,0,0);
                path = new Vector3(random, -10, transform.position.z);
                rb.transform.position = path;
                break;
            case StartPosition.Down:
                moveDirection = new Vector2(0, -1);
                transform.rotation = Quaternion.Euler(0, 0, 180);
                path = new Vector3(random, 10, transform.position.z);
                rb.transform.position = path;
                break;
            case StartPosition.Left:
                moveDirection = new Vector2(1, 0);
                transform.rotation = Quaternion.Euler(0, 0, 270);
                path = new Vector3(-10, random, transform.position.z);
                rb.transform.position = path;
                break;
            case StartPosition.Right:
                moveDirection = new Vector2(-1, 0);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                path = new Vector3(10, random, transform.position.z);
                rb.transform.position = path;
                break;
        }
        //Debug.Log(moveDirection + " " + random);
    }

    private void FixedUpdate()
    {

        if (roaming)
        {
            if (onPath)
            {
                //move towards opposite wall
                velocity = moveDirection * speed * Time.fixedDeltaTime;
                Vector2 newPos = (Vector2)transform.position + velocity;
                rb.MovePosition(newPos);
            }
            else
            {
                //find the coorect rotation to get to the player
                Vector3 targetPos;
                if (spawnPosition == StartPosition.Up || spawnPosition == StartPosition.Down)
                {
                    targetPos = new Vector3((path.x - transform.position.x),transform.position.y,transform.position.z);
                }
                else
                {
                    targetPos = new Vector3(transform.position.x, (path.y - transform.position.y), transform.position.z);
                }
                float targetSpin = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
                Quaternion turnRotation = Quaternion.Euler(0, 0, targetSpin - 90 + transform.rotation.z);
                transform.rotation = turnRotation;

                //move towards opposite wall
                Vector2 targetDirection = (Vector2)targetPos.normalized;
                velocity = targetDirection * speed * Time.fixedDeltaTime;
                Vector2 newPos = (Vector2)transform.position + velocity;
                rb.MovePosition(newPos);
            }

            switch (spawnPosition)
            {
                case StartPosition.Up:
                    if ((path.x - transform.position.x) <= 0.25f)
                    {
                        onPath = true;
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        path.y = transform.position.y;
                    }
                    break;
                case StartPosition.Down:
                    if ((path.x - transform.position.x) <= 0.25f)
                    {
                        onPath = true;
                        transform.rotation = Quaternion.Euler(0, 0, 180);
                        path.y = transform.position.y;
                    }
                    break;
                case StartPosition.Left:
                    if ((path.y - transform.position.y) <= 0.25f)
                    {
                        onPath = true;
                        transform.rotation = Quaternion.Euler(0, 0, 270);
                        path.x = transform.position.x;
                    }
                    break;
                case StartPosition.Right:
                    if ((path.y - transform.position.y) <= 0.25f)
                    {
                        onPath = true;
                        transform.rotation = Quaternion.Euler(0, 0, 90);
                        path.x = transform.position.x;
                    }
                    break;
            }
            
        }
        else
        {
            //find the coorect rotation to get to the player
            Vector3 targetPos = SpawnMonster.Instance.PlayerPosition - transform.position;
            float targetSpin = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            Quaternion turnRotation = Quaternion.Euler(0, 0, targetSpin - 90 + transform.rotation.z);
            transform.rotation = turnRotation;

            //move towards player
            Vector2 targetDirection = (Vector2)targetPos.normalized;
            velocity = targetDirection * speed * Time.fixedDeltaTime;
            Vector2 newPos = (Vector2)transform.position + velocity;
            rb.MovePosition(newPos);


            //Debug.Log(SpawnMonster.Instance.PlayerPosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collision with the outside radius around the player
        if (collision.gameObject.tag == "Player")
        {
            roaming = false;
            onPath = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            roaming = true;
            //Debug.Log("Player");
        }
        //Debug.Log("Not Player");
        switch (spawnPosition)
        {
            case StartPosition.Up:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                path.y = transform.position.y;
                break;
            case StartPosition.Down:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                path.y = transform.position.y;
                break;
            case StartPosition.Left:
                transform.rotation = Quaternion.Euler(0, 0, 270);
                path.x = transform.position.x;
                break;
            case StartPosition.Right:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                path.x = transform.position.x;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // collision with the true player collider
            //if the player is colliding with the enemy,
                //check for the player attacking (use the spawner manager, which takes in the playerAttacking property for this step)
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector2.zero, moveDirection);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, new Vector2(path.x, transform.position.y));
    }
}
