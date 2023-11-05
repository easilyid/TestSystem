using MVC.Scripts.MVC.Controller;
using UnityEngine;

public class TestMVC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //只能生成一次界面 不能重复生成
            MainController.ShowMe();
            
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            MainController.HideMe();
        }
    }
}
