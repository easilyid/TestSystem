using System;
using System.Data;
using System.IO;
using System.Text;
using Excel;
using UnityEditor;
using UnityEngine;

namespace Binary.Editor
{
    public class EditorTool
    {
        /// <summary>
        /// Excel文件存放路径
        /// </summary>
        public static string EXCEL_PATH = Application.dataPath + "/Binary/ArtRes/Excel/";

        /// <summary>
        /// 数据结构类存放路径
        /// </summary>
        public static string DATA_CLASS_PATH = Application.dataPath + "/Binary/Scripts/DataClass/";

        /// <summary>
        /// 数据容器类存放路径
        /// </summary>
        public static string DATA_CONTAINER_PATH = Application.dataPath + "/Binary/Scripts/Container/";

        /// <summary>
        /// 二进制数据存放路径
        /// </summary>
        public static string DATA_BINARY_PATH = Application.dataPath + "/Binary/Data/";
        
        /// <summary>
        /// 真正内容开始的索引
        /// </summary>
        public static int BEGIN_INDEX = 4;

        [MenuItem("GameTools/生成Excel信息")]
        public static void GenerateExcelInfo()
        {
            var dInfo = Directory.CreateDirectory(EXCEL_PATH);
            var files = dInfo.GetFiles(); //得到文件夹下所有的文件

            //数据表集合 容器
            DataTableCollection tableCollection;
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Extension != ".xlsx" && files[i].Extension != ".xls")
                    continue; //如果不是Excel文件就跳过
                using (FileStream fs = files[i].Open(FileMode.Open, FileAccess.Read))
                {
                    IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                    tableCollection = excelDataReader.AsDataSet().Tables;
                    fs.Close();
                }

                foreach (DataTable dataTable in tableCollection)
                {
                    //遍历文件中所有表的信息
                    //生成数据结构类
                    GenerateDataClass(dataTable);
                    //生成数据容器类
                    GenerateExcelContainer(dataTable);
                    //生成二进制数据
                    GenerateExcelBinary(dataTable);
                }
            }
        }


        /// <summary>
        /// 生成数据结构类Excel表对应的
        /// </summary>
        /// <param name="table"></param>
        public static void GenerateDataClass(DataTable table)
        {
            DataRow rowName = GetVariableNameRow(table);
            DataRow rowType = GetVariableTypeRow(table);

            //生成数据结构类 判断路径  生成代码脚本无非就是字符串拼接
            if (!Directory.Exists(DATA_CLASS_PATH))
                Directory.CreateDirectory(DATA_CLASS_PATH);

            string str =
                "public class " + table.TableName + "\n{\n"; //生成类名 以Excel表名为类名
            for (int i = 0; i < table.Columns.Count; i++)
            {
                str += "   public " + rowType[i].ToString() + " " + rowName[i].ToString() + ";\n";
            }

            str += "}";

            File.WriteAllText(DATA_CLASS_PATH + table.TableName + ".cs", str);
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 获取变量名所在行
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private static DataRow GetVariableNameRow(DataTable table)
        {
            return table.Rows[0];
            
        }

        /// <summary>
        /// 获取变量类型所在行
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private static DataRow GetVariableTypeRow(DataTable table)
        {
            return table.Rows[1];
            
        }

        /// <summary>
        /// 生成容器类
        /// </summary>
        /// <param name="table"></param>
        public static void GenerateExcelContainer(DataTable table)
        {
            int keyIndex = GetKeyIndex(table); //主键索引
            //得到字段类型行
            DataRow rowType = GetVariableTypeRow(table);
            if (!Directory.Exists(DATA_CONTAINER_PATH))
            {
                Directory.CreateDirectory(DATA_CONTAINER_PATH);
            }

            string s = "using System.Collections.Generic;\n\n\n";
            s += "public class " + table.TableName + "Container" + "\n{\n";
            s += " ";
            s += "  public Dictionary<" + rowType[keyIndex].ToString() + "," + table.TableName + ">";
        s += "dataDic = new " + "Dictionary<" + rowType[keyIndex].ToString() + ", " + table.TableName + ">();\n";

            s += "}";
            File.WriteAllText(DATA_CONTAINER_PATH + table.TableName + "Container.cs", s);
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 获取主键索引
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private static int GetKeyIndex(DataTable table)
        {
            DataRow row = table.Rows[2];
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (row[i].ToString() == "key")
                    return i;
            }

            return 0;
        }

        /// <summary>
        /// 生成二进制数据
        /// </summary>
        /// <param name="table"></param>
        public static void GenerateExcelBinary(DataTable table)
        {
            if (!Directory.Exists(DATA_BINARY_PATH))
            {
                Directory.CreateDirectory(DATA_BINARY_PATH);
            }

            using (FileStream fs = new FileStream(DATA_BINARY_PATH + table.TableName + ".tt", FileMode.OpenOrCreate,
                       FileAccess.Write))
            {
                //1、存储具体的excel对应的2进制信息
                //先要存储我们需要写多少行的数据 方便我们读取 -4是我们这文件的前4行是配置规则
                fs.Write(BitConverter.GetBytes(table.Rows.Count-4),0,4);
                //2.存储主键的变量名
            string keyName = GetVariableNameRow(table)[GetKeyIndex(table)].ToString();

                byte[] bytes = Encoding.UTF8.GetBytes(keyName);
                //存储字符串字节数组的长度
                fs.Write(BitConverter.GetBytes(bytes.Length),0,4);
                //存储字符串字节数组
                fs.Write(bytes, 0, bytes.Length);

                DataRow row;
                DataRow rowType = GetVariableTypeRow(table);//得到类型行 根据类型写入数据
                //正常存储数据 上面都是存储配置信息
                for (int i = BEGIN_INDEX; i < table.Rows.Count; i++)
                {
                    row = table.Rows[i];
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        //根据类型写入数据
                        switch (rowType[j].ToString())
                        {
                            case "int":
                                fs.Write(BitConverter.GetBytes(int.Parse(row[j].ToString())),0,4);
                                break;
                            case "float":
                                fs.Write(BitConverter.GetBytes(float.Parse(row[j].ToString())),0,4);
                                break;
                            case "bool":
                                fs.Write(BitConverter.GetBytes(bool.Parse(row[j].ToString())),0,1);
                                break;
                            case "string":
                                //字符串存储规则 字符串长度+字符串字节数组 先写入长度 再写入字节数组(套路写法)
                                bytes =Encoding.UTF8.GetBytes(row[j].ToString());
                                fs.Write(BitConverter.GetBytes(bytes.Length),0,4);
                                fs.Write(bytes,0,bytes.Length);
                                break;
                        }
                    }
                }
                fs.Close();
            }

            AssetDatabase.Refresh();
        }
    }
}