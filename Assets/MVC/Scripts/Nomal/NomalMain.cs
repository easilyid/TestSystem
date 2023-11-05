using UnityEngine;

public class NomalMain : MonoBehaviour
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
            MainPanel.ShowMe();
            
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            MainPanel.HideMe();
        }
    }
}