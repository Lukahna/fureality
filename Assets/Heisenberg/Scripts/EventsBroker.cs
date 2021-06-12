using System;

public class EventsBroker
{
    public static event Action OnPressLaserToggle;
    public static void PressToggle()
    {
        if (OnPressLaserToggle != null)
        {
            OnPressLaserToggle();
        }
    }

    public static event Action OnStopPressLaserToggle;
    public static void StopPressToggle()
    {
        if (OnStopPressLaserToggle != null)
        {
            OnStopPressLaserToggle();
        }
    }
}
