using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BallForce : MonoBehaviour {

    public Vector2 direction;
    public float speed;

    private Rigidbody2D rb;
	private float leftEdge;
	private float rightEdge;
	private float topEdge;
	private float bottomEdge;
	private Camera cam;

	private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;

		cam = Camera.main;

		rightEdge = cam.orthographicSize * cam.aspect;
		leftEdge = -cam.orthographicSize * cam.aspect;
		topEdge = cam.orthographicSize;
		bottomEdge = -cam.orthographicSize;
	}

	private void Update() {
		if (transform.hasChanged) {
			if (!Application.isPlaying) {
				transform.position = FindObjectOfType<Grid>().WorldToCell(transform.position);
			}
		}

		float xPosition = rb.position.x;
		float yPosition = rb.position.y;

		if (xPosition > rightEdge) {
			xPosition = leftEdge;
		}
		if (xPosition < leftEdge) {
			xPosition = rightEdge;
		}
		if (yPosition > topEdge) {
			yPosition = bottomEdge;
		}
		if (yPosition < bottomEdge) {
			yPosition = topEdge;
		}

		rb.position = new Vector2(xPosition, yPosition);
	}
}
