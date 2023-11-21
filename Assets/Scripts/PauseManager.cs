using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update() {
        if (gameManager.GetGameState() == GameState.playing) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                PauseGame();
            }
        } else if (gameManager.GetGameState() == GameState.paused) {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) {
                ResumeGame();
            }
        }
    }

    private void PauseGame() {
        gameManager.SetCommand(CommandType.pause);
        Time.timeScale = 0;
    }

    private void ResumeGame() {
        gameManager.SetCommand(CommandType.resume);
        Time.timeScale = 1;
    }
}
