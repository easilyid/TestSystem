using MVC.Scripts.PureMVC.Model;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace MVC.Scripts.PureMVC.Controller
{
    public class StartUpCommand : SimpleCommand
    {
        //1.继承Command 相关
        //2. 重写执行函数方法
        public override void Execute(INotification notification)
        {
            base.Execute(notification);
            //当命令执行的时候调用的方法 Execute
            // 启动命令中往往是做一些初始化的操作
            if (!Facade.HasProxy(PlayerProxy.NAME))
            {
                Facade.RegisterProxy(new PlayerProxy());
            }
        }
    }
}