using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IBulletTarget
{
    private BulletCreatorController bulletCreatorController;
    [SerializeField] private float shootingCooldown = 1;

    private float cooldownLeft = 0.1f;
    private bool enemyActive = false;
    private Coroutine shootingAndCooldown;
    private PlayerController player;

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
        //shootingAndCooldown = StartCoroutine(ShootingLoop(playerController));
        player = playerController;
        enemyActive = true;
    }

    public void DeactivateEnemy()
    {
        //StopCoroutine(shootingAndCooldown);
        enemyActive = false;
    }

    private IEnumerator ShootingLoop(PlayerController playerController)
    {
        bulletCreatorController.FireFireAtPosition(transform.position, playerController.PlayerPosition);
        yield return new WaitForSeconds(shootingCooldown);
        shootingAndCooldown = StartCoroutine(ShootingLoop(playerController));
    }

    private void FixedUpdate()
    {
        if (enemyActive == false) { return; }

        if (cooldownLeft > 0)
        {
            cooldownLeft -= Time.fixedDeltaTime * ObjectMovements.timeScale;
        }
        else
        {
            bulletCreatorController.FireFireAtPosition(transform.position, player.PlayerPosition);
            cooldownLeft = shootingCooldown;
        }
    }
}
