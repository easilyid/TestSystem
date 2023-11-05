namespace MVC.Scripts.PureMVC
{
    /// <summary>
    /// PureMVC中的通知名类 
    /// 主要是用来存储通知名的
    /// 方便使用和管理
    /// </summary>
    public class PureNotification 
    {
        /// <summary>
        /// 显示面板通知
        /// </summary>
        public const string SHOW_PANEL = "showPanel";//显示面板
        /// <summary>
        /// 代表玩家数据更新的通知名字
        /// </summary>
        public const string UPDATE_PLAYER_INFO ="updatePlayerInfo"; //更新玩家信息

        public const string START_UP = "startUp"; //启动

        /// <summary>
        /// 隐藏面板通知
        /// </summary>
        public const string HIDE_PANEL = "hidePanel"; //隐藏面板

        /// <summary>
        /// 升级通知
        /// </summary>
        public const string LEV_UP = "levUp"; //升级
    }
}
