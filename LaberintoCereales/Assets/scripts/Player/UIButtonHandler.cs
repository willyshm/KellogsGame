using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 direction; // Direcci�n asignada al bot�n
    private PlayerMovement playerMovement; // Referencia al script de movimiento del jugador

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>(); // Encuentra el script del jugador
    }

    // M�todo que se activa cuando el bot�n es presionado
    public void OnPointerDown(PointerEventData eventData)
    {
        if (playerMovement != null)
        {
            playerMovement.SetTouchInput(direction); // Inicia el movimiento en la direcci�n asignada
        }
    }

    // M�todo que se activa cuando el bot�n deja de ser presionado
    public void OnPointerUp(PointerEventData eventData)
    {
        if (playerMovement != null)
        {
            playerMovement.StopTouchInput(); // Detiene el movimiento cuando se suelta el bot�n
        }
    }
}
