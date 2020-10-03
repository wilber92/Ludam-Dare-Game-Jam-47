using System;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour {

    public Color normalColour;
    public Color higlightColour;
    public TileType type;
    public AudioClip clip;
    public GameObject physicsObject;
    public GameObject visualObject;
    public float rotationTime;

    private bool placed;
    private Collider2D col;
    private SpriteRenderer spriteRenderer;

    private float rotation;

	private void Awake() {
        col = GetComponentInChildren<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

    public void Init() {
        col.enabled = true;
	}

	private void Update() {
        if (!Application.isPlaying) {
            if (transform.hasChanged) {
                transform.position = FindObjectOfType<Grid>().WorldToCell(transform.position);
            }
            return;
		}
    }

	public void Rotate(float angle) {
        StartCoroutine(rotate(angle));
	}

	public void Hit() {
        playSound();
        Rotate(90);
	}
    private void playSound() {
        if (clip != null) {
            AudioManager.instance.audioSource.PlayOneShot(clip);
        }
    }
	IEnumerator rotate(float angle) {
        var previousRotation = rotation;
        rotation += angle;
        physicsObject.transform.eulerAngles = new Vector3(0, 0, rotation);

        var startRotation = visualObject.transform.eulerAngles.z;
        float t = 0;
        float elapsedTime = 0;
        while (t <= 1) {
            elapsedTime += Time.deltaTime;
            t = elapsedTime / rotationTime;
            var targetRotation = Mathf.Lerp(previousRotation, rotation, t);

            visualObject.transform.eulerAngles = new Vector3(0, 0, targetRotation);
            yield return null;
		}
        visualObject.transform.eulerAngles = new Vector3(0, 0, rotation);
    }

	private void OnMouseEnter() {
        setHighlightColour();
	}
	private void OnMouseExit() {
        setNormalColour();
	}
    private void setHighlightColour() {
        spriteRenderer.color = higlightColour;
    }
    private void setNormalColour() {
        spriteRenderer.color = normalColour;
    }
	private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            if (Input.GetKey(KeyCode.LeftControl)) {
                Destroy(gameObject);
            }
			else {
                StartCoroutine(rotate(-90));
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            StartCoroutine(rotate(90));
        }
    }
}
