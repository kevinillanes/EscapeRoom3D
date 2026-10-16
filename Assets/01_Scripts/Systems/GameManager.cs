using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int pistasEncontradas = 0;
    [SerializeField] private int totalPistas = 4;
    [SerializeField] private bool juegoTerminado = false;

    public void RegistrarPista()
    {
        pistasEncontradas++;
        Debug.Log("Pistas: " + pistasEncontradas + "/" + totalPistas);
    }

    public void TerminarJuego()
    {
        juegoTerminado = true;
        Debug.Log("Juego terminado");
    }
}
