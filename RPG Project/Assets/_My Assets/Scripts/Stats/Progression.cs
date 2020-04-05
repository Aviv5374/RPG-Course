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
            [SerializeField] ProgressionStat[] stats;             

            public CharacterClass CharacterClass { get => characterClass; }            
            public ProgressionStat[] Stats { get => stats; }
        }

        [System.Serializable]
        class ProgressionStat
        {
            [SerializeField] Stat stat;
            [SerializeField] float[] levels;

            public Stat Stat { get => stat;}
            public float[] Levels { get => levels; }
        }

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            for (int index1 = 0; index1 < characterClasses.Length; index1++)
            {
                if (characterClasses[index1].CharacterClass == characterClass) { continue; }

                ProgressionStat[] stats = characterClasses[index1].Stats;

                for (int index2 = 0; index2 < stats.Length; index2++)
                {
                    #region option 1
                    //if(stats[index2].Stat != stat) { continue; }
                    //if (level <= -1) { continue; }
                    //if (level > stats[index2].Levels.Length) { continue; }
                    #endregion

                    bool isLevelInRange = level > -1 && level <= stats[index2].Levels.Length;
                    if (stats[index2].Stat == stat && isLevelInRange)
                    {
                        return stats[index2].Levels[level - 1];
                    }
                }

            }
            return 0;
        }

    }
}