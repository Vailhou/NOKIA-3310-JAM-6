using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private BulletCreatorController bulletCreatorController;

    private Vector2 moveAmount = Vector2.zero;
    public Vector2 LastDirection { get; private set; }

    private Rigidbody2D rb;

    public void OnMove(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        moveAmount = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        bulletCreatorController.Fire();
    }

    void MoveCharacter(Vector2 direction)
    {
        rb.velocity = direction * moveSpeed;
        LastDirection = direction;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveCharacter(moveAmount);
    }
}
