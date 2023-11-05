using MVC.Scripts.PureMVC.Model;
using MVC.Scripts.PureMVC.View;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace MVC.Scripts.PureMVC.Controller
{
    public class ShowPanelCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            base.Execute(notification);
            //当命令执行的时候调用的方法 Execute
            
            //这里写面板创建的命令
            string panel = notification.Body.ToString();

            switch (panel)
            {
                case "MainPanel":
                    //显示面板相关内容
                    //如果要使用Mediator 就得去 Facade中注册 Command和proxy都是一样的，要用就得注册
                    if (!Facade.HasMediator(PureMainViewMediator.NAME))
                    {
                        Facade.RegisterMediator(new PureMainViewMediator()); //等于新建一个Mediator对象，注册到Facade中
                    }
                    //得到Mediator之后关联到ViewComponent
                    PureMainViewMediator mm = Facade.RetrieveMediator(PureMainViewMediator.NAME) as PureMainViewMediator;
                    if (mm.ViewComponent==null)
                    {
                        //有了Mediator之后就可以创建面板了 创建预设体
                        GameObject res = Resources.Load<GameObject>("UI/MainPanel");
                        GameObject obj = GameObject.Instantiate(res, GameObject.Find("Canvas").transform, false);
                        //mm.ViewComponent = obj.GetComponent<PureMainView>();//关联成功
                        mm.SetView(obj.GetComponent<PureMainView>()); //关联脚本 同时监听按钮事件
                    }
                    //往往在显示面板之后就显示更新
                    SendNotification(PureNotification.UPDATE_PLAYER_INFO,Facade.RetrieveProxy(PlayerProxy.NAME).Data);//通过发通知来更新数据
                    //得到Proxy之后将数据也传入到通知里 然后Mediator里处理通知 ListNotificationInterests和HandleNotification函数很重要
                    break;
                case "RolePanel":
                    if (!Facade.HasMediator(PureRoleViewMediator.NAME))
                    {
                        Facade.RegisterMediator(new PureRoleViewMediator());
                    }
                    
                    var rv = Facade.RetrieveMediator(PureRoleViewMediator.NAME) as PureRoleViewMediator;
                    if (rv.ViewComponent == null) 
                    {
                        GameObject res = Resources.Load<GameObject>("UI/RolePanel");
                        GameObject obj = GameObject.Instantiate(res, GameObject.Find("Canvas").transform, false);
                        //rv.ViewComponent = obj.GetComponent<PureRoleView>();//关联成功
                        rv.SetView(obj.GetComponent<PureRoleView>());
                    }
                    SendNotification(PureNotification.UPDATE_PLAYER_INFO,Facade.RetrieveProxy(PlayerProxy.NAME).Data);//通过发通知来更新数据
                    //得到Proxy之后将数据也传入到通知里 然后Mediator里处理通知 ListNotificationInterests和HandleNotification函数很重要
                    break;
            }
        }
    }
}