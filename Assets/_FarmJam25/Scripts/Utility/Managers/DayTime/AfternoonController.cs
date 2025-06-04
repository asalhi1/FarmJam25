using NJG.Utilities.EventBus;
using UnityEngine;
using static TimeOfDayController;

public class AfternoonController : MonoBehaviour
{
    public void OnEnable()
    {
        EventBus.StartListening<DayCycleEvent>(OnDayCycleChanged);
        Debug.Log("it is afternoon time!");
    }

    public void OnDisable()
    {
        EventBus.StopListening<DayCycleEvent>(OnDayCycleChanged);
    }

    private void OnDayCycleChanged(DayCycleEvent e)
    {

    }
}
