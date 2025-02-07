using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEnd : MonoBehaviour
{
    /// <summary>
    /// 클릭 시
    /// </summary>
    public void Click()
    {
        StageManager.Instance.ClickStageBtn(GameDataManager.Instance.m_stageDic
            [StageManager.Instance.m_viewStageCode[StageManager.Instance.m_viewStageCode.IndexOf(GameDataManager.Instance.m_nowStageCode) + 1]].m_floor);
    }
}
