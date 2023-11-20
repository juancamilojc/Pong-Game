using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float moveSpeed = 10.0f;
    private float minY = -4.5f;
    private float maxY = 4.5f;
    private Vector3 initialPosition;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start() {
        initialPosition = transform.position;

        gameManager = FindObjectOfType<GameManager>();
        OnGameManagerEvents(gameManager);
    }

    // Update is called once per frame
    void Update() {
        if (gameManager.GetGameState() == GameState.playing) {
            MovePlayer();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            gameManager.SetCommand(CommandType.reset);
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            gameManager.SetCommand(CommandType.play);
        }
    }

    private void OnGameManagerEvents(GameManager gm) {
        gm.OnReset += ResetHandler;
        gm.OnGameOver += GameOverHandler;
    }

    private void MovePlayer() {
        float moveInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(0.0f, moveInput, 0.0f) * moveSpeed * Time.deltaTime;

        transform.Translate(movement);

        Vector3 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
    }

    private void ResetPosition() {
        transform.position = initialPosition;
    }

    private void ResetHandler() {
        ResetPosition();
    }

    private void GameOverHandler() {
        ResetPosition();
    }
}