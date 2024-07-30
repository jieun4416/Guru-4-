using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject startButton;
    public GameObject gameOverText;
    public GameObject nextStageButton;
    public GameObject[] heartImages;
    public Transform playerTransform;
    public List<GameObject> stage1Obstacles = new List<GameObject>();
    public List<GameObject> stage2Obstacles = new List<GameObject>();
    public List<GameObject> stage3Obstacles = new List<GameObject>();
    public GameObject[] backgrounds;

    public int playerLives = 3;
    public bool isPlay = false;
    public float gameSpeed = 1f;
    private int clearedObstacles = 0;

    public int currentStage = 1;
    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip clearSound;
    public AudioClip gainLifeSound;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        UpdateUI();
        gameOverText.SetActive(false);
        nextStageButton.SetActive(false);
        LoadStage(currentStage);
    }

    public void PlayBtn()
    {
        startButton.SetActive(false);
        isPlay = true;
        playerTransform.position = new Vector3(-7, playerTransform.position.y, playerTransform.position.z);
        ActivateObstacles();
    }

    public void NextStageBtn()
    {
        nextStageButton.SetActive(false);
        playerLives = 3;
        UpdateUI();
        NextStage();
    }

    public void LoseLife()
    {
        playerLives--;
        UpdateUI();
        if (playerLives <= 0)
        {
            GameOver();
        }
        else
        {
            PlayHitSound();
        }
    }

    public void GainLife()
    {
        if (playerLives < heartImages.Length)
        {
            playerLives++;
            UpdateUI();
            PlayGainLifeSound();
        }
    }

    private void GameOver()
    {
        isPlay = false;
        gameOverText.SetActive(true);
    }

    private void Update()
    {
        if (isPlay)
        {
            List<GameObject> currentObstacles = GetCurrentStageObstacles();
            foreach (GameObject obstacle in currentObstacles)
            {
                if (obstacle.activeSelf && obstacle.transform.position.x <= -10f)
                {
                    obstacle.SetActive(false);
                    ClearObstacle();
                }
            }
        }
    }

    public void ClearObstacle()
    {
        clearedObstacles++;
        Debug.Log("Cleared Obstacles: " + clearedObstacles + " / " + GetCurrentStageObstacles().Count);
        if (clearedObstacles >= GetCurrentStageObstacles().Count)
        {
            GameClear();
        }
    }

    public void GameClear()
    {
        Debug.Log("Game Cleared!");
        isPlay = false;
        nextStageButton.SetActive(true);
        PlayClearSound();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].SetActive(i < playerLives);
        }
    }

    public void NextStage()
    {
        currentStage++;
        if (currentStage > 3)
        {
            GameComplete();
        }
        else
        {
            LoadStage(currentStage);
            isPlay = true;
        }
    }

    private void LoadStage(int stage)
    {
        Debug.Log($"Loading Stage {stage}");

        foreach (GameObject bg in backgrounds)
        {
            bg.SetActive(false);
        }

        if (stage - 1 < backgrounds.Length)
        {
            backgrounds[stage - 1].SetActive(true);
        }

        switch (stage)
        {
            case 1:
                gameSpeed = 3f;
                break;
            case 2:
                gameSpeed = 4f;
                break;
            case 3:
                gameSpeed = 5f;
                break;
            default:
                gameSpeed = 1f;
                break;
        }

        Debug.Log($"Stage {stage} loaded with game speed {gameSpeed}");

        ResetObstacles();
        clearedObstacles = 0;
        ActivateObstacles();
    }

    private void ResetObstacles()
    {
        foreach (GameObject obstacle in stage1Obstacles)
        {
            obstacle.SetActive(false);
        }
        foreach (GameObject obstacle in stage2Obstacles)
        {
            obstacle.SetActive(false);
        }
        foreach (GameObject obstacle in stage3Obstacles)
        {
            obstacle.SetActive(false);
        }
    }

    private void ActivateObstacles()
    {
        foreach (GameObject obstacle in GetCurrentStageObstacles())
        {
            obstacle.SetActive(true);
        }
    }

    private List<GameObject> GetCurrentStageObstacles()
    {
        switch (currentStage)
        {
            case 1:
                return stage1Obstacles;
            case 2:
                return stage2Obstacles;
            case 3:
                return stage3Obstacles;
            default:
                return new List<GameObject>();
        }
    }

    private void GameComplete()
    {
        Debug.Log("Game Completed!");
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }

    public void PlayClearSound()
    {
        audioSource.PlayOneShot(clearSound);
    }

    public void PlayGainLifeSound()
    {
        audioSource.PlayOneShot(gainLifeSound);
    }
}
