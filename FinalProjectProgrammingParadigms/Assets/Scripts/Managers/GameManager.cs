using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public enum GameStates
{
    Start,
    Playing,
    GameOver
}
public class GameManager : Singleton<GameManager>
{

    [SerializeField] private float worldSpeed = 5f;
    [SerializeField] private float scoreMultiplier = 10f;
    private float distanceRun;
    private float gameOverDelay = 1f;

    public int Score
    {
        get
        {
            return (int)distanceRun + ObtainedDiamonds * (int)scoreMultiplier;
        }
    }

    public float MultiplierValue { get; set; }
    public GameStates CurrentState { get; set; }
    public int ObtainedDiamonds { get; set; }

    public int BestScore => PlayerPrefs.GetInt(bestScoreKey);
    private string bestScoreKey = "myBestScore";
    private int bestScoreCheck;
    public static event Action<GameStates> OnStateChange;


    private void Start()
    {
        MultiplierValue = 1f;
        bestScoreCheck = BestScore;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(GameStates.Playing);
        }

        if (CurrentState == GameStates.Start)
        {
            return;
        }

        if (CurrentState == GameStates.GameOver)
        {
            PlayerPrefs.SetInt("finalScoreKey", Score);
            PlayerPrefs.SetInt("finalDiamondsKey", GameManager.Instance.ObtainedDiamonds);
            PlayerPrefs.Save();
            StartCoroutine(HandleGameOverDelay());
        }


        distanceRun += Time.deltaTime * worldSpeed * MultiplierValue;
    }

    private IEnumerator HandleGameOverDelay()
    {

        yield return new WaitForSeconds(gameOverDelay);

        SceneManager.LoadScene("GameOverMenu"); 
    }
    public void ChangeState(GameStates newState)
    {
        if (CurrentState != newState)
        {
            CurrentState = newState;
            OnStateChange?.Invoke(newState);
        }
    }

    private Coroutine multiplierCoroutine;

    private void UpdateBestScore()
    {
        if (Score > bestScoreCheck)
        {
            PlayerPrefs.SetInt(bestScoreKey, Score);
        }
    }
    public void StartMultiplierCount(float time)
    {
        if (multiplierCoroutine != null)
        {
            StopCoroutine(multiplierCoroutine);
        }
        multiplierCoroutine = StartCoroutine(COMultiplierCount(time));
    }
    private IEnumerator COMultiplierCount(float time)
    {
        yield return new WaitForSeconds(time);
        MultiplierValue = 1;
        multiplierCoroutine = null;

    }

    private void EventStateChange(GameStates newState)
    {
        if (newState == GameStates.GameOver)
        {
            UpdateBestScore();
        }
    }

    private void OnEnable()
    {
        OnStateChange += EventStateChange;
    }

    private void OnDisable()
    {
        OnStateChange -= EventStateChange;
    }
}
