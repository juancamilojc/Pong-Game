using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    playing,
    paused,
    stopped
}

public enum CommandType {
    play,
    pause,
    reset,
    gameover,
    noop
}

public class GameManager : MonoBehaviour {
    public delegate void GameDelegate();
    public event GameDelegate OnPlay;
    public event GameDelegate OnPause;
    public event GameDelegate OnReset;
    public event GameDelegate OnPointScored;
    public event GameDelegate OnGameOver;

    private Scoreboard playerScoreboard;
    private Scoreboard iaScoreboard;

    private GameState currentState;
    private CommandType command;

    // Start is called before the first frame update
    void Start() {
        playerScoreboard = GameObject.Find("PlayerScoreZone").GetComponent<Scoreboard>();
        iaScoreboard = GameObject.Find("IAScoreZone").GetComponent<Scoreboard>();

        playerScoreboard.OnScored += ScoredPointHandler;
        iaScoreboard.OnScored += ScoredPointHandler;

        command = CommandType.play;
        currentState = GameState.stopped;
    }

    // Update is called once per frame
    void Update() {
        switch(currentState) {
            case GameState.playing:
            if (command == CommandType.pause) {
                    this.currentState = GameState.paused;
                    OnPause?.Invoke();
                } else if (command == CommandType.reset) {
                    this.currentState = GameState.stopped;
                    OnReset?.Invoke();
                } else if (command == CommandType.gameover) {
                    this.currentState = GameState.stopped;
                    OnGameOver?.Invoke();
                }
            break;
            case GameState.paused:
                if (command == CommandType.play) {
                    this.currentState = GameState.playing;
                    OnPlay?.Invoke();
                } else if (command == CommandType.reset) {
                    this.currentState = GameState.stopped;
                    OnReset?.Invoke();
                }
            break;
            case GameState.stopped:
                if (command == CommandType.play) {
                    this.currentState = GameState.playing;
                    OnPlay?.Invoke();
                } else if (command == CommandType.gameover) {
                    OnGameOver?.Invoke();
                }
            break;
        }
    }

    public void SetCommand(CommandType cmd) {
        this.command = cmd;
    }

    void ScoredPointHandler() {
        currentState = GameState.stopped;
        OnPointScored?.Invoke();
    }
}