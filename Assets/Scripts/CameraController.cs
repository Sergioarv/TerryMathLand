using UnityEngine;

public class CameraController : MonoBehaviour
{

    private GameObject player;
    private GameObject activeLevel;

    private BoxCollider2D bounds;

    private float minPosX;
    private float maxPosX;
    private float minPosY;
    private float maxPosY;

    [Range(-15,15)]
    public float minModX, maxModX, minModY, maxModY;

    public static CameraController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        player = GameObject.Find("Jugador");
        activeLevel = GameObject.Find("ActiveLevel");
    }

    void Update()
    {
        bounds = activeLevel.transform.GetComponent<BoxCollider2D>();

        minPosX = bounds.bounds.min.x + minModX;
        maxPosX = bounds.bounds.max.x + maxModX;
        minPosY = bounds.bounds.min.y + minModY;
        maxPosY = bounds.bounds.max.y + maxModY;

        Vector3 clampedPos = new Vector3(
            Mathf.Clamp(player.transform.position.x, minPosX, maxPosX),
            Mathf.Clamp(player.transform.position.y, minPosY, maxPosY),
            Mathf.Clamp(player.transform.position.z, -10f, -10f)
            );

        transform.position = new Vector3(clampedPos.x, clampedPos.y, clampedPos.z);
    }
}
