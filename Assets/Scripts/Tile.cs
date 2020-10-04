using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour {

    public Color normalColour;
    public Color higlightColour;
    protected Color currentColour;
    public TileType type;
    public List<AudioClip> clips = new List<AudioClip>();
    public GameObject physicsObject;
    public GameObject visualObject;
    public float rotateSpeed = 1000;
    public float colourSpeed = 200;
    public float colourReductionOnHit = 2;

    private Collider2D col;
    private SpriteRenderer _spriteRenderer;
    protected SpriteRenderer spriteRenderer => _spriteRenderer == null ? _spriteRenderer = GetComponentInChildren<SpriteRenderer>() : _spriteRenderer;
    private float rotation;
    private float visualRotation;

	private void Awake() {
        col = GetComponentInChildren<Collider2D>();
	}
	private void OnValidate() {
        setNormalColour();
	}
	private void Update() {
        visualRotation = Mathf.MoveTowardsAngle(visualObject.transform.eulerAngles.z, physicsObject.transform.eulerAngles.z, rotateSpeed * Time.deltaTime);
        visualObject.transform.eulerAngles = new Vector3(0, 0, visualRotation);

        currentColour = Vector4.MoveTowards(currentColour, normalColour, colourSpeed * Time.deltaTime);
        spriteRenderer.color = currentColour;
	}

	public void Init() {
        col.enabled = true;
	}

	public void Rotate(float angle) {
        physicsObject.transform.Rotate(new Vector3(0, 0, angle));
    }

	public virtual void Hit() {
        playSound();
        currentColour /= colourReductionOnHit;
    }
    private void playSound() {
        if (clips.Count != 0) {
            AudioManager.instance.audioSource.PlayOneShot(clips.GetRandomFromList());
        }
    }    
    protected void setHighlightColour() {
        spriteRenderer.color = higlightColour;
    }
    protected void setNormalColour() {
        spriteRenderer.color = normalColour;
        currentColour = spriteRenderer.color;
    }
}
