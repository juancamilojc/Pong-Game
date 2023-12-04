using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;

public class Scoreboard : MonoBehaviour {
    private TextMeshProUGUI playerScoreText;
    private TextMeshProUGUI iaScoreText;
    [SerializeField] private int finalScore = 3;
    [SerializeField] private GameObject scoreTextAnimation;
    private int scorePlayer = 0;
    private int scoreIA = 0;
    private GameManager gameManager;

    public UnityEvent PlayerWon;
    public UnityEvent IAWon;
    public UnityEvent PointScored;

    // Awake is called when the script instance is being loaded
    void Awake() {
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

    private void UpdatePlayerScore() {
        scorePlayer++;
        playerScoreText.text = scorePlayer.ToString();

        if (CheckWinCondition(scorePlayer)) {
            gameManager.SetCommand(CommandType.gameover);
            PlayerWon?.Invoke();
        } else {
            StartCoroutine(scoreAnimation());
            PointScored?.Invoke();
        }
    }

    private void UpdateIAScore() {
        scoreIA++;
        iaScoreText.text = scoreIA.ToString();

        if (CheckWinCondition(scoreIA)) {
            gameManager.SetCommand(CommandType.gameover);
            IAWon?.Invoke();
        } else {
            StartCoroutine(scoreAnimation());
            PointScored?.Invoke();
        }
    }

    private bool CheckWinCondition(int score) {
        return score >= finalScore;
    }

    private void ResetScoreboard() {
        scorePlayer = 0;
        scoreIA = 0;
        playerScoreText.text = "0";
        iaScoreText.text = "0";
    }

    public void OnPlayerScored() {
        UpdatePlayerScore();
    }

    public void OnIAScored() {
        UpdateIAScore();
    }

    private IEnumerator scoreAnimation() {
        scoreTextAnimation.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        scoreTextAnimation.SetActive(false);
    }
}