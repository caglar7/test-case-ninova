using Sirenix.OdinInspector;
using UnityEngine;

namespace Template
{
    public class SourceManager : BaseMono
    {
        [Header("Sources")]
        public Source[] sources;

        /// <summary>
        /// get count of source with given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public float GetSource(SourceType type)
        {
            if(CheckContains(type) == false)
            {
                return 0f;
            }

            float value = 0f;

            foreach(Source s in sources)
            {
                if(s.sourceType == type)
                {
                    value = s.currentValue;
                    break;
                }
            }

            return value;
        }

        [Button]
        public void AddSource(SourceType type, float amount)
        {
            foreach (Source s in sources)
            {
                if (s.sourceType == type)
                {
                    s.currentValue += amount;

                    SourceEvents.OnUpdatedSource?.Invoke(s);

                    break;
                }
            }
        }

        public bool TrySpendSource(SourceType type, float amount)
        {
            bool canSpend = false;

            foreach (Source s in sources)
            {
                if (s.sourceType == type)
                {
                    if(s.currentValue >= amount)
                    {
                        s.currentValue -= amount;
                        canSpend = true;

                        SourceEvents.OnUpdatedSource?.Invoke(s);

                        break;
                    }
                    else
                    {
                        canSpend = false;
                        break;
                    }
                }
            }

            return canSpend;
        }

        private bool CheckContains(SourceType type)
        {
            bool contains = false;

            foreach(Source s in sources)
            {
                if(s.sourceType == type)
                {
                    contains = true;
                    break;
                }
            }

            if(contains == false)
            {
                Debug.LogWarning("Source with given name: - " + name + " - does not exist!");
            }

            return contains;
        }

        // #region Save Load
        // public void Reset(SaveData saveData)
        // {
        //     saveData.sources = new float[sources.Length];
        //     for (int i = 0; i < saveData.sources.Length; i++)
        //     {
        //         saveData.sources[i] = sources[i].initialValue;
        //     }
        // }
        //
        // public void Load(SaveData saveData)
        // {
        //     for (int i = 0; i < sources.Length; i++)
        //     {
        //         sources[i].currentValue = saveData.sources[i];
        //     }
        // }
        //
        // public void Save(SaveData saveData)
        // {
        //     for (int i = 0; i < saveData.sources.Length; i++)
        //     {
        //         saveData.sources[i] = sources[i].currentValue;
        //     }
        // }
        // #endregion
    }
}