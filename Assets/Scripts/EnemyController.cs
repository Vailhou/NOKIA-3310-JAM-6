using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IBulletTarget
{
    private BulletCreatorController bulletCreatorController;
    [SerializeField] private float shootingCooldown = 1;
    private Coroutine shootingAndCooldown;

    private void Start()
    {
        TryGetComponent(out bulletCreatorController);
    }

    public void Hit()
    {
        DeactivateEnemy();
        AudioPlayer.Instance.PlayInterruptableSFX(SFXType.EnemyDeath);
        Destroy(gameObject);
    }

    public void ActivateEnemy(PlayerController playerController)
    {
        shootingAndCooldown = StartCoroutine(ShootingLoop(playerController));
    }

    public void DeactivateEnemy()
    {
        StopCoroutine(shootingAndCooldown);
    }

    private IEnumerator ShootingLoop(PlayerController playerController)
    {
        bulletCreatorController.FireFireAtPosition(transform.position, playerController.PlayerPosition);
        yield return new WaitForSeconds(shootingCooldown);
        shootingAndCooldown = StartCoroutine(ShootingLoop(playerController));
    }
}
