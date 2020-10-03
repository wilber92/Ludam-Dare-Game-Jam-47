using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileUiButton : MonoBehaviour {
	public TileType type;

	public void Create() {
		TileCreator.instance.CreateTile(type);
	}
}
