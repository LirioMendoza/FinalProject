using UnityEngine;
using TMPro;
using System.IO;

[System.Serializable]
public class SharkInfoJSON
{
    public string name; // Nombre del tiburón
    public string[] info; // Información del tiburón
}

[System.Serializable]
public class SharkData
{
    public SharkInfoJSON[] sharks; // Lista de tiburones
}

public class SharkInfo : MonoBehaviour
{
    public GameObject infoDialog; // Panel del cuadro de diálogo
    public TextMeshProUGUI infoText; // Texto dentro del cuadro de diálogo

    public SharkInfoJSON[] sharks; // Lista de tiburones cargados desde el JSON
    private int activeInfoIndex = 0; // Índice del dato activo
    private int currentSharkIndex = 0; // Índice de la especie activa (0: martillo, 1: blanco, 2: tigre)

    // Métodos para mostrar/ocultar el cuadro de diálogo
    public void ToggleDialog()
    {
        if (infoDialog != null && infoText != null)
        {
            bool isActive = infoDialog.activeSelf; // Verifica si el cuadro de diálogo está activo
            infoDialog.SetActive(!isActive); // Alterna la visibilidad del cuadro de diálogo

            if (!isActive) // Solo cambia el dato cuando el cuadro se activa (no cuando se desactiva)
            {
                ShowInfoForCurrentShark(); // Muestra el dato actual según la especie activa
            }
        }
        else
        {
            Debug.LogError("Info Dialog or Info Text is not assigned in the Inspector!");
        }
    }

    // Método para mostrar la información del tiburón actual según la especie
    private void ShowInfoForCurrentShark()
    {
        if (sharks != null && sharks.Length > currentSharkIndex)
        {
            string[] infoToShow = sharks[currentSharkIndex].info; // Obtiene la información del tiburón actual
            if (infoToShow != null && infoToShow.Length > 0)
            {
                infoText.text = infoToShow[activeInfoIndex]; // Muestra el dato activo
            }
        }
    }

    // Método para cambiar al siguiente dato de la especie activa
    public void NextInfo()
    {
        // Asegurarse de que hay información disponible para el tiburón actual
        if (sharks != null && sharks.Length > currentSharkIndex)
        {
            string[] infoToShow = sharks[currentSharkIndex].info; // Obtiene la información del tiburón actual

            // Asegurarse de que la información no esté vacía
            if (infoToShow != null && infoToShow.Length > 0)
            {
                // Si el índice supera el número de datos disponibles, lo restablece al principio
                if (activeInfoIndex >= infoToShow.Length)
                {
                    activeInfoIndex = 0;
                }

                // Si el cuadro de diálogo está activo, actualiza el texto y se incrementa el índice
                if (infoDialog.activeSelf)
                {
                    infoText.text = infoToShow[activeInfoIndex]; // Muestra el dato activo
                    activeInfoIndex++; // Incrementa el índice
                }
            }
            else
            {
                Debug.LogWarning("No information available for the current shark.");
            }
        }
        else
        {
            Debug.LogError("Sharks data is null or currentSharkIndex is out of range.");
        }
    }



    // Método para actualizar el tiburón activo (se llamará desde ModelChange)
    public void SetActiveShark(int sharkIndex)
    {
        if (sharks != null && sharkIndex < sharks.Length)
        {
            currentSharkIndex = sharkIndex;
            activeInfoIndex = 0; // Resetea el índice de información cuando se cambia de tiburón
            ShowInfoForCurrentShark(); // Muestra la primera información del tiburón activo
        }
    }

    // Start se llama antes de la primera actualización
    void Start()
    {
        LoadSharkData(); // Cargar datos de los tiburones desde el JSON

        // Inicia el cuadro de información desactivado
        if (infoDialog != null)
        {
            infoDialog.SetActive(false);
        }
    }

    // Método para cargar los datos del JSON
    void LoadSharkData()
    {
        string filePath = "Assets/Scripts/sharkData.json"; // Ruta del archivo JSON

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath); // Leer el archivo JSON
            SharkData sharkData = JsonUtility.FromJson<SharkData>(jsonData); // Deserializar el JSON
            if (sharkData != null)
            {
                sharks = sharkData.sharks; // Asignar los tiburones cargados desde el JSON
                Debug.Log("Shark data loaded successfully.");
            }
            else
            {
                Debug.LogError("Error: Failed to deserialize JSON data.");
            }
        }
        else
        {
            Debug.LogError("Error: Shark data file not found.");
        }
    }
}
