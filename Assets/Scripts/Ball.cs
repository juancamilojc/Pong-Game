using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ball : MonoBehaviour {
    [SerializeField]
    private float speed = 10.0f;
    private Vector3 initialDirection = Vector3.zero;
    private Vector3 initialPosition = Vector3.zero;
    private Rigidbody rb;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OnPlay += PlayHandler;
        gameManager.OnReset += ResetHandler;
        gameManager.OnPointScored += PointScoredHandler;
        gameManager.OnGameOver += GameOverHandler;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }

    private void InitializeMovement() {
        int directionX = Random.Range(0, 2) == 0 ? -1 : 1;
        float angle = Random.Range(-45.0f, 45.0f);

        initialDirection = new Vector3(directionX, 0, 0);

        var newDirection = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1)) * initialDirection;

        rb.AddForce(newDirection * speed, ForceMode.VelocityChange);
    }

    private void ResetPosition() {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = Vector3.zero;
    }

    void PlayHandler() {
        InitializeMovement();
    }

    void ResetHandler() {
        ResetPosition();
    }

    void PointScoredHandler() {
        ResetPosition();
    }

    void GameOverHandler() {
        ResetPosition();
    }
}
