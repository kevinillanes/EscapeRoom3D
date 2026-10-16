using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool juegoTerminado = false;

    public void TerminarJuego()
    {
        juegoTerminado = true;
        Debug.Log("Juego terminado");
    }
}
