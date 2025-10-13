using UnityEngine;

public class ChildrenMovement : MonoBehaviour
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

    SpawnMonster spawner;

    public int Direction { get { return random; } set { random = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SpawnMonster.Instance != null)
        {
            random = SpawnMonster.Instance.GetDifferentRandom();
        }

        // the spawn point of the child (Up, Down, Left, or Right)
        spawnPosition = (StartPosition)random;

        //random position throughout the row or column the child is on
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
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rb.transform.position = new Vector3(random, -10, transform.position.z);
                break;
            case StartPosition.Down:
                moveDirection = new Vector2(0, -1);
                transform.rotation = Quaternion.Euler(0, 0, 180);
                rb.transform.position = new Vector3(random, 10, transform.position.z);
                break;
            case StartPosition.Left:
                moveDirection = new Vector2(1, 0);
                transform.rotation = Quaternion.Euler(0, 0, 270);
                rb.transform.position = new Vector3(-10, random, transform.position.z);
                break;
            case StartPosition.Right:
                moveDirection = new Vector2(-1, 0);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                rb.transform.position = new Vector3(10, random, transform.position.z);
                break;
        }
        Debug.Log(moveDirection + " " + random + " child");
    }

    private void FixedUpdate()
    {
        //move towards opposite wall
        velocity = moveDirection * speed * Time.fixedDeltaTime;
        Vector2 newPos = (Vector2)transform.position + velocity;
        rb.MovePosition(newPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player")
        //{
        //    //Debug.Log("Player");
        //}
        //Debug.Log("Not Player");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player")
        //{
        //    //Debug.Log("Player");
        //}
        //Debug.Log("Not Player");
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
