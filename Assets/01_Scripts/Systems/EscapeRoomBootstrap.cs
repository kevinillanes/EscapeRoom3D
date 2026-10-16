using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeRoomBootstrap : MonoBehaviour
{
    private static readonly string[] LegacyObjects =
    {
        "Chest",
        "Medieval_Chest",
        "Cubo_Prueba_Interaccion",
        "Pista_1",
        "Pista_2",
        "Pista_3",
        "Pista_4",
        "PuzzleManager",
        "Canvas",
        "floor"
    };

    private Material floorMaterial;
    private Material wallMaterial;
    private Material woodMaterial;
    private Material darkWoodMaterial;
    private Material goldMaterial;
    private Material clueMaterial;
    private Material doorMaterial;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Install()
    {
        if (FindAnyObjectByType<EscapeRoomBootstrap>() != null)
        {
            return;
        }

        GameObject bootstrapObject = new GameObject("_EscapeRoomRuntime");
        bootstrapObject.AddComponent<EscapeRoomBootstrap>();
    }

    private void Awake()
    {
        DisableLegacyObjects();
        CreateMaterials();
        ConfigureStaticCamera();
        ConfigureLighting();
        BuildRoom();

        GameManager gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager == null)
        {
            gameManager = new GameObject("GameManager").AddComponent<GameManager>();
        }

        PlayerController player = ConfigurePlayer();
        BuildGameplayObjects();

        if (player == null)
        {
            Debug.LogError("No se pudo crear el jugador.");
        }

        gameManager.ShowMessage("Pista: los pedestales azules forman el codigo 1 - 2 - 3 - 4.", 8f);
        Debug.Log("[ESCAPE ROOM] Juego listo: camara fija, cofre, llave y puerta creados.");
    }

    private void DisableLegacyObjects()
    {
        Scene scene = SceneManager.GetActiveScene();
        foreach (GameObject rootObject in scene.GetRootGameObjects())
        {
            if (IsLegacyObject(rootObject.name))
            {
                rootObject.SetActive(false);
            }
        }
    }

    private bool IsLegacyObject(string objectName)
    {
        foreach (string legacyName in LegacyObjects)
        {
            if (objectName == legacyName)
            {
                return true;
            }
        }

        return false;
    }

    private void CreateMaterials()
    {
        floorMaterial = CreateMaterial(new Color(0.16f, 0.18f, 0.21f));
        wallMaterial = CreateMaterial(new Color(0.29f, 0.25f, 0.22f));
        woodMaterial = CreateMaterial(new Color(0.33f, 0.13f, 0.055f));
        darkWoodMaterial = CreateMaterial(new Color(0.12f, 0.045f, 0.025f));
        goldMaterial = CreateMaterial(new Color(1f, 0.58f, 0.06f), true);
        clueMaterial = CreateMaterial(new Color(0.12f, 0.55f, 0.75f), true);
        doorMaterial = CreateMaterial(new Color(0.18f, 0.07f, 0.035f));
    }

    private Material CreateMaterial(Color color, bool emission = false)
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null)
        {
            shader = Shader.Find("Standard");
        }

        Material material = new Material(shader);
        material.color = color;

        if (emission && material.HasProperty("_EmissionColor"))
        {
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", color * 1.6f);
        }

        return material;
    }

    private void ConfigureStaticCamera()
    {
        Camera camera = Camera.main;
        if (camera == null)
        {
            camera = new GameObject("Main Camera").AddComponent<Camera>();
            camera.gameObject.tag = "MainCamera";
            camera.gameObject.AddComponent<AudioListener>();
        }

        camera.transform.SetParent(null);
        camera.transform.position = new Vector3(0f, 7f, -12.5f);
        camera.transform.rotation = Quaternion.LookRotation(new Vector3(0f, 1.1f, 1.8f) - camera.transform.position);
        camera.orthographic = false;
        camera.fieldOfView = 58f;
        camera.nearClipPlane = 0.1f;
        camera.farClipPlane = 100f;
        camera.backgroundColor = new Color(0.035f, 0.045f, 0.065f);
    }

    private void ConfigureLighting()
    {
        Light mainLight = FindAnyObjectByType<Light>();
        if (mainLight == null)
        {
            mainLight = new GameObject("Directional Light").AddComponent<Light>();
            mainLight.type = LightType.Directional;
        }

        mainLight.transform.rotation = Quaternion.Euler(48f, -35f, 0f);
        mainLight.color = new Color(1f, 0.86f, 0.67f);
        mainLight.intensity = 1.35f;

        RenderSettings.ambientLight = new Color(0.22f, 0.24f, 0.3f);
    }

    private void BuildRoom()
    {
        GameObject room = new GameObject("EscapeRoom");
        room.transform.SetParent(transform);

        CreateCube("Floor", new Vector3(0f, -0.15f, 0.8f), new Vector3(16f, 0.3f, 14f), floorMaterial, room.transform);
        CreateCube("BackWall", new Vector3(0f, 2.4f, 7.75f), new Vector3(16f, 4.8f, 0.3f), wallMaterial, room.transform);
        CreateCube("LeftWall", new Vector3(-7.85f, 2.4f, 0.8f), new Vector3(0.3f, 4.8f, 14f), wallMaterial, room.transform);
        CreateCube("RightWall", new Vector3(7.85f, 2.4f, 0.8f), new Vector3(0.3f, 4.8f, 14f), wallMaterial, room.transform);

        CreateCube("Carpet", new Vector3(0f, 0.025f, 0.5f), new Vector3(4.2f, 0.05f, 10f), darkWoodMaterial, room.transform);
        CreateCube("ChestPlatform", new Vector3(-4.6f, 0.15f, 3.4f), new Vector3(3.6f, 0.3f, 2.8f), darkWoodMaterial, room.transform);

        CreateClue("1", new Vector3(-5.6f, 0.35f, -3.8f), room.transform);
        CreateClue("2", new Vector3(5.5f, 0.35f, -2.4f), room.transform);
        CreateClue("3", new Vector3(-1.8f, 0.35f, 4.9f), room.transform);
        CreateClue("4", new Vector3(2.1f, 0.35f, 2.8f), room.transform);
    }

    private PlayerController ConfigurePlayer()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        if (player == null)
        {
            GameObject playerObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            playerObject.name = "Player";
            playerObject.GetComponent<Renderer>().material = clueMaterial;
            player = playerObject.AddComponent<PlayerController>();
            playerObject.AddComponent<PlayerInteraction>();
            playerObject.AddComponent<Rigidbody>();
        }

        player.gameObject.SetActive(true);
        player.transform.position = new Vector3(0f, 0.55f, -3.2f);
        player.transform.rotation = Quaternion.identity;

        if (player.GetComponent<PlayerInteraction>() == null)
        {
            player.gameObject.AddComponent<PlayerInteraction>();
        }

        Rigidbody body = player.GetComponent<Rigidbody>();
        if (body == null)
        {
            body = player.gameObject.AddComponent<Rigidbody>();
        }

        body.useGravity = false;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;

        Transform model = player.transform.Find("Dinosaurio");
        if (model != null)
        {
            NormalizePlayerModel(player.transform, model);

            DinosaurMotionAnimator motionAnimator = player.GetComponent<DinosaurMotionAnimator>();
            if (motionAnimator == null)
            {
                motionAnimator = player.gameObject.AddComponent<DinosaurMotionAnimator>();
            }

            motionAnimator.Configure(model);
        }

        return player;
    }

    private void BuildGameplayObjects()
    {
        GameObject gameplay = new GameObject("Gameplay");
        gameplay.transform.SetParent(transform);

        GameObject chestObject = new GameObject("AnimatedChest");
        chestObject.transform.SetParent(gameplay.transform);
        Vector3 chestPosition = new Vector3(-4.6f, 0.3f, 3.4f);
        chestObject.transform.position = chestPosition;

        BoxCollider chestCollider = chestObject.AddComponent<BoxCollider>();
        chestCollider.center = new Vector3(0f, 0.8f, 0f);
        chestCollider.size = new Vector3(2.5f, 1.8f, 1.7f);

        CreateCube("ChestBase", new Vector3(0f, 0.55f, 0f), new Vector3(2.4f, 1.1f, 1.6f), woodMaterial, chestObject.transform, true);
        CreateCube("ChestBand", new Vector3(0f, 0.58f, -0.82f), new Vector3(0.22f, 1.15f, 0.08f), goldMaterial, chestObject.transform, true);

        GameObject lidPivot = new GameObject("LidPivot");
        lidPivot.transform.SetParent(chestObject.transform);
        lidPivot.transform.localPosition = new Vector3(0f, 1.08f, 0.72f);
        CreateCube("ChestLid", new Vector3(0f, 0.28f, -0.72f), new Vector3(2.45f, 0.55f, 1.65f), woodMaterial, lidPivot.transform, true);
        CreateCube("LidBand", new Vector3(0f, 0.3f, -1.56f), new Vector3(0.22f, 0.58f, 0.08f), goldMaterial, lidPivot.transform, true);

        Light chestLight = new GameObject("ChestLight").AddComponent<Light>();
        chestLight.transform.SetParent(chestObject.transform);
        chestLight.transform.localPosition = new Vector3(0f, 2.2f, -0.4f);
        chestLight.type = LightType.Point;
        chestLight.color = new Color(1f, 0.56f, 0.18f);
        chestLight.range = 5f;
        chestLight.intensity = 2.2f;

        GameObject key = BuildKey(chestPosition + new Vector3(0f, 1.45f, 0f), gameplay.transform);
        PuzzleManager puzzle = new GameObject("PuzzleManager_Runtime").AddComponent<PuzzleManager>();
        puzzle.transform.SetParent(gameplay.transform);

        ChestController chest = chestObject.AddComponent<ChestController>();
        puzzle.Configure(chest, "1234");
        chest.Configure(lidPivot.transform, key, puzzle);

        BuildDoor(gameplay.transform);
    }

    private void NormalizePlayerModel(Transform player, Transform model)
    {
        model.localPosition = Vector3.zero;
        model.localScale = Vector3.one;

        Renderer[] renderers = model.GetComponentsInChildren<Renderer>(true);
        if (renderers.Length == 0)
        {
            return;
        }

        Bounds bounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
        {
            bounds.Encapsulate(renderers[i].bounds);
        }

        float scale = 2.2f / Mathf.Max(bounds.size.y, 0.01f);
        model.localScale = Vector3.one * scale;

        bounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
        {
            bounds.Encapsulate(renderers[i].bounds);
        }

        Vector3 correction = player.position - bounds.center;
        correction.y = 0.05f - bounds.min.y;
        model.position += correction;
    }

    private GameObject BuildKey(Vector3 position, Transform parent)
    {
        GameObject key = new GameObject("GoldenKey");
        key.transform.SetParent(parent);
        key.transform.position = position;
        key.AddComponent<SphereCollider>().radius = 0.9f;
        key.AddComponent<KeyItem>();

        GameObject shaft = CreateCylinder("KeyShaft", Vector3.zero, new Vector3(0.11f, 0.65f, 0.11f), goldMaterial, key.transform, true);
        shaft.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        CreateCylinder("KeyBow", new Vector3(-0.67f, 0f, 0f), new Vector3(0.38f, 0.08f, 0.38f), goldMaterial, key.transform, true);
        CreateCube("KeyToothA", new Vector3(0.63f, -0.18f, 0f), new Vector3(0.22f, 0.35f, 0.18f), goldMaterial, key.transform, true);
        CreateCube("KeyToothB", new Vector3(0.86f, -0.12f, 0f), new Vector3(0.18f, 0.24f, 0.18f), goldMaterial, key.transform, true);
        return key;
    }

    private void BuildDoor(Transform parent)
    {
        GameObject frame = new GameObject("ExitDoorFrame");
        frame.transform.SetParent(parent);
        frame.transform.position = new Vector3(4.6f, 0f, 7.35f);

        CreateCube("FrameLeft", new Vector3(-0.2f, 1.7f, 0f), new Vector3(0.25f, 3.4f, 0.5f), goldMaterial, frame.transform, true);
        CreateCube("FrameRight", new Vector3(2.05f, 1.7f, 0f), new Vector3(0.25f, 3.4f, 0.5f), goldMaterial, frame.transform, true);
        CreateCube("FrameTop", new Vector3(0.92f, 3.35f, 0f), new Vector3(2.5f, 0.25f, 0.5f), goldMaterial, frame.transform, true);

        GameObject pivot = new GameObject("DoorPivot");
        pivot.transform.SetParent(frame.transform);
        pivot.transform.localPosition = Vector3.zero;
        CreateCube("DoorPanel", new Vector3(0.92f, 1.65f, 0f), new Vector3(1.85f, 3.25f, 0.3f), doorMaterial, pivot.transform, true);
        CreateSphere("DoorHandle", new Vector3(1.62f, 1.6f, -0.25f), new Vector3(0.16f, 0.16f, 0.16f), goldMaterial, pivot.transform, true);

        BoxCollider interactionCollider = frame.AddComponent<BoxCollider>();
        interactionCollider.center = new Vector3(0.92f, 1.65f, 0f);
        interactionCollider.size = new Vector3(2f, 3.3f, 0.5f);

        DoorController door = frame.AddComponent<DoorController>();
        door.Configure(pivot.transform);
    }

    private void CreateClue(string number, Vector3 position, Transform parent)
    {
        GameObject marker = CreateCylinder("Clue_" + number, position, new Vector3(0.55f, 0.12f, 0.55f), clueMaterial, parent, false);
        marker.transform.localRotation = Quaternion.identity;

        GameObject textObject = new GameObject("Digit_" + number);
        textObject.transform.SetParent(marker.transform);
        textObject.transform.localPosition = new Vector3(0f, 0.7f, 0f);
        textObject.transform.rotation = Camera.main.transform.rotation;
        TextMesh textMesh = textObject.AddComponent<TextMesh>();
        textMesh.text = number;
        textMesh.fontSize = 90;
        textMesh.characterSize = 0.08f;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.color = Color.white;
    }

    private GameObject CreateCube(string objectName, Vector3 localPosition, Vector3 localScale, Material material, Transform parent, bool local = false)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = objectName;
        cube.transform.SetParent(parent);
        if (local)
        {
            cube.transform.localPosition = localPosition;
        }
        else
        {
            cube.transform.position = localPosition;
        }
        cube.transform.localScale = localScale;
        cube.GetComponent<Renderer>().material = material;
        return cube;
    }

    private GameObject CreateSphere(string objectName, Vector3 localPosition, Vector3 localScale, Material material, Transform parent, bool local)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = objectName;
        sphere.transform.SetParent(parent);
        sphere.transform.localPosition = local ? localPosition : parent.InverseTransformPoint(localPosition);
        sphere.transform.localScale = localScale;
        sphere.GetComponent<Renderer>().material = material;
        return sphere;
    }

    private GameObject CreateCylinder(string objectName, Vector3 localPosition, Vector3 localScale, Material material, Transform parent, bool local)
    {
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.name = objectName;
        cylinder.transform.SetParent(parent);
        if (local)
        {
            cylinder.transform.localPosition = localPosition;
        }
        else
        {
            cylinder.transform.position = localPosition;
        }
        cylinder.transform.localScale = localScale;
        cylinder.GetComponent<Renderer>().material = material;
        return cylinder;
    }
}
