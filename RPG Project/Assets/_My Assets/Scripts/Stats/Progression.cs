using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
	[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
	public class Progression : ScriptableObject
	{
        [System.Serializable]
        class ProgressionCharacterClass
        {
            [SerializeField] CharacterClass characterClass;
            [SerializeField] ProgressionStat[] stats;

            public CharacterClass CharacterClass { get => characterClass; }
            public ProgressionStat[] Stats { get => stats; }
        }

        [System.Serializable]
        class ProgressionStat
        {
            [SerializeField] Stat stat;
            [SerializeField] float[] levels;

            public Stat Stat { get => stat; }
            public float[] Levels { get => levels; }
        }

        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;

        void BuildLookup()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            for (int index1 = 0; index1 < characterClasses.Length; index1++)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();
                ProgressionStat[] stats = characterClasses[index1].Stats;
                for (int index2 = 0; index2 < stats.Length; index2++)
                {
                    statLookupTable[stats[index2].Stat] = stats[index2].Levels;
                }

                lookupTable[characterClasses[index1].CharacterClass] = statLookupTable;
            }
        }

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();

            float[] levels = lookupTable[characterClass][stat];
            bool isLevelInRange = level > -1 && level <= levels.Length;

            if (isLevelInRange)
            {
                return levels[level - 1];
            }
            else
            {
                return 0;
            }            
        }
        
        public int GetLevelsLength(Stat stat, CharacterClass characterClass)
        {
            BuildLookup();

            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }

    }
}