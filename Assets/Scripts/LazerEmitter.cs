using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerEmitter : MonoBehaviour {

    public Lazer lazerPrefab;

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			var lazerLight = Instantiate(lazerPrefab);
			lazerLight.Shoot(transform.up, transform.position);
		}
	}
}
