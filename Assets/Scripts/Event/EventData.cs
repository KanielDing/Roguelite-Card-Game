public class EventData
{
    public Card card;
    public DataCard dataCard;
    public int integer;
    public string text;
    public CombatUnit combatUnit;

    public EventData()
    {
    }

    public EventData With(
        Card card = null,
        DataCard dataCard = null,
        int? integer = null,
        string text = null,
        CombatUnit combatUnit = null)
    {
        if (card) this.card = card;
        if (dataCard) this.dataCard = dataCard;
        if (integer.HasValue) this.integer = integer.Value;
        if (null != text) this.text = text;
        if (combatUnit) this.combatUnit = combatUnit;
        return this;
    }
}