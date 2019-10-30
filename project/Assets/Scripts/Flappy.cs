using UnityEngine;

public class Flappy : MonoBehaviour {
    public float jumpSpeed;
    private Vector3 startPosition;
    private Rigidbody2D rb;
    private Obstacles[] obstacles;

    private void Start () {
        rb = GetComponent<Rigidbody2D> ();
        obstacles = FindObjectsOfType<Obstacles> ();
        startPosition = transform.position;
    }

    public bool jumpRequest;
    private void Update () {
        if (Input.GetMouseButtonDown (0)) {
            jumpRequest = true;
        }

        if (rb.velocity.y > 0) {
            transform.eulerAngles = new Vector3 (0, 0, 45);
        } else if (rb.velocity.y < 0) {
            transform.eulerAngles = new Vector3 (0, 0, -45);
        } else {
            transform.eulerAngles = new Vector3 (0, 0, 0);
        }
    }
    private void FixedUpdate () {
        if (jumpRequest) {
            jumpRequest = false;
            rb.velocity = Vector2.zero;
            rb.AddForce (Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D () {
        transform.position = startPosition;
        rb.velocity = Vector2.zero;

        foreach(var obs in obstacles){
            obs.ReturnToStart();
        }
    }
}