using System.Collections.Generic;
using UnityEngine;

namespace Selivura.DemoClicker
{
    public class LootTable<T> : ScriptableObject
    {
        [SerializeField] List<LootElement<T>> _loot;

        [System.NonSerialized]
        private float _totalWeight = 0;
        public float TotalWeight 
        { 
            get 
            { 
                if(_totalWeight == 0)
                {
                    foreach (var loot in _loot)
                    {
                        _totalWeight += loot.Weight;
                    }
                }
                return _totalWeight;
            } 
        }
        protected virtual bool ValidateDrop(T drop)
        {
            return true;
        }
        public T GetDrop(float chanceMultiplier = 1, float chanceAdditive = 0)
        {
            T drop = RollRandomDrop(chanceMultiplier, chanceAdditive);
            int validateTokens = 1000;
            while (!ValidateDrop(drop) && validateTokens > 0)
            {
                drop = RollRandomDrop(chanceMultiplier, chanceAdditive);
                validateTokens--;
            }
            if(validateTokens < 0)
            {
                Debug.LogError("Was unable to validate drop.");
            }
            return drop;
        }
        private T RollRandomDrop(float chanceMultiplier = 1, float chanceAdditive = 0)
        {
            float roll = Random.Range(0, TotalWeight) * chanceMultiplier;
            roll += chanceAdditive;
            if (roll > TotalWeight)
            {
                roll = TotalWeight;
            }
            foreach (var loot in _loot)
            {
                roll -= loot.Weight;

                if (roll < 0)
                {
                    return loot.Drop;
                }
            }
            return _loot[0].Drop;
        }
    }
    [System.Serializable]
    public class LootElement<T>
    {
        public T Drop;
        public float Weight;
    }
}
