using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasher : MonoBehaviour
{
    SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Flash(int flashCount, float delay)
    {
        if (!gameObject.activeInHierarchy)
            return;

        StartCoroutine(flashEnum(flashCount, delay));
    }

    IEnumerator flashEnum(int flashCount, float delay)
    {
        for (int i = 0; i < flashCount; i++)
        {
            renderer.enabled = true;
            yield return new WaitForSeconds(delay);
            renderer.enabled = false;
            yield return new WaitForSeconds(delay);
        }
    }
}
