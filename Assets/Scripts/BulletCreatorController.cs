using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletCreatorController : MonoBehaviour
{
    [SerializeField] private BulletController bulletPrefab;

    public void FireAtDirection(Vector2 firingPosition, Vector2 direction)
    {
        BulletController bullet = Instantiate(bulletPrefab, firingPosition, new Quaternion());
        bullet.direction = direction;

        Collider2D[] shooterColliders = gameObject.GetComponents<Collider2D>();

        foreach (Collider2D shooterCollider in shooterColliders) {
            Physics2D.IgnoreCollision(shooterCollider, bullet.BulletCollider);
        }

        AudioPlayer.Instance.PlayInterruptableSFX(SFXType.Fire);
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
}
