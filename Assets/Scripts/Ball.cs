using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour {
    [SerializeField] private float speed = 11.0f;
    private Rigidbody rb;
    private GameManager gameManager;

    public UnityEvent PlayerScored;
    public UnityEvent IAScored;

    // Awake is called when the script instance is being loaded
    void Awake() {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        SubscribeToEvents(gameManager);
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.x > 11) {
            PlayerScored?.Invoke();
            ResetPosition();
        } else if (transform.position.x < -11) {
            IAScored?.Invoke();
            ResetPosition();
        }
    }

    private void SubscribeToEvents(GameManager gm) {
        gm.OnPlay += InitializeMovement;
        gm.OnReset += ResetPosition;
        gm.OnGameOver += ResetPosition;
    }

    private void InitializeMovement() {
        Vector3 initialDirection = new(Random.Range(0, 2) == 0 ? -1 : 1, 0, 0);

        float angle = Random.Range(20.0f, 30.0f) * (Random.Range(0, 2) == 0 ? 1 : -1);
        var newDirection = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1)) * initialDirection;

        rb.AddForce(newDirection * speed, ForceMode.VelocityChange);
    }

    private void ResetPosition() {
        transform.position = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void OnPointScored() {
        Invoke(nameof(InitializeMovement), 0.3f);
    }
}