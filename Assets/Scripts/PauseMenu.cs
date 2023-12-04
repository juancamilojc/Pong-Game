using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public static bool isPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    private GameManager gameManager;

    // Awake is called when the script instance is being loaded
    void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update() {
        if (gameManager.State == GameState.playing) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (isPaused) {
                    Resume();
                } else {
                    Pause();
                }
            }
        }
    }

    private void Resume() {
        HidePauseMenu();
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Pause() {
        ShowPauseMenu();
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void ShowPauseMenu() {
        pauseMenuUI.SetActive(true);
    }

    private void HidePauseMenu() {
        pauseMenuUI.SetActive(false);
    }

    public void OnResumeButtonClicked() {
        Resume();
    }

    public void OnRestartButtonClicked() {
        Resume();
        gameManager.SetCommand(CommandType.reset);
        gameManager.StartGame();
    }

    public void OnExitButtonClicked() {
        Debug.Log("Sair!");
        Application.Quit();
    }
}