using UnityEngine;

public class IA : MonoBehaviour {
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float errorChance = 0.4f;
    private readonly float smoothness = 2.0f;
    private readonly float minY = -4.5f;
    private readonly float maxY = 4.5f;
    private Vector3 initialPosition;
    private Transform ball;
    private GameManager gameManager;

    // Awake is called when the script instance is being loaded
    void Awake() {
        initialPosition = transform.position;

        ball = FindObjectOfType<Ball>().transform;

        if (ball == null) {
            Debug.LogError("Bola não encontrada.");
            return;
        }

        gameManager = FindObjectOfType<GameManager>();
        SubscribeToEvents(gameManager);
    }

    // Update is called once per frame
    void Update() {
        if (gameManager.GetGameState() == GameState.playing) {
            Move();
        }
    }

    private void SubscribeToEvents(GameManager gm) {
        gm.OnPointScored += ResetPosition;
        gm.OnReset += ResetPosition;
        gm.OnGameOver += ResetPosition;
    }

    private void Move() {
        if (ball != null) {
            float timeToIntercept = Mathf.Abs((transform.position.x - ball.position.x) / moveSpeed);
            float predictedY = ball.position.y + (ball.GetComponent<Rigidbody>().velocity.y * timeToIntercept);

            if (Random.value < errorChance) {
                predictedY += Random.Range(-1f, 1f);
            }

            float newY = Mathf.Clamp(predictedY, minY, maxY);

            float smoothMovement = Mathf.Lerp(transform.position.y, newY, Time.deltaTime * smoothness);
            transform.position = new Vector3(transform.position.x, smoothMovement, transform.position.z);
        }
    }

    public void SetSpeed(float newSpeed) {
        moveSpeed = newSpeed;
    }

    public void SetErrorChance(float newErrorChance) {
        errorChance = Mathf.Clamp01(newErrorChance);
    }

    public void ResetPosition() {
        transform.position = initialPosition;
    }
}