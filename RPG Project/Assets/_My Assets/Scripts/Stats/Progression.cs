using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
	[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
	public class Progression : ScriptableObject
	{
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        [System.Serializable]
        class ProgressionCharacterClass
        {
            [SerializeField] CharacterClass characterClass;
            [SerializeField] float[] health;

            public CharacterClass CharacterClass { get => characterClass; }
            public float[] Health { get => health; }
        }

        public float GetHealth(CharacterClass characterClass, int level)
        {
            for (int i = 0; i < characterClasses.Length; i++)
            {
                if (characterClasses[i].CharacterClass == characterClass)
                {
                    return characterClasses[i].Health[level - 1];
                }
            }
            return 0;
        }

    }
}