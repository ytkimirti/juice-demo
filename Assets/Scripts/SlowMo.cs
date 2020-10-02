using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMo : MonoBehaviour//Dr strange
{

    public static SlowMo main;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void Freeze(int frames)
    {
        //If it's already freezing, don't freeze. It will break the game :P
        if (Time.timeScale == 0)
            return;

        StartCoroutine(freezeEnum(frames));
    }

    IEnumerator freezeEnum(int frames)
    {

        float defTimeScale = Time.timeScale;

        //Freeze time
        Time.timeScale = 0;

        //Wait
        for (int i = 0; i < frames; i++)
        {
            yield return new WaitForEndOfFrame();
        }

        //Time is normal again
        Time.timeScale = defTimeScale;
    }
}
