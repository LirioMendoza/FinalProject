using UnityEngine;
using TMPro;

public class Myths : MonoBehaviour
{
    public GameObject mythDialog; // Panel del cuadro de di�logo
    public TextMeshProUGUI mythText; // Texto dentro del cuadro de di�logo
    private string[] myths = new string[] // Arreglo con los mitos sobre tiburones
    {
        "Mito 1: Los tiburones atacan a los humanos de forma frecuente. \nFalso: No buscan atacar a los humanos, y en realidad, las personas son mucho m�s peligrosas para los tiburones que al rev�s.",
        "Mito 2: Los tiburones atacan a los humanos constantemente. \nFalso: Hay m�s de 500 especies de tiburones, y solo unas pocas son grandes y potencialmente peligrosas.",
        "Mito 3: Los tiburones son ciegos. \nFalso: Su visi�n est� adaptada para detectar movimientos y formas en condiciones de poca luz, lo cual les ayuda a localizar presas en las profundidades del oc�ano. ",
        "Mito 4: Los tiburones pueden oler sangre a kil�metros de distancia. \nFalso: Pueden detectar olores a niveles de partes por mill�n y solo en distancias relativamente cortas, especialmente si la sangre est� diluida en el agua.",
        "Mito 5: Los tiburones son animales malvados. \nFalso: Los tiburones juegan un papel crucial en los ecosistemas marinos al mantener el equilibrio de las poblaciones de otras especies."
    };
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

    // Start se llama antes de la primera actualizaci�n
    void Start()
    {
        // No es necesario cargar mitos desde un archivo JSON, ya que ya est�n definidos en el arreglo
        if (myths.Length == 0)
        {
            Debug.LogError("No myths available in the myths array.");
        }
    }
}
