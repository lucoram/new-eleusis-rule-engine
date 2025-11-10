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
        playedCardsOnTable.Add(new CardData(13, HEART));

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

        // Atout
        PlayedCardValidator mustBeInAtoutDecreasingOrder = AllRules[5];
        isCardValid = true;
        mustBeInAtoutDecreasingOrder(playedCardsOnTable, new CardData(12, HEART), ref isCardValid); // is valid expected
        Debug.Assert(isCardValid);

        isCardValid = true;
        mustBeInAtoutDecreasingOrder(playedCardsOnTable, new CardData(9, HEART), ref isCardValid); // is not valid expected
        Debug.Assert(!isCardValid);

        isCardValid = true;
        mustBeInAtoutDecreasingOrder(playedCardsOnTable, new CardData(11, DIAMOND), ref isCardValid); // is not valid expected
        Debug.Assert(!isCardValid);

        // Sans Atout
        PlayedCardValidator mustBeInSansAtoutDecreasingOrder = AllRules[6];
        isCardValid = true;
        mustBeInSansAtoutDecreasingOrder(playedCardsOnTable, new CardData(12, HEART), ref isCardValid); // is valid expected
        Debug.Assert(isCardValid);

        isCardValid = true;
        mustBeInAtoutDecreasingOrder(playedCardsOnTable, new CardData(1, HEART), ref isCardValid); // is not valid expected
        Debug.Assert(!isCardValid);

        isCardValid = true;
        mustBeInAtoutDecreasingOrder(playedCardsOnTable, new CardData(11, DIAMOND), ref isCardValid); // is not valid expected
        Debug.Assert(!isCardValid);

        // Same Color
        PlayedCardValidator mustBeSameColor = AllRules[7];
        isCardValid = true;
        mustBeSameColor(playedCardsOnTable, new CardData(3, HEART), ref isCardValid); // is valid expected
        Debug.Assert(isCardValid);

        isCardValid = true;
        mustBeSameColor(playedCardsOnTable, new CardData(9, SPADES), ref isCardValid); // is not valid expected
        Debug.Assert(!isCardValid);


        Console.WriteLine("********************* All New Eleusis Rule Engine tests passed *********************");
    }
}