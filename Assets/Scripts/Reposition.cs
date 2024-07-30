using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    void LateUpdate()
    {
        if (transform.position.x <= -16)
        {
            transform.Translate(32, 0, 0, Space.Self);
        }
    }
}