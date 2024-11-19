
using UnityEngine;

public class MoveModel : MonoBehaviour
{
    public Transform[] sharkModels; // Arreglo con todos los tiburones
    public float rotationSpeed = 0.2f;

    private Transform activeSharkModel; // Tiburón actualmente activo
    private Vector2 previousTouchPosition;
    private bool isRotating = false;

    void Start()
    {
        // Seleccionar el primer tiburón como activo por defecto
        SetActiveShark(0);
    }

    void Update()
    {
        if (activeSharkModel == null) return;

        if (Input.touchCount == 1) // Rotación con un dedo
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                previousTouchPosition = touch.position;
                isRotating = true;
            }
            else if (touch.phase == TouchPhase.Moved && isRotating)
            {
                Vector2 delta = touch.deltaPosition;
                activeSharkModel.Rotate(Vector3.up, -delta.x * rotationSpeed, Space.World);
                activeSharkModel.Rotate(Vector3.right, -delta.y * rotationSpeed, Space.World);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isRotating = false;
            }
        }
        
    }

    // Cambiar el tiburón activo
    public void SetActiveShark(int index)
    {
        for (int i = 0; i < sharkModels.Length; i++)
        {
            sharkModels[i].gameObject.SetActive(i == index);
        }

        activeSharkModel = sharkModels[index];
    }
}