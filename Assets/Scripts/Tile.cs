using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour {

    public Color normalColour;
    public Color higlightColour;
    public TileType type;
    public List<AudioClip> clips = new List<AudioClip>();
    public GameObject physicsObject;
    public GameObject visualObject;
    public float rotationTime;

    private Collider2D col;
    private SpriteRenderer _spriteRenderer;
    protected SpriteRenderer spriteRenderer => _spriteRenderer == null ? _spriteRenderer = GetComponentInChildren<SpriteRenderer>() : _spriteRenderer;
    private float rotation;

	private void Awake() {
        col = GetComponentInChildren<Collider2D>();
	}
	private void OnValidate() {
        setNormalColour();
	}

	public void Init() {
        col.enabled = true;
	}

	public void Rotate(float angle) {
        StartCoroutine(rotate(angle));
	}

	public virtual void Hit() {
        playSound();
	}
    private void playSound() {
        if (clips.Count != 0) {
            AudioManager.instance.audioSource.PlayOneShot(clips.GetRandomFromList());
        }
    }    

	IEnumerator rotate(float angle) {
        var previousRotation = rotation;
        rotation += angle;
        physicsObject.transform.eulerAngles = new Vector3(0, 0, rotation);

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
    protected void setHighlightColour() {
        spriteRenderer.color = higlightColour;
    }
    protected void setNormalColour() {
        spriteRenderer.color = normalColour;
    }
}
