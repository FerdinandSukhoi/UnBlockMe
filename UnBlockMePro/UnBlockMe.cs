namespace UnBlockMePro;

public partial class UnBlockMe : Form
{
    private const int BoardSize = 6;
    private List<byte[,]> answer=new();
    private readonly Bitmap bitMap;
    private readonly int d;

    private readonly Graphics g;
    private int index = -1;
    private readonly byte[,] inputBoard;
    private int red, redX;
    private readonly int w = 3;

    public UnBlockMe()
    {
        InitializeComponent();

        inputBoard = new byte[BoardSize, BoardSize];
        d = board.Width / BoardSize;
        bitMap = new Bitmap(board.Width, board.Height);
        g = Graphics.FromImage(bitMap);
    }

    private void plankNumber_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
    }

    private void unBlockMe_Load(object sender, EventArgs e)
    {
        Reset();

        board.Enabled = true;
        plankNumber.Enabled = true;
        Init();
        board.Image = bitMap;
        btnGoToSolve.Visible = true;
        btnReset.Visible = true;
    }

    private void Reset(bool msgBox = false)
    {
        if (msgBox && MessageBox.Show("重置所有输入?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) !=
            DialogResult.OK)
            return;
        for (var i = 0; i < BoardSize; i++)
        for (var j = 0; j < BoardSize; j++)
            inputBoard[i, j] = 0;
        plankNumber.Visible = true;
        plankNumber.Text = "1";
        btnGoToSolve.Visible = true;
        btnPrev.Visible = false;
        btnNext.Visible = false;
        index = -1;
        Init();
        board.Image = bitMap;
    }
    // 本来打算做撤销功能，但是太麻烦了
    private void btnCancel_Click(object sender, EventArgs e)
    {
        var workState = byte.Parse(plankNumber.Text);
        for (var i = 0; i < BoardSize; i++)
        {
            for (var j = 0; j < BoardSize; j++)
            {
                if (inputBoard[i, j] == workState)
                {
                    inputBoard[i, j] = 0;
                    g.FillRectangle(Brushes.Orange, i + w - 1, j + w - 1, d - 2 * w + 1,
                        d - 2 * w + 1);
                }
            }
        }

        if (workState == 1) return;
        plankNumber.Text = (workState-1).ToString();
    }

    private void Init()
    {
        board.Height = board.Width;
        g.FillRectangle(new SolidBrush(Color.FromArgb(192, 64, 0)), 0, 0, board.Width, board.Height);
        for (var i = board.Width / BoardSize; i < board.Width; i += board.Width / BoardSize)
        {
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 2), 0, i, board.Height, i);
            g.DrawLine(new Pen(new SolidBrush(Color.Black)), i, 0, i, board.Width);
        }
    }

    private void board_Click(object sender, EventArgs e)
    {
        var arg = (MouseEventArgs) e;


        if (arg.Button == MouseButtons.Right)
        {
            if (inputBoard.Cast<byte>().Any(x => x > 0))
            {
                plankNumber.Text = string.IsNullOrEmpty(plankNumber.Text)
                    ? "1"
                    : (int.Parse(plankNumber.Text) + 1).ToString();
            }

        }


        if (!string.IsNullOrWhiteSpace(plankNumber.Text))
        {
            var x = arg.X;
            var y = arg.Y;

            x = x / d * d;
            y = y / d * d;

            var num = byte.Parse(plankNumber.Text);
            if (inputBoard[x / d, y / d] > 0 && inputBoard[x / d, y / d] != num)
                return;

            if (inputBoard[x / d, y / d] > 0 && inputBoard[x / d, y / d] == num)
            {
                inputBoard[x / d, y / d] = 0;
                g.FillRectangle(new SolidBrush(Color.FromArgb(192, 64, 0)), x + w - 1, y + w - 1, d - 2 * w + 1,
                    d - 2 * w + 1);
                board.Image = bitMap;

                if (!inputBoard.Cast<byte>().Any(x => x == num))
                {
                    plankNumber.Text = "1"==plankNumber.Text
                        ? "1"
                        : (num-1).ToString();
                }

                return;
            }

            inputBoard[x / d, y / d] = num;
            g.FillRectangle(Brushes.Orange, x + w, y + w, d - 2 * w, d - 2 * w);
            g.DrawString(plankNumber.Text, new Font("Arial", 18, FontStyle.Regular), Brushes.Black, x + d / 4 + 2,
                y + d / 4 + 2);
            board.Image = bitMap;
        }
    }


    private void btnGoToSolve_Click(object sender, EventArgs e)
    {

        var input = new byte[BoardSize, BoardSize];
        red = 0;
        for (var i = 0; i < BoardSize; i++)
        for (var j = 0; j < BoardSize; j++)
        {
            input[i, j] = inputBoard[j, i];
            if (inputBoard[j, i] == red) redX = i;
            red = Math.Max(red, inputBoard[j, i]);
        }

        for (var i = 0; i < BoardSize; i++)
        for (var j = 0; j < BoardSize; j++)
            if (inputBoard[i, j] == red)
            {
                var x = i * d;
                var y = j * d;
                g.FillRectangle(Brushes.Red, x + w, y + w, d - 2 * w, d - 2 * w);
                g.DrawString(plankNumber.Text, new Font("Arial", 18, FontStyle.Regular), Brushes.Black, x + d / 4 + 2,
                    y + d / 4 + 2);
            }

        btnGoToSolve.Visible = false;
        var ps = new PuzzleSolution();
        var result = ps.SolvePuzzle(input);


        board.Image = bitMap;

        if (result is null)
        {
            MessageBox.Show("没有找到解法。请检查输入。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        answer = result;

        plankNumber.Text = string.Empty;
        plankNumber.Visible = false;
        btnPrev.Enabled = false;
        btnPrev.Visible = true;
        btnNext.Visible = true;
    }

    private void btnPrev_Click(object sender, EventArgs e)
    {
        GoLeft();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
        GoRight();
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        Reset(true);
    }

    private void GoRight()
    {
        if (index == answer.Count - 1)
        {
            Reset(true);
            return;
        }
        if (!btnPrev.Enabled)
        {
            btnPrev.Enabled = true;
            Init();
        }

        index++;
        DrawState(answer[ index]);
    }

    private void GoLeft()
    {
        if (index == 0)
            return;
        index--;
        DrawState(answer[index]);
    }

    private void DrawState(byte[,] state)
    {
        Init();
        var vis = 0;
        for (var i = 0; i < BoardSize; i++)
        for (var j = 0; j < BoardSize; j++)
        {
            int num = state[i, j];
            if ((vis & (1 << num)) == 1) continue;
            int sx = 0, sy = 0, dx = 0, dy = 0;
            if (num <= 0) continue;
            int ii = i, jj = j;
            if (j is > 0 and < BoardSize - 1 && (state[ii, jj - 1] == num || state[ii, jj + 1] == num))
            {
                vis |= 1 << num;
                while (jj > 0 && state[ii, jj - 1] == num)
                    jj--;

                sx = ii;
                sy = jj;
                while (jj < BoardSize && state[ii, jj] == num)
                    jj++;

                dx = 1;
                dy = jj - sy;
            }
            else if (i is > 0 and < BoardSize - 1 && (state[ii - 1, jj] == num || state[ii + 1, jj] == num))
            {
                vis |= 1 << num;
                while (ii > 0 && state[ii - 1, jj] == num)
                    ii--;
                sx = ii;
                sy = jj;
                while (ii < BoardSize && state[ii, jj] == num)
                    ii++;
                dx = ii - sx;
                dy = 1;
            }

            var brush = num == red ? Brushes.Red : Brushes.Orange;
            g.FillRectangle(brush, sy * d + w, sx * d + w, dy * d - 2 * w, dx * d - 2 * w);
            //g.DrawString(num.ToString(), new Font("Arial", 18, FontStyle.Regular), Brushes.Black, k * d + d / 4 + 2, j * d + d / 4 + 2);
        }

        if (answer.Any()&& state==answer.Last())
            g.DrawLine(new Pen(new SolidBrush(Color.Green), 3), 0, redX * d + d / 2, board.Width, redX * d + d / 2);
        board.Image = bitMap;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.A)
            GoLeft();
        else if (e.KeyCode == Keys.D)
            GoRight();
        else if (e.KeyCode == Keys.R)
            Reset(true);

        base.OnKeyDown(e);
    }
}