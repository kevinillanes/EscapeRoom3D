using UnityEngine;

[DisallowMultipleComponent]
public class DinosaurMotionAnimator : MonoBehaviour
{
    [SerializeField] private Transform visualModel;
    [SerializeField] private float walkCyclesPerSecond = 2.35f;
    [SerializeField] private float bodyBounce = 0.075f;
    [SerializeField] private float bodyTilt = 4.5f;

    private PlayerController playerController;
    private MeshFilter meshFilter;
    private Mesh animatedMesh;
    private Vector3[] originalVertices;
    private Vector3[] animatedVertices;
    private Vector3 baseLocalPosition;
    private Quaternion baseLocalRotation;
    private Vector3 baseLocalScale;
    private Bounds meshBounds;
    private float phase;
    private float movementBlend;
    private bool initialized;
    private bool meshIsDeformed;

    public void Configure(Transform model)
    {
        visualModel = model;
        Initialize();
    }

    private void Start()
    {
        if (!initialized)
        {
            if (visualModel == null)
            {
                visualModel = transform.Find("Dinosaurio");
            }

            Initialize();
        }
    }

    private void Initialize()
    {
        if (visualModel == null || initialized)
        {
            return;
        }

        playerController = GetComponent<PlayerController>();
        baseLocalPosition = visualModel.localPosition;
        baseLocalRotation = visualModel.localRotation;
        baseLocalScale = visualModel.localScale;

        meshFilter = visualModel.GetComponentInChildren<MeshFilter>();
        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            animatedMesh = Instantiate(meshFilter.sharedMesh);
            animatedMesh.name = meshFilter.sharedMesh.name + "_Walking";
            animatedMesh.MarkDynamic();
            meshFilter.sharedMesh = animatedMesh;
            originalVertices = animatedMesh.vertices;
            animatedVertices = new Vector3[originalVertices.Length];
            meshBounds = animatedMesh.bounds;

            Bounds expandedBounds = meshBounds;
            expandedBounds.Expand(meshBounds.size * 0.35f);
            animatedMesh.bounds = expandedBounds;
        }

        initialized = true;
    }

    private void LateUpdate()
    {
        if (!initialized || visualModel == null)
        {
            return;
        }

        bool isMoving = playerController != null && playerController.IsMoving;
        float targetBlend = isMoving ? 1f : 0f;
        movementBlend = Mathf.MoveTowards(movementBlend, targetBlend, Time.deltaTime * 7f);

        phase += Time.deltaTime * Mathf.PI * 2f * (isMoving ? walkCyclesPerSecond : 0.55f);
        float step = Mathf.Sin(phase);
        float breathing = Mathf.Sin(phase * 0.5f);
        float bounce = Mathf.Abs(step) * bodyBounce * movementBlend;
        bounce += breathing * 0.012f * (1f - movementBlend);

        visualModel.localPosition = baseLocalPosition + Vector3.up * bounce;
        visualModel.localRotation = Quaternion.Euler(
            Mathf.Abs(step) * 2.2f * movementBlend,
            0f,
            step * bodyTilt * movementBlend) * baseLocalRotation;

        float squash = Mathf.Abs(step) * 0.025f * movementBlend;
        float breathScale = breathing * 0.006f * (1f - movementBlend);
        visualModel.localScale = Vector3.Scale(
            baseLocalScale,
            new Vector3(1f + squash, 1f - squash + breathScale, 1f + squash));

        AnimateLegs();
    }

    private void AnimateLegs()
    {
        if (animatedMesh == null || originalVertices == null)
        {
            return;
        }

        if (movementBlend <= 0.001f)
        {
            if (meshIsDeformed)
            {
                animatedMesh.vertices = originalVertices;
                meshIsDeformed = false;
            }

            return;
        }

        float minHeight = meshBounds.min.z;
        float heightRange = Mathf.Max(meshBounds.size.z, 0.00001f);
        float sideRange = Mathf.Max(meshBounds.extents.x, 0.00001f);
        float strideDistance = meshBounds.size.y * 0.055f;
        float liftDistance = meshBounds.size.z * 0.035f;

        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];
            float normalizedHeight = (vertex.z - minHeight) / heightRange;
            float lowerBodyWeight = 1f - Mathf.SmoothStep(0.18f, 0.52f, normalizedHeight);
            float side = (vertex.x - meshBounds.center.x) / sideRange;
            float legWeight = lowerBodyWeight * Mathf.SmoothStep(0.05f, 0.28f, Mathf.Abs(side));
            float legPhase = side >= 0f ? phase : phase + Mathf.PI;
            float legStep = Mathf.Sin(legPhase);

            vertex.y += legStep * strideDistance * legWeight * movementBlend;
            vertex.z += Mathf.Max(0f, legStep) * liftDistance * legWeight * movementBlend;
            animatedVertices[i] = vertex;
        }

        animatedMesh.vertices = animatedVertices;
        meshIsDeformed = true;
    }

    private void OnDestroy()
    {
        if (animatedMesh != null)
        {
            Destroy(animatedMesh);
        }
    }
}
