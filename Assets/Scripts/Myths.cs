using UnityEngine;
using TMPro;
using System.IO;

public class Myths : MonoBehaviour
{
    public GameObject mythDialog; // Panel del cuadro de di�logo
    public TextMeshProUGUI mythText; // Texto dentro del cuadro de di�logo
    private string[] myths; // Lista de mitos sobre tiburones
    private int activeMythIndex = 0; // �ndice del mito activo

    // M�todo para mostrar/ocultar el cuadro de di�logo
    public void ToggleDialog()
    {
        // Verifica si los objetos no son null antes de proceder
        if (mythDialog != null && mythText != null)
        {
            bool isActive = mythDialog.activeSelf; // Verifica si el cuadro de di�logo est� activo
            mythDialog.SetActive(!isActive); // Alterna la visibilidad del cuadro de di�logo

            // Si el cuadro se activa, actualiza el texto con el mito correspondiente
            if (!isActive) // Solo cambia el mito cuando el cuadro se activa (no cuando se desactiva)
            {
                mythText.text = myths[activeMythIndex]; // Muestra el mito actual
            }
        }
        else
        {
            Debug.LogError("Myth Dialog or Myth Text is not assigned in the Inspector!");
        }
    }

    // M�todo para cargar los mitos desde un archivo JSON
    void LoadMyths()
    {
        // Verifica si el archivo existe en la ruta correcta
        string filePath = "Assets/Scripts/myths.json"; // Ruta del archivo JSON con los mitos
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath); // Leer el contenido del archivo JSON
            MythData mythData = JsonUtility.FromJson<MythData>(jsonData); // Convertir el JSON en un objeto
            myths = mythData.myths; // Asignar los mitos al arreglo

            if (myths.Length == 0)
            {
                Debug.LogError("No myths found in the JSON file.");
            }
        }
        else
        {
            Debug.LogError("Shark myths file not found."); // Si no se encuentra el archivo, mostrar error
        }
    }

    // M�todo para cambiar al siguiente mito (solo se ejecuta cuando el cuadro se activa)
    public void NextMyth()
    {
        // Asegurarse de que la lista de mitos no est� vac�a
        if (myths.Length > 0)
        {
            // Si el �ndice supera el n�mero de mitos, lo restablece al principio
            if (activeMythIndex >= myths.Length)
            {
                activeMythIndex = 0;
            }

            // Si el cuadro de di�logo est� activo, actualiza el texto y se incrementa el �ndice
            if (mythDialog.activeSelf)
            {
                mythText.text = myths[activeMythIndex]; // Muestra el siguiente mito                           
                activeMythIndex++; // Incrementa el �ndice del mito actual
            }

            
        }
        else
        {
            Debug.LogWarning("No myths available to display.");
        }
    }

    // Clase para mapear los datos del JSON
    [System.Serializable]
    public class MythData
    {
        public string[] myths; // Arreglo de mitos que se leer�n del archivo JSON
    }

    // Start se llama antes de la primera actualizaci�n
    void Start()
    {
        LoadMyths(); // Cargar los mitos al inicio
    }
}
