using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Binary.Scripts
{
    public class TypeByte : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //常用类型的最大值和最小值
            print("有符号");
            print("sbyte:" + sizeof(sbyte) + "字节");
            print("int:" + sizeof(int) + "字节");
            print("short:" + sizeof(short) + "字节");
            print("long:" + sizeof(long) + "字节");

            print("无符号");
            print("byte:" + sizeof(byte) + "字节");
            print("uint:" + sizeof(uint) + "字节");
            print("ushort:" + sizeof(ushort) + "字节");
            print("ulong:" + sizeof(ulong) + "字节");

            print("浮点数");
            print("float:" + sizeof(float) + "字节");
            print("double:" + sizeof(double) + "字节");

            print("特殊");
            print("char:" + sizeof(char) + "字节");
            print("bool:" + sizeof(bool) + "字节");
            print("decimal:" + sizeof(decimal) + "字节");
        }

        // Update is called once per frame
        void Update()
        {
            // //超过255的数值 会被截断 进位 
            // var bytes = BitConverter.GetBytes(100);
            // print(bytes.Length);
            //
            // var int16 = BitConverter.ToInt16(bytes, 0);
            //
            // byte[] bytes1 = Encoding.UTF8.GetBytes("Heart");
            //
            // string s = Encoding.UTF8.GetString(bytes1);
            //
            //
            // //文件流 参数 1 文件路径 2 文件打开方式 3 文件权限 4 文件共享权限
            // var fileStream = new FileStream(Application.dataPath + "/Binary/Scripts/TypeByte.cs", FileMode.OpenOrCreate,
            //     FileAccess.ReadWrite, FileShare.ReadWrite);
            //

            Student s = new Student();
            s.age = 18;
            s.name = "Heart";
            s.number = 1;
            s.sex = false;
            
            s.Save("Heart");
            
            Student s2 =Student.Load("Heart");
        }
    }
}