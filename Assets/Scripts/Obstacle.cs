using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾�� �浹 ��
            GameManager.instance.LoseLife();
            gameObject.SetActive(false); // �浹 �� ��ֹ��� ��Ȱ��ȭ
            GameManager.instance.ClearObstacle(); // ��ֹ� Ŭ���� ó��
        }
    }
}
