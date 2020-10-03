using UnityEngine;

public class TriangleStaticClick : Tile {
    public float rotateAmount = 90f;

    private void OnMouseEnter() {
        setHighlightColour();
    }
    private void OnMouseExit() {
        setNormalColour();
    }
    
    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            Rotate(rotateAmount);
        }
        if (Input.GetMouseButtonDown(1)) {
            Rotate(-rotateAmount);
        }
    }
}
