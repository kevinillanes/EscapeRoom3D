using UnityEngine;
using TMPro;

public class PuzzleUI : MonoBehaviour
{
    public PuzzleManager puzzleManager;
    public TMP_InputField inputField;
    public TextMeshProUGUI mensajeTexto;

    public void ValidarCodigo()
    {
        string codigoIngresado = inputField.text;
        bool esCorrecto = puzzleManager.ValidateCode(codigoIngresado);

        if (esCorrecto)
        {
            mensajeTexto.text = "¡Código correcto! Cofre desbloqueado.";
            mensajeTexto.color = Color.green;
        }
        else
        {
            mensajeTexto.text = "Código incorrecto. Intenta de nuevo.";
            mensajeTexto.color = Color.red;
        }
    }
}