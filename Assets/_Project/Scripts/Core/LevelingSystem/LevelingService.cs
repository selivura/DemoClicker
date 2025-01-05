using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker
{
    public class LevelingService : MonoBehaviour
    {
        [Inject]
        private GameRulesConfig _gameRules;

        public int Level => _level;
        [SerializeField] private int _level = 1;

        public int Experience => _experience;
        [SerializeField] private int _experience = 0;

        public int MaxExperience => _maxExperience;
        private int _maxExperience;
        private void Awake()
        {
            _maxExperience = _gameRules.BaseMaxExperience;
        }
        public void AddExperience(int experience)
        {
            _experience += experience;
            CheckForLevelUp();
        }

        private void CheckForLevelUp()
        {
            if(_experience >= _maxExperience)
            {
                _level++;
                _experience -= _maxExperience;
                _maxExperience = (int)(_maxExperience * _gameRules.MaxExpMultPerLevel);
                Debug.Log("LEVEL UP!");
            }
        }
    }
}
