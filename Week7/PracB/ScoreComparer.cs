using System.Collections.Generic;

public class ScoreComparer : IComparer<ScoreEntry>
{
    public int Compare(ScoreEntry? x, ScoreEntry? y)
    {
        if (x is null && y is null) return 0;
        if (x is null) return -1;
        if (y is null) return 1;

        // Compare scores first, in ascending order.
        int result = x.Score.CompareTo(y.Score);
        if (result != 0)
        {
            return result;
        }

        // Equal scores are ordered by stage reached.
        return x.StageReached.CompareTo(y.StageReached);
    }
}
