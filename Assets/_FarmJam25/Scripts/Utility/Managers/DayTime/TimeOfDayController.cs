using MEC;
using NJG.Utilities.EventBus;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDayController : MonoBehaviour
{
    [SerializeField] private float _fullDayLength = 120f;
    [SerializeField] private bool _freezeTime = false;


    public enum DayCycle
    {
        Morning,
        Evening,
        Afternoon,
        Midnight
    }
    public struct DayCycleEvent
    {
        public DayCycle Cycle { get; private set; }

        public DayCycleEvent(DayCycle cycle)
        {
            Cycle = cycle;
        }
    }

    private float _currentTime;
    private DayCycle _currentCycle;

    private CoroutineHandle _timeLoopHandle;

    private void OnEnable()
    {
        _currentTime = 0f;
        _currentCycle = DayCycle.Morning;
        FireCycleEvent(_currentCycle);

        _timeLoopHandle = Timing.RunCoroutine(TimeCycleLoop().CancelWith(gameObject));
    }

    private void OnDisable()
    {
        if (_timeLoopHandle.IsRunning)
            Timing.KillCoroutines(_timeLoopHandle);
    }

    private IEnumerator<float> TimeCycleLoop()
    {
        float segment = _fullDayLength / 4f;

        while (true)
        {
            if (!_freezeTime)
            {
                _currentTime += Time.deltaTime;

                float normalizedTime = _currentTime % _fullDayLength;
                DayCycle newCycle = GetCycleFromTime(normalizedTime, segment);

                if (newCycle != _currentCycle)
                {
                    _currentCycle = newCycle;
                    FireCycleEvent(_currentCycle);
                }
            }

            yield return Timing.WaitForOneFrame;
        }
    }

    private DayCycle GetCycleFromTime(float time, float segment)
    {
        if (time < segment) return DayCycle.Morning;
        else if (time < segment * 2f) return DayCycle.Afternoon;
        else if (time < segment * 3f) return DayCycle.Evening;
        else return DayCycle.Midnight;
    }

    private void FireCycleEvent(DayCycle cycle)
    {
        EventBus.TriggerEvent(new DayCycleEvent(cycle));
        Debug.Log($"[TimeOfDayManager] New cycle: {cycle}");
    }

    public void SetCycle(DayCycle newCycle)
    {
        if (newCycle != _currentCycle)
        {
            _currentCycle = newCycle;
            _currentTime = GetTimeFromCycle(newCycle);
            FireCycleEvent(_currentCycle);
        }
    }

    private float GetTimeFromCycle(DayCycle cycle)
    {
        float segment = _fullDayLength / 4f;

        return cycle switch
        {
            DayCycle.Morning => segment * 0.5f,
            DayCycle.Afternoon => segment * 1.5f,
            DayCycle.Evening => segment * 2.5f,
            DayCycle.Midnight => segment * 3.5f,
            _ => 0f,
        };
    }

    public void FreezeTime(bool freeze)
    {
        _freezeTime = freeze;
    }
}
