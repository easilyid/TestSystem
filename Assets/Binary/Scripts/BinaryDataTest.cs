using System;
using UnityEngine;

namespace Binary.Scripts
{
    public class BinaryDataTest: MonoBehaviour
    {
        private void Start()
        {
            BinaryDataManager.Instance.InitData();
            var data = BinaryDataManager.Instance.GetTable<TowerInfoContainer>();
            print(data.dataDic[5].name);
        }
    }
}