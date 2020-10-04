using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerEmitter : MonoBehaviour {

    public Lazer lazerPrefab;
	public int startCount;
	public float startShotsInterval;

	private void Start() {
		for (int i = 0; i < startCount; i++) {
			Invoke("shoot", startShotsInterval * i);
		}
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			shoot();
		}
	}

	private void shoot() {
		var lazerLight = Instantiate(lazerPrefab, transform.position, Quaternion.identity);
		lazerLight.Init(transform.position, transform.up);
	}
}
