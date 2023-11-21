using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ball : MonoBehaviour {
    [SerializeField]
    private float speed = 11.0f;
    private Vector3 initialPosition;
    private Rigidbody rb;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        
        gameManager = FindObjectOfType<GameManager>();
        SubscribeToEvents(gameManager);
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void SubscribeToEvents(GameManager gm) {
        gm.OnPlay += InitializeMovement;
        gm.OnReset += ResetPosition;
        gm.OnGameOver += ResetPosition;
        gm.OnPointScored += PointScoredHandler;
    }

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }

    private void InitializeMovement() {
        Vector3 initialDirection = new Vector3(Random.Range(0, 2) == 0 ? -1 : 1, 0, 0);

        float angle = Random.Range(20.0f, 30.0f) * (Random.Range(0, 2) == 0 ? 1 : -1);
        var newDirection = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1)) * initialDirection;

        rb.AddForce(newDirection * speed, ForceMode.VelocityChange);
    }

    private void ResetPosition() {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = initialPosition;
    }

    private void PointScoredHandler() {
        ResetPosition();
        Invoke("InitializeMovement", 0.5f);
    }
}