using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralForce : MonoBehaviour
{
    public Rigidbody2D rb;
    public float forceAmount;
    public float maxVel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        /// Mathf.Pow(Vector2.Distance(transform.position, Vector2.zero), 2)
        rb.AddForce((forceAmount * (-transform.position).normalized) * Mathf.Pow(Vector2.Distance(transform.position, Vector2.zero), 2));

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVel);
    }

    public void Boost()
    {
        rb.velocity = -transform.position;
    }
}
