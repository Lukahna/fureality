using System.Collections;
using System.Collections.Generic;

public class LaserActiveState : Laser_BaseState
{
    public override void EnterState(LaserGate_FSM laser)
    {
        laser.ActivateLaser();
    }

    public override void OnCollisionEnterState(LaserGate_FSM laser)
    {
        
    }

    public override void UpdateState(LaserGate_FSM laser)
    {
        //laser.EventsBroker_OnStopPressLaserToggle();
    }
}
