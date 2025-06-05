using System;
using UnityEngine;
using UnityEngine.AI;

namespace NJG.Prototypes
{
    // whatever character we have should inherit from this
    public class PlayerPawn : MonoBehaviour
    {
        protected NavMeshAgent Agent;
        protected PlayerController Controller;
        public NavMeshAgent GetAgent() { return Agent; }

        public void OnAttach(PlayerController controller)
        {
            Controller = controller;
            Agent = GetComponent<NavMeshAgent>();
        }

        public void OnDetach()
        {
            Controller = null;
        }

        public virtual bool GoTo(Vector3 targetPosition)
        {
            return Agent.SetDestination(targetPosition);
        }
    }
}