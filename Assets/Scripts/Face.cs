using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    public Transform[] parts;
    public float eyeMoveAmount;
    public float mouthMoveAmount;
    public float maxClampDist;
    public float maxLookDist;
    public float lerpSpeed;
    Vector2 lookPos;
    public Transform lookTrans;
    Vector2[] defPos;

    void Start()
    {
        GameManager.main.currFaces.Add(this);

        defPos = new Vector2[3];

        for (int i = 0; i < parts.Length; i++)
        {
            defPos[i] = parts[i].localPosition;
        }
    }

    private void OnDestroy()
    {
        GameManager.main.currFaces.Remove(this);
    }

    Transform FindNearestFace()
    {
        int nearID = -1;
        float nearDist = maxLookDist;

        for (int i = 0; i < GameManager.main.currFaces.Count; i++)
        {
            if (!GameManager.main.currFaces[i] || GameManager.main.currFaces[i] == this)
                continue;

            float newDist = ((Vector2)GameManager.main.currFaces[i].transform.position - (Vector2)transform.position).magnitude;

            if (newDist < nearDist)
            {
                nearID = i;
                nearDist = newDist;
            }
        }

        if (nearID == -1)
        {
            return null;
        }

        return GameManager.main.currFaces[nearID].transform;
    }

    void Update()
    {
        //lookPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Time.frameCount % 10 == 0)
        {
            lookTrans = FindNearestFace();
        }

        if (lookTrans)
            lookPos = lookTrans.transform.position;
        else
            lookPos = transform.position;


        for (int i = 0; i < parts.Length; i++)
        {
            float moveAmount = i == 0 ? mouthMoveAmount : eyeMoveAmount;

            Vector2 lookVec = Vector2.zero;

            if (lookPos != (Vector2)transform.position)
                lookVec = (Vector2.ClampMagnitude((lookPos - (Vector2)parts[i].position), maxClampDist) / maxClampDist) * moveAmount;

            lookVec = transform.InverseTransformVector(lookVec);

            parts[i].transform.localPosition = Vector2.Lerp(parts[i].transform.localPosition, defPos[i] + lookVec, lerpSpeed * Time.deltaTime);
        }
    }
}
