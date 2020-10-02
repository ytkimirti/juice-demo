using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public bool enableFollowCam;

    [HideInInspector]
    public Camera cam;

    [HideInInspector]
    public bool followCam;

    public static CameraController main;

    private void Awake()
    {
        main = this;
        cam = Camera.main;
    }

    void Start()
    {

    }

    void Update()
    {
        if (followCam && enableFollowCam)
        {
            Vector2 newPos = Vector2.Lerp(transform.position, Ball.centerBall.transform.position, 4 * Time.fixedDeltaTime);

            transform.position = (Vector3)newPos + Vector3.forward * -5;

            cam.DOOrthoSize(4, 1f).SetUpdate(true);
        }
    }
}
