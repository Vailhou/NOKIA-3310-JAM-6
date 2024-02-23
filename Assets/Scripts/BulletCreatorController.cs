using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletCreatorController : MonoBehaviour
{
    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private float cooldownSeconds = 1;

    private bool canShoot = true;

    public void FireAtDirection(Vector2 firingPosition, Vector2 direction)
    {
        if (canShoot == false) { return; }
        BulletController bullet = Instantiate(bulletPrefab, firingPosition, new Quaternion());
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

    public void FireFireAtPosition(Vector2 firingPosition, Vector2 targetPosition)
    {
        Vector2 direction = AngleAsVectorBetweenTwoPoints(firingPosition, targetPosition);
        FireAtDirection(firingPosition, direction);
    }

    private Vector2 AngleAsVectorBetweenTwoPoints(Vector2 a, Vector2 b)
    {
        // Calculate the angle in radians
        float angleInRadians = Mathf.Atan2(b.y - a.y, b.x - a.x);

        // Convert the angle to a Vector2 representing the cosine and sine components
        return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownSeconds);
        canShoot = true;
    }
}
