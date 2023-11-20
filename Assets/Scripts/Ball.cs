using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ball : MonoBehaviour {
    [SerializeField]
    private float speed = 10.0f;
    private Vector3 initialPosition = Vector3.zero;
    private Rigidbody rb;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        
        gameManager = FindObjectOfType<GameManager>();
        OnGameManagerEvents(gameManager);
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnGameManagerEvents(GameManager gm) {
        gm.OnPlay += PlayHandler;
        gm.OnReset += ResetHandler;
        gm.OnPointScored += PointScoredHandler;
        gm.OnGameOver += GameOverHandler;
    }

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }

    private void InitializeMovement() {
        int directionX = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector3 initialDirection = new Vector3(directionX, 0, 0);

        float angle = Random.Range(-45.0f, 45.0f);
        var newDirection = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1)) * initialDirection;

        rb.AddForce(newDirection * speed, ForceMode.VelocityChange);
    }

    private void ResetPosition() {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = initialPosition;
    }

    private void PlayHandler() {
        InitializeMovement();
    }

    private void ResetHandler() {
        ResetPosition();
    }

    private void PointScoredHandler() {
        ResetPosition();
        InitializeMovement();
    }

    private void GameOverHandler() {
        ResetPosition();
    }
}