using UnityEngine;

public enum GameState {
    playing,
    stopped
}

public enum CommandType {
    play,
    reset,
    gameover,
    noop
}

public class GameManager : MonoBehaviour {
    public delegate void GameDelegate();
    public event GameDelegate OnPlay;
    public event GameDelegate OnReset;
    public event GameDelegate OnGameOver;

    private GameState currentState;
    private CommandType command;

    public GameState State { get { return currentState; } }

    // Start is called before the first frame update
    void Start() {
        currentState = GameState.stopped;
        Invoke(nameof(InitializeGame), 1.5f);
    }

    // Update is called once per frame
    void Update() {
        switch(currentState) {
            case GameState.playing:
                if (command == CommandType.reset) {
                    currentState = GameState.stopped;
                    OnReset?.Invoke();
                } else if (command == CommandType.gameover) {
                    currentState = GameState.stopped;
                    OnGameOver?.Invoke();
                }
            break;
            case GameState.stopped:
                if (command == CommandType.play) {
                    currentState = GameState.playing;
                    OnPlay?.Invoke();
                } else if (command == CommandType.gameover) {
                    OnGameOver?.Invoke();
                }
            break;
        }
    }

    public void SetCommand(CommandType cmd) {
        command = cmd;
    }

    private void InitializeGame() {
        command = CommandType.play;
    }

    public void StartGame() {
        Invoke(nameof(InitializeGame), 0.5f);
    }
}