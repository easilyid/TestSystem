using UnityEngine;
using UnityEngine.UI;

namespace MVC.Scripts.MVX.MVP.VIew
{
   public class MvpMainView : MonoBehaviour
   {
      //找控件
      public Button btnRole;
      //public Button btnBag;

      public Text txtName;
      public Text txtLev;
      public Text txtMoney;
      public Text txtGem;
      public Text txtPower;
      //更新数据
      public void UpdateInfo(string name, int lev, int money, int gem, int power)
      {
         txtName.text = name;
         txtLev.text = lev.ToString();
         txtMoney.text = money.ToString();
         txtGem.text = gem.ToString();
         txtPower.text = power.ToString();
      }
      
      
      //2.提供面板更新的方法给外部
      // 方法可选 可从主持人 Presenter里访问控件 去修改
   }
}
