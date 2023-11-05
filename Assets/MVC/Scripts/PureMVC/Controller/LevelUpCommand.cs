using MVC.Scripts.PureMVC.Model;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

namespace MVC.Scripts.PureMVC.Controller
{
    public class LevelUpCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            base.Execute(notification);
            var retrieveProxy = Facade.RetrieveProxy(PlayerProxy.NAME) as PlayerProxy;
            if (retrieveProxy != null)
            {
                //升级 
                retrieveProxy.LevUp();
                retrieveProxy.SaveData();//保存数据
                //通知更新
                SendNotification(PureNotification.UPDATE_PLAYER_INFO, retrieveProxy.Data);
            }
        }
    }
}