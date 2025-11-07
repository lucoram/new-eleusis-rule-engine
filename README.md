# new-eleusis-rule-engine
New Eleusis game rule engine open for contribution

__INSTRUCTIONS ON HOW TO CONTRIBUTE__

1. **_Create a new branch from main for each any new rule :_** Each new rule must be created in a new branch from main, with the rule name as branch name
2. **_Add new rule definitions in `RuleEngine.cs` at the end of the file :_** Following the format of existing rules with exact same signature, i.e: `private static void RuleName(List<CardData> dealtCardsCopy, CardData playedCard, ref bool isCardValid)`
3. **_Returned value must be `isCardValid = isCardValid && <the specific condition of your rule>`_**
4. **_Do not forget to :_**
    - Increase the `AllRules` array capacity
    - Add the new rule to `AllRules`
    - Update the `incompatibilityDict` with the new rule index
5. **_To test your new rule, make changes in `Program.cs` :_**
    - You may re-assign new `CardData`s in the `playedCardsOnTable` list or update it
    - Assign your new rule to the delegate and call it at least twice, one for each "correct" and "incorrect" outcomes
    - Re-init the `isCardValid` ref before each call, and assert it afterwards
6. **_Run the `Program` and make sure `All New Eleusis Rule Engine tests passed` is printed before asking for a pull request_**