using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public enum EscapeRoomState
{
    Exploring,
    ChestUnlocked,
    ChestOpened,
    KeyCollected,
    Escaped
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool HasKey { get; private set; }
    public bool IsEnteringCode => activePuzzle != null;
    public EscapeRoomState State { get; private set; } = EscapeRoomState.Exploring;

    private PuzzleManager activePuzzle;
    private string codeInput = string.Empty;
    private string statusMessage = "Explora la habitacion y encuentra el cofre.";
    private float messageUntil;
    private GUIStyle titleStyle;
    private GUIStyle bodyStyle;
    private GUIStyle centeredStyle;
    private int initializationRuns;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        InitializeGameState();
    }

    private void Initialize()
    {
        initializationRuns++;
        Debug.Log($"[CASO 16] Initialize ejecutado {initializationRuns} vez/veces.");

        if (initializationRuns > 1)
        {
            ShowMessage("ERROR CASO 16: el juego se inicializo dos veces.", 10f);
        }
    }

    private void InitializeGameState()
    {
        Initialize();
    }

    private void Update()
    {
        if (activePuzzle == null || Keyboard.current == null)
        {
            return;
        }

        Keyboard keyboard = Keyboard.current;

        TryAddDigit(keyboard.digit0Key, "0");
        TryAddDigit(keyboard.digit1Key, "1");
        TryAddDigit(keyboard.digit2Key, "2");
        TryAddDigit(keyboard.digit3Key, "3");
        TryAddDigit(keyboard.digit4Key, "4");
        TryAddDigit(keyboard.digit5Key, "5");
        TryAddDigit(keyboard.digit6Key, "6");
        TryAddDigit(keyboard.digit7Key, "7");
        TryAddDigit(keyboard.digit8Key, "8");
        TryAddDigit(keyboard.digit9Key, "9");
        TryAddDigit(keyboard.numpad0Key, "0");
        TryAddDigit(keyboard.numpad1Key, "1");
        TryAddDigit(keyboard.numpad2Key, "2");
        TryAddDigit(keyboard.numpad3Key, "3");
        TryAddDigit(keyboard.numpad4Key, "4");
        TryAddDigit(keyboard.numpad5Key, "5");
        TryAddDigit(keyboard.numpad6Key, "6");
        TryAddDigit(keyboard.numpad7Key, "7");
        TryAddDigit(keyboard.numpad8Key, "8");
        TryAddDigit(keyboard.numpad9Key, "9");

        if (keyboard.backspaceKey.wasPressedThisFrame && codeInput.Length > 0)
        {
            codeInput = codeInput.Substring(0, codeInput.Length - 1);
        }

        if (keyboard.escapeKey.wasPressedThisFrame)
        {
            activePuzzle = null;
            codeInput = string.Empty;
            ShowMessage("Entrada de codigo cancelada.");
        }

        if (keyboard.enterKey.wasPressedThisFrame || keyboard.numpadEnterKey.wasPressedThisFrame)
        {
            ValidateCurrentCode();
        }
    }

    public void BeginPuzzle(PuzzleManager puzzle)
    {
        if (puzzle == null || State >= EscapeRoomState.ChestUnlocked)
        {
            return;
        }

        activePuzzle = puzzle;
        codeInput = string.Empty;
        ShowMessage("Escribe los cuatro numeros de la pista y pulsa ENTER.", 30f);
    }

    public void NotifyChestUnlocked()
    {
        if (State < EscapeRoomState.ChestUnlocked)
        {
            State = EscapeRoomState.ChestUnlocked;
        }

        activePuzzle = null;
        codeInput = string.Empty;
        ShowMessage("Codigo correcto. Acercate al cofre y pulsa E para abrirlo.", 7f);
    }

    public void NotifyChestOpened()
    {
        if (State < EscapeRoomState.ChestOpened)
        {
            State = EscapeRoomState.ChestOpened;
        }

        ShowMessage("Cofre abierto. Recoge la llave dorada con E.", 7f);
    }

    public void ObtainKey()
    {
        if (HasKey)
        {
            return;
        }

        HasKey = true;
        State = EscapeRoomState.KeyCollected;
        ShowMessage("Llave obtenida. Ve a la puerta de salida y pulsa E.", 8f);
    }

    public void FinishGame()
    {
        State = EscapeRoomState.Escaped;
        ShowMessage("ESCAPE COMPLETADO", 60f);
    }

    public void ShowMessage(string message, float duration = 4f)
    {
        statusMessage = message;
        messageUntil = Time.unscaledTime + duration;
    }

    private void TryAddDigit(KeyControl key, string digit)
    {
        if (key != null && key.wasPressedThisFrame && codeInput.Length < 4)
        {
            codeInput += digit;
        }
    }

    private void ValidateCurrentCode()
    {
        if (activePuzzle.ValidateCode(codeInput))
        {
            activePuzzle = null;
            codeInput = string.Empty;
        }
        else
        {
            codeInput = string.Empty;
            ShowMessage("Codigo incorrecto. Revisa la pista e intenta de nuevo.", 5f);
        }
    }

    private void OnGUI()
    {
        BuildStyles();

        float width = Mathf.Min(520f, Screen.width - 24f);
        GUI.Box(new Rect(12f, 12f, width, 78f), GUIContent.none);
        GUI.Label(new Rect(22f, 17f, 180f, 24f), "ESCAPE ROOM 3D", titleStyle);
        GUI.Label(new Rect(202f, 18f, width - 212f, 22f), "Mover: WASD/Flechas   Interactuar: E", bodyStyle);
        GUI.Label(new Rect(24f, 47f, width - 24f, 24f), CurrentObjective(), bodyStyle);

        if (Time.unscaledTime <= messageUntil || State == EscapeRoomState.Escaped)
        {
            float messageWidth = Mathf.Min(700f, Screen.width - 40f);
            Rect messageRect = new Rect((Screen.width - messageWidth) * 0.5f, Screen.height - 62f, messageWidth, 44f);
            GUI.Box(messageRect, GUIContent.none);
            GUI.Label(messageRect, statusMessage, centeredStyle);
        }

        if (activePuzzle != null)
        {
            float boxWidth = Mathf.Min(520f, Screen.width - 40f);
            float boxHeight = 165f;
            Rect puzzleRect = new Rect((Screen.width - boxWidth) * 0.5f, (Screen.height - boxHeight) * 0.5f, boxWidth, boxHeight);
            GUI.Box(puzzleRect, GUIContent.none);
            GUI.Label(new Rect(puzzleRect.x + 20f, puzzleRect.y + 14f, boxWidth - 40f, 30f), "CODIGO DEL COFRE", titleStyle);
            GUI.Label(new Rect(puzzleRect.x + 20f, puzzleRect.y + 49f, boxWidth - 40f, 45f), FormatCode(), centeredStyle);
            GUI.Label(new Rect(puzzleRect.x + 20f, puzzleRect.y + 100f, boxWidth - 40f, 48f), "Numeros + ENTER. ESC cancela.", centeredStyle);
        }

        if (State == EscapeRoomState.Escaped)
        {
            Rect winRect = new Rect(Screen.width * 0.5f - 240f, Screen.height * 0.5f - 65f, 480f, 130f);
            GUI.Box(winRect, GUIContent.none);
            GUI.Label(winRect, "¡ESCAPASTE!\nLa puerta esta abierta y el juego termino.", centeredStyle);
        }
    }

    private string CurrentObjective()
    {
        switch (State)
        {
            case EscapeRoomState.ChestUnlocked:
                return "Objetivo: abre el cofre.";
            case EscapeRoomState.ChestOpened:
                return "Objetivo: recoge la llave.";
            case EscapeRoomState.KeyCollected:
                return "Objetivo: abre la puerta de salida.";
            case EscapeRoomState.Escaped:
                return "Objetivo completado.";
            default:
                return "Objetivo: busca la pista y desbloquea el cofre.";
        }
    }

    private string FormatCode()
    {
        string result = string.Empty;
        for (int i = 0; i < 4; i++)
        {
            result += i < codeInput.Length ? codeInput[i].ToString() : "_";
            if (i < 3)
            {
                result += "   ";
            }
        }
        return result;
    }

    private void BuildStyles()
    {
        if (titleStyle != null)
        {
            return;
        }

        titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            normal = { textColor = new Color(1f, 0.82f, 0.25f) }
        };

        bodyStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 13,
            alignment = TextAnchor.MiddleLeft,
            normal = { textColor = Color.white }
        };

        centeredStyle = new GUIStyle(bodyStyle)
        {
            fontSize = 17,
            wordWrap = true,
            alignment = TextAnchor.MiddleCenter
        };
    }
}
