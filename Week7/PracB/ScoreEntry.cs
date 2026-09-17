using System;

public class ScoreEntry
{
    public ScoreEntry(
        int score,
        int stageReached,
        DateTime achievedAt)
    {
        Score = score;
        StageReached = stageReached;
        AchievedAt = achievedAt;
    }

    public ScoreEntry(int score, int stageReached)
        : this(score, stageReached, DateTime.Now)
    {
    }

    public int Score { get; set; }
    public int StageReached { get; set; }
    public DateTime AchievedAt { get; set; }
}