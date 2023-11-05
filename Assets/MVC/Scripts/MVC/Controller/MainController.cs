using System;
using MVC.Scripts.MVC.Model;
using MVC.Scripts.MVC.View;
using UnityEngine;

namespace MVC.Scripts.MVC.Controller
{
    /// <summary>
    /// Controller层的主要作用是接收View层的事件，
    /// 然后调用Model层的方法，更新数据，然后再更新View层的显示。
    /// 主要是业务逻辑层
    /// </summary>
    public class MainController : MonoBehaviour
    {
        private MainView _mainView;
        private static MainController controller = null;
        public MainController Controller => controller;


        private void Start()
        {
            _mainView = this.GetComponent<MainView>();
            _mainView.UpdateInfo(PlayerModel.Data);

            //注册事件 用拉姆达表达式
            _mainView.btnRole.onClick.AddListener(ClickBtnRole);
            
            //升级之后的界面更新 
            PlayerModel.Data.AddEventListener(UpdateInfo);
        }

        //界面更新的通知
        private void UpdateInfo(PlayerModel arg0)
        {
            if (_mainView!=null)
            {//更新界面
                _mainView.UpdateInfo(arg0);
            }
        }

        private void ClickBtnRole()
        {
            RoleController.ShowMe();
        }

        public static void ShowMe()
        {
            if (controller == null)
            {
                var res = Resources.Load<GameObject>("UI/MainPanel");
                var go = Instantiate(res, GameObject.Find("Canvas").transform, false);

                controller = go.gameObject.GetComponent<MainController>();
            }
        }

        public static void HideMe()
        {
            if (controller != null)
            {
                Destroy(controller.gameObject);
                controller = null;
            }
        }

        private void OnDestroy()
        {
            //移除事件
            PlayerModel.Data.RemoveEventListener(UpdateInfo);
        }
    }
}