using MVC.Scripts.PureMVC.Model;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

namespace MVC.Scripts.PureMVC.View
{
    public class PureRoleViewMediator : Mediator
    {
        public new static string NAME = "PureRoleViewMediator";

        public PureRoleViewMediator() : base(NAME)
        {
        }

        public override string[] ListNotificationInterests()
        {
            return new string[]
            {
                PureNotification.UPDATE_PLAYER_INFO,
            };
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case PureNotification.UPDATE_PLAYER_INFO:
                    if (ViewComponent != null)
                    {//建议在所有界面Mediator中都加上这个判断 ！！！重要
                        //这是个很重要的情况 如果有界面会被移除的情况 但Mediator是会一直存在的 所以 我们得判断一下
                        (ViewComponent as PureRoleView).UpdateInfo(notification.Body as PlayerDataObj);
                    }
                    break;
            }
        }

        public void SetView(PureRoleView view)
        {
            ViewComponent = view;
            view.btnClose.onClick.AddListener(ClickClose);
            view.btnLevUp.onClick.AddListener(ClickLevUp);
        }

        private void ClickLevUp()
        {
            //去升级 不是必须要加参数 有的时候只需要通知就行了
            SendNotification(PureNotification.LEV_UP);
        }

        private void ClickClose()
        {
            SendNotification(PureNotification.HIDE_PANEL, this);
        }
    }
}