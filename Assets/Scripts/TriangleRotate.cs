public class TriangleRotate : Tile {
    public bool clockwise;
    public float rotateAmount = 90f;

    public override void Hit() {
        base.Hit();

        var localRotateAmount = (clockwise ? 1 : -1) * rotateAmount; 
        Rotate(localRotateAmount);
	}
}
