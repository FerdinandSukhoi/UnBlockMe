using System.Text;

namespace UnBlockMePro;

public record State
{
    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < BoardSize; i++)
        {
            for (var j = 0; j < BoardSize; j++)
            {
                sb.Append(Board[i, j].ToString());
            }

            sb.Append('\n');
        }

        return sb.ToString();
    }
    public string Figure => ToString();
    public State()
    {
        From = null;
    }

    public State(List<Plank> planks, byte[,] board, State? from = null)
    {
        From = from;
        Board = board;
        Planks = planks;
    }

    public byte[,] Board { get; } = new byte[BoardSize, BoardSize];
    public List<Plank> Planks { get; } = new();
    public State? From { get; }

    public override int GetHashCode()
    {
        unchecked
        {
            var r = 0;
            for (var i = 0; i < BoardSize; i++)
                for (var j = 0; j < BoardSize; j++)
                    r = r * HashBase + Board[i, j];
            return r;
        }
    }

    public State CreateNext()
    {
        return new State(new List<Plank>(Planks),
            BoardDeepCopy(Board), this);
    }

    public bool IsSolution()
    {
        var x = Planks.Last().Last.X;
        var y = Planks.Last().Last.Y + 1;
        while (y < BoardSize && Board[x, y] == 0) y++;

        if (y == BoardSize) Console.WriteLine();
        return y == BoardSize;
    }

    public List<byte[,]> ExportSolution()
    {
        var r = new List<byte[,]>();
        ExportSolution(r);
        return r;
    }

    private void ExportSolution(IList<byte[,]> result)
    {

        From?.ExportSolution(result); result.Add(Board);
    }
}