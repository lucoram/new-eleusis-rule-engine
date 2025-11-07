class RuleEngine
{
    // ********************* DO NOT MODIFY
    private static System.Random RANDOM = new System.Random();
    private static Dictionary<int, HashSet<int>> incompatibilityDict = new Dictionary<int, HashSet<int>>();
    public delegate void PlayedCardValidator(List<CardData> dealtCardsCopy, CardData playedCard, ref bool isCardValid);
    // ********************* END DO NOT MODIFY

    // MAKE SURE TO UPDATE THE ARRAY CAPACITY
    public static PlayedCardValidator[] AllRules = new PlayedCardValidator[5];

    static RuleEngine()
    {
        AllRules[0] = AlternateBetweenOddAndEven;
        AllRules[1] = ValueDiffMustBeSmallerOrEqualToThree;
        AllRules[2] = MustAlternateColor;
        AllRules[3] = MustBeInDecreasingOrder;
        AllRules[4] = MustBeInIncreasingOrder;
        // ADD NEW RULES FROM HERE

        // MAKE SURE TO UPDATE INCOMPATIBILITIES
        incompatibilityDict.Add(0, new HashSet<int>(new int[] { }));
        incompatibilityDict.Add(1, new HashSet<int>(new int[] { }));
        incompatibilityDict.Add(2, new HashSet<int>(new int[] { }));
        incompatibilityDict.Add(3, new HashSet<int>(new int[] { 4 }));
        incompatibilityDict.Add(4, new HashSet<int>(new int[] { 3 }));
    }

    // ********************* DO NOT MODIFY
    public static PlayedCardValidator SelectRandomRules(int nbOfRulesToSelect)
    {
        PlayedCardValidator playedCardValidator = null;
        HashSet<int> selectedRules = new HashSet<int>();

        while (nbOfRulesToSelect > 0)
        {
            int candidateRuleIndex = RANDOM.Next(AllRules.Length);

            if (selectedRules.Contains(candidateRuleIndex))
            {
                continue;
            }

            if (!CanSelectRule(candidateRuleIndex, selectedRules))
            {
                continue;
            }

            if (playedCardValidator == null)
            {
                playedCardValidator = AllRules[candidateRuleIndex];
            }
            else
            {
                playedCardValidator += AllRules[candidateRuleIndex];
            }

            selectedRules.Add(candidateRuleIndex);
            nbOfRulesToSelect--;
        }

        return playedCardValidator;
    }

    private static bool CanSelectRule(int candidateRuleIndex, HashSet<int> selectedRules)
    {
        HashSet<int> ruleIncompatibility = incompatibilityDict[candidateRuleIndex];
        foreach (int selectedRuleIndex in selectedRules)
        {
            if (ruleIncompatibility.Contains(selectedRuleIndex))
            {
                return false;
            }
        }

        return true;
    }
    // ********************* END DO NOT MODIFY

    // DO NOT MODIFY EXISTING RULES
    private static void AlternateBetweenOddAndEven(List<CardData> dealtCardsCopy, CardData playedCard, ref bool isCardValid)
    {
        CardData lastCardOnTable = dealtCardsCopy[dealtCardsCopy.Count - 1];
        int lastCardValue = lastCardOnTable.cardValue;
        int currentCardValue = playedCard.cardValue;

        isCardValid = isCardValid && ((lastCardValue % 2 == 0 && currentCardValue % 2 != 0) || (lastCardValue % 2 != 0 && currentCardValue % 2 == 0) ||
            (currentCardValue != lastCardValue && ((currentCardValue == 1 && (lastCardValue % 2 != 0)) || (lastCardValue == 1 && (currentCardValue % 2 != 0)))));
    }

    private static void ValueDiffMustBeSmallerOrEqualToThree(List<CardData> dealtCardsCopy, CardData playedCard, ref bool isCardValid)
    {
        CardData lastCardOnTable = dealtCardsCopy[dealtCardsCopy.Count - 1];
        int lastCardValue = lastCardOnTable.cardValue;
        int currentCardValue = playedCard.cardValue;

        if (currentCardValue == 1 && lastCardValue > 4)
        {
            currentCardValue = 14;
        }
        else if (lastCardValue == 1 && currentCardValue > 4)
        {
            lastCardValue = 14;
        }

        isCardValid = isCardValid && Math.Abs(currentCardValue - lastCardValue) <= 3;
    }

    private static void MustAlternateColor(List<CardData> dealtCardsCopy, CardData playedCard, ref bool isCardValid)
    {
        CardData lastCardOnTable = dealtCardsCopy[dealtCardsCopy.Count - 1];
        CardData.CardSuits lastCardSuit = lastCardOnTable.cardSuit;
        CardData.CardSuits currentCardSuit = playedCard.cardSuit;

        bool blackThenRed = (lastCardSuit == CardData.CardSuits.SPADES || lastCardSuit == CardData.CardSuits.CLUBS)
        && (currentCardSuit == CardData.CardSuits.HEART || currentCardSuit == CardData.CardSuits.DIAMOND);

        bool redThenBlack = (lastCardSuit == CardData.CardSuits.HEART || lastCardSuit == CardData.CardSuits.DIAMOND)
        && (currentCardSuit == CardData.CardSuits.SPADES || currentCardSuit == CardData.CardSuits.CLUBS);

        isCardValid = isCardValid && (blackThenRed || redThenBlack);
    }

    private static void MustBeInDecreasingOrder(List<CardData> dealtCardsCopy, CardData playedCard, ref bool isCardValid)
    {
        CardData lastCardOnTable = dealtCardsCopy[dealtCardsCopy.Count - 1];
        int lastCardValue = lastCardOnTable.cardValue;
        int currentCardValue = playedCard.cardValue;

        if (lastCardValue == 1)
        {
            lastCardValue = 14;
        }

        isCardValid = isCardValid && (currentCardValue < lastCardValue);
    }

    private static void MustBeInIncreasingOrder(List<CardData> dealtCardsCopy, CardData playedCard, ref bool isCardValid)
    {
        CardData lastCardOnTable = dealtCardsCopy[dealtCardsCopy.Count - 1];
        int lastCardValue = lastCardOnTable.cardValue;
        int currentCardValue = playedCard.cardValue;

        if (currentCardValue == 1)
        {
            currentCardValue = 14;
        }

        isCardValid = isCardValid && (currentCardValue > lastCardValue);
    }

    // ADD NEW RULES DEFINITIONS ABOVE THIS LINE
}