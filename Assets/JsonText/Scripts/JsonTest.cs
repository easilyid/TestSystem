using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace JsonText.Scripts
{
    [System.Serializable]
    public class Student
    {
        public int age;
        public string name;

        public Student(int age, string name)
        {
            this.age = age;
            this.name = name;
        }
    }


    public class MyHeart
    {
        public string name;
        public int age;
        public bool sex;
        public float testF;
        public double testD;
        public int[] array;
        public List<int> list;
        public Dictionary<int, string> dic;
        public Dictionary<string, string> dic2;
        public Student s1;
        public List<Student> s2;
        [SerializeField]
        private int private1 = 1;
        [SerializeField]
        protected int protected1 = 2;
    }

    public class JsonTest : MonoBehaviour
    {
        void Update()
        {
            MyHeart h = new MyHeart();
            h.name = " Heart";
            h.age = 22;
            h.sex = true;
            h.testF = 1.1f;
            h.testD = 2.2;
            h.array = new int[] { 1, 2, 3, 4 };
            h.list = new List<int>() { 5, 6, 7, 8 };
            h.dic = new Dictionary<int, string>() { { 1, "123" }, { 2, "456" } };
            h.dic2 = new Dictionary<string, string>() { { "1", "123" }, { "2", "456" } };
            h.s1 = new Student(1, "Heart");
            h.s2 = new List<Student>() { new Student(2, "easily"), new Student(3, "Sakura") };
            
            //序列化前面数据
            //注意JsonUtility 序列化的时候在自定义类上要加上 [System.Serializable]
            //在私有变量上加上 [SerializeField]
            //JsonUtility 不支持字典
            //JsonUtility 存储null对象时不会为null 会变成默认值的数据
            var jsonStr = JsonUtility.ToJson(h);
            File.WriteAllText(Application.persistentDataPath+"/MyHeart.json",jsonStr);
            print(Application.persistentDataPath);
            
            //读取文件中的json字符串
            jsonStr =File.ReadAllText(Application.persistentDataPath+"/MyHeart.json");
            //反序列化 两种写法
           // MyHeart fromJson = JsonUtility.FromJson(jsonStr,typeof(MyHeart)) as MyHeart;
            MyHeart h3 = JsonUtility.FromJson<MyHeart>(jsonStr);
            
            
            // JsonUtility 无法直接读取数据集合 
            string jstr = "";
            jstr = File.ReadAllText(Application.streamingAssetsPath + "/roleInfo.json");
            print(Application.streamingAssetsPath);
            //List<RoleInfo>list = JsonUtility.FromJson<List<RoleInfo>>(jstr);
            RoleInfo data = JsonUtility.FromJson<RoleInfo>(jstr);
        }
    }
}

 
public class RoleInfo
{
    public int hp;
    public int speed;
    public int volume;
    public string resName;
    public int scale;
}

public class MyClass
{
    public string name;
    public int age;
    public bool sex;
    public List<int> ids;
    public List<Person> students;
    public Home home;
    public Person son;
}

public class Home
{
    public string adress;
    public string street;
}

public class Person
{
    public string name;
    public int age;
    public bool sex;
}


public class Item
{
    public int id;
    public int num;

    public Item(int id, int num)
    {
        this.id = id;
        this.num = num;
    }
}

public class PlayerInfo2
{
    public string name;
    public int atk;
    public int def;
    public float moveSpeed;
    public double roundSpeed;
    public Item weapon;
    public List<int> listInt;
    public List<Item> ItemList;
    public Dictionary<int, Item> itemDic;
    public Dictionary<string, Item> ItemDic2;
    private int privateI = 1;
    protected int protrctedI = 2;
}

