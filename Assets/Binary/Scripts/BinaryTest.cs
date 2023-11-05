using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Binary.Scripts
{
    public class BinaryTest
    {
        
    }

   
    public class Student
    {
        public int age;
        public string name;
        public int number;
        public bool sex;

        public void Save(string fileName)
        {
            Debug.Log(Application.persistentDataPath);
            //创建文件流
            if (!Directory.Exists(Application.persistentDataPath + "/Binary"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/Binary");
            }


            //新建文件流 并且返回文件流进行字节的存储
            using (FileStream fs = new FileStream(Application.persistentDataPath + "/Binary/" + fileName + ".txt",
                       FileMode.OpenOrCreate, FileAccess.Write))
            {
                //写age int
                var bytes = BitConverter.GetBytes(age);
                fs.Write(bytes, 0, bytes.Length);

                //写name string  除了字符串其他的写入都是简单的
                bytes = Encoding.UTF8.GetBytes(name);
                //存储字符串字节数组的长度
                fs.Write(BitConverter.GetBytes(bytes.Length), 0, 4); //4个字节的字节数组
                //存储字符串字节数组
                fs.Write(bytes, 0, bytes.Length);

                //写number int
                bytes = BitConverter.GetBytes(number);
                fs.Write(bytes, 0, bytes.Length);

                //写sex bool
                bytes = BitConverter.GetBytes(sex);
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        public static Student Load(string fileName)
        {
            if (!File.Exists(Application.persistentDataPath + "/Binary/" + fileName + ".txt"))
            {
                Debug.Log("文件不存在");
                return null;
            }

            Student s = new Student();
            //加载文件流
            using (FileStream fs = File.Open(Application.persistentDataPath + "/Binary/" + fileName + ".txt",
                       FileMode.Open, FileAccess.Read))
            {
                //把文件子节读取出来
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                //挨个读取内容

                int index = 0;
                //读取age
                s.age = BitConverter.ToInt32(bytes, index);
                index += 4; //以4个字节为单位 读取
                //读取name  字符串 读取字符串的长度  读取字符串
                int length = BitConverter.ToInt32(bytes, index);
                index += 4;
                s.name = Encoding.UTF8.GetString(bytes, index, length);
                index+= length;
                
                //读取number
                s.number = BitConverter.ToInt32(bytes, index);
                index += 4;
                //读取Sex
                s.sex = BitConverter.ToBoolean(bytes, index);
                index += 1;
            }
            
            return s;
        }
    }

}