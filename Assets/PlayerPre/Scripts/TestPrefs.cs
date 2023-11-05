using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerPre.Scripts
{
    public class Item
    {
        public int num;
        public int id;
    }

    public class Player
    {
        public string Name;
        public int Age;
        public float Atk;
        public float Def;

        public List<Item> Itemlist;

        //这个变量是应该存储和读取的一个唯一性标识
        private string _keyName;

        public void Save()
        {
            PlayerPrefs.SetString(_keyName + "Name", Name);
            PlayerPrefs.SetInt(_keyName + "Age", Age);
            PlayerPrefs.SetFloat(_keyName + "Atk", Atk);
            PlayerPrefs.SetFloat(_keyName + "Def", Def);


            PlayerPrefs.SetInt(_keyName + "ItemNum", Itemlist.Count);
            for (int i = 0; i < Itemlist.Count; i++)
            {
                PlayerPrefs.SetInt(_keyName + "itemId" + i, Itemlist[i].id);
                PlayerPrefs.SetInt(_keyName + "itemNum" + i, Itemlist[i].num);
            }

            PlayerPrefs.Save(); //存储
        }

        public void Load(string keyName)
        {
            Name = PlayerPrefs.GetString(keyName + "Name", "Heart");
            Age = PlayerPrefs.GetInt(keyName + "Age", 18);
            Atk = PlayerPrefs.GetFloat(keyName + "Atk", 10.5f);
            Def = PlayerPrefs.GetFloat(keyName + "Def", 5.5f);

            int num = PlayerPrefs.GetInt(keyName + "ItemNum", 0);
            Itemlist = new List<Item>();
            Item item;
            for (int i = 0; i < num; i++)
            {
                item = new Item();
                item.id = PlayerPrefs.GetInt(keyName + "itemId" + i);
                item.num = PlayerPrefs.GetInt(keyName + "itemNum" + i);
                Itemlist.Add(item);
            }
        }
    }

    /// <summary>
    /// 排行榜具体信息
    /// </summary>
    public class RankListInfo
    {
        public List<RankInfo> rankInfos;

        public RankListInfo()
        {
            //初始化
            Load();
        }

        public void Add(string name, int score, int time)
        {
            rankInfos.Add(new RankInfo(name, score, time));
        }

        public void Save()
        {
            PlayerPrefs.SetInt("rankListNum", rankInfos.Count);
            for (int i = 0; i < rankInfos.Count; i++)
            {
                RankInfo info = rankInfos[i];
                PlayerPrefs.SetString("rankInfo" + i, info.PlayerName);
                PlayerPrefs.SetInt("rankScore" + i, info.PlayerScore);
                PlayerPrefs.SetInt("rankTime" + i, info.PlayerTime);
            }
        }

        private void Load()
        {
            int num = PlayerPrefs.GetInt("rankListNum", 0);
            rankInfos = new List<RankInfo>();
            for (int i = 0; i < num; i++)
            {
                RankInfo info = new RankInfo(PlayerPrefs.GetString("rankInfo" + i), 
                    PlayerPrefs.GetInt("rankScore" + i),
                    PlayerPrefs.GetInt("rankTime" + i));
                rankInfos.Add(info);
            }
        }
    }

    /// <summary>
    /// 排行榜单条信息
    /// </summary>
    public class RankInfo
    {
        public string PlayerName;
        public int PlayerScore;
        public int PlayerTime;

        public RankInfo(string name, int score, int time)
        {
            PlayerName = name;
            PlayerScore = score;
            PlayerTime = time;
        }
    }

    public class TestPrefs : MonoBehaviour
    {
        private void Start()
        {
            // Player p = new Player();
            // p.Load("Player1");
            // print(p.Name);
            // print(p.Age);
            // print(p.Atk);
            // print(p.Def);
            // p.Name = "easily";
            // p.Age = 20;
            // p.Atk = 9.5f;
            // p.Def = 4.5f;
            // p.Save();
            //
            // p.Load("Player2");
            // print(p.Itemlist.Count);
            // for (int i = 0; i < p.Itemlist.Count; i++)
            // {
            //     print("道具ID" + p.Itemlist[i].id);
            //     print("道具数量" + p.Itemlist[i].num);
            // }
            //
            // Item item = new Item();
            // item.id = 1;
            // item.num = 1;
            // p.Itemlist.Add(item);
            // item = new Item();
            // item.id = 2;
            // item.num = 2;
            // p.Itemlist.Add(item);
            //
            // p.Save();
            
            RankListInfo info = new RankListInfo();
            print(info.rankInfos.Count);
            for (int i = 0; i < info.rankInfos.Count; i++)
            {
                print("姓名"+info.rankInfos[i].PlayerName);
                print("分数"+info.rankInfos[i].PlayerScore);
                print("时间"+info.rankInfos[i].PlayerTime);
            }
            info.Add("Heart", 100, 10);
            info.Save();
        }
    }
}