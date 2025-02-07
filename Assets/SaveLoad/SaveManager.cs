using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace SaveLoad
{
    public class SaveManager
    {
        /// <summary>
        /// 세이브
        /// </summary>
        /// <param name="argPath">저장할 위치</param>
        /// <param name="argData">저장할 데이터</param>
        public void Save(string argPath, string argData)
        {
            StreamWriter _sw = new StreamWriter(argPath, false, System.Text.Encoding.UTF8);
            _sw.WriteLine(argData);
            _sw.Close();
        }
    }
}

