using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.Scripts
{
    public class Slots : MonoBehaviour
    {
        public int slotId;
        public Image ItemImage;
        public Text ItemNum;
        
        public GameObject ItemInSlot;

        private string itemInfo;


        public void ItemOnClick()
        {
            InventoryManager.ShowItemInfo(itemInfo);
        }
        
        public void SetUPSlot(Item myBagItems)
        {
            if (myBagItems==null)
            {
                ItemInSlot.SetActive(false);
                return;
            }

            ItemImage.sprite = myBagItems.itemImage;
            ItemNum.text = myBagItems.itemHeld.ToString();
            itemInfo = myBagItems.itemInfo;
        }
    }
}