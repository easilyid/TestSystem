using System;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

namespace JsonText.Scripts
{
    public class Student2
    {
        public int age;
        public string name;

        public Student2()
        {
            
        }
        
        public Student2(int age, string name)
        {
            this.age = age;
            this.name = name;
        }
    }


    public class MyHeart2
    {
        public string name;
        public int age;
        public bool sex;
        public float testF;
        public double testD;
        public int[] array;
        public List<int> list;
        //public Dictionary<int, string> dic;
        public Dictionary<string, string> dic2;
        public Student2 s1;
        public List<Student2> s2;
       
        private int private1 = 1;
       
        protected int protected1 = 2;
    }
    
    public class ListJsonTest : MonoBehaviour
    {
        private void Start()
        {
            MyHeart2 h2 = new MyHeart2();
            h2.name = " Heart";
            h2.age = 22;
            h2.sex = true;
            h2.testF = 1.1f;
            h2.testD = 2.2;
            h2.array = new int[] { 1, 2, 3, 4 };
            h2.list = new List<int>() { 5, 6, 7, 8 };
           // h2.dic = new Dictionary<int, string>() { { 1, "123" }, { 2, "456" } };
            h2.dic2 = new Dictionary<string, string>() { { "1", "123" }, { "2", "456" } };
            h2.s1 = new Student2(1, "Heart");
            h2.s2 = new List<Student2>() { new Student2(2, "easily"), new Student2(3, "Sakura") };
            
            //ListJson 支持字典  空值就用空值 私有变量不会被序列化
            //不需要加上 [System.Serializable] 和 [SerializeField]
            string js= JsonMapper.ToJson(h2);//第二个重载，我们可以自定义存储规则
            File.WriteAllText(Application.persistentDataPath+"/MyHeart2.json",js);
            
            //反序列化
            js =File.ReadAllText(Application.persistentDataPath+"/MyHeart2.json");
            //jsonData 是ListJson 提供的类对象 可以直接使用键值对的方式来获取 使用 数据
            JsonData jsonData = JsonMapper.ToObject(js);
            print(jsonData["name"]);
            print(jsonData["age"]);
            
            //通过泛型去读取更加方便
            //注意 字典类型 因为json的原因 键 会变成string 所以int 类型的键 会变成string类型 报错
            // 且必须 带 无参构造函数 因为lIstjson在反序列化的时候会去创建对象
            MyHeart2 data = JsonMapper.ToObject<MyHeart2>(js);
            print(data.name);
            print(data.age);

        }
    }
}