using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 direction; // Dirección asignada al botón
    private PlayerMovement playerMovement; // Referencia al script de movimiento del jugador

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>(); // Encuentra el script del jugador
    }

    // Método que se activa cuando el botón es presionado
    public void OnPointerDown(PointerEventData eventData)
    {
        if (playerMovement != null)
        {
            playerMovement.SetTouchInput(direction); // Inicia el movimiento en la dirección asignada
        }
    }

    // Método que se activa cuando el botón deja de ser presionado
    public void OnPointerUp(PointerEventData eventData)
    {
        if (playerMovement != null)
        {
            playerMovement.StopTouchInput(); // Detiene el movimiento cuando se suelta el botón
        }
    }
}
