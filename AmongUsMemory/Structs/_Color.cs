public struct Color 
{ 
    public float r; 
    public float g; 
    public float b; 
    public float a;

    public Color(float r, float g, float b) : this()
    {
        this.r = r;
        this.g = g;
        this.b = b;
        a = 1;
    }

    public Color(float r, float g, float b, float a)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }
}