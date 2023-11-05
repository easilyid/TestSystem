using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    //传统做法
    //1.获得控件
    public Text textName; //名字
    public Text textLevel; //等级
    public Text textMoney; //金币
    public Text textGem; //宝石
    public Text textPower; //力量
    public Button btnRole; //角色按钮

    private static MainPanel _panel;

    public static MainPanel Panel => _panel;

    void Start()
    {
        //2.添加事件
        btnRole.onClick.AddListener(OnRoleClick);
    }

    private void OnRoleClick()
    {
        //打开角色面板
        Debug.Log("打开角色面板");
        RolePanel.ShowMe();
    }


    //3.更新信息
    public void UpdateInfo()
    {
        //使用PlayerFrefabs 存储信息
        textName.text = PlayerPrefs.GetString("PlayerName", "Heart");
        textLevel.text = "LV. " + PlayerPrefs.GetInt("PlayerLevel", 1).ToString();
        textMoney.text = PlayerPrefs.GetInt("PlayerMoney", 999).ToString();
        textGem.text = PlayerPrefs.GetInt("PlayerGem", 888).ToString();
        textPower.text = PlayerPrefs.GetInt("PlayerPower", 10).ToString();
    }

    //4.动态显隐藏
    public static void ShowMe()
    {
        //从Resources文件夹中加载预制体 
        //只能生成一次 不能重复生成
        if (_panel == null)
        {
            GameObject res = Resources.Load<GameObject>("UI/MainPanel");
            GameObject go = Instantiate(res, GameObject.Find("Canvas").transform, false);
            _panel = go.GetComponent<MainPanel>();
        }

        _panel.gameObject.SetActive(true); //隐藏的话就得设置为显示 大项目用删除 
        _panel.UpdateInfo();
    }

    public static void HideMe()
    {
        //1.直接删
        if (_panel!=null)
        {
            // Destroy(_panel.gameObject);
            // _panel = null;
            
            //2. 设置为隐藏
            _panel.gameObject.SetActive(false);
        }
        
    }
}