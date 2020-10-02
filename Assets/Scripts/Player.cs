using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 targetPos = CameraController.main.cam.ScreenToWorldPoint(Input.mousePosition);

        rb.velocity = (targetPos - (Vector2)transform.position) * moveSpeed;
    }
}
