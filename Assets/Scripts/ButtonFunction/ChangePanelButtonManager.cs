using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePanelButtonManager : MonoBehaviour
{
    /// <summary>
    /// 패널 변경 매니저
    /// </summary>
    public ChangePanelManager m_changePanelManager = null;

    /// <summary>
    /// 클릭 시
    /// </summary>
    /// <param name="argPanelNum">패널 순서 번호</param>
    public void Click(int argPanelNum)
    {
        m_changePanelManager.ManagePanel(argPanelNum);
    }
}
