using System.Collections.Generic;
using System;

namespace Platformer
{
    public class Level : IResetable
    {
        public enum ResultState
        {
            Undefined,
            Win,
            Lose
        }

        private List<ICondition> winConditionList = new List<ICondition>();
        private List<ICondition> loseConditionList = new List<ICondition>();

        public event Action OnGameResult;

        private ResultState resultState = ResultState.Undefined;

        public Level(List<ICondition> winConditionList, List<ICondition> loseConditionList)
        {
            this.winConditionList = winConditionList;
            this.loseConditionList = loseConditionList;

            foreach (var condition in winConditionList)
            {
                condition.OnConditionMet += CheckConditions;
            }

            foreach (var condition in loseConditionList)
            {
                condition.OnConditionMet += CheckConditions;
            }
        }

        private void CheckConditions()
        {
            if (resultState != ResultState.Undefined)
                return;

            if (AreAllConditionsMet(winConditionList))
            {
                resultState = ResultState.Win;
            }
            else if (AreAllConditionsMet(loseConditionList))
            {
                resultState =ResultState.Lose;
            }

            if (resultState != ResultState.Undefined)
                OnGameResult?.Invoke();
        }

        private bool AreAllConditionsMet(List<ICondition> conditions)
        {
            foreach (var condition in conditions)
            {
                if (!condition.IsConditionMet())
                {
                    return false;
                }
            }

            return true;
        }

        public ResultState GetResult()
        {
            return resultState;
        }

        public void Reset()
        {
            resultState = ResultState.Undefined;
            foreach (var condition in winConditionList)
            {
                condition.Reset();
            }

            foreach (var condition in loseConditionList)
            {
                condition.Reset();
            }
        }

        ~Level()
        {
            foreach (var condition in winConditionList)
            {
                condition.OnConditionMet -= CheckConditions;
            }

            foreach (var condition in loseConditionList)
            {
                condition.OnConditionMet -= CheckConditions;
            }
        }

    }

}