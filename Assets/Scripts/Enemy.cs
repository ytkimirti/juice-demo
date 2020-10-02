using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    void Start()
    {
        GameManager.main.currEnemies.Add(this);
    }

    private void OnDestroy()
    {

    }

    void Update()
    {

    }
}
