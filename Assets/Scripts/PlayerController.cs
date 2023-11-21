using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 10.0f;
    private readonly float minY = -4.5f;
    private readonly float maxY = 4.5f;
    private Vector3 initialPosition;
    private GameManager gameManager;

    // Awake is called when the script instance is being loaded
    void Awake() {
        initialPosition = transform.position;

        gameManager = FindObjectOfType<GameManager>();
        SubscribeToEvents(gameManager);
    }

    void Update() {
        if (gameManager.GetGameState() == GameState.playing) {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            gameManager.SetCommand(CommandType.reset);
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            gameManager.SetCommand(CommandType.play);
        }
    }

    private void SubscribeToEvents(GameManager gm) {
        gm.OnReset += ResetPosition;
        gm.OnGameOver += ResetPosition;
    }

    private void Move() {
        float moveInput = Input.GetAxis("Vertical");
        float newY = transform.position.y + moveInput * moveSpeed * Time.deltaTime;
        float clampedY = Mathf.Clamp(newY, minY, maxY);

        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    private void ResetPosition() {
        transform.position = initialPosition;
    }
}