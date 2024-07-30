using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0f, 0f));
    }
}