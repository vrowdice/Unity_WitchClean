using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningPanelManager : MonoBehaviour
{
    /// <summary>
    /// 경고 패널 택스트
    /// </summary>
    public Text m_warningPanelText = null;

    /// <summary>
    /// 경고 패널
    /// </summary>
    static WarningPanelManager g_warningPanel = null;

    private void Awake()
    {
        g_warningPanel = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 경고패널 활성화
    /// </summary>
    /// <param name="argText">경고할 택스트</param>
    public void Warning(string argText)
    {
        gameObject.SetActive(true);
        m_warningPanelText.text = argText;
    }

    public static WarningPanelManager Instance
    {
        get
        {
            return g_warningPanel;
        }
    }
}
