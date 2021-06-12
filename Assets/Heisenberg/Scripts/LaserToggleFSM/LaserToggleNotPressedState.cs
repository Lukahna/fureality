using System.Collections;
using System.Collections.Generic;

public class LaserToggleNotPressedState : LaserToggle_BaseState
{
    public override void EnterState(LaserToggle_FSM toggle)
    {
        //toggle.m_SpriteRenderer.sprite = toggle.m_NotPressedButtonSprite;
        toggle.NoWeightOnButton();
    }

    public override void OnTriggerEnterState(LaserToggle_FSM toggle)
    {
        if (toggle.m_ColliderOfOther.TryGetComponent<IInteractable>(out var comp))
        {
            toggle.TransitionToState(toggle.PressedState);
        }
    }

    public override void OnTriggerExitState(LaserToggle_FSM toggle)
    {
        
    }
}
