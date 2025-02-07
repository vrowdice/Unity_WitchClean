using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoad
{
    public class LoadManager : MonoBehaviour
    {
        /// <summary>
        /// 로드
        /// </summary>
        /// <param name="argPath">경로</param>
        /// <returns>불러올 데이터</returns>
        public string Load(string argPath)
        {
            string _data = string.Empty;
            StreamReader _sr = new StreamReader(argPath, System.Text.Encoding.UTF8);
            _data = _sr.ReadToEnd();
            _sr.Close();

            return _data;
        }
    }
}

