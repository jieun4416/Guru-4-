using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float speedRate = 1f;

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isPlay)
        {
            float totalSpeed = GameManager.instance.gameSpeed * speedRate * Time.deltaTime * -1f;
            transform.Translate(totalSpeed, 0, 0);
        }
    }
}
