using UnityEngine;
using TMPro;

public class ConservationInfo : MonoBehaviour
{
    public GameObject conservationDialog; // Panel del cuadro de di�logo
    public TextMeshProUGUI conservationText; // Texto dentro del cuadro de di�logo
    public string[] conservationStatuses; // Lista de estados de conservaci�n
    private int activeSharkIndex = 0; // �ndice del tibur�n activo

    // M�todo para mostrar/ocultar el cuadro de di�logo
    public void ToggleDialog()
    {
        bool isActive = conservationDialog.activeSelf;
        conservationDialog.SetActive(!isActive);

        if (!isActive) // Si el cuadro se activa, actualiza el texto
        {
            conservationText.text = $"Edo. de Conservaci�n: {conservationStatuses[activeSharkIndex]}";
        }
    }

    // M�todo para actualizar el tibur�n activo
    public void SetActiveShark(int index)
    {
        activeSharkIndex = index;
    }
}
