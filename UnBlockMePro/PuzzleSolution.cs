global using static UnBlockMePro.PuzzleSolution;


namespace UnBlockMePro;

public class PuzzleSolution
{
    public const int BoardSize = 6;
    public const int HashBase = 19260817;
    private readonly List<Point>[] _planks;
    private readonly Queue<State> _queue = new();
    private readonly HashSet<int> _visited = new();


    public PuzzleSolution()
    {
        _planks = new List<Point>[20];
        for (var i = 0; i < 20; i++) _planks[i] = new List<Point>();
    }

    public static byte[,] BoardDeepCopy(byte[,] board)
    {
        var r = new byte[BoardSize, BoardSize];
        Array.Copy(board, r, board.Length);
        return r;
    }


    public List<byte[,]>? SolvePuzzle(byte[,] input)
    {
        void EnQueue(State newState)
        {
            var hash = newState.GetHashCode();
            if (_visited.Contains(hash)) return;
            _visited.Add(hash);
            _queue.Enqueue(newState);
        }

        var initState = new State();
        var red = input.Cast<byte>().Max();
        //Array.Copy(initState.Board, input,input.Length);
        //  本模块的矩阵实际上是输入的转置...
        for (var i = 0; i < BoardSize; i++)
        for (var j = 0; j < BoardSize; j++)
            initState.Board[i, j] = input[i,j];
        var vis = 0;
        for (var i = 0; i < BoardSize; i++)
        for (var j = 0; j < BoardSize; j++)
        {
            var num = initState.Board[i, j];
            if ((vis & (1 << num)) != 0) continue;
            if (num <= 0) continue;
            int ii = i, jj = j;
            if (j is > 0 and < BoardSize - 1 &&
                (initState.Board[ii, jj - 1] == num || initState.Board[ii, jj + 1] == num))
            {
                vis |= 1 << num;
                while (jj > 0 && initState.Board[ii, jj - 1] == num)
                    jj--;

                while (jj < BoardSize && initState.Board[ii, jj] == num)
                {
                    _planks[num].Add(new Point(ii, jj));
                    jj++;
                }
            }
            else if (i is > 0 and < BoardSize - 1 &&
                     (initState.Board[ii - 1, jj] == num || initState.Board[ii + 1, jj] == num))
            {
                vis |= 1 << num;
                while (ii > 0 && initState.Board[ii - 1, jj] == num)
                    ii--;
                while (ii < BoardSize && initState.Board[ii, jj] == num)
                {
                    _planks[num].Add(new Point(ii, jj));
                    ii++;
                }
            }
        }

        for (var i = 0; i < 20; i++)
            if (_planks[i].Count > 0)
                //sort(_planks[i].begin(), _planks[i].end(), [](plank a, plank b) . bool { return a.First.X < b.First.X; });
                initState.Planks.Add(new Plank(_planks[i].First(), _planks[i].Last()));

        EnQueue(initState);
        while (_queue.Any())
        {
            var ff = _queue.Dequeue();

            if (_visited.Count >= 25000000) return null;

            if (ff.IsSolution()) 
                return ff.ExportSolution();

            for (var i = (byte) 0; i < red; i++)
            {
                // 是否为水平滑块

                var first = ff.Planks[i].First;
                var last = ff.Planks[i].Last;
                var horizontal = first.X == last.X;
                if (horizontal)
                {
                    var xx = first.X;
                    var yy = last.Y;

                    // right
                    var newState = ff.CreateNext();
                    for (; yy < BoardSize - 1 && ff.Board[xx, yy + 1] == 0; yy++) ;
                    newState.Planks[i] =
                        new Plank(new Point(first.X, first.Y + yy - last.Y),
                            new Point(last.X, yy));
                    for (var ty = first.Y; ty <= last.Y; ty++)
                        newState.Board[first.X, ty] = 0;
                    for (var ty = first.Y + yy - last.Y; ty <= yy; ty++)
                        newState.Board[first.X, ty] = (byte) (i + 1);

                    EnQueue(newState);

                    yy = first.Y;
                    // left
                    newState = ff.CreateNext();
                    for (; yy > 0 && ff.Board[xx, yy - 1] == 0; yy--) ;
                    //while (yy > 0)
                    //{
                    //    if (ff.Board[xx, yy - 1] != 0) break;
                    //    yy--;

                    //}
                    newState.Planks[i] =
                        new Plank(new Point(first.X, yy),
                            new Point(last.X, last.Y - (first.Y - yy)));
                    for (var ty = first.Y; ty <= last.Y; ty++)
                        newState.Board[first.X, ty] = 0;
                    for (var ty = yy; ty <= last.Y - (first.Y - yy); ty++)
                        newState.Board[first.X, ty] = (byte) (i + 1);

                    EnQueue(newState);
                }
                else
                {
                    var xx = first.X;
                    var yy = first.Y;

                    var newState = ff.CreateNext();
                    // up
                    for (; xx > 0 && ff.Board[xx - 1, yy] == 0; xx--) ;

                    newState.Planks[i] =
                        new Plank(new Point(xx, first.Y),
                            new Point(last.X - (first.X - xx), last.Y));
                    for (var tx = first.X; tx <= last.X; tx++)
                        newState.Board[tx, first.Y] = 0;
                    for (var tx = xx; tx <= last.X - (first.X - xx); tx++)
                        newState.Board[tx, first.Y] = (byte) (i + 1);

                    EnQueue(newState);
                    newState = ff.CreateNext();
                    xx = last.X;
                    // down
                    for (; xx < BoardSize - 1 && ff.Board[xx + 1, yy] == 0; xx++) ;

                    newState.Planks[i] =
                        new Plank(new Point(first.X + (xx - last.X), first.Y),
                            new Point(xx, last.Y));
                    for (var tx = first.X; tx <= last.X; tx++)
                        newState.Board[tx, first.Y] = 0;
                    for (var tx = first.X + (xx - last.X); tx <= xx; tx++)
                        newState.Board[tx, first.Y] = (byte) (i + 1);

                    EnQueue(newState);
                }
            }
        }

        return null;
    }
}