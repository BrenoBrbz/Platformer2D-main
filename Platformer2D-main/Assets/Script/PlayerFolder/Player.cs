using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    Animator animator;

    public float speed = 5f;
    public float forceJump = 5f;
    private Vector3 originalScale;

    private bool hasJumped = false; // controla se já apertou espaço

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        originalScale = transform.localScale;
    }

    void Update()
    {
        Walk();
        Jump();
        UpdateJumpAnimation();
    }

    private void Walk()
    {
        float move = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            move = -speed;
            if (transform.localScale.x > 0)
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move = speed;
            if (transform.localScale.x < 0)
                transform.localScale = new Vector3(Mathf.Abs(originalScale.x), transform.localScale.y, transform.localScale.z);
        }

        // aplica movimento horizontal
        myRigidbody.linearVelocity = new Vector2(move, myRigidbody.linearVelocity.y);

        // animação de andar
        if (animator != null && !hasJumped)
        {
            bool isWalking = move != 0;
            animator.SetBool("isWalking", isWalking);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !hasJumped)
        {
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, forceJump);

            hasJumped = true;

            if (animator != null)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isJumping", true); // ativa animação de pulo
            }
        }
    }

    private void UpdateJumpAnimation()
    {
        // se o personagem estiver caindo ou parado na Y, considera que caiu
        if (hasJumped && myRigidbody.linearVelocity.y <= 0f)
        {
            hasJumped = false;

            if (animator != null)
            {
                animator.SetBool("isJumping", false);
            }
        }
    }
}
