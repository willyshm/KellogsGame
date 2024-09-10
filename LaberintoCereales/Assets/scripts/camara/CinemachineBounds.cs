using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineBounds : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;  // C�mara de Cinemachine que queremos limitar
    public BoxCollider2D cameraBounds;  // BoxCollider2D que define los l�mites del escenario

    private Transform cameraTransform;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        // Obtenemos la Transform de la c�mara virtual
        cameraTransform = virtualCamera.VirtualCameraGameObject.transform;

        // Obtenemos los l�mites del BoxCollider2D
        minBounds = cameraBounds.bounds.min;
        maxBounds = cameraBounds.bounds.max;

        // Calculamos la mitad del tama�o de la c�mara en unidades del mundo
        Camera cam = Camera.main;
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;  // Esto ajusta el ancho basado en el tama�o de la pantalla
    }

    void LateUpdate()
    {
        // Limita la posici�n de la c�mara dentro de los l�mites del BoxCollider2D
        Vector3 newPosition = cameraTransform.position;

        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        // Actualizamos la posici�n de la c�mara de Cinemachine
        cameraTransform.position = newPosition;
    }
}
