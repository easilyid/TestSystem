using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Binary.Scripts
{
    public class CsharpSerializable : MonoBehaviour
    {
        private Person p = new Person();

        private void Awake()
        {
            // print(Application.persistentDataPath);
            
            //BinaryDataManager.Instance.Save(p, "Person");

            var person = BinaryDataManager.Instance.Load<Person>("person");
        }

        // private void Start()
        // {
        //     //1.使用内存流得到字节数组
        //     using (MemoryStream ms = new MemoryStream())
        //     {
        //         BinaryFormatter bf = new BinaryFormatter();
        //         bf.Serialize(ms, p); //在要序列化的类上添加[Serializable]特性
        //
        //         byte[] bytes = ms.GetBuffer(); //得到字节数组
        //         //异或加密
        //         byte key = 199;
        //         for (int i = 0; i < bytes.Length; i++)
        //         {
        //             bytes[i] ^= key;
        //         }
        //
        //         // //2.使用文件流写入文件
        //         // if (!File.Exists(Application.dataPath + "/Binary/Heart"))
        //         // {
        //         //     Directory.CreateDirectory(Application.dataPath + "/Binary/Heart");
        //         //     File.WriteAllBytes(Application.dataPath + "/Binary/Heart/Person.Heart", bytes);
        //         //     ms.Close();
        //         // }
        //
        //         File.WriteAllBytes(Application.dataPath + "/Binary/Heart/Person.Heart", bytes);
        //         ms.Close();
        //     }
        //
        //     //2.使用文件流进行存储 这个方法用的比较多
        //     // using (FileStream fs = new FileStream(Application.dataPath + "/Binary/Heart/Person2.Heart",
        //     //            FileMode.OpenOrCreate, FileAccess.ReadWrite))
        //     // {
        //     //     BinaryFormatter bf = new BinaryFormatter();
        //     //     bf.Serialize(fs, p);
        //     //     fs.Flush();
        //     //     fs.Close();
        //     // }
        // }
        //
        // private void FixedUpdate()
        // {
        //     //文件流的读取
        //     // using (FileStream fs = new FileStream(Application.dataPath + "/Binary/Heart/Person2.Heart", FileMode.Open,
        //     //            FileAccess.Read))
        //     // {
        //     //     BinaryFormatter bf = new BinaryFormatter();
        //     //     var p = bf.Deserialize(fs) as Person;
        //     //     fs.Close();
        //     //
        //     //     // Debug.Log(p.id);
        //     //     // Debug.Log(p.id2);
        //     //     // Debug.Log(p.id3);
        //     //     // Debug.Log(p.name);
        //     //     // Debug.Log(p.name2);
        //     //     // Debug.Log(p.ints);
        //     //     // Debug.Log(p.Lists);
        //     //     // Debug.Log(p.dic);
        //     //     // Debug.Log(p.structTest.i);
        //     //     // Debug.Log(p.structTest.s);
        //     //     // Debug.Log(p.clssTest);
        //     // }
        //     //
        //     // print("=====================================");
        //     // //网络传输的字节数组的读取 没有网络传输就用本地文件
        //     // byte[] bytes = File.ReadAllBytes(Application.dataPath + "/Binary/Heart/Person2.Heart");
        //     // using (MemoryStream ms = new MemoryStream(bytes))
        //     // {
        //     //     BinaryFormatter bf = new BinaryFormatter();
        //     //     var deserialize = bf.Deserialize(ms) as Person;
        //     //     // Debug.Log(deserialize.id);
        //     //     // Debug.Log(deserialize.id2);
        //     //     // Debug.Log(deserialize.id3);
        //     //     // Debug.Log(deserialize.name);
        //     //     // Debug.Log(deserialize.name2);
        //     //     // Debug.Log(deserialize.ints);
        //     //     // Debug.Log(deserialize.Lists);
        //     //     // Debug.Log(deserialize.dic);
        //     //     // Debug.Log(deserialize.structTest.i);
        //     //     // Debug.Log(deserialize.structTest.s);
        //     //     // Debug.Log(deserialize.clssTest);
        //     //     ms.Close();
        //     // }
        //     //
        //
        //     print("=====================================");
        //     //异或解密
        //
        //     byte[] bytes2 = File.ReadAllBytes(Application.dataPath + "/Binary/Heart/Person.Heart");
        //     byte key = 199;
        //     for (int i = 0; i < bytes2.Length; i++)
        //     {
        //         bytes2[i] ^= key;
        //     }
        //
        //     using (MemoryStream ms = new MemoryStream(bytes2))
        //     {
        //         BinaryFormatter bf = new BinaryFormatter();
        //         var person = bf.Deserialize(ms) as Person;
        //         ms.Close();
        //     }
        // }
        
        
    }

    [System.Serializable]
    public class Person
    {
        public int id = 1;
        public float id2 = 1.1f;
        public double id3 = 2.2;
        public string name = "Heart";
        public char name2 = 'H';
        public int[] ints = { 1, 2, 3, 4, 5, 6 };
        public List<int> Lists = new List<int>() { 7, 8, 9, 0 };
        public Dictionary<int, string> dic = new Dictionary<int, string>() { { 1, "123" }, { 2, "456" }, { 3, "789" } };
        public StructTest structTest = new StructTest(1, "Heart");
        public ClssTest clssTest = new ClssTest();
    }

    [System.Serializable]
    public class StructTest
    {
        public int i = 1;
        public string s = "Heart";

        public StructTest(int i, string s)
        {
            this.i = i;
            this.s = s;
        }
    }

    [System.Serializable]
    public class ClssTest
    {
        public ClssTest()
        {
        }
    }
}