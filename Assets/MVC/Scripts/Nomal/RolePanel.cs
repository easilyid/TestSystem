using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class RolePanel : MonoBehaviour
{
    public Text txtLev;
    public Text txtHp;
    public Text txtAtk;
    public Text txtDef;
    public Text txtCrit;
    public Text txtMiss;
    public Text txtLuck;
    public Button btnClose;
    public Button btnLevUp;

    private static RolePanel _panel;

    private void Start()
    {
        btnClose.onClick.AddListener(ClosePanel);
        btnLevUp.onClick.AddListener(LevUp);
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        txtLev.text = "LV. " + PlayerPrefs.GetInt("PlayerLevel", 1).ToString();
        txtHp.text = PlayerPrefs.GetInt("PlayerHp", 100).ToString();
        txtAtk.text = PlayerPrefs.GetInt("PlayerAtk", 10).ToString();
        txtDef.text = PlayerPrefs.GetInt("PlayerDef", 20).ToString();
        txtCrit.text = PlayerPrefs.GetInt("PlayerCrit", 30).ToString();
        txtMiss.text = PlayerPrefs.GetInt("PlayerMiss", 10).ToString();
        txtLuck.text = PlayerPrefs.GetInt("PlayerLuck", 60).ToString();
    }

    private void LevUp()
    {
        Debug.Log("升级");
        //1. 获取数据
        int lev = PlayerPrefs.GetInt("PlayerLevel", 1);
        int hp = PlayerPrefs.GetInt("PlayerHp", 100);
        int atk = PlayerPrefs.GetInt("PlayerAtk", 10);
        int def = PlayerPrefs.GetInt("PlayerDef", 20);
        int crit = PlayerPrefs.GetInt("PlayerCrit", 30);
        int miss = PlayerPrefs.GetInt("PlayerMiss", 10);
        int luck = PlayerPrefs.GetInt("PlayerLuck", 60);
        //2.通过一定的规则去更新
        lev += 1;
        hp += lev;
        atk += lev;
        def += lev;
        crit += lev;
        miss += lev;
        luck += lev;
        
        //3.保存数据
        PlayerPrefs.SetInt("PlayerLevel", lev);
        PlayerPrefs.SetInt("PlayerHp", hp);
        PlayerPrefs.SetInt("PlayerAtk", atk);
        PlayerPrefs.SetInt("PlayerDef", def);
        PlayerPrefs.SetInt("PlayerCrit", crit);
        PlayerPrefs.SetInt("PlayerMiss", miss);
        PlayerPrefs.SetInt("PlayerLuck", luck);
        
        //4.同步更新面板的数据
        UpdateInfo();
        MainPanel.Panel.UpdateInfo();
    }

    public void ClosePanel()
    {
        //gameObject.SetActive(false);
        HideMe();
    }

    public static void ShowMe()
    {
        if (_panel == null)
        {
            GameObject res = Resources.Load<GameObject>("UI/RolePanel");
            var go = Instantiate(res, GameObject.Find("Canvas").transform, false);
            _panel = go.GetComponent<RolePanel>();
        }

        //_panel.gameObject.SetActive(true);
        _panel.UpdateInfo();
    }

    public static void HideMe()
    {
        if (_panel != null)
        {
            Destroy(_panel.gameObject);
            _panel = null;
        }
    }
}