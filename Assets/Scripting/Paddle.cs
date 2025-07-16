using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerType { Player1, Player2 }

public class Paddle : MonoBehaviour
{
    public PlayerType currentPlayer;
    public float speed = 10f;
    public float boundary = 4f;

    [SerializeField] private InputAction input;
    private float moveDirection;

    void Awake()
    {
        input.performed += OnMovePerformed;
        input.canceled += OnMoveCanceled;
    }

    void OnEnable()
    {
        input.Enable();
    }
    
    void OnDisable()
    {
        input.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<float>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveDirection = 0;
    }

    void Update()
    {
        float movement = this.moveDirection * speed * Time.deltaTime;
        transform.Translate(0f, movement, 0f);

        float clampedY = Mathf.Clamp(transform.position.y, -boundary, boundary);
        transform.position = new Vector3(transform.position.x, clampedY, 0f);
    }
}
