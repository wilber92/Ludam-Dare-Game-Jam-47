public class TriangleRotateFlip : Tile {
    public float rotateAmount = 90f;
    public int rotationDirection = 1;
    public override void Hit() {
        base.Hit();

        Rotate(rotateAmount * rotationDirection);

        rotationDirection *= -1;
    }
}