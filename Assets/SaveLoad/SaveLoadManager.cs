using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoad
{
    public class SaveLoadManager : MonoBehaviour
    {
        /// <summary>
        /// 저장할 경로
        /// </summary>
        [TextArea]
        public string m_path = string.Empty;

        /// <summary>
        /// 저장 매니저
        /// </summary>
        SaveManager m_saveManager = null;

        /// <summary>
        /// 로드 매니저
        /// </summary>
        LoadManager m_loadManager = null;

        /// <summary>
        /// 저장 로드 매니저
        /// </summary>
        static public SaveLoadManager g_saveLoadManager = null;

        /// <summary>
        /// awake
        /// </summary>
        private void Awake()
        {
            if(Instance == null)
            {
                g_saveLoadManager = this;
                m_saveManager = new SaveManager();
                m_loadManager = gameObject.AddComponent<LoadManager>();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// 데이터 로드
        /// </summary>
        /// <param name="argPath">데이터 패스</param>
        /// <returns>데이터 문자열</returns>
        public string Load(string argPath)
        {
            string _path = Application.persistentDataPath + "/" + argPath + ".json";
            return m_loadManager.Load(_path);
        }

        /// <summary>
        /// 데이터 세이브
        /// </summary>
        /// <param name="argPath">데이터 패스</param>
        /// <param name="argData">데이터 문자열</param>
        public void Save(string argPath, string argData)
        {
            string _path = Application.persistentDataPath + "/" + argPath + ".json";
            m_saveManager.Save(_path, argData);
        }



        /// <summary>
        /// 저장 로드 매니저 불러오기
        /// </summary>
        public static SaveLoadManager Instance
        {
            get
            {
                return g_saveLoadManager;
            }
        }
    }
}
