using MVC.Scripts.MVC.Model;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MVC.Scripts.MVC.View
{
    public class MainView : MonoBehaviour
    {
        //1.找控件
        public Button btnRole;
        //public Button btnBag;

        public Text txtName;
        public Text txtLev;
        public Text txtMoney;
        public Text txtGem;
        public Text txtPower;

        //2.提供更新的方法给外部
        public void UpdateInfo(PlayerModel data)
        {
            txtName.text = data.PlayerName;
            txtLev.text = "Lv." + data.Lev;
            txtMoney.text = data.Money.ToString();
            txtGem.text = data.Gem.ToString();
            txtPower.text = data.Power.ToString();
        }
    }
}