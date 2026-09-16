using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [Header("Projectile")]
    public float speed = 10f;
    public float timeToDestroy = 2f;
    public int damageAmount = 1;

    private float _direction = 1f;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    public void SetDirection(float direction)
    {
        _direction = Mathf.Sign(direction);

        // Opcional: vira o sprite do projétil
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * _direction;
        transform.localScale = scale;
    }

    private void Update()
    {
        transform.Translate(
            Vector3.right * _direction * speed * Time.deltaTime,
            Space.World
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyBase enemy = collision.transform.GetComponent<EnemyBase>();

        if (enemy != null)
        {
            enemy.Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}
