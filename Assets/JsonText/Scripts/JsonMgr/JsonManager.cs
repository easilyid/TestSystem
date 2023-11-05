using System.IO;
using LitJson;
using UnityEngine;

namespace JsonText.Scripts.JsonMgr
{
    /// <summary>
    /// 序列化和反序列化Json时  使用的是哪种方案
    /// </summary>
    public enum JsonType
    {
        JsonUtlity,
        LitJson,
    }

    /// <summary>
    /// Json数据管理类
    /// 用于 序列化和反序列化
    /// </summary>
    public class JsonManager
    {
        private static JsonManager _instance = new JsonManager();
        public static JsonManager Instance => _instance;

        private JsonManager()
        {
        }

        /// <summary>
        /// 存储数据 序列化
        /// </summary>
        public void SaveData(object data, string fileName, JsonType type = JsonType.LitJson)
        {
            //确定存储路径
            string path = Application.persistentDataPath + "/" + fileName + ".json";
            //序列化 得到json 字符串
            string jsonStr = "";
            switch (type)
            {
                case JsonType.JsonUtlity:
                    jsonStr = JsonUtility.ToJson(data);
                    break;
                case JsonType.LitJson:
                    jsonStr = JsonMapper.ToJson(data);
                    break;
            }

            //把序列化的Json存储到路径
            File.WriteAllText(path, jsonStr);
        }

        public T LoadData<T>(string fileName, JsonType type = JsonType.LitJson) where T: new()
        {
            //确定从哪个路径读取

            string path = Application.streamingAssetsPath + "/" + fileName + ".json";
            if (!File.Exists(path))
                path = Application.persistentDataPath + "/" + fileName + ".json";
            
            if (!File.Exists(path))
                return new T();

            //进行反序列化
            string jsonStr = File.ReadAllText(path);
            T data =default(T);
            switch (type)
            {
                case JsonType.JsonUtlity:
                    data = JsonUtility.FromJson<T>(jsonStr);
                    break;
                case JsonType.LitJson:
                    data = JsonMapper.ToObject<T>(jsonStr);
                    break;
            }

            //把对象返回出去
            return data;
        }
    }
}