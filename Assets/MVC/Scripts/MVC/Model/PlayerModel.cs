using UnityEngine;
using UnityEngine.Events;

namespace MVC.Scripts.MVC.Model
{
    //数据相关的操作
    /// <summary>
    /// 作为一个唯一的数据模型 一般情况下 要不自己是个单例对象
    /// 要么自己存在于一个单例对象中  以此来保护唯一性
    /// </summary>
    public class PlayerModel 
    {
        //数据内容
        private string _playerName;

        public string PlayerName => _playerName; //通过属性来包裹一次 能防止外部改变数据

        private int _lev;
        public int Lev => _lev;
        private int _money;
        public int Money => _money;
        private int _gem;
        public int Gem => _gem;
        private int _power;
        public int Power => _power;
        private int _hp;
        public int Hp => _hp;
        private int _atk;
        public int Atk => _atk;
        private int _def;
        public int Def => _def;
        private int _crit;
        public int Crit => _crit;
        private int _miss;
        public int Miss => _miss;
        private  int _luck;
        public int Luck => _luck;

        public event UnityAction<PlayerModel> UpdateEvent; //通知外部更新的事件 而不是直接获取外部的面板
        
        //在外部第一次获取数据的时候进行初始化 一般情况下玩家数据是只有一个的
        //通过单列模式进行数据的获取
        private static PlayerModel _instance;
        public static PlayerModel Data
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayerModel();
                    _instance.Init();
                }

                return _instance;
            }
        }
        
        
        //初始化
        public void Init()
        {
            _playerName = PlayerPrefs.GetString("PlayerName", "Heart");
            _lev =PlayerPrefs.GetInt("PlayerLev", 1);
            _money = PlayerPrefs.GetInt("PlayerMoney", 888);
            _gem = PlayerPrefs.GetInt("PlayerGem", 999);
            _power = PlayerPrefs.GetInt("PlayerPower", 99);
            _hp = PlayerPrefs.GetInt("PlayerHp", 100);
            _atk = PlayerPrefs.GetInt("PlayerAtk", 20);
            _def = PlayerPrefs.GetInt("PlayerDef", 10);
            _crit = PlayerPrefs.GetInt("PlayerCrit", 30);
            _miss = PlayerPrefs.GetInt("PlayerMiss", 15);
            _luck = PlayerPrefs.GetInt("PlayerLuck", 25);
            
        }
        
        // 更新 升级
        public void LevUp()
        {
            _lev += 1;
            _hp += _lev;
            _atk += _lev;
            _def += _lev;
            _crit += _lev;
            _miss += _lev;
            _luck += _lev;
            
            SaveData();
        }
        
        //保存
        public void SaveData()
        {
            //保存数据
            PlayerPrefs.SetString("PlayerName", _playerName);
            PlayerPrefs.SetInt("PlayerLev", _lev);
            PlayerPrefs.SetInt("PlayerMoney", _money);
            PlayerPrefs.SetInt("PlayerGem", _gem);
            PlayerPrefs.SetInt("PlayerPower", _power);
            
            PlayerPrefs.SetInt("PlayerHp", _hp);
            PlayerPrefs.SetInt("PlayerAtk", _atk);
            PlayerPrefs.SetInt("PlayerDef", _def);
            PlayerPrefs.SetInt("PlayerCrit", _crit);
            PlayerPrefs.SetInt("PlayerMiss", _miss);
            PlayerPrefs.SetInt("PlayerLuck", _luck);
            
            UpdateInfo(); //通过这种形式和外部进行联系
        }
        
        
        public void AddEventListener(UnityAction<PlayerModel> action)
        {
            UpdateEvent += action;
        }

        public void RemoveEventListener(UnityAction<PlayerModel> action)
        {
            UpdateEvent -= action;
        }
        
        //通知外部更新数据的方法
        public void UpdateInfo()
        {
           //找到对应使用数据的脚本 去更新数据
           if (UpdateEvent != null )
           {
               //外面谁关心model 的变化就传一个函数进行监听 
               //当数据变化的时候执行这方法 将自己传出去
               //外部接到数据后进行更新
                UpdateEvent(this);
           }
        }
    }
}