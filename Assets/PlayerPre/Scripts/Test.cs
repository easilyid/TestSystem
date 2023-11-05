using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerPre.Scripts
{
    public class PlayerInfo
    {
        public string Name = "Heart";
        public int Age = 22;
        public int Height = 170;
        public bool Sex = true;
        public List<int> list = new List<int>() { 1, 2, 3, 4 };

        public Dictionary<int, string> dic = new Dictionary<int, string>()
        {
            { 1, "123" },
            { 2, "456" }
        };

        public ItemInfo ItemInfo = new ItemInfo(3, 99);

        public List<ItemInfo> itemList = new List<ItemInfo>()
        {
            new ItemInfo(1, 10),
            new ItemInfo(2, 20)
        };

        public Dictionary<int, ItemInfo> dics = new Dictionary<int, ItemInfo>()
        {
            { 3, new ItemInfo(3, 22) },
            { 4, new ItemInfo(4, 44) }
        };
    }

    public class ItemInfo
    {
        public int id;
        public int num;

        public ItemInfo()
        {
            //要记得写无参构造
        }

        public ItemInfo(int id, int num)
        {
            this.id = id;
            this.num = num;
        }
    }

    public class Test : MonoBehaviour
    {
        private void Start()
        {
            //读取数据
            PlayerInfo p = PlayerPrefsDataMgr.Instance.LoadData(typeof(PlayerInfo), "Player1") as PlayerInfo;

            //游戏逻辑中 会去 修改这个玩家数据
            p.Age = 18;
            p.Name = "easily";
            p.Height = 1000;
            p.Sex = true;

            p.itemList.Add(new ItemInfo(1, 99));
            p.itemList.Add(new ItemInfo(2, 199));

            p.dics.Add(3, new ItemInfo(3, 1));
            p.dics.Add(4, new ItemInfo(4, 2));
            
            //保存数据
            PlayerPrefsDataMgr.Instance.SaveData(p, "Player1");
        }
    }
}