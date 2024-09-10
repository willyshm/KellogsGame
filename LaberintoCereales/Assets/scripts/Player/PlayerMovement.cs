using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput; // Direcci�n de movimiento
    public float speed = 5f; // Velocidad de movimiento
    private bool facingRight = true; // Para controlar hacia d�nde mira el personaje

    private PlayerControls controls; // Sistema de entrada
    private Rigidbody2D rb; // Rigidbody2D para aplicar las f�sicas
    private SpriteRenderer spriteRenderer; // Para cambiar la orientaci�n del personaje

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

        // Configura las acciones espec�ficas para el movimiento t�ctil
        controls.Player.MoveUp.performed += _ => SetTouchInput(Vector2.up);
        controls.Player.MoveDown.performed += _ => SetTouchInput(Vector2.down);
        controls.Player.MoveLeft.performed += _ => SetTouchInput(Vector2.left);
        controls.Player.MoveRight.performed += _ => SetTouchInput(Vector2.right);

        // Detiene el movimiento t�ctil cuando se libera el bot�n
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

        // Elimina las referencias a los eventos t�ctiles
        controls.Player.MoveUp.performed -= _ => SetTouchInput(Vector2.up);
        controls.Player.MoveDown.performed -= _ => SetTouchInput(Vector2.down);
        controls.Player.MoveLeft.performed -= _ => SetTouchInput(Vector2.left);
        controls.Player.MoveRight.performed -= _ => SetTouchInput(Vector2.right);

        // Elimina las referencias a los eventos de cancelaci�n del movimiento t�ctil
        controls.Player.MoveUp.canceled -= _ => StopTouchInput();
        controls.Player.MoveDown.canceled -= _ => StopTouchInput();
        controls.Player.MoveLeft.canceled -= _ => StopTouchInput();
        controls.Player.MoveRight.canceled -= _ => StopTouchInput();
    }

    // M�todo que se activa con el teclado para actualizar la direcci�n de movimiento
    private void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>(); // Lee la entrada del teclado
    }

    // M�todo llamado por los botones t�ctiles para asignar la direcci�n
    public void SetTouchInput(Vector2 direction)
    {
        movementInput = direction; // Actualiza la entrada de movimiento t�ctil
    }

    // M�todo para detener el movimiento cuando se suelta el bot�n t�ctil
    public void StopTouchInput()
    {
        movementInput = Vector2.zero; // Detiene el movimiento
    }

    private void FixedUpdate()
    {
        // Aplica el movimiento al Rigidbody2D basado en la entrada actual
        Vector2 move = movementInput * speed;
        rb.velocity = move;

        // Voltea el personaje seg�n la direcci�n horizontal del movimiento
        if (move.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (move.x < 0 && facingRight)
        {
            Flip();
        }
    }

    // M�todo para voltear el personaje
    private void Flip()
    {
        facingRight = !facingRight; // Cambia la direcci�n a la que est� mirando el personaje
        Vector3 theScale = transform.localScale; // Obtiene el tama�o del personaje
        theScale.x *= -1; // Invierte el valor de x para voltear el personaje
        transform.localScale = theScale; // Aplica la escala invertida
    }
}
