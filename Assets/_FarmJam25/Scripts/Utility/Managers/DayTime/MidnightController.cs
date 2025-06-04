using NJG.Utilities.EventBus;
using UnityEngine;
using static TimeOfDayController;

public class MidnightController : MonoBehaviour
{
    public void OnEnable()
    {
        EventBus.StartListening<DayCycleEvent>(OnDayCycleChanged);
        Debug.Log("it is midnight!");
    }

    public void OnDisable()
    {
        EventBus.StopListening<DayCycleEvent>(OnDayCycleChanged);
    }

    private void OnDayCycleChanged(DayCycleEvent e)
    {

    }
}
