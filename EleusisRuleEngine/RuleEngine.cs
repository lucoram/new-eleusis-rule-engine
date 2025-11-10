class RuleEngine
{
    // ********************* DO NOT MODIFY
    private static System.Random RANDOM = new System.Random();
    private static Dictionary<int, HashSet<int>> incompatibilityDict = new Dictionary<int, HashSet<int>>();
    public delegate void PlayedCardValidator(List<CardData> dealtCardsCopy, CardData playedCard, ref bool isCardValid);
    // ********************* END DO NOT MODIFY

    private static readonly int[] NaturalCardOrder = { 0, 14, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
    private static readonly int[] AtoutCardOrder = { 0, 11, 0, 0, 0, 0, 0, 0, 0, 14, 10, 20, 3, 4 };
    private static readonly int[] SansAtoutCardOrder = { 0, 11, 0, 0, 0, 0, 0, 0, 0, 0, 10, 2, 3, 4 };

    // MAKE SURE TO UPDATE THE ARRAY CAPACITY
    public static PlayedCardValidator[] AllRules = new PlayedCardValidator[8];

    static RuleEngine()
    {
        AllRules[0] = AlternateBetweenOddAndEven;
        AllRules[1] = ValueDiffMustBeSmallerOrEqualToThree;
        AllRules[2] = MustAlternateColor;
        AllRules[3] = MustBeInDecreasingOrder;
        AllRules[4] = MustBeInIncreasingOrder;
        // ADD NEW RULES FROM HERE
        AllRules[5] = MustBeInAtoutDecreasingOrder;
        AllRules[6] = MustBeInSansAtoutDecreasingOrder;
        AllRules[7] = MustBeSameColor;

        // MAKE SURE TO UPDATE INCOMPATIBILITIES
        incompatibilityDict.Add(0, new HashSet<int>(new int[] { }));
        incompatibilityDict.Add(1, new HashSet<int>(new int[] { }));
        incompatibilityDict.Add(2, new HashSet<int>(new int[] { }));
        incompatibilityDict.Add(3, new HashSet<int>(new int[] { 4 }));
        incompatibilityDict.Add(4, new HashSet<int>(new int[] { 3 }));
        
        incompatibilityDict.Add(5, new HashSet<int>(new int[] { 0, 1, 3, 4 }));
        incompatibilityDict.Add(6, new HashSet<int>(new int[] { 0, 1, 3, 4, 5 }));
        incompatibilityDict.Add(7, new HashSet<int>(new int[] { 2 }));
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

    private static void MustBeInAtoutDecreasingOrder(List<CardData> dealtCardsCopy, CardData playedCard, ref bool isCardValid)
    {
        CardData lastCardOnTable = dealtCardsCopy[dealtCardsCopy.Count - 1];
        int lastCardValue = AtoutCardOrder[lastCardOnTable.cardValue];
        int currentCardValue = AtoutCardOrder[playedCard.cardValue];

        isCardValid = isCardValid && (currentCardValue <= lastCardValue);
    }

    private static void MustBeInSansAtoutDecreasingOrder(List<CardData> dealtCardsCopy, CardData playedCard, ref bool isCardValid)
    {
        CardData lastCardOnTable = dealtCardsCopy[dealtCardsCopy.Count - 1];
        int lastCardValue = SansAtoutCardOrder[lastCardOnTable.cardValue];
        int currentCardValue = SansAtoutCardOrder[playedCard.cardValue];

        isCardValid = isCardValid && (currentCardValue <= lastCardValue);
    }

    private static void MustBeSameColor(List<CardData> dealtCardsCopy, CardData playedCard, ref bool isCardValid)
    {
        CardData lastCardOnTable = dealtCardsCopy[dealtCardsCopy.Count - 1];
        isCardValid = isCardValid && (lastCardOnTable.cardSuit == playedCard.cardSuit);
    }

    // ADD NEW RULES DEFINITIONS ABOVE THIS LINE
}