using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IBulletTarget
{
    private BulletCreatorController bulletCreatorController;


    private void Start()
    {
        TryGetComponent(out bulletCreatorController);
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.TryGetComponent(out PlayerController playerController);
        if (playerController == null) { return; }

        bulletCreatorController.FireAtDirection(transform.position, playerController.PlayerPosition);
    }
}
