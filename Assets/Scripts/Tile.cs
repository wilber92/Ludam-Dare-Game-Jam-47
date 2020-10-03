using UnityEngine;

public class Tile : MonoBehaviour {
    public TileType type;
    public AudioClip clip;

    private bool placed;
    private Collider2D col;

	private void Awake() {
        col = GetComponent<Collider2D>();
	}

	public void Init() {
        placed = false;
        col.enabled = false;

    }

	private void Update() {
        if (!placed) {
            updatePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (Input.GetMouseButtonDown(0)) {
                placeTile();
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                rotate(90);
            }
            if (Input.GetKeyDown(KeyCode.Q)) {
                rotate(-90);
            }
        }
	}
	private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log($"Hit tile {name}");
        if (clip != null) {
            AudioManager.instance.audioSource.PlayOneShot(clip);
        }
    }
	private void rotate(float angle) {
        transform.Rotate(new Vector3(0, 0, angle));
	}
    private void updatePosition(Vector2 position) {
        transform.position = TileCreator.instance.grid.WorldToCell(position); ;
	}

	private void placeTile() {
        placed = true;
        col.enabled = true;
	}
}
