using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private string correctCode = "1234";
    [SerializeField] private ChestController chest;

    public void Configure(ChestController targetChest, string code = "1234")
    {
        chest = targetChest;
        correctCode = code;
    }

    public bool ValidateCode(string code)
    {
        string normalizedCode = code?.Trim();
        bool valid = normalizedCode == correctCode;

        if (valid)
        {
            Debug.Log("Código correcto.");
            if (chest != null)
            {
                chest.Unlock();
            }
        }
        else
        {
            Debug.Log("Código incorrecto.");
        }

        return valid;
    }
}
