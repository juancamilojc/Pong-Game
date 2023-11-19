using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour {
    private TextMeshProUGUI playerScoreText;
    private TextMeshProUGUI iaScoreText;
    private int scorePlayer = 0;
    private int scoreIA = 0;
    [SerializeField]
    private int finalScore = 3;
    private GameManager gameManager;

    public event Action OnScored;
    public event Action OnGameOver;


    // Start is called before the first frame update
    void Start() {
        playerScoreText = GameObject.Find("PlayerScoreText").GetComponent<TextMeshProUGUI>();
        iaScoreText = GameObject.Find("IAScoreText").GetComponent<TextMeshProUGUI>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OnReset += ResetHandler;
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ball")) {
            if (gameObject.CompareTag("PlayerScoreZone")) {
                UpdatePlayerScore();
            } else if (gameObject.CompareTag("IAScoreZone")) {
                UpdateIAScore();
            }
        }

        OnScored?.Invoke();
    }

    public int GetPlayerScore() {
        return scorePlayer;
    }

    public int GetIAScore() {
        return scoreIA;
    }

    private void UpdatePlayerScore() {
        scorePlayer++;
        playerScoreText.text = scorePlayer.ToString();
        CheckWinCondition(scorePlayer);
    }

    private void UpdateIAScore() {
        scoreIA++;
        iaScoreText.text = scoreIA.ToString();
        CheckWinCondition(scoreIA);
    }

    private void CheckWinCondition(int score) {
        if (score >= finalScore) {
            ResetPlayerScore();
            ResetIAScore();
            OnGameOver?.Invoke();
        }
    }

    private void ResetPlayerScore() {
        scorePlayer = 0;
        playerScoreText.text = "0";
    }

    private void ResetIAScore() {
        scoreIA = 0;
        iaScoreText.text = "0";
    }

    void ResetHandler() {
        ResetPlayerScore();
        ResetIAScore();
    }
}