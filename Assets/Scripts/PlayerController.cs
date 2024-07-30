using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State { Stand, Run, Jump, Slide }
    public float startJumpPower;
    public float jumpPower;
    public bool isGround;
    public string obstacleTag = "Obstacle";
    public string healingPotionTag = "HealingPotion";  // 생명수 태그를 HealingPotion으로 변경

    Rigidbody2D rigid;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && GameManager.instance.isPlay)
        {
            rigid.AddForce(Vector2.up * startJumpPower, ForceMode2D.Impulse);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (GameManager.instance.isPlay)
        {
            if (!isGround)
            {
                ChangeAnim(State.Run);
                jumpPower = 1;
            }
            isGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        ChangeAnim(State.Jump);
        isGround = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(obstacleTag))
        {
            GameManager.instance.LoseLife();
            if (GameManager.instance.playerLives <= 0)
            {
                rigid.simulated = false; // 게임 오버
            }
        }
        else if (collision.CompareTag(healingPotionTag))
        {
            GameManager.instance.GainLife();
            collision.gameObject.SetActive(false); // 생명수(치료제) 비활성화
        }
    }

    void ChangeAnim(State state)
    {
        anim.SetInteger("State", (int)state);
    }
}