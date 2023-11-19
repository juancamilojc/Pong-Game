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
    noop
}

public class GameManager : MonoBehaviour {
    public delegate void GameDelegate();
    public event GameDelegate OnReset;
    public event GameDelegate OnPlay;
    public event GameDelegate OnPause;

    private GameState currentState;
    private CommandType command;

    // Start is called before the first frame update
    void Start() {
        command = CommandType.play;
        currentState = GameState.stopped;
    }

    // Update is called once per frame
    void Update() {
        switch(currentState) {
            case GameState.playing:
                if (command == CommandType.reset) {
                    this.currentState = GameState.stopped;
                    OnReset?.Invoke();
                } else if (command == CommandType.pause) {
                    this.currentState = GameState.paused;
                    OnPause?.Invoke();
                }
            break;
            case GameState.paused:
                if (command == CommandType.play) {
                    currentState = GameState.playing;
                    OnPlay?.Invoke();
                }
            break;
            case GameState.stopped:
                if (command == CommandType.play) {
                    currentState = GameState.playing;
                    OnPlay?.Invoke();
                }
            break;
        }
    }

    public void SetCommand(CommandType cmd) {
        this.command = cmd;
    }
}