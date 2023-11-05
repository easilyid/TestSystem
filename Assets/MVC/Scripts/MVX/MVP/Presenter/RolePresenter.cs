using System;
using MVC.Scripts.MVC.Model;
using MVC.Scripts.MVX.MVP.VIew;
using UnityEngine;

namespace MVC.Scripts.MVX.MVP.Presenter
{
    public class RolePresenter : MonoBehaviour
    {
        private MvpRoleView _roleView;

        private static RolePresenter _presenter = null;
        public static RolePresenter Presenter => _presenter;

        private void Start()
        {
            _roleView = GetComponent<MvpRoleView>();

            _roleView.btnClose.onClick.AddListener(ClickClose);
            _roleView.btnLevUp.onClick.AddListener(CliclLevUp);
            PlayerModel.Data.AddEventListener(UpdateInfo);
        }

        private void OnDestroy()
        {
            PlayerModel.Data.RemoveEventListener(UpdateInfo);
        }


        private void ClickClose()
        {
            HideMe();
        }

        private void CliclLevUp()
        {
            Debug.Log("升级");
            PlayerModel.Data.LevUp();
        }

        public static void ShowMe()
        {
            if (_presenter == null)
            {
                var res = Resources.Load<GameObject>("UI/RolePanel");
                var go = Instantiate(res, GameObject.Find("Canvas").transform, false);
                _presenter = go.gameObject.GetComponent<RolePresenter>();
            }
        }

        public void HideMe()
        {
            if (_presenter != null)
            {
                Destroy(_presenter.gameObject);
                _presenter = null;
            }
        }

        public void UpdateInfo(PlayerModel data)
        {
            if (_roleView != null)
            {
                //直接在这里得到V界面的控件进行修改 断开M和V的联系
                _roleView.txtLev.text = "LV." + data.Lev;
                _roleView.txtHp.text = data.Hp.ToString();
                _roleView.txtAtk.text = data.Atk.ToString();
                _roleView.txtDef.text = data.Def.ToString();
                _roleView.txtCrit.text = data.Crit.ToString();
                _roleView.txtLuck.text = data.Luck.ToString();
                _roleView.txtMiss.text = data.Miss.ToString();
            }
        }
    }
}