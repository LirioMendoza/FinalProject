using UnityEngine;
using TMPro;

[System.Serializable]
public class SharkInfoJSON
{
    public string name; // Nombre del tiburón
    public string[] info; // Información del tiburón
}

public class SharkInfo : MonoBehaviour
{
    public GameObject infoDialog; // Panel del cuadro de diálogo
    public TextMeshProUGUI infoText; // Texto dentro del cuadro de diálogo

    public SharkInfoJSON[] sharks; // Lista de tiburones cargados desde un arreglo en el código
    private int activeInfoIndex = 0; // Índice del dato activo
    private int currentSharkIndex = 0; // Índice de la especie activa 

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
        // Cargar los datos predefinidos de los tiburones
        sharks = new SharkInfoJSON[]
        {
            new SharkInfoJSON
            {
                name = "Tiburón Martillo",
                info = new string[]
                {
                    "Nombre científico: Sphyrnidae.",
                    "Esperanza de vida media en estado salvaje: de 20 a 30 años.",
                    "Tamaño: entre 4 y 6 metros.",
                    "Peso: entre 230 y 450 kilos.",
                    "¿Sabías que...? El tiburón martillo también cuenta con un grupo " +
                    "de órganos sensoriales llamados “ámpulas de Lorenzini”, gracias a los " +
                    "cuales puede detectar los campos eléctricos creados por sus presas."
                }
            },
            new SharkInfoJSON
            {
                name = "Tiburón Ballena",
                info = new string[]
                {
                    "Nombre científico: Rhincodon typus.",
                    "Tamaño: Hasta 20 metros de largo.",
                    "Peso: Más de 20 toneladas.",
                    "¿Sabías que...? Las observaciones de científicos y conservacionistas han determinado " +
                    "que cada individuo tiene un patrón único de manchas, tal como una huella dactilar humana.",
                    "Tristemente, en el 2000 fue declarada especie vulnerable en la Lista Roja de la " +
                    "Unión Internacional para la Conservación de la Naturaleza (UICN)."
                }
            },
            new SharkInfoJSON
            {
                name = "Tiburón Blanco",
                info = new string[]
                {
                    "Nombre científico: Carcharodon carcharias",
                    "Tamaño: entre 5 y 5.8 metros.",
                    "Reproducción: Es una especie ovovivípara.",
                    "¿Sabías que...? Los tiburones blancos deben su nombre a su vientre blanco, pero su " +
                    "parte superior puede ser marrón o gris.",
                    "¿Sabías que...? Los tiburones blancos pueden desplazarse por el agua a velocidades " +
                    "cercanas a los 50 kilómetros por hora."
                }
            }
        };

        // Inicia el cuadro de información desactivado
        if (infoDialog != null)
        {
            infoDialog.SetActive(false);
        }
    }
}
