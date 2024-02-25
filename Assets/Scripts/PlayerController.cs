using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour, IBulletTarget
{
    public Vector2 LastDirection { get; private set; } = Vector2.up;
    public Vector2 PlayerPosition { get => transform.position; }

    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private float deathDelay = 1;
    [SerializeField] private float shootingCooldownSeconds = 1;

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Animator anim;
    private BulletCreatorController bulletCreatorController;

    private Vector2 moveAmount;
    private Coroutine cooldown;
    private bool characterMoving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        TryGetComponent(out bulletCreatorController);

        playerInput.actions["Move"].canceled += OnMovementCanceled;
        playerInput.actions["Move"].started += OnMovementStarted;
    }
    private void FixedUpdate()
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

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        ObjectMovements.timeScale = 1;
    }

    private void OnMovementStarted(InputAction.CallbackContext context)
    {
        ObjectMovements.timeScale = 0;
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
        rb.velocity = moveSpeed * direction;
    }

    private IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(shootingCooldownSeconds);
        cooldown = null;
    }
}
