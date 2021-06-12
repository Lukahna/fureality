using System.Collections;
using System.Collections.Generic;

public abstract class LaserToggle_BaseState
{
    public abstract void EnterState(LaserToggle_FSM laser);
    public abstract void OnTriggerEnterState(LaserToggle_FSM laser);

    public abstract void OnTriggerExitState(LaserToggle_FSM laser);

}
