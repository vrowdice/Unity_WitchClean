using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    /// <summary>
    /// 스테이지 매니저
    /// </summary>
    StageManager m_stageManager = null;

    /// <summary>
    /// 스테이지 인덱스
    /// </summary>
    public int m_stageIndex = 0;

    private void Start()
    {
        m_stageManager = StageManager.Instance;
    }

    /// <summary>
    /// 클릭 시
    /// </summary>
    public void Click()
    {
        m_stageManager.ClickStageBtn(m_stageIndex);
    }
}
