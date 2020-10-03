using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType {
    Triangle,
}

public class TileCreator : MonoBehaviour {

    public static TileCreator instance;
    public List<Tile> tiles;
    public Grid grid;

    private Tile ghostTile;

	private bool placing = false;

	private void Awake() {
        instance = this;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			stopPlacing();
		}

        if (placing) {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var gridPosition = grid.WorldToCell(mousePosition);
            gridPosition.z = 0;
            ghostTile.transform.position = gridPosition;

            if (Input.GetMouseButtonDown(0)) {
                placeTile();
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                ghostTile.Rotate(90);
            }
            if (Input.GetKeyDown(KeyCode.Q)) {
                ghostTile.Rotate(-90);
            }
        }
    }

    private void placeTile() {
        if (ghostTile != null) {
            var newTile = Instantiate(ghostTile, ghostTile.transform.position, ghostTile.transform.rotation);
            newTile.Init();
		}
	}
    public void stopPlacing() {
        Destroy(ghostTile.gameObject);
        placing = false;
	}

	public void CreateTile(TileType type) {
        var tile = tiles.Find(e => e.type == type);
        if (tile != null) {
            placing = true;
            ghostTile = Instantiate(tile);
            ghostTile.GetComponentInChildren<Collider2D>().enabled = false;
        }        
	}

}
