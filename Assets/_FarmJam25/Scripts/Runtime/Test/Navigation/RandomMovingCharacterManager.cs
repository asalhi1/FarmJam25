using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace NJG.Runtime.Test.Navigation
{
    public class RandomMovingCharacterManager : MonoBehaviour
    {
        [SerializeField] List<SRandomCharacterGroup> _groups = new();

        [SerializeField] private RandomMovingCharacter _prefabCharacter;

        [SerializeField] private int _groupSize = 5;

        [Button]
        private void PopulateEmptyStructs()
        {
            for (int i = 0; i < _groups.Count; i++)
            {
                var group = _groups[i];

                if (group.Characters == null || group.Characters.Length == 0)
                {
                    RandomMovingCharacter[] newCharacters = new RandomMovingCharacter[5];

                    for (int j = 0; j < _groupSize; j++)
                    {
                        var instance = Instantiate(_prefabCharacter, Vector3.zero, Quaternion.identity);
                        instance.TeleportToRandomLocation();
                        newCharacters[j] = instance;
                    }

                    group.Characters = newCharacters;

                    _groups[i] = group;
                }
            }
        }
        
        [System.Serializable]
        private struct SRandomCharacterGroup
        {
            [Button]
            private void GoToRandomPos()
            {
                foreach (var VARIABLE in Characters)
                    VARIABLE.GoToARandomLocation();
            }
            public RandomMovingCharacter[] Characters { get; set; }
        }
    }
}