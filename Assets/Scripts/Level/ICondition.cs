using System;
namespace Platformer
{
    public interface ICondition : IResetable
    {
        event Action OnConditionMet;

        bool IsConditionMet();
    }

}