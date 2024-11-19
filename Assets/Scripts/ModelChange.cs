using UnityEngine;
using TMPro;

public class ModelChange : MonoBehaviour
{
    public GameObject[] sharks; // Array de tiburones
    private int currentSharkIndex = 0; // Índice del tiburón visible

    public MoveModel moveInteraction; // Referencia al script MoveModel
    public SharkInfo sharkInfo; // Referencia al script SharkInfo

    public TextMeshProUGUI NameText; // Referencia al TextMeshPro para mostrar el nombre del tiburón


    public void ShowNextShark()
    {
        sharks[currentSharkIndex].SetActive(false); // Oculta el tiburón actual
        currentSharkIndex = (currentSharkIndex + 1) % sharks.Length; // Incrementa el índice
        sharks[currentSharkIndex].SetActive(true); // Muestra el siguiente tiburón

        // Actualiza el nombre del tiburón
        UpdateSharkName();

        // Actualiza el modelo activo en moveInteraction
        moveInteraction.SetActiveShark(currentSharkIndex);

        // Actualiza los datos del tiburón activo
        sharkInfo.SetActiveShark(currentSharkIndex);
        FindObjectOfType<ConservationInfo>().SetActiveShark(currentSharkIndex);

    }

    public void ShowPreviousShark()
    {
        sharks[currentSharkIndex].SetActive(false); // Oculta el tiburón actual
        currentSharkIndex = (currentSharkIndex - 1 + sharks.Length) % sharks.Length; // Decrementa el índice
        sharks[currentSharkIndex].SetActive(true); // Muestra el tiburón anterior

        // Actualiza el nombre del tiburón
        UpdateSharkName();

        // Actualiza el modelo activo en moveInteraction
        moveInteraction.SetActiveShark(currentSharkIndex);

        // Actualiza los datos del tiburón activo
        sharkInfo.SetActiveShark(currentSharkIndex);
    }

    // Método para actualizar el nombre del tiburón en el UI
    private void UpdateSharkName()
    {
        if (NameText != null && sharkInfo != null)
        {
            // Obtén el nombre del tiburón desde el SharkInfo
            string sharkName = sharkInfo.sharks[currentSharkIndex].name;
            NameText.text = sharkName; // Actualiza el texto en el UI con el nombre
        }
    }

    void Start()
    {
        foreach (GameObject shark in sharks)
        {
            shark.SetActive(false);
        }
        sharks[0].SetActive(true); // El primer tiburón es el visible por defecto

        // Actualiza el modelo activo en moveInteraction
        if (moveInteraction != null)
        {
            moveInteraction.SetActiveShark(0);
        }

        // Actualiza el nombre del tiburón
        UpdateSharkName();

    }
}

