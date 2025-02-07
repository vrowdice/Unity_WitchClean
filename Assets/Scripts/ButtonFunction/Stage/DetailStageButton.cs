using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailStageButton : MonoBehaviour
{
    /// <summary>
    /// 스테이지 인덱스
    /// </summary>
    public int m_detailStageIndex = 0;

    /// <summary>
    /// 클릭 시
    /// </summary>
    public void Click()
    {
        StageManager.g_stageManager.ClickDetailStageBtn(m_detailStageIndex);
    }
}
