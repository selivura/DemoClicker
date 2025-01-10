using System.Collections.Generic;
using UnityEngine;

namespace Selivura.DemoClicker
{
    [CreateAssetMenu]
    public class GameItemsList : ScriptableObject
    {
        public List<Item> Items => _items;
        [SerializeField] private List<Item> _items;
    }
}
