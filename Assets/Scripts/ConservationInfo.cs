using UnityEngine;
using TMPro;

public class ConservationInfo : MonoBehaviour
{
    public GameObject conservationDialog; // Panel del cuadro de diálogo
    public TextMeshProUGUI conservationText; // Texto dentro del cuadro de diálogo
    public string[] conservationStatuses; // Lista de estados de conservación
    private int activeSharkIndex = 0; // Índice del tiburón activo

    // Método para mostrar/ocultar el cuadro de diálogo
    public void ToggleDialog()
    {
        bool isActive = conservationDialog.activeSelf;
        conservationDialog.SetActive(!isActive);

        if (!isActive) // Si el cuadro se activa, actualiza el texto
        {
            conservationText.text = $"Edo. de Conservación: {conservationStatuses[activeSharkIndex]}";
        }
    }

    // Método para actualizar el tiburón activo
    public void SetActiveShark(int index)
    {
        activeSharkIndex = index;
    }
}
