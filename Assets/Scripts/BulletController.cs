using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 35f;

    private Rigidbody2D rb;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(TheLifeAndDeathOfTheBullet());
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * moveSpeed;
    }

    private IEnumerator TheLifeAndDeathOfTheBullet()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
