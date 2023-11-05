using UnityEngine;
using UnityEngine.UI;

namespace MVC.Scripts.MVX.MVP.VIew
{
    public class MvpRoleView : MonoBehaviour
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
        
        //提供更新的方法给外部 可从Persenter里进行调用 编写
    }
}