using System;
using MVC.Scripts.PureMVC.View;
using UnityEngine;

namespace MVC.Scripts.PureMVC
{
    public class PureMvcMain : MonoBehaviour
    {
        private void Start()
        {
            GameFacade.Instance.StartUp();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                GameFacade.Instance.SendNotification(PureNotification.SHOW_PANEL, "MainPanel");
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                //通过发送通知的方式来隐藏面板 单例得到的
                GameFacade.Instance.SendNotification(PureNotification.HIDE_PANEL,
                    GameFacade.Instance.RetrieveMediator(PureMainViewMediator.NAME));
            }
        }
    }
}