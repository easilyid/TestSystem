using MVC.Scripts.MVC.Model;
using MVC.Scripts.PureMVC.Model;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.Scripts.PureMVC.View
{
    public class PureMainView : MonoBehaviour
    {
        //1.找控件
        public Button btnRole;
        //public Button btnBag;

        public Text txtName;
        public Text txtLev;
        public Text txtMoney;
        public Text txtGem;
        public Text txtPower;
        
        
        // 按MVC的思想是直接在这里写更新数据的方法
        public void UpdateInfo(PlayerDataObj data)
        {
            txtName.text = data.PlayerName;
            txtLev.text = "Lv." + data.Lev;
            txtMoney.text = data.Money.ToString();
            txtGem.text = data.Gem.ToString();
            txtPower.text = data.Power.ToString();
        }
    }
}