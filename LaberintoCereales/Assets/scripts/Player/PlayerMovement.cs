using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput; // Dirección de movimiento
    public float speed = 5f; // Velocidad de movimiento
    private bool facingRight = true; // Para controlar hacia dónde mira el personaje

    private PlayerControls controls; // Sistema de entrada
    private Rigidbody2D rb; // Rigidbody2D para aplicar las físicas
    private SpriteRenderer spriteRenderer; // Para cambiar la orientación del personaje

    private void Awake()
    {
        controls = new PlayerControls(); // Inicializa los controles
        rb = GetComponent<Rigidbody2D>(); // Obtiene el Rigidbody2D del jugador
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtiene el SpriteRenderer del jugador
    }

    private void OnEnable()
    {
        controls.Enable(); // Habilita los controles

        // Configura las acciones para el teclado
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;

        // Configura las acciones específicas para el movimiento táctil
        controls.Player.MoveUp.performed += _ => SetTouchInput(Vector2.up);
        controls.Player.MoveDown.performed += _ => SetTouchInput(Vector2.down);
        controls.Player.MoveLeft.performed += _ => SetTouchInput(Vector2.left);
        controls.Player.MoveRight.performed += _ => SetTouchInput(Vector2.right);

        // Detiene el movimiento táctil cuando se libera el botón
        controls.Player.MoveUp.canceled += _ => StopTouchInput();
        controls.Player.MoveDown.canceled += _ => StopTouchInput();
        controls.Player.MoveLeft.canceled += _ => StopTouchInput();
        controls.Player.MoveRight.canceled += _ => StopTouchInput();
    }

    private void OnDisable()
    {
        controls.Disable(); // Deshabilita los controles

        // Elimina las referencias a los eventos de teclado
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnMove;

        // Elimina las referencias a los eventos táctiles
        controls.Player.MoveUp.performed -= _ => SetTouchInput(Vector2.up);
        controls.Player.MoveDown.performed -= _ => SetTouchInput(Vector2.down);
        controls.Player.MoveLeft.performed -= _ => SetTouchInput(Vector2.left);
        controls.Player.MoveRight.performed -= _ => SetTouchInput(Vector2.right);

        // Elimina las referencias a los eventos de cancelación del movimiento táctil
        controls.Player.MoveUp.canceled -= _ => StopTouchInput();
        controls.Player.MoveDown.canceled -= _ => StopTouchInput();
        controls.Player.MoveLeft.canceled -= _ => StopTouchInput();
        controls.Player.MoveRight.canceled -= _ => StopTouchInput();
    }

    // Método que se activa con el teclado para actualizar la dirección de movimiento
    private void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>(); // Lee la entrada del teclado
    }

    // Método llamado por los botones táctiles para asignar la dirección
    public void SetTouchInput(Vector2 direction)
    {
        movementInput = direction; // Actualiza la entrada de movimiento táctil
    }

    // Método para detener el movimiento cuando se suelta el botón táctil
    public void StopTouchInput()
    {
        movementInput = Vector2.zero; // Detiene el movimiento
    }

    private void FixedUpdate()
    {
        // Aplica el movimiento al Rigidbody2D basado en la entrada actual
        Vector2 move = movementInput * speed;
        rb.velocity = move;

        // Voltea el personaje según la dirección horizontal del movimiento
        if (move.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (move.x < 0 && facingRight)
        {
            Flip();
        }
    }

    // Método para voltear el personaje
    private void Flip()
    {
        facingRight = !facingRight; // Cambia la dirección a la que está mirando el personaje
        Vector3 theScale = transform.localScale; // Obtiene el tamaño del personaje
        theScale.x *= -1; // Invierte el valor de x para voltear el personaje
        transform.localScale = theScale; // Aplica la escala invertida
    }
}
