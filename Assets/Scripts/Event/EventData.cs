public class EventData
{
    private Card card;
    public DataCard dataCard;
    private int integer;
    private string text;
    private CombatUnit combatUnit;

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
