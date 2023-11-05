using UnityEngine;

namespace InventorySystem.Scripts
{
    public class ItemOnWord : MonoBehaviour
    {
        public Item Item; //链接数据库
        public Inventory Inventory; //属于哪个背包


        //接触到物品时，将物品添加到背包中
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                AddItem(Item);
                Destroy(gameObject);
            }
        }

        private void AddItem(Item item)
        {
            if (!Inventory.itemsList.Contains(item))
            {
                for (int i = 0; i < Inventory.itemsList.Count; i++)
                {
                    if (Inventory.itemsList[i]==null)
                    {
                        Inventory.itemsList[i] = item;
                        break;
                    }
                }
            }
            else
            {//如果有物品就给数量加一
                item.itemHeld += 1;
            }
            //然后刷新UI数量
            InventoryManager.RefreshItem();
        }
    }
}