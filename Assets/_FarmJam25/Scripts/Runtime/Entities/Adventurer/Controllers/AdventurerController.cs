using System;
using NJG.Runtime.Entities.Adventurer.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NJG.Runtime.Entities.Adventurer
{
    public class AdventurerController : MonoBehaviour, IDamagable, IDamageGiver
    {
        #region States
        [FoldoutGroup("States"), SerializeField]
        private AdventurerState[] _states;
        [FoldoutGroup("States"), SerializeField]
        private AdventurerState _deathState;
        [FoldoutGroup("States"), SerializeField, ReadOnly]
        private AdventurerState _currentState;
        #endregion

        #region Components
        public IMoveComp CMove { get; protected set; }
        public IAttackComp CAttack { get; protected set; }
        public TargetSelector CTargetSelector { get; protected set; }
        
        public IDamagable AttackTarget { get; protected set; }
        public IDamagable ChaseTarget { get; protected set; }
        #endregion

        #region Getters
        public Transform Transform => transform;
        #endregion

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            CalculateState(); // add a timer here, dont do this in each update
            _currentState.OnLogicUpdate();
        }

        protected virtual void Initialize()
        {
            SetComponents();
            InitializeStates();
            CalculateState();
        }

        protected virtual void InitializeStates()
        {
            for (int i = 0; i < _states.Length; i++)
            {
                _states[i] = Instantiate(_states[i]);
                _states[i].Initialize(this);
            }
        }

        protected virtual void SetComponents()
        {
            CMove = FindComponent<IMoveComp>();
            CAttack = FindComponent<IAttackComp>();
            CTargetSelector = FindComponent<TargetSelector>();
        }

        public void CalculateState()
        {
            if(_currentState && !_currentState.CanBeExited)
                return;
            STargets targetsData = CTargetSelector.GetTargets();
            AttackTarget = targetsData.AttackTarget;
            ChaseTarget = targetsData.ChaseTarget;
            
            float maxPriority = float.MinValue;
            AdventurerState maxPrioritiyState = null;

            foreach (var VARIABLE in _states)
            {
                float statePriority = VARIABLE.GetStatePriority();
                if (statePriority > maxPriority)
                {
                    maxPriority = statePriority;
                    maxPrioritiyState = VARIABLE;
                }
            }
            
            if(maxPriority > 0)
                ChangeState(maxPrioritiyState);
            else 
                Debug.LogError("There is no desired state, there must be at least one desired state");
        }

        private void ChangeState(AdventurerState newState)
        {
            if(_currentState == newState)
                return;
            
            _currentState?.OnStateExit();
            _currentState = newState;
            _currentState?.OnStateEnter();
        }

        private T FindComponent<T>()
        {
            T t = GetComponent<T>();
            if(t == null)
                Debug.LogError("There is no component of type " + typeof(T).ToString());
            return t;
        }

        public void Damage(float damage, Vector3 DamageDirection, IDamageGiver damageGiver = null)
        {
            print($"Recieve Damage: {damage}");
        }
    }
}