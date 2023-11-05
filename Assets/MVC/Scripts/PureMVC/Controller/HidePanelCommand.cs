using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using PureMVC.Patterns.Mediator;
using UnityEngine;

namespace MVC.Scripts.PureMVC.Controller
{
    public class HidePanelCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            base.Execute(notification);
            //隐藏的目的： 得到Mediator之后关联到ViewComponent 
            //然后要么删除要么隐藏
            //得到传入的Mediator 
            Mediator mediator = notification.Body as Mediator;

            if (mediator != null && mediator.ViewComponent != null)
            {
                //view都是继承MonoBehaviour的 所以能转成mono类型找到GameObject
                GameObject.Destroy(((MonoBehaviour)mediator.ViewComponent).gameObject);
                //( mediator.ViewComponent as MonoBehaviour)?.gameObject.SetActive(false);
                //删完一定要为空
                mediator.ViewComponent = null;
            }
        }
    }
}