using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Selivura.DemoClicker
{
    public class StatItem : Item
    {
        public Dictionary<StatType, StatModifier> Stats => _stats;

        [SerializedDictionary("Stat type", "Modifier")]
        [SerializeField] SerializedDictionary<StatType, StatModifier> _stats => new();

        [Inject]
        PlayerStatsService _statsService;
        protected override void OnStackIncreased(int amount)
        {
            RefreshStats();
        }
        protected void RefreshStats()
        {
            foreach (var keyValuePair in Stats)
            {
                if (_statsService.Stats.TryGetValue(keyValuePair.Key, out Stat stat))
                {
                    stat.RemoveAllModifiersFromSource(this);
                }
            }
            foreach (var keyValuePair in Stats)
            {
                if (_statsService.Stats.TryGetValue(keyValuePair.Key, out Stat stat))
                {
                    StatModifier modifier = keyValuePair.Value;
                    stat.AddModifier(new StatModifier(modifier.Value * Stack, modifier.Type, modifier.Order, this));
                }
            }
        }
    }
}
