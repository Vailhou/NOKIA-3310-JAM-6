using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour, IBulletTarget
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private float deathDelay = 1;
    
    private BulletCreatorController bulletCreatorController;
    private Animator anim;

    private Vector2 moveAmount;
    private Rigidbody2D rb;

    public Vector2 LastDirection { get; private set; } = Vector2.up;
    public Vector2 PlayerPosition { get => transform.position; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TryGetComponent(out bulletCreatorController);
        TryGetComponent(out anim);
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
        if (bulletCreatorController == null) { return; }
        
        bulletCreatorController.FireAtDirection(gameObject.transform.position, LastDirection);
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.velocity = direction * moveSpeed;
    }


    private void Update()
    {
        MoveCharacter(moveAmount);
    }

    public void Hit()
    {
        AudioPlayer.Instance.PlaySFX(SFXType.PlayerDeath);
        SceneLoader.Instance.ReloadCurrentScene(deathDelay);
    }
}
