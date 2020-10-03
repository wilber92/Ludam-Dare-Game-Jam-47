using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour {
	 
	public float speed = 1;
	public float maxDistance;
	public float endPointThreshold;

	public void Shoot(Vector2 direction, Vector2 origin) {
		transform.position = origin;
		var hit = Physics2D.Raycast(origin, direction, maxDistance);
		if (hit) {
			StartCoroutine(traverseTheLine(direction, hit.point, hit));
		}
		else {
			var endPoint = (Vector2)transform.position + direction * maxDistance;
			StartCoroutine(traverseTheLine(direction, endPoint, default));
		}
	}

	private IEnumerator traverseTheLine(Vector2 direction, Vector2 endPoint, RaycastHit2D hit) {
		while(Vector2.Distance(transform.position, endPoint) > endPointThreshold) {
			transform.position = Vector2.MoveTowards(transform.position, endPoint, speed);
			yield return null;
		}
		if (hit) {
			var tile = hit.collider.GetComponentInParent<Tile>();
			if (tile != null) {
				tile.Hit();
			}

			var newDirection = Vector2.Reflect(direction, hit.normal);
			var newOrigin = endPoint + newDirection * 0.1f;
			Shoot(newDirection, newOrigin);
		}
		else {
			Destroy(gameObject);
		}
	}
}
