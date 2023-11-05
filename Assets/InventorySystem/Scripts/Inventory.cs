using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Scripts
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory2/New Inventory2")]
    public class Inventory : ScriptableObject
    {
        public List<Item>  itemsList = new List<Item>();
        
    }
}