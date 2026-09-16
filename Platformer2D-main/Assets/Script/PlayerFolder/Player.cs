using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator animator;

    public float speed = 5f;
    public float forceJump = 5f;

    private Vector3 originalScale;

    private bool hasJumped = false;

    public HealthBase healthBase;
    public float timeToDestroy = 1f;

    public string triggerDeath = "Death";

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        healthBase = GetComponent<HealthBase>();

        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }
    }

    private void OnPlayerKill()
    {
        if (healthBase != null)
        {
            healthBase.OnKill -= OnPlayerKill;
        }

        animator.SetTrigger(triggerDeath);
        Destroy(gameObject, timeToDestroy);
    }

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
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
            {
                transform.localScale = new Vector3(
                    -Mathf.Abs(originalScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move = speed;

            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(
                    Mathf.Abs(originalScale.x),
                    transform.localScale.y,
                    transform.localScale.z
                );
            }
        }

        // Aplica movimento horizontal
        myRigidbody.linearVelocity = new Vector2(
            move,
            myRigidbody.linearVelocity.y
        );

        // Animação de andar
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
            myRigidbody.linearVelocity = new Vector2(
                myRigidbody.linearVelocity.x,
                forceJump
            );

            hasJumped = true;

            if (animator != null)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isJumping", true);
            }
        }
    }

    private void UpdateJumpAnimation()
    {
        // Se o personagem estiver caindo ou parado na Y, considera que caiu
        if (hasJumped && myRigidbody.linearVelocity.y <= 0f)
        {
            hasJumped = false;

            if (animator != null)
            {
                animator.SetBool("isJumping", false);
            }
        }
    }

    private void OnDestroy()
    {
        if (healthBase != null)
        {
            healthBase.OnKill -= OnPlayerKill;
        }
    }
}