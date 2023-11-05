using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.Scripts
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance;

        public Inventory myBag;
        public GameObject slotGrid;
        public GameObject ItemOnWord;
        public Text itemInfo;

        public List<GameObject> slots = new List<GameObject>();


        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
            }

            instance = this;
        }

        private void OnEnable()
        {
            RefreshItem();
            instance.itemInfo.text = "请选择你的物品";
        }

        public static void ShowItemInfo(string info)
        {
            instance.itemInfo.text = info;
        }

        public static void RefreshItem()
        {
            for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
            {
                //如果物品槽中没有物品
                if (instance.slotGrid.transform.childCount == 0)
                {
                    break;
                }

                //销毁物品槽中的物品
                Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
                
                //清空物品槽列表
                instance.slots.Clear();
            }

            for (int i = 0; i < instance.myBag.itemsList.Count; i++)
            {
                instance.slots.Add(Instantiate(instance.ItemOnWord));
                //设置物品槽的父物体
                instance.slots[i].transform.SetParent(instance.slotGrid.transform);
                //设置物品槽物品ID
                instance.slots[i].GetComponent<Slots>().slotId = i;
                //显示物品的信息
                instance.slots[i].GetComponent<Slots>().SetUPSlot(instance.myBag.itemsList[i]);
            }
        }
    }
}