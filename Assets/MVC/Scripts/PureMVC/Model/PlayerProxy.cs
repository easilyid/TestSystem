using PureMVC.Patterns.Proxy;
using UnityEngine;

namespace MVC.Scripts.PureMVC.Model
{
    /// <summary>
    /// 玩家数据代理对象
    /// 主要处理 玩家数据的获取和更新
    /// 数据更新相关的逻辑
    /// </summary>
    public class PlayerProxy : Proxy
    {
        public new const string NAME = "PlayerProxy"; //代理的名字
        
        //1. 继承Proxy类
        //2. 写构造函数
        //重点 代理的名字和数据的名字一致 代理相关的数据
        //将外部传入的数据赋值给代理 完成数据的关联
        //方法1
        // public PlayerProxy(PlayerProxy data) : base(NAME,data)
        // {
             //初始化外部传入的数据
        // }
        
        //方法2 PS 数据的传入根据关系来决定外部传入还是内部初始
        //存在 一对一和一对多的关系
        public PlayerProxy() : base(NAME)
        {
            //在构造函数中初始化数据
            PlayerDataObj obj = new PlayerDataObj();
            //初始化数据
            obj.PlayerName = PlayerPrefs.GetString("PlayerName", "Heart");
            obj. Lev =PlayerPrefs.GetInt("PlayerLev", 1);
            obj.Money = PlayerPrefs.GetInt("PlayerMoney", 888);
            obj.Gem = PlayerPrefs.GetInt("PlayerGem", 999);
            obj.Power = PlayerPrefs.GetInt("PlayerPower", 99);
            
            obj.Hp = PlayerPrefs.GetInt("PlayerHp", 100);
            obj.Atk = PlayerPrefs.GetInt("PlayerAtk", 20);
            obj.Def = PlayerPrefs.GetInt("PlayerDef", 10);
            obj.Crit = PlayerPrefs.GetInt("PlayerCrit", 30);
            obj.Miss = PlayerPrefs.GetInt("PlayerMiss", 15);
            obj.Luck = PlayerPrefs.GetInt("PlayerLuck", 25);  
            //这些就是通过代理关联数据
            
            //将数据存储到代理中
            Data = obj;
        }
        
        // 更新 升级
        public void LevUp()
        {
            //Data是父类装子类的形式装载的
            PlayerDataObj data = Data as PlayerDataObj;
            
            data.Lev += 1;
            data.Hp += data.Lev;
            data.Atk += data.Lev;
            data.Def += data.Lev;
            data.Crit += data.Lev;
            data.Miss += data.Lev;
            data.Luck += data.Lev;
            
            SaveData();
        }
        
        public void SaveData()
        {
            PlayerDataObj data = Data as PlayerDataObj;
            
            //保存数据
            PlayerPrefs.SetString("PlayerName", data.PlayerName);
            PlayerPrefs.SetInt("PlayerLev", data.Lev);
            PlayerPrefs.SetInt("PlayerMoney", data.Money);
            PlayerPrefs.SetInt("PlayerGem", data.Gem);
            PlayerPrefs.SetInt("PlayerPower", data.Power);
            
            
            PlayerPrefs.SetInt("PlayerHp", data.Hp);
            PlayerPrefs.SetInt("PlayerAtk", data.Atk);
            PlayerPrefs.SetInt("PlayerDef", data.Def);
            PlayerPrefs.SetInt("PlayerCrit", data.Crit);
            PlayerPrefs.SetInt("PlayerMiss", data.Miss);
            PlayerPrefs.SetInt("PlayerLuck", data.Luck);
            
            
            //UpdateInfo(); //通过这种形式和外部进行联系
        }
    }
}