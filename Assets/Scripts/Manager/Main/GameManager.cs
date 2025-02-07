using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 경고 패널
    /// </summary>
    public GameObject m_warningPanel = null;

    private void Start()
    {
        Setting();

        m_warningPanel.SetActive(true);
    }

    /// <summary>
    /// 초기 셋팅
    /// </summary>
    void Setting()
    {
        OnPanel();
        //PlayerValueManager.Instance.IsMaxHealth = 30;
        //PlayerValueManager.Instance.IsNowHealth = 30;
    }

    /// <summary>
    /// 패널 활성화
    /// </summary>
    public void OnPanel()
    {
        for (int i = ChangePanelManager.Instance.m_panel.Length - 1; i >= 0; i--)
        {
            ChangePanelManager.Instance.m_panel[i].SetActive(true);
        }
    }
}
