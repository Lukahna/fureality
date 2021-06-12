using System.Collections;
using System.Collections.Generic;

public class LaserDeactivedState : Laser_BaseState
{
    public override void EnterState(LaserGate_FSM laser)
    {
        laser.DeactivateLaser();
    }

    public override void OnCollisionEnterState(LaserGate_FSM laser)
    {
        
    }

    public override void UpdateState(LaserGate_FSM laser)
    {
        //laser.EventsBroker_OnPressLaserToggle();
    }
}
