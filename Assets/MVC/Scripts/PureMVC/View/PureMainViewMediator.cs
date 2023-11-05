using MVC.Scripts.PureMVC.Model;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

namespace MVC.Scripts.PureMVC.View
{
    public class PureMainViewMediator : Mediator
    {
        public static new string NAME = "PureMainViewMediator"; //通过名字来获取Mediator

        //套路写法 继承PureMvC的Mediator脚本
        //构造函数
        public PureMainViewMediator() : base(NAME)
        {
            //这里可以写创建面板的代码
            //但是界面显示应该是触发控制的
            //且创建界面代码重复性高 所以不写
        }

        //重写监听通知的方法  这是告诉PureMVC我们关心这个通知 触发的时候得处理
        public override string[] ListNotificationInterests()
        {
            //这是一个规则
            //你需要监听哪些通知 那就在这里把通知通过字符串数组的形式返回
            //PureMVC就会帮助我们监听这些通知
            //return base.ListNotificationInterests();
            return new string[]
            {
                PureNotification.UPDATE_PLAYER_INFO,
            };
        }

        //重写处理通知的方法
        public override void HandleNotification(INotification notification)
        {
            //INotification 这个接口对象包含对我们来说重要的参数
            // 通知名 通知包含的信息
            switch (notification.Name)
            {
                case PureNotification.UPDATE_PLAYER_INFO:
                    //收到更新通知 做处理 使用ViewComponent串联起UI
                    if (ViewComponent != null)
                    {//建议在所有界面Mediator中都加上这个判断 ！！！重要
                        (ViewComponent as PureMainView).UpdateInfo(notification.Body as PlayerDataObj);
                    }

                    break;
            }
        }

        //重写注册通知的方法 可选
        public override void OnRegister()
        {
            //初始化一些内容
            base.OnRegister();
        }

        public override void OnRemove()
        {
            base.OnRemove();
        }

        public void SetView(PureMainView view)
        {
            ViewComponent = view;
            view.btnRole.onClick.AddListener(clickRole);
        }

        public void clickRole()
        {
            //直接facade发送通知
            SendNotification(PureNotification.SHOW_PANEL, "RolePanel");
        }
    }
}