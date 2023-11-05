using MVC.Scripts.MVC.Model;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.Scripts.MVC.View
{
    public class RoleView : MonoBehaviour
    {
        public Button btnClose;
        public Button btnLevUp;

        public Text txtLev;
        public Text txtHp;
        public Text txtAtk;
        public Text txtDef;
        public Text txtCrit;
        public Text txtMiss;
        public Text txtLuck;

        public void UpdateInfo(PlayerModel data)
        {
            txtLev.text = "LV." + data.Lev;
            txtHp.text = data.Hp.ToString();
            txtAtk.text = data.Atk.ToString();
            txtDef.text = data.Def.ToString();
            txtCrit.text = data.Crit.ToString();
            txtMiss.text = data.Miss.ToString();
            txtLuck.text = data.Luck.ToString();
            
        }
    }
}