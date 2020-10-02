using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircleFader : MonoBehaviour
{
    public Transform circleTrans;

    void Start()
    {
        circleTrans.localScale = Vector3.zero;
        circleTrans.DOScale(Vector3.one, 2).SetEase(Ease.OutQuad).SetUpdate(true);
    }

    void Update()
    {
        circleTrans.transform.position = Ball.centerBall.transform.position;
    }

    public void Fade()
    {
        circleTrans.DOScale(Vector3.one * 0, 1.5f).SetEase(Ease.OutExpo).SetUpdate(true);

    }
}
