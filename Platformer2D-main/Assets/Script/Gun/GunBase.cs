using System.Collections;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [Header("References")]
    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;
    public Transform playerSideReference;

    [Header("Shoot Settings")]
    public float timeBetweenShoot = 3f;

    private Coroutine _currentCoroutine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_currentCoroutine == null)
            {
                _currentCoroutine = StartCoroutine(StartShoot());
            }
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            StopShooting();
        }
    }

    private IEnumerator StartShoot()
    {
        while (true)
        {
            Shoot();

            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    private void StopShooting()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }

    public void Shoot()
    {
        if (prefabProjectile == null)
        {
            Debug.LogError("Projectile Prefab não foi configurado!");
            return;
        }

        if (positionToShoot == null)
        {
            Debug.LogError("Position To Shoot não foi configurado!");
            return;
        }

        if (playerSideReference == null)
        {
            Debug.LogError("Player Side Reference não foi configurado!");
            return;
        }

        // Pega apenas a direção que o personagem está olhando
        float side = Mathf.Sign(playerSideReference.localScale.x);

        // Cria o projétil EXATAMENTE na posição da arma
        ProjectileBase projectile = Instantiate(
            prefabProjectile,
            positionToShoot.position,
            Quaternion.identity
        );

        // Informa ao projétil para qual lado ele deve ir
        projectile.SetDirection(side);
    }
}
