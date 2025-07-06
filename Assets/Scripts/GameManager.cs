using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    private enum State
    {
        waitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,

    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countDownToStartTimer = 3f;
    private float gamePlayingTimer = 10f;

    private void Awake()
    {
        Instance = this;
        state = State.waitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            case State.waitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                }
                break;

            case State.CountdownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
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

}
