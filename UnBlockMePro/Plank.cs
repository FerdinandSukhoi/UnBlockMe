namespace UnBlockMePro;

public struct Plank
{
    public Plank() : this(default, default)
    {
    }

    public Plank(Point first, Point last)
    {
        (First, Last) = (first, last);
    }

    public Point First { get; set; }
    public Point Last { get; set; }
}