using System;
using MVC.Scripts.MVC.Model;
using MVC.Scripts.MVX.MVP.VIew;
using UnityEngine;

namespace MVC.Scripts.MVX.MVP.Presenter
{
    public class MainPresenter : MonoBehaviour
    {
        private MvpMainView _mainView;
        private static MainPresenter _presenter = null;
        public static MainPresenter Presenter => _presenter;

        private void Start()
        {
            _mainView = GetComponent<MvpMainView>();
            _mainView.btnRole.onClick.AddListener(OnRoleClick);
            
            //第一次更新数据
            UpdateInfo(PlayerModel.Data); //通过P自己的更新方法去更新View  重点
            
            PlayerModel.Data.AddEventListener(UpdateInfo);
        }

        private void OnDestroy()
        {
            PlayerModel.Data.RemoveEventListener(UpdateInfo);
        }

        // 监听更新数据的方法
        private void UpdateInfo(PlayerModel data)
        {
            if (_mainView!=null)
            {
                // 这就是MVP模式的关键点 更新的数据可以直接在Presenter里处理 断绝 model和view的联系
                // 而 MVC模式的关键点是 model和view的联系 将数据M传到V中左更新 现在全是用P来做
                _mainView.txtName.text = data.PlayerName;
                _mainView.txtLev.text = "Lv." + data.Lev;
                _mainView.txtMoney.text = data.Money.ToString();
                _mainView.txtGem.text = data.Gem.ToString();
                _mainView.txtPower.text = data.Power.ToString();
              
            }
        }

        private void OnRoleClick()
        {
            RolePresenter.ShowMe();
        }

        public static void ShowMe()
        {
            if (_presenter==null)
            {
                var res = Resources.Load<GameObject>("UI/MainPanel");
                var go = Instantiate(res, GameObject.Find("Canvas").transform, false);
                
                _presenter = go.gameObject.GetComponent<MainPresenter>();
            }
        }

        public static void HideMe()
        {
            if (_presenter!=null)
            {
                Destroy(_presenter.gameObject);
                _presenter = null;
            }
        }
    }
}