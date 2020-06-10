using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public int speed;
    private float moveInput;

    private Rigidbody2D rb;

    public GameObject PoisonedEffect;

    public bool isPoisoned;
    public float poisonStartTime;
    public Text poisonedText;

    private bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isPoisoned = false;
        PoisonedEffect.SetActive(false);
    }

    void FixedUpdate()
    {
        moveInput = SimpleInput.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(moveInput)); 

        if(isPoisoned == true && poisonStartTime >= 0)
        {
            rb.velocity = new Vector2(speed * -moveInput, rb.velocity.y);
            poisonStartTime -= Time.deltaTime;
            int p = (int)poisonStartTime + 1;
            poisonedText.text = p.ToString();
            PoisonedEffect.SetActive(true);
        }
        else
        {
            rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);
            PoisonedEffect.SetActive(false);
            isPoisoned = false;
           
        }

        if (isPoisoned == false)
        {
            if (facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if (facingRight == true && moveInput < 0)
            {
                Flip();
            }
        }
        else
        {
            if (facingRight == false && moveInput < 0)
            {
                Flip();
            }
            else if (facingRight == true && moveInput > 0)
            {
                Flip();
            }
        }
    }
    
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
