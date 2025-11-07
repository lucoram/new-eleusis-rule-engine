// DO NOT MODIFY THIS FILE AT ALL
class CardData
{
    public enum CardSuits { SPADES, CLUBS, HEART, DIAMOND };

    public CardSuits cardSuit;
    public int cardValue;

    public CardData(int value, CardSuits suit)
    {
        this.cardValue = value;
        this.cardSuit = suit;
    }
}
