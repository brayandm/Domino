using System.Diagnostics;

class History
{
    private List<HistoryRound> _historyRounds = new List<HistoryRound>();

    public void NewHistoryRound()
    {
        this._historyRounds.Add(new HistoryRound());
    }

    public HistoryRound GetCurrentHistoryRound()
    {
        Debug.Assert(this._historyRounds.Count > 0);
        return this._historyRounds.Count > 0 ? this._historyRounds.Last() : new HistoryRound();
    }

    public List<HistoryRound> GetHistoryRounds()
    {
        return this._historyRounds;
    }
}