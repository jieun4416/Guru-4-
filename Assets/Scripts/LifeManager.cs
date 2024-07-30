using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public Text life_text;

    public int life;
    public void Update()
    {
        life_text.text = "Life : " + life;
    }

    public void Dead()
    {
        life--;
    }
}
