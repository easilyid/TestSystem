using System.Data;
using System.IO;
using Excel;
using UnityEditor;
using UnityEngine;

namespace Binary.Editor
{
    public class TestEditor
    {
        [MenuItem("GameTools/打开Excel表")]
        public static void OpenExcel()
        {
            using (FileStream fs = File.Open(Application.dataPath + "/Binary/ArtRes/Excel/PlayerInfo.xlsx",
                       FileMode.Open, FileAccess.Read))
            {
                //通过文件流读取
                IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                //将数据转为DataSet
                var asDataSet = excelDataReader.AsDataSet();
                //得到数据
                for (int i = 0; i < asDataSet.Tables.Count; i++)
                {
                    //能得到表内所有的信息
                    Debug.Log("表名: " + asDataSet.Tables[i].TableName);
                    Debug.Log("行数: " + asDataSet.Tables[i].Rows.Count);
                    Debug.Log("列数: " + asDataSet.Tables[i].Columns.Count);
                }
                fs.Close();
            }
        }
        
        [MenuItem("GameTools/读取Excel表")]
        public static void ReadExcel()
        {
            using (FileStream fs = File.Open(Application.dataPath + "/Binary/ArtRes/Excel/PlayerInfo.xlsx",
                       FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                var asDataSet = excelDataReader.AsDataSet();
                for (int i = 0; i < asDataSet.Tables.Count; i++)
                {
                    DataTable table = asDataSet.Tables[i];

                    DataRow rows;
                    for (int j = 0; j < table.Rows.Count; j++)
                    {
                        rows = table.Rows[j];
                        Debug.Log("第" + j + "行");
                        for (int k = 0; k < table.Columns.Count; k++)
                        {
                            Debug.Log(rows[k].ToString());
                        }
                    }
                }
                fs.Close();
            }
        }
    }
}