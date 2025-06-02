using NJG.Utilities.EventBus;
using Sirenix.OdinInspector;
using UnityEngine;

public enum DayCycle { Morning, Evening, Afternoon, Midnight }
public class TimeOfDayController : MonoBehaviour
{
    [SerializeField] private float _fullDayLength = 120f;
    
    [SerializeField] private bool _freezeTime = false;

    private float _currentTime;
    private float segment;
    private DayCycle _currentCycle;

    private void Start()
    {
        segment = _fullDayLength / 4f;
        SetCycle(DayCycle.Morning, false);
    }

    public void FreezeTime(bool freeze)
    {
        _freezeTime = freeze;
    }

    private void Update()
    {
        if (_freezeTime)
            return;

        _currentTime += Time.deltaTime;
        if (_currentTime >= segment)
            SwitchToNextCycle();
    }

    [Button]
    private void SwitchToNextCycle()
    {
        DayCycle newCycle = (DayCycle)(((int)_currentCycle + 1) % 4);
        SetCycle(newCycle);
    }
    
    private void SetCycle(DayCycle newCycle, bool returnIfSame = true)
    {
        if (newCycle == _currentCycle && returnIfSame)
            return;

        _currentTime = 0;
        _currentCycle = newCycle;
        EventBus.TriggerEvent(new DayCycleEvent(_currentCycle));
    }
}

public struct DayCycleEvent
{
    public DayCycle Cycle { get; private set; }

    public DayCycleEvent(DayCycle cycle)
    {
        Cycle = cycle;
    }
}
