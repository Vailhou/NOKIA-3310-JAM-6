using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class BulletController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 35f;
    [SerializeField] public float lifeTimeSeconds = 2f;

    private Rigidbody2D rb;
    public Vector2 direction;
    public Collider2D BulletCollider { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        BulletCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        StartCoroutine(TheLifeAndDeathOfTheBullet());
    }

    private void Update()
    {
        rb.velocity = direction * moveSpeed;
    }

    private IEnumerator TheLifeAndDeathOfTheBullet()
    {
        yield return new WaitForSeconds(lifeTimeSeconds);

        if (!gameObject.IsDestroyed())
        {
            Destroy(gameObject);
        }
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
