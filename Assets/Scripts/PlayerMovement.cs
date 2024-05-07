using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;
    public Vector2 m_Movement ;

    public static PlayerMovement playerMovement;

    public float moveSpeed { get; set; } = 5f;

    public bool moveEnabled { get; set; } = true;

    public Vector2 mousePos { get; set; }
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (moveEnabled)
        {
            ProcessMovementInputs();
        }
        ProcessCursorInputs();

        m_Movement.Normalize();
        
        
    }

    private void FixedUpdate()
    {
        if (moveEnabled)
        {
            rb.MovePosition(rb.position + m_Movement * (moveSpeed * Time.fixedDeltaTime));
            LookTowardsCursor();
        }
    }

    private void LookTowardsCursor()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void ProcessMovementInputs()
    {
        m_Movement.x = Input.GetAxisRaw("Horizontal");
        m_Movement.y = Input.GetAxisRaw("Vertical");
    }

    private void ProcessCursorInputs()
    {
        if (Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width &&
            Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}