using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int pistasEncontradas = 0;
    [SerializeField] private int totalPistas = 4;

    public void RegistrarPista()
    {
        pistasEncontradas++;
        Debug.Log("Pistas: " + pistasEncontradas + "/" + totalPistas);
    }
}
