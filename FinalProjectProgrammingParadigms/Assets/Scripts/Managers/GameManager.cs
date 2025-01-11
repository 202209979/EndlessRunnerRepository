using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Start()
    {
        MultiplierValue = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(GameStates.Playing);
        }

        if (CurrentState == GameStates.Start || CurrentState == GameStates.GameOver)
        {
            return;
        }

        distanceRun += Time.deltaTime * worldSpeed * MultiplierValue;
    }
    public void ChangeState(GameStates newState)
    {
        if (CurrentState != newState)
        {
            CurrentState = newState;
        }
    }

    private Coroutine multiplierCoroutine;

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
}
