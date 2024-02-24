using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class EnemyActivationArea : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;

    private Collider2D activationCollider;

    private void Awake()
    {
        activationCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.TryGetComponent(out PlayerController playerController);
        if (playerController == null) { return; }

        enemyController.ActivateEnemy(playerController);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.TryGetComponent(out PlayerController playerController);
        if (playerController == null) { return; }

        enemyController.DeactivateEnemy();
    }
}
