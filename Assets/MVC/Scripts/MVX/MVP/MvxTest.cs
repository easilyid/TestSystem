using System;
using MVC.Scripts.MVX.MVP.Presenter;
using UnityEngine;

namespace MVC.Scripts.MVX.MVP
{
    public class MvxTest : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                MainPresenter.ShowMe();
            }else if (Input.GetKeyDown(KeyCode.N))
            {
                MainPresenter.HideMe();
            }
        }
    }
}