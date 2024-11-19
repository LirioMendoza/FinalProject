using UnityEngine;
using TMPro;

public class ModelChange : MonoBehaviour
{
    public GameObject[] sharks; // Array de tiburones
    private int currentSharkIndex = 0; // �ndice del tibur�n visible

    public MoveModel moveInteraction; // Referencia al script MoveModel
    public SharkInfo sharkInfo; // Referencia al script SharkInfo

    public TextMeshProUGUI NameText; // Referencia al TextMeshPro para mostrar el nombre del tibur�n


    public void ShowNextShark()
    {
        sharks[currentSharkIndex].SetActive(false); // Oculta el tibur�n actual
        currentSharkIndex = (currentSharkIndex + 1) % sharks.Length; // Incrementa el �ndice
        sharks[currentSharkIndex].SetActive(true); // Muestra el siguiente tibur�n

        // Actualiza el nombre del tibur�n
        UpdateSharkName();

        // Actualiza el modelo activo en moveInteraction
        moveInteraction.SetActiveShark(currentSharkIndex);

        // Actualiza los datos del tibur�n activo
        sharkInfo.SetActiveShark(currentSharkIndex);
        FindObjectOfType<ConservationInfo>().SetActiveShark(currentSharkIndex);

    }

    public void ShowPreviousShark()
    {
        sharks[currentSharkIndex].SetActive(false); // Oculta el tibur�n actual
        currentSharkIndex = (currentSharkIndex - 1 + sharks.Length) % sharks.Length; // Decrementa el �ndice
        sharks[currentSharkIndex].SetActive(true); // Muestra el tibur�n anterior

        // Actualiza el nombre del tibur�n
        UpdateSharkName();

        // Actualiza el modelo activo en moveInteraction
        moveInteraction.SetActiveShark(currentSharkIndex);

        // Actualiza los datos del tibur�n activo
        sharkInfo.SetActiveShark(currentSharkIndex);
    }

    // M�todo para actualizar el nombre del tibur�n en el UI
    private void UpdateSharkName()
    {
        if (NameText != null && sharkInfo != null)
        {
            // Obt�n el nombre del tibur�n desde el SharkInfo
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
        sharks[0].SetActive(true); // El primer tibur�n es el visible por defecto

        // Actualiza el modelo activo en moveInteraction
        if (moveInteraction != null)
        {
            moveInteraction.SetActiveShark(0);
        }

        // Actualiza el nombre del tibur�n
        UpdateSharkName();

    }
}

