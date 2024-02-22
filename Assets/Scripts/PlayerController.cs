using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IBulletTarget
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private BulletCreatorController bulletCreatorController;
    [SerializeField] private float deathDelay = 1;

    [SerializeField] private Animator anim;

    private Vector2 moveAmount;
    public Vector2 LastDirection { get; private set; } = Vector2.up;

    private Rigidbody2D rb;

    public void OnMove(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        moveAmount = context.ReadValue<Vector2>();
        if (moveAmount.Equals(Vector2.zero)) { return; }

        LastDirection = moveAmount;
        //if (moveAmount.x + moveAmount.y != 0)
        //{
        //    LastDirection = moveAmount;
        //    Debug.Log(LastDirection.x + " " + LastDirection.y);

            //Animator hommia
        //    anim.SetFloat("LastDirX", LastDirection.x);
        //    anim.SetFloat("LastDirY", LastDirection.y);
        //}
    }

    public void Fire()
    {
        bulletCreatorController.Fire(gameObject.transform.position, LastDirection);
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.velocity = direction * moveSpeed;

        //Animator hommia
        anim.SetFloat("Velocity", rb.velocity.magnitude);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
