using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어와 충돌 시
            GameManager.instance.LoseLife();
            gameObject.SetActive(false); // 충돌 시 장애물을 비활성화
            GameManager.instance.ClearObstacle(); // 장애물 클리어 처리
        }
    }
}
