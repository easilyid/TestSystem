using UnityEngine;

namespace InventorySystem.Scripts
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory2/Item2 ")]
    public class Item : ScriptableObject
    {
        public string itemName;
        public Sprite itemImage;
        public int itemHeld;
        [TextArea]
        public string itemInfo;

        public bool isUse;
    }
}