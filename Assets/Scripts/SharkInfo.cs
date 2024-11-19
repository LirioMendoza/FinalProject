using UnityEngine;
using TMPro;
using System.IO;

[System.Serializable]
public class SharkInfoJSON
{
    public string name; // Nombre del tibur�n
    public string[] info; // Informaci�n del tibur�n
}

[System.Serializable]
public class SharkData
{
    public SharkInfoJSON[] sharks; // Lista de tiburones
}

public class SharkInfo : MonoBehaviour
{
    public GameObject infoDialog; // Panel del cuadro de di�logo
    public TextMeshProUGUI infoText; // Texto dentro del cuadro de di�logo

    public SharkInfoJSON[] sharks; // Lista de tiburones cargados desde el JSON
    private int activeInfoIndex = 0; // �ndice del dato activo
    private int currentSharkIndex = 0; // �ndice de la especie activa (0: martillo, 1: blanco, 2: tigre)

    // M�todos para mostrar/ocultar el cuadro de di�logo
    public void ToggleDialog()
    {
        if (infoDialog != null && infoText != null)
        {
            bool isActive = infoDialog.activeSelf; // Verifica si el cuadro de di�logo est� activo
            infoDialog.SetActive(!isActive); // Alterna la visibilidad del cuadro de di�logo

            if (!isActive) // Solo cambia el dato cuando el cuadro se activa (no cuando se desactiva)
            {
                ShowInfoForCurrentShark(); // Muestra el dato actual seg�n la especie activa
            }
        }
        else
        {
            Debug.LogError("Info Dialog or Info Text is not assigned in the Inspector!");
        }
    }

    // M�todo para mostrar la informaci�n del tibur�n actual seg�n la especie
    private void ShowInfoForCurrentShark()
    {
        if (sharks != null && sharks.Length > currentSharkIndex)
        {
            string[] infoToShow = sharks[currentSharkIndex].info; // Obtiene la informaci�n del tibur�n actual
            if (infoToShow != null && infoToShow.Length > 0)
            {
                infoText.text = infoToShow[activeInfoIndex]; // Muestra el dato activo
            }
        }
    }

    // M�todo para cambiar al siguiente dato de la especie activa
    public void NextInfo()
    {
        // Asegurarse de que hay informaci�n disponible para el tibur�n actual
        if (sharks != null && sharks.Length > currentSharkIndex)
        {
            string[] infoToShow = sharks[currentSharkIndex].info; // Obtiene la informaci�n del tibur�n actual

            // Asegurarse de que la informaci�n no est� vac�a
            if (infoToShow != null && infoToShow.Length > 0)
            {
                // Si el �ndice supera el n�mero de datos disponibles, lo restablece al principio
                if (activeInfoIndex >= infoToShow.Length)
                {
                    activeInfoIndex = 0;
                }

                // Si el cuadro de di�logo est� activo, actualiza el texto y se incrementa el �ndice
                if (infoDialog.activeSelf)
                {
                    infoText.text = infoToShow[activeInfoIndex]; // Muestra el dato activo
                    activeInfoIndex++; // Incrementa el �ndice
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



    // M�todo para actualizar el tibur�n activo (se llamar� desde ModelChange)
    public void SetActiveShark(int sharkIndex)
    {
        if (sharks != null && sharkIndex < sharks.Length)
        {
            currentSharkIndex = sharkIndex;
            activeInfoIndex = 0; // Resetea el �ndice de informaci�n cuando se cambia de tibur�n
            ShowInfoForCurrentShark(); // Muestra la primera informaci�n del tibur�n activo
        }
    }

    // Start se llama antes de la primera actualizaci�n
    void Start()
    {
        LoadSharkData(); // Cargar datos de los tiburones desde el JSON

        // Inicia el cuadro de informaci�n desactivado
        if (infoDialog != null)
        {
            infoDialog.SetActive(false);
        }
    }

    // M�todo para cargar los datos del JSON
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
