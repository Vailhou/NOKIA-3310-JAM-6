using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private BulletCreatorController bulletCreatorController;

    private Vector2 moveAmount;
    public Vector2 LastDirection { get; private set; } = Vector2.up;

    private Rigidbody2D rb;

    public void OnMove(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        moveAmount = context.ReadValue<Vector2>();

        if (moveAmount.Equals(Vector2.zero)) { return; }

        LastDirection = moveAmount;
    }

    public void Fire()
    {
        bulletCreatorController.Fire(gameObject.transform.position, LastDirection);
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.velocity = direction * moveSpeed;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveCharacter(moveAmount);
    }
}
