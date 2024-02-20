using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;

    Vector2 moveAmount = Vector2.zero;

    private Rigidbody2D rb;

    public void OnMove(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        moveAmount = context.ReadValue<Vector2>();
    }

    void MoveCharacter(Vector2 direction)
    {
        rb.velocity = direction * moveSpeed;
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
