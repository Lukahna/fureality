using System;

public class EventsBroker
{
    public static event Action<int> OnPressLaserToggle;
    public static void PressToggle(int id)
    {
        if (OnPressLaserToggle != null)
        {
            OnPressLaserToggle(id);
        }
    }

    public static event Action<int> OnStopPressLaserToggle;
    public static void StopPressToggle(int id)
    {
        if (OnStopPressLaserToggle != null)
        {
            OnStopPressLaserToggle(id);
        }
    }
}
