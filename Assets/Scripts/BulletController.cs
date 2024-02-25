using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class BulletController : MonoBehaviour
{
    public Vector2 direction;
    public Collider2D BulletCollider { get; private set; }

    [SerializeField] private float moveSpeed = 35f;
    [SerializeField] private float lifeTimeSeconds = 2f;

    private Rigidbody2D rb;
    private float lifeTimeLeft;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        BulletCollider = GetComponent<Collider2D>();
        lifeTimeLeft = lifeTimeSeconds;
    }

    private void FixedUpdate()
    {
        // The Life And Death Of The Bullet
        if (lifeTimeLeft > 0)
        {
            lifeTimeLeft -= Time.fixedDeltaTime * ObjectMovements.timeScale;
        }
        else
        {
            if (!gameObject.IsDestroyed())
            {
                Destroy(gameObject);
            }
        }

        rb.velocity = moveSpeed * ObjectMovements.timeScale * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.TryGetComponent(out IBulletTarget bulletTarget);
        Destroy(gameObject);
        if (bulletTarget != null)
        {
            bulletTarget.Hit();
        }
        else
        {
            AudioPlayer.Instance.PlayInterruptableSFX(SFXType.BulletHitColliderDefault);
        }
    }
}
