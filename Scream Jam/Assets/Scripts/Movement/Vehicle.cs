using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    Vector2 velocity;

    [SerializeField]
    Vector2 moveDirection;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    Vector2 position;

    int score = 0;

    Vector2 screenBounds;

    SpawnMonster spawner;

    bool attacked;

    public int Score { get { return score; } set { score = value; } }
    public bool PlayerAttacked { get { return attacked; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        position = rb.transform.position;

        //how to calculate the camera screen bounds
        screenBounds.y = Camera.main.orthographicSize;
        screenBounds.x = screenBounds.y * Camera.main.aspect;

        score = 0;
    }


    private void FixedUpdate()
    {
        if (moveDirection.magnitude > 0)
        {
            velocity = moveDirection * speed * Time.fixedDeltaTime;
            Vector2 newPos = (Vector2)transform.position + velocity;
            rb.MovePosition(newPos);
        }
        
    }

    private void Update()
    {
        if (SpawnMonster.Instance != null)
        {
            SpawnMonster.Instance.PlayerPosition = transform.position;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void onAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            attacked = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector2.zero, moveDirection);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + rb.linearVelocity);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && attacked == true)
        {
            attacked = false;
        }
    }
}
