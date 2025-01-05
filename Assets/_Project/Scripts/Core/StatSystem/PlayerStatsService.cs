using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Selivura
{
    public enum StatType
    {
        ExpPerClick,
        CoinChance,
        CoinAmount,
    }
    public enum AmplifyType
    {
        PercentScaleAdd = 0,
        PercentScaleMulti = 1,
        Multiplier = 2,
    }
    public enum ScaleType
    {
        Percentage = 0,
        Multiplier = 1,
    }

    public class PlayerStatsService : MonoBehaviour
    {
        [SerializedDictionary("Stat type", "Stat")] public SerializedDictionary<StatType, Stat> Stats = new SerializedDictionary<StatType, Stat>();

        public bool GetStatValue(StatType statType, out float statValue)
        {
            statValue = 0;
            if (GetStat(statType, out var stat))
            {
                statValue = stat.Value;
                return true;
            }
            return false;
        }
        public bool GetStat(StatType statType, out Stat stat)
        {
            if (Stats.TryGetValue(statType, out stat))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Returns stat scaled by value. Returns 0 if stat does not exist.
        /// </summary>
        /// <param name="statType">Stat that will be scaled</param>
        /// <param name="scale">Scale value</param>
        /// <param name="scaleType">Type of scale</param>
        /// <returns></returns>
        public float ScaleStat(StatType statType, float scale, ScaleType scaleType)
        {
            if(GetStatValue(statType, out var statValue))
            {
                switch (scaleType)
                {
                    case ScaleType.Percentage:
                        return statValue * (scale / 100);
                        
                    case ScaleType.Multiplier:
                        return statValue * scale;
                       
                    default:
                        return 0;
                        
                }
            }
            return 0;
        }
    }
}
