using System;
using MVC.Scripts.MVC.Model;
using MVC.Scripts.MVC.View;
using UnityEngine;

namespace MVC.Scripts.MVC.Controller
{
    /// <summary>
    /// 业务逻辑
    /// </summary>
    public class RoleController : MonoBehaviour
    {
        private RoleView _roleView;

        private static RoleController controller = null;
        public static RoleController Controller => controller;

        private void Start()
        {
            _roleView = GetComponent<RoleView>();
            _roleView.UpdateInfo(PlayerModel.Data);
            _roleView.btnClose.onClick.AddListener(ClickBtnClose);
            _roleView.btnLevUp.onClick.AddListener(ClickLevUp);

            //告知数据模块 当更新时 那个函数做处理
            PlayerModel.Data.AddEventListener(UpdateInfo);
        }

        
        private void UpdateInfo(PlayerModel arg0)
        {
            if (_roleView != null)
            {
                _roleView.UpdateInfo(arg0);
            }
        }

        private void ClickBtnClose()
        {
            HideMe();
        }

        private void ClickLevUp()
        {
            //通过数据模块升级
            PlayerModel.Data.LevUp();
        }

        public static void ShowMe()
        {
            if (controller == null)
            {
                var res = Resources.Load<GameObject>("UI/RolePanel");
                var go = Instantiate(res, GameObject.Find("Canvas").transform, false);

                controller = go.gameObject.GetComponent<RoleController>();
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
            PlayerModel.Data.RemoveEventListener(UpdateInfo);
        }
    }
}