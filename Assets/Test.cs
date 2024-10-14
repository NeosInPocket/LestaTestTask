using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        var size = new Vector3(3f, 0.3f, 3f);
        var pos = new Vector3(1f, 0f, 1f);

        var bounds1 = new Bounds(Vector3.zero, size);
        var bounds2 = new Bounds(pos, size);

        Debug.Log(bounds1.Intersects(bounds2));
    }
}

public class TestSaveable : ISaveable
{
    public int testInt = 1;
    public int testInt2 = 2;
    public int testInt3 = 3;
}