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

	private void Awake() {
        instance = this;
	}

	public void CreateTile(TileType type) {
        var tile = tiles.Find(e => e.type == type);
        if (tile != null) {
            Tile newTile = Instantiate(tile);
            newTile.Init();
        }        
	}

}
