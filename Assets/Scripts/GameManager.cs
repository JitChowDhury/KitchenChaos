using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State
    {
        waitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,

    }

    private State state;
    [SerializeField] private float countDownToStartTimer = 3f;
    private float gamePlayingTimer;
    [SerializeField] private float gamePlayingTimerMax = 10f;
    public bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        state = State.waitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.waitingToStart)
        {
            state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }


    private void Update()
    {
        switch (state)
        {
            case State.waitingToStart:
             
                break;

            case State.CountdownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                }
                break;


            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                }
                break;
            case State.GameOver:
                break;

        }

        Debug.Log(state);
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool isCountDownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountDownToStartTimer()
    {
        return countDownToStartTimer;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }


    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this,EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this,EventArgs.Empty);
        }

        }

    }
