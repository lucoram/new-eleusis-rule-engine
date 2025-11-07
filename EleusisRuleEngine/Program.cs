using System.Diagnostics;

using static RuleEngine;
using static CardData.CardSuits;

class MainTest
{
    static void Main(string[] args)
    {
        Console.WriteLine("********************* New Eleusis Rule Engine tests begins *********************");

        List<CardData> playedCardsOnTable = new List<CardData>();

        playedCardsOnTable.Add(new CardData(7, SPADES));
        playedCardsOnTable.Add(new CardData(7, HEART));
        playedCardsOnTable.Add(new CardData(10, CLUBS));
        playedCardsOnTable.Add(new CardData(13, DIAMOND));
        playedCardsOnTable.Add(new CardData(4, CLUBS));
        playedCardsOnTable.Add(new CardData(6, HEART));

        PlayedCardValidator mustAlternateColor = AllRules[2];

        bool isCardValid = true;
        mustAlternateColor(playedCardsOnTable, new CardData(6, SPADES), ref isCardValid); // is valid expected
        Debug.Assert(isCardValid);

        isCardValid = true;
        mustAlternateColor(playedCardsOnTable, new CardData(12, HEART), ref isCardValid); // is not valid expected
        Debug.Assert(!isCardValid);

        isCardValid = true;
        mustAlternateColor(playedCardsOnTable, new CardData(6, DIAMOND), ref isCardValid); // is not valid expected
        Debug.Assert(!isCardValid);

        // TEST YOUR NEW RULE HERE

        Console.WriteLine("********************* All New Eleusis Rule Engine tests passed *********************");
    }
}