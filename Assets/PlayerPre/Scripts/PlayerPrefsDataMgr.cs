using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace PlayerPre.Scripts
{
    /// <summary>
    /// 数据管理类 统一管理数据的读取和存储
    /// </summary>
    public class PlayerPrefsDataMgr
    {
        private static PlayerPrefsDataMgr _instance = new PlayerPrefsDataMgr();

        public static PlayerPrefsDataMgr Instance => _instance;

        //私有构造 保证单例 让外部无法创建
        private PlayerPrefsDataMgr()
        {
        }

        /// <summary>
        /// 存储数据
        /// </summary>
        /// <param name="data"> 数据对象</param>
        /// <param name="keyName">数据对象的唯一性</param>
        public void SaveData(object data, string keyName)
        {
            //就是要通过Type得到传入数据的所有字段 然后结合PlayerPrefs进行存储
            Type dataType = data.GetType();
            var infos = dataType.GetFields();
            string saveKeyName = "";
            for (int i = 0; i < infos.Length; i++)
            {
                FieldInfo info = infos[i]; //获取字段
                //定义数据的存储规则 通过数据的类型和数据的唯一性来进行存储
                //keyname_字段类型_字段名字
                saveKeyName = keyName + "_" + dataType.Name + "_" + info.Name;
                SaveValue(info.GetValue(data), saveKeyName);
            }

            PlayerPrefs.Save();
        }

        private void SaveValue(object value, string keyName)
        {
            //直接使用PlayerPrefs进行存储 根据数据类型的不同来决定存储类型 int float string 
            var fieldType = value.GetType();
            if (fieldType == typeof(int))
            {
                Debug.Log("存储int类型" + value);
                PlayerPrefs.SetInt(keyName, (int)value);
            }
            else if (fieldType == typeof(float))
            {
                Debug.Log("存储float类型" + value);
                PlayerPrefs.SetFloat(keyName, (float)value);
            }
            else if (fieldType == typeof(string))
            {
                Debug.Log("存储string类型" + value);
                PlayerPrefs.SetString(keyName, (string)value);
            }
            else if (fieldType == typeof(bool))
            {
                Debug.Log("存储bool类型" + value);
                PlayerPrefs.SetInt(keyName, (bool)value ? 1 : 0);
            }
            else if (typeof(IList).IsAssignableFrom(fieldType))
            {
                //父类装子类 通过父类来确定泛型的类型
                Debug.Log("存储List类型" + keyName);
                IList list = value as IList;
                PlayerPrefs.SetInt(keyName, list.Count);
                int index = 0;
                foreach (var obj in list)
                {
                    //存储具体的值
                    SaveValue(obj, keyName + index);
                    ++index;
                }
            }
            else if (typeof(IDictionary).IsAssignableFrom(fieldType))
            {
                Debug.Log("存储Dictionary类型" + keyName);
                IDictionary dic = value as IDictionary;
                PlayerPrefs.SetInt(keyName, dic.Count);
                int index = 0;
                foreach (var obj in dic.Keys) //要遍历的是key
                {
                    SaveValue(obj, keyName + "_key_" + index);
                    SaveValue(dic[obj], keyName + "_value_" + index);
                    ++index;
                }
            } //判断是不是自定义类
            else
            {
                SaveData(value, keyName);
            }
        }

        /// <summary>
        /// 读取数据 
        /// </summary>
        /// <param name="data">想要读取数据的 数据类型</param>
        /// <param name="keyName">数据对象的唯一性</param>
        /// <returns></returns>
        public object LoadData(Type type, string keyName)
        {
            //不用object传入 而用Type 传入 主要节约一行代码(在外部)
            //假设你要读取一个player 数据 如果是object 就必须在外部 new对象传入
            //如果是Type 只需要传入 typeof(Player) 就可以了 就能在内部创建 达到在外部少些一行代码的目的

            //根据传入的类型和keyName 依据存储数据和 key的规则 来进行数据的获取赋值 然后返回出去
            object data = Activator.CreateInstance(type);
            var infos = type.GetFields();
            string loadName = "";
            FieldInfo info;
            for (int i = 0; i < infos.Length; i++)
            {
                info = infos[i];
                loadName = keyName + "_" + type.Name + "_" + "_" + info.FieldType.Name + "_" + info.Name;
                info.SetValue(data, LoadValue(info.FieldType, loadName));
            }

            return data;
        }

        private object LoadValue(Type fieldType, string keyName)
        {
            if (fieldType == typeof(int))
            {
                return PlayerPrefs.GetInt(keyName, 0);
            }
            else if (fieldType == typeof(float))
            {
                return PlayerPrefs.GetFloat(keyName, 0);
            }
            else if (fieldType == typeof(string))
            {
                return PlayerPrefs.GetString(keyName, "");
            }
            else if (fieldType == typeof(bool))
            {
                return PlayerPrefs.GetInt(keyName, 0) == 1 ? true : false;
            }
            else if (typeof(IList).IsAssignableFrom(fieldType))
            {
                //得到长度
                int count = PlayerPrefs.GetInt(keyName, 0);
                IList list = Activator.CreateInstance(fieldType) as IList;
                for (int i = 0; i < count; i++)
                {
                    list.Add(LoadValue(fieldType.GetGenericArguments()[0], keyName + i));
                }

                return list;
            }
            else if (typeof(IDictionary).IsAssignableFrom(fieldType))
            {
                int count = PlayerPrefs.GetInt(keyName, 0);
                IDictionary dic = Activator.CreateInstance(fieldType) as IDictionary;
                for (int i = 0; i < count; i++)
                {
                    dic.Add(LoadValue(fieldType.GetGenericArguments()[0], keyName + "_key_" + i),
                        LoadValue(fieldType.GetGenericArguments()[1], keyName + "_value_" + i));
                }

                return dic;
            }
            else
            {
                return LoadData(fieldType, keyName);
            }

            return null;
        }
    }
}