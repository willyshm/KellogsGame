using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapWithImage : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador en el mundo 3D
    public RectTransform minimapIcon; // El ícono del jugador en el minimapa
    public RectTransform minimapImage; // La imagen del mapa dentro del Canvas

    // El tamaño del mundo real (ajústalo según el tamaño de tu nivel)
    public Vector2 worldSize = new Vector2(100, 100);

    void Update()
    {
        // Obtener la posición del jugador en el plano XZ
        Vector2 playerPosInWorld = new Vector2(player.position.x, player.position.z);

        // Normalizar la posición del jugador en el rango 0-1 dentro del mundo
        Vector2 normalizedPos = new Vector2(
            (playerPosInWorld.x + (worldSize.x / 2)) / worldSize.x,
            (playerPosInWorld.y + (worldSize.y / 2)) / worldSize.y
        );

        // Convertir la posición normalizada en coordenadas dentro del minimapa
        Vector2 minimapSize = minimapImage.sizeDelta;
        Vector2 minimapPos = new Vector2(
            normalizedPos.x * minimapSize.x - (minimapSize.x / 2),
            normalizedPos.y * minimapSize.y - (minimapSize.y / 2)
        );

        // Asegúrate de que el ícono esté dentro del área visible del minimapa
        minimapPos.x = Mathf.Clamp(minimapPos.x, -minimapSize.x / 2, minimapSize.x / 2);
        minimapPos.y = Mathf.Clamp(minimapPos.y, -minimapSize.y / 2, minimapSize.y / 2);

        // Actualizar la posición del ícono del jugador en el minimapa
        minimapIcon.anchoredPosition = minimapPos;
    }
}
