using MVC.Scripts.PureMVC.Controller;
using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using UnityEngine;

namespace MVC.Scripts.PureMVC
{
    public class GameFacade : Facade
    {
        //1. 继承PureMvC的Facade脚本
        //2.为了方便使用Facade 写单例
        //3.初始化控制层相关的内容
        //4.
        public static GameFacade Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameFacade();
                return instance as GameFacade;
            }
        }

        /// <summary>
        /// 控制器相关的内容
        /// </summary>
        protected override void InitializeController()
        {
            base.InitializeController(); //这个方法是必须调用的
            //这里写一些关于命令 通知 绑定的逻辑
            RegisterCommand(PureNotification.START_UP, () =>
            {
                return new StartUpCommand(); //这就是一个最简单的方式来绑定通知和命令
            });
            
            RegisterCommand(PureNotification.SHOW_PANEL, () =>
            {
                return new ShowPanelCommand();
            });

            RegisterCommand(PureNotification.HIDE_PANEL, () =>
            {
                return new HidePanelCommand();
            });
            
            RegisterCommand(PureNotification.LEV_UP, () =>
            {
                return new LevelUpCommand();
            });
        }
        
        //4.一定有一个启动函数
        public void StartUp()
        {
            //启动函数
            //启动的时候发送一个通知
            SendNotification(PureNotification.START_UP);
            
            //SendNotification(PureNotification.SHOW_PANEL,""); //这里可以传入参数
        }
    }
}