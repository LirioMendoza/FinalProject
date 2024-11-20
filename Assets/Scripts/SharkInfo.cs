using UnityEngine;
using TMPro;

[System.Serializable]
public class SharkInfoJSON
{
    public string name; // Nombre del tibur�n
    public string[] info; // Informaci�n del tibur�n
}

public class SharkInfo : MonoBehaviour
{
    public GameObject infoDialog; // Panel del cuadro de di�logo
    public TextMeshProUGUI infoText; // Texto dentro del cuadro de di�logo

    public SharkInfoJSON[] sharks; // Lista de tiburones cargados desde un arreglo en el c�digo
    private int activeInfoIndex = 0; // �ndice del dato activo
    private int currentSharkIndex = 0; // �ndice de la especie activa 

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
        // Cargar los datos predefinidos de los tiburones
        sharks = new SharkInfoJSON[]
        {
            new SharkInfoJSON
            {
                name = "Tibur�n Martillo",
                info = new string[]
                {
                    "Nombre cient�fico: Sphyrnidae.",
                    "Esperanza de vida media en estado salvaje: de 20 a 30 a�os.",
                    "Tama�o: entre 4 y 6 metros.",
                    "Peso: entre 230 y 450 kilos.",
                    "�Sab�as que...? El tibur�n martillo tambi�n cuenta con un grupo " +
                    "de �rganos sensoriales llamados ��mpulas de Lorenzini�, gracias a los " +
                    "cuales puede detectar los campos el�ctricos creados por sus presas."
                }
            },
            new SharkInfoJSON
            {
                name = "Tibur�n Ballena",
                info = new string[]
                {
                    "Nombre cient�fico: Rhincodon typus.",
                    "Tama�o: Hasta 20 metros de largo.",
                    "Peso: M�s de 20 toneladas.",
                    "�Sab�as que...? Las observaciones de cient�ficos y conservacionistas han determinado " +
                    "que cada individuo tiene un patr�n �nico de manchas, tal como una huella dactilar humana.",
                    "Tristemente, en el 2000 fue declarada especie vulnerable en la Lista Roja de la " +
                    "Uni�n Internacional para la Conservaci�n de la Naturaleza (UICN)."
                }
            },
            new SharkInfoJSON
            {
                name = "Tibur�n Blanco",
                info = new string[]
                {
                    "Nombre cient�fico: Carcharodon carcharias",
                    "Tama�o: entre 5 y 5.8 metros.",
                    "Reproducci�n: Es una especie ovoviv�para.",
                    "�Sab�as que...? Los tiburones blancos deben su nombre a su vientre blanco, pero su " +
                    "parte superior puede ser marr�n o gris.",
                    "�Sab�as que...? Los tiburones blancos pueden desplazarse por el agua a velocidades " +
                    "cercanas a los 50 kil�metros por hora."
                }
            }
        };

        // Inicia el cuadro de informaci�n desactivado
        if (infoDialog != null)
        {
            infoDialog.SetActive(false);
        }
    }
}
