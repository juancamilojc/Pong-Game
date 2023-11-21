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

    // Start is called before the first frame update
    void Start() {
        playerScoreText = GameObject.Find("PlayerScoreText").GetComponent<TextMeshProUGUI>();
        iaScoreText = GameObject.Find("IAScoreText").GetComponent<TextMeshProUGUI>();

        gameManager = FindObjectOfType<GameManager>();
        SubscribeToEvents(gameManager);
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void SubscribeToEvents(GameManager gm) {
        gm.OnReset += ResetScoreboard;
        gm.OnGameOver += ResetScoreboard;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ball")) {
            if (gameObject.CompareTag("PlayerScoreZone")) {
                OnScored?.Invoke();
                UpdatePlayerScore();
            } else if (gameObject.CompareTag("IAScoreZone")) {
                OnScored?.Invoke();
                UpdateIAScore();
            }
        }
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
            gameManager.SetCommand(CommandType.gameover);
        }
    }

    private void ResetScoreboard() {
        scorePlayer = 0;
        scoreIA = 0;
        playerScoreText.text = "0";
        iaScoreText.text = "0";
    }
}