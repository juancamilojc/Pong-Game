using UnityEngine;
using UnityEngine.Events;

public class GameOverSceenManager : MonoBehaviour {
    private GameManager gameManager;

    public GameObject gameOverScreen;
    public GameObject playerWonText;
    public GameObject iaWonText;

    public UnityEvent OnRestart;

    // Awake is called when the script instance is being loaded
    void Awake() {
        gameManager = FindAnyObjectByType<GameManager>();
        gameManager.OnGameOver += ShowGameOverScreen;
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void ShowGameOverScreen() {
        gameOverScreen.SetActive(true);
    }

    private void HideGameOverScreen() {
        playerWonText.SetActive(false);
        iaWonText.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    public void OnRestartButtonClicked() {
        OnRestart?.Invoke();
        Invoke(nameof(HideGameOverScreen), 0.3f);
        gameManager.SetCommand(CommandType.reset);
        gameManager.StartGame();
    }

    public void OnExitButtonClicked() {
        Debug.Log("Sair!");
        Application.Quit();
    }

    public void OnPlayerWin() {
        ShowGameOverScreen();
        playerWonText.SetActive(true);
        iaWonText.SetActive(false);
    }

    public void OnIAWin() {
        ShowGameOverScreen();
        playerWonText.SetActive(false);
        iaWonText.SetActive(true);
    }
}