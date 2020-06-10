using UnityEngine;

public class DashMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public int dashSpeed;
    private float dashTime;
    public float dashStartTime;
    private int direction;

    private PlayerMovement playerMoveScript;
    private DoubleJump playerJumpScript;

    private bool isPoisoned, isGrounded;

    public int extraDash;
    private int dashCount;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = dashStartTime;
        playerMoveScript = gameObject.GetComponent<PlayerMovement>();
        playerJumpScript = gameObject.GetComponent<DoubleJump>();
        dashCount = extraDash;
    }

    // Update is called once per frame
    void Update()
    {

        isPoisoned = playerMoveScript.isPoisoned;
        isGrounded = playerJumpScript.isGrounded;

        if (dashCount > 0)
        {
            if (direction == 0)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    direction = 1;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    direction = 2;
                }
            }
            else
            {
                if (dashTime <= 0)
                {
                    direction = 0;
                    dashTime = dashStartTime;
                    rb.velocity = Vector2.zero;
                    dashCount--;
                }
                else
                {
                    dashTime -= Time.deltaTime;

                    if (isPoisoned == true)
                    {
                        if (direction == 1)
                        {
                            rb.AddForce(Vector2.right * dashSpeed);
                        }
                        else if (direction == 2)
                        {
                            rb.AddForce(Vector2.left * dashSpeed);
                        }
                    }
                    else
                    {
                        if (direction == 1)
                        {
                            rb.AddForce(Vector2.left * dashSpeed);
                        }
                        else if (direction == 2)
                        {
                            rb.AddForce(Vector2.right * dashSpeed);
                        }
                    }
                }
            }

        }
        else
        {
            if(isGrounded == true)
            {
                dashCount = extraDash;
            }
        }

    }


}
