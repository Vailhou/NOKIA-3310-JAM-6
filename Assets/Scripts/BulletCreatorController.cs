using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 
/// </summary>
public class BulletCreatorController : MonoBehaviour
{
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private float cooldownSeconds = 1;

    private bool canShoot = true;

    public void Fire(Vector2 position, Vector2 direction)
    {
        if (canShoot == false) { return; }
        BulletController bullet = Instantiate(bulletPrefab, position, new Quaternion());
        bullet.direction = direction;
        
        gameObject.TryGetComponent(out Collider2D shooterCollider);
        bullet.gameObject.TryGetComponent(out Collider2D bulletCollider);

        if (shooterCollider != null && bulletCollider != null)
        {
            Physics2D.IgnoreCollision(shooterCollider, bulletCollider);
        }

        AudioPlayer.Instance.PlaySFX(SFXType.Fire);
        canShoot = false;
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownSeconds);
        canShoot = true;
    }
}
