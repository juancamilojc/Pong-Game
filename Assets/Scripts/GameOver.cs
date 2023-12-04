using UnityEngine;
using UnityEngine.Events;

public class GameOver : MonoBehaviour {
    private GameManager gameManager;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject playerWonText;
    [SerializeField] private GameObject iaWonText;

    public UnityEvent OnRestart;

    // Awake is called when the script instance is being loaded
    void Awake() {
        gameManager = FindAnyObjectByType<GameManager>();
        gameManager.OnGameOver += ShowGameOver;
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void ShowGameOver() {
        gameOverUI.SetActive(true);
    }

    private void HideGameOver() {
        playerWonText.SetActive(false);
        iaWonText.SetActive(false);
        gameOverUI.SetActive(false);
    }

    public void OnRestartButtonClicked() {
        OnRestart?.Invoke();
        Invoke(nameof(HideGameOver), 0.3f);
        gameManager.SetCommand(CommandType.reset);
        gameManager.StartGame();
    }

    public void OnExitButtonClicked() {
        Debug.Log("Sair!");
        Application.Quit();
    }

    public void OnPlayerWin() {
        ShowGameOver();
        playerWonText.SetActive(true);
        iaWonText.SetActive(false);
    }

    public void OnIAWin() {
        ShowGameOver();
        playerWonText.SetActive(false);
        iaWonText.SetActive(true);
    }
}