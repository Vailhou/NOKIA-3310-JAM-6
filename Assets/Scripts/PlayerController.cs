using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour, IBulletTarget
{
    public Vector2 LastDirection { get; private set; } = Vector2.up;
    public Vector2 PlayerPosition { get => transform.position; }

    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private float deathDelay = 1;
    [SerializeField] private float shootingCooldownSeconds = 1;

    private BulletCreatorController bulletCreatorController;
    private Animator anim;
    private Vector2 moveAmount;
    private Rigidbody2D rb;

    private Coroutine cooldown;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TryGetComponent(out bulletCreatorController);
        TryGetComponent(out anim);
    }
    private void Update()
    {
        MoveCharacter(moveAmount);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        moveAmount = context.ReadValue<Vector2>();

        if (moveAmount.Equals(Vector2.zero)) { return; }

        LastDirection = moveAmount;

        anim.SetFloat("LastDirX", LastDirection.x);
        anim.SetFloat("LastDirY", LastDirection.y);

    }

    public void Fire()
    {
        if (bulletCreatorController == null || cooldown != null) { return; }

        cooldown = StartCoroutine(ShootingCooldown());
        bulletCreatorController.FireAtDirection(gameObject.transform.position, LastDirection);
    }

    public void Hit()
    {
        AudioPlayer.Instance.PlayUninterruptableSFX(SFXType.PlayerDeath);
        anim.SetTrigger("Death");
        SceneLoader.Instance.ReloadCurrentScene(deathDelay);
    }
    private void MoveCharacter(Vector2 direction)
    {
        rb.velocity = direction * moveSpeed;
    }

    private IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(shootingCooldownSeconds);
        cooldown = null;
    }
}
