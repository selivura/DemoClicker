using UnityEngine;

namespace Selivura.DemoClicker
{
    [CreateAssetMenu]
    public class GameRulesConfig : ScriptableObject
    {
        [Header("Basic")]

        public int BaseMaxExperience => _baseMaxExperience;
        [SerializeField] private int _baseMaxExperience = 25;
        public float MaxExpMultPerLevel => _maxExperienceMultPerLevel;
        [SerializeField] private float _maxExperienceMultPerLevel = 1.5f;

        public Item CoinPrefab => _coinPrefab;
        [SerializeField] private Item _coinPrefab;


    }
}
