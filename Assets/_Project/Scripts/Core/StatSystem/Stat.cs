using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private float _baseValue;

        [TextArea()]
        public string Descripcion = "Cool stat";
        private bool _isDirty = true;
        private float _value;
        private float _lastBaseValue = float.MinValue;
        public System.Action OnStatModified;
        public float Value
        {
            get
            {
                if (_isDirty || _lastBaseValue != _baseValue)
                {
                    _lastBaseValue = _baseValue;
                    _value = CalculateFinalValue();
                    _isDirty = false;
                }
                return _value;
            }
        }
        private readonly List<StatModifier> statModifiers;
        public Stat()
        {
            statModifiers = new List<StatModifier>();
        }
        public Stat(float baseValue)
        {
            _baseValue = baseValue;
            statModifiers = new List<StatModifier>();
        }
        public void AddModifier(StatModifier mod)
        {
            if (mod.Value == 0)
                return;
            _isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
            OnStatModified?.Invoke();
        }
        public bool RemoveModifier(StatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                _isDirty = true;
                OnStatModified?.Invoke();
                return true;
            }
            return false;
        }
        public void RemoveAllModifiers()
        {
            _isDirty = true;
            statModifiers.Clear();
        }
        public void RemoveAllModifiersFromSource(object source)
        {
            _isDirty = true;
            for (int i = 0; i < statModifiers.Count; i++)
            {
                if (statModifiers[i].Source == source)
                    statModifiers.RemoveAt(i);
            }
        }
        private int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0;
        }
        private float CalculateFinalValue()
        {
            float finalValue = _baseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];

                if (mod.Type == StatModType.Flat)
                {
                    finalValue += mod.Value;
                }
                else if (mod.Type == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;

                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + (sumPercentAdd / 100);
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.Type == StatModType.PercentMult)
                {
                    finalValue *= 1 + (mod.Value / 100);
                }
            }

            return finalValue;
        }
    }
}
