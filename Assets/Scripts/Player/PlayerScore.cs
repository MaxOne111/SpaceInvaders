public class PlayerScore
{
    public static int _Score;

    public void Reset() => _Score = 0;

    public void AddScore(int _value)
    {
        if (_value < 0)
            return;

        _Score += _value;
    }
}