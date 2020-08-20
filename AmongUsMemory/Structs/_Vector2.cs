
using System.Runtime.InteropServices;

[System.Serializable]
[StructLayout(LayoutKind.Sequential)]
public struct Vector2
{
    public float x,y;

    public Vector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public bool IsZero()
    {
        if (this.x == 0 && this.y == 0) return true;
        else return false;
    }
    public bool IsGarbage()
    {
        if (this.x == float.MaxValue && this.y == float.MaxValue) return true;
        else return false;
    }
    public static Vector2 Zero
    {
        get
        {
            return new Vector2(0, 0);
        }
    }
}