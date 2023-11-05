using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace Binary.Scripts
{
    /// <summary>
    /// 二进制数据管理器
    /// </summary>
    public class BinaryDataManager
    {
        private static BinaryDataManager _instance = new BinaryDataManager();

        public static BinaryDataManager Instance => _instance;

        public static string SAVE_PATH = Application.dataPath + "/Binary/Data/";

        private Dictionary<string, object> tableDic = new Dictionary<string, object>();

        /// <summary>
        /// 二进制数据存放路径
        /// </summary>
        public static string DATA_BINARY_PATH = Application.dataPath + "/Binary/Data/";

        public BinaryDataManager()
        {
        }

        public void InitData()
        {
            LoadTable<TowerInfoContainer, TowerInfo>();
        }

        /// <summary>
        /// 加载Excel表二进制数据到内存中
        /// </summary>
        /// <typeparam name="T">容器类名</typeparam>
        /// <typeparam name="K">数据结构体名</typeparam>
        public void LoadTable<T, K>()
        {
            using (FileStream fs = File.Open(DATA_BINARY_PATH + typeof(K).Name + ".tt", FileMode.Open,
                       FileAccess.Read))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                int index = 0; //同于记录当前读取到的位置

                //读取多少行数据 
                int count = BitConverter.ToInt32(bytes, index);
                index += 4;
                //读取主键的数据
                int keyNameLength = BitConverter.ToInt32(bytes, index);
                index += 4;
                string keyName = Encoding.UTF8.GetString(bytes, index, keyNameLength); //读取主键的名字
                index += keyNameLength;

                //创建容器类对象
                Type contaninerType = typeof(T);
                object contaninerObj = Activator.CreateInstance(contaninerType);
                //获取数据结构类对象
                Type classType = typeof(K);
                //获取数据结构类的所有字段信息 PS
                FieldInfo[] infos = classType.GetFields();
                //读取每一行的数据
                for (int i = 0; i < count; i++)
                {
                    //实例化类对象
                    object dataObj = Activator.CreateInstance(classType);

                    foreach (FieldInfo info in infos)
                    {
                        if (info.FieldType == typeof(int))
                        {
                            //给类对象的字段赋值 转int值赋值给对应的字段
                            info.SetValue(dataObj, BitConverter.ToInt32(bytes, index));
                            index += 4;
                        }
                        else if (info.FieldType == typeof(float))
                        {
                            info.SetValue(dataObj, BitConverter.ToSingle(bytes, index));
                            index += 4;
                        }
                        else if (info.FieldType == typeof(bool))
                        {
                            info.SetValue(dataObj, BitConverter.ToBoolean(bytes, index));
                            index += 1; //bool类型占用1个字节
                        }
                        else if (info.FieldType == typeof(string))
                        {
                            int length = BitConverter.ToInt32(bytes, index);
                            index += 4;
                            info.SetValue(dataObj, Encoding.UTF8.GetString(bytes, index, length));
                            index += length; //每次读取字符串的长度 必须加！！！！！
                        }
                    }

                    //将数据存储到容器类中
                    object dicObject = contaninerType.GetField("dataDic").GetValue(contaninerObj);
                    //获取Add方法
                    MethodInfo mInfo = dicObject.GetType().GetMethod("Add");
                    //获取主键的值
                    object keyValue = classType.GetField(keyName).GetValue(dataObj);
                    // Add方法的参数
                    mInfo.Invoke(dicObject, new object[] { keyValue, dataObj });
                }

                //表读取完就记录下来
                tableDic.Add(typeof(T).Name, contaninerObj);
                fs.Close();
            }
        }


        /// <summary>
        /// 得到表中的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetTable<T>() where T : class
        {
            string tableName = typeof(T).Name;
            if (tableDic.ContainsKey(tableName))
            {
                return tableDic[tableName] as T;
            }

            return null;
        }


        /// <summary>
        /// 存储类对象数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        public void Save(object obj, string fileName)
        {
            if (!Directory.Exists(SAVE_PATH))
            {
                Directory.CreateDirectory(SAVE_PATH);
            }

            byte encryptionKey = 0xAA;
            byte[] data = SerializeObject(obj);

            for (int i = 0; i < data.Length; i++)
            {
                data[i] ^= encryptionKey;
            }

            using (FileStream fs = new FileStream(SAVE_PATH + fileName + ".tt", FileMode.OpenOrCreate,
                       FileAccess.Write))
            {
                // BinaryFormatter bf = new BinaryFormatter();
                // bf.Serialize(fs, obj);
                // fs.Close();
                fs.Write(data, 0, data.Length);
                fs.Close();
            }
        }

        private byte[] SerializeObject(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 读取2进制数据转换成对象
        /// </summary>
        /// <param name="fileName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Load<T>(string fileName) where T : class
        {
            if (!File.Exists(SAVE_PATH + fileName + ".tt"))
            {
                return default(T);
            }

            T obj;
            byte encryptionKey = 0xAA;

            using (FileStream fs = new FileStream(SAVE_PATH + fileName + ".tt", FileMode.Open, FileAccess.Read))
            {
                byte[] encryptionData = new byte[fs.Length];
                fs.Read(encryptionData, 0, encryptionData.Length);
                for (int i = 0; i < encryptionData.Length; i++)
                {
                    encryptionData[i] ^= encryptionKey;
                }

                // BinaryFormatter bf = new BinaryFormatter();
                // obj = bf.Deserialize(fs) as T;
                // fs.Close();

                using (MemoryStream ms = new MemoryStream(encryptionData))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    obj = bf.Deserialize(ms) as T;
                }
            }

            return obj;
        }
    }
}