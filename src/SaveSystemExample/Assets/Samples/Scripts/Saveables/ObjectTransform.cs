internal struct ObjectTransform
{
    public ObjectTransform(
        float positionX,
        float positionY,
        float rotationX,
        float rotationY,
        float rotationZ)
    {
        this.positionX = positionX;
        this.positionY = positionY;
        this.rotationX = rotationX;
        this.rotationY = rotationY;
        this.rotationZ = rotationZ;
    }

    public float positionX;
    public float positionY;

    public float rotationX;
    public float rotationY;
    public float rotationZ;
}
