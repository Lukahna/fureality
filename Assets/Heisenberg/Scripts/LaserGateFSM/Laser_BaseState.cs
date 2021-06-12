using System.Collections;
using System.Collections.Generic;

public abstract class Laser_BaseState
{
    public abstract void EnterState(LaserGate_FSM laser);

    public abstract void UpdateState(LaserGate_FSM laser);

    public abstract void OnCollisionEnterState(LaserGate_FSM laser);
}
