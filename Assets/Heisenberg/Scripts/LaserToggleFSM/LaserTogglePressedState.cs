using System.Collections;
using System.Collections.Generic;

public class LaserTogglePressedState : LaserToggle_BaseState
{
    public override void EnterState(LaserToggle_FSM toggle)
    {
        //toggle.m_SpriteRenderer.sprite = toggle.m_PressedButtonSprite;
        toggle.WeightOnButton();
    }

    public override void OnTriggerEnterState(LaserToggle_FSM toggle)
    {
        
    }

    public override void OnTriggerExitState(LaserToggle_FSM toggle)
    {
        if (toggle.m_ColliderOfOther.TryGetComponent<IInteractable>(out var comp))
        {
            toggle.TransitionToState(toggle.NotPressedState);
        }
    }

}
