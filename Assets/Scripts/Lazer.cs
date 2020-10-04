using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {
	 
	public float speed = 1;
	public float maxDistance;
	public float endPointThreshold;

	private Vector2 currentPosition;
	private Vector2 targetPosition;
	private Vector2 direction;

	private int layerMask;

	private void Awake() {
		currentPosition = transform.position;
		layerMask = LayerMask.GetMask("block");
	}
	public void Init(Vector2 position, Vector2 direction) {
		currentPosition = position;
		this.direction = direction;
	}

	private void Update() {
		//Ray cast in the direction you are going 
		var hit = Physics2D.Raycast(currentPosition, direction, maxDistance, layerMask);

		//If the ray cast is under a certain distance away .. hit
		if (hit) {
			if (hit.distance < endPointThreshold) {
				//if the collider is a tile
				var tile = hit.collider.GetComponentInParent<Tile>();
				if (tile != null) {
					tile.Hit();
				}

				//reflect the direction
				direction = Vector2.Reflect(direction, hit.normal);
				currentPosition = hit.point;
			}
		}

		targetPosition = currentPosition + (direction * maxDistance);
		currentPosition = Vector2.MoveTowards(currentPosition, targetPosition, speed);
		transform.position = currentPosition;
	}
}
