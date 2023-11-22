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
        SubscribeToEvents(playerScoreboard, iaScoreboard);

        currentState = GameState.stopped;
        Invoke(nameof(InitializeGame), 2.5f);
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

    private void SubscribeToEvents(Scoreboard player, Scoreboard ia) {
        player.OnScored += ScoredPointHandler;
        ia.OnScored += ScoredPointHandler;
    }

    public void SetCommand(CommandType cmd) {
        command = cmd;
    }

    public GameState GetGameState() {
        return currentState;
    }

    private void InitializeGame() {
        command = CommandType.play;
    }

    void ScoredPointHandler() {
        OnPointScored?.Invoke();
    }
}