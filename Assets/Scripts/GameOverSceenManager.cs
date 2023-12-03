using UnityEngine;

public class GameOverSceenManager : MonoBehaviour {
    private GameManager gameManager;

    public GameObject gameOverScreen;
    public GameObject playerWonText;
    public GameObject iaWonText;

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
        HideGameOverScreen();
        gameManager.SetCommand(CommandType.reset);
        //gameManager.SetCommand(CommandType.play);
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