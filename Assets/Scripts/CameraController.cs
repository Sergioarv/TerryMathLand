using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    public Transform activeLevel;

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
        
    }

    void Update()
    {
        bounds = activeLevel.GetComponent<BoxCollider2D>();

        minPosX = bounds.bounds.min.x + minModX;
        maxPosX = bounds.bounds.max.x + maxModX;
        minPosY = bounds.bounds.min.y + minModY;
        maxPosY = bounds.bounds.max.y + maxModY;

        Vector3 clampedPos = new Vector3(
            Mathf.Clamp(player.position.x, minPosX, maxPosX),
            Mathf.Clamp(player.position.y, minPosY, maxPosY),
            Mathf.Clamp(player.position.z, -10f, -10f)
            );

        transform.position = new Vector3(clampedPos.x, clampedPos.y, clampedPos.z);
    }
}
