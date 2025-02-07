using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePanelManager : MonoBehaviour
{
    /// <summary>
    /// 돌아가기 스프라이트
    /// </summary>
    public Sprite m_reternSprite = null;

    /// <summary>
    /// 패널 변경 버튼
    /// 0Lab 1Request 2Store 3Chest
    /// </summary>
    public GameObject[] m_changeBtn = null;

    /// <summary>
    /// 주요 패널들
    /// 0main 1Lab 2Request 3Store 4Chest
    /// </summary>
    public GameObject[] m_panel = null;

    /// <summary>
    /// 창으로 이루어진 패널들
    /// </summary>
    public GameObject[] m_windowPanel = null;

    /// <summary>
    /// 캐릭터 정보 패널
    /// </summary>
    public GameObject m_imfoPanel = null;

    /// <summary>
    /// 패널 변경 버튼 패널
    /// </summary>
    public GameObject m_moveButtonPanel = null;

    /// <summary>
    /// 게임 종료 패널
    /// </summary>
    public GameObject m_exitPanel = null;

    /// <summary>
    /// 돌아가기 표시 확인
    /// </summary>
    bool[] m_returnToggle = new bool[5];

    /// <summary>
    /// 자기 자신 글로벌화
    /// </summary>
    static ChangePanelManager g_changePanelManager = null;

    void Awake()
    {
        g_changePanelManager = this;

        for (int i = 0; i < m_changeBtn.Length; i++)
        {
            m_returnToggle[i] = false;
        }
        
        ChangePanel(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_exitPanel.SetActive(true);
        }
    }

    /// <summary>
    /// 앱 종료
    /// </summary>
    public void QuitApk()
    {
        Application.Quit();
    }

    /// <summary>
    /// 패널 변경 시 특수한 기능들 구현
    /// </summary>
    /// <param name="argIndex">보여지길 원하는 윈도우 인덱스</param>
    public void ManagePanel(int argIndex)
    {
        switch (argIndex)
        {
            case 0:
                ResetChangeBtn();

                ChangePanel(0);
                break;
            case 1:
            case 2:
            case 3:
                /*
                int _changeIndex = argIndex - 1;

                if (m_returnToggle[_changeIndex] == true)
                {
                    m_changeBtn[_changeIndex].GetComponent<Image>().sprite = m_tmpSprite[_changeIndex];
                    m_returnToggle[_changeIndex] = false;

                    for (int i = 0; i < m_changeBtn.Length; i++)
                    {
                        m_changeBtn[i].SetActive(true);
                    }

                    ChangePanel(0);
                    return;
                }
                
                ResetText();

                m_returnToggle[_changeIndex] = true;
                m_changeBtn[_changeIndex].GetComponent<Image>().sprite = m_reternSprite;
                */
                ChangePanel(argIndex);
                break;
            case 4:
                ItemManager.Instance.UpdateChest();
                ChangePanel(argIndex);
                break;
            case 5:
                if(m_returnToggle[0] == true)
                {
                    ResetChangeBtn();
                    ChangePanel(argIndex);
                    return;
                }

                m_returnToggle[0] = true;;

                for (int i = 0; i < m_changeBtn.Length; i++)
                {
                    m_changeBtn[i].SetActive(false);
                }
                m_changeBtn[0].SetActive(true);
                ChangePanel(5);
                break;
            case 6:
                ResetChangeBtn();

                ChangePanel(6);
                break;
            case 7:
                if (m_returnToggle[0] == true)
                {
                    ResetChangeBtn();
                    ChangePanel(argIndex);
                    return;
                }

                m_returnToggle[0] = true;

                for (int i = 0; i < m_changeBtn.Length; i++)
                {
                    m_changeBtn[i].SetActive(false);
                }
                m_changeBtn[0].SetActive(true);
                m_windowPanel[0].SetActive(false);
                ChangePanel(7);
                break;
            default:
                return;
        }
    }

    /// <summary>
    /// 패널 변경 버튼 리셋
    /// </summary>
    void ResetChangeBtn()
    {
        for (int i = 0; i < m_changeBtn.Length; i++)
        {
            m_changeBtn[i].SetActive(true);
            m_returnToggle[i] = false;
        }
    }

    /// <summary>
    /// 단순 패널 변경
    /// </summary>
    /// <param name="argIndex">변경할 패널의 인덱스</param>
    public void ChangePanel (int argIndex)
    {
        if(argIndex == 6)
        {
            m_imfoPanel.transform.localScale = new Vector2(0, 0);
            m_moveButtonPanel.transform.localScale = new Vector2(0, 0);
        }
        else
        {
            m_imfoPanel.transform.localScale = new Vector2(1, 1);
            m_moveButtonPanel.transform.localScale = new Vector2(1, 1);
        }

        for (int i = 0; i < m_panel.Length; i++)
        {
            m_panel[i].transform.localScale = new Vector2(0, 0);
        }

        m_panel[argIndex].transform.localScale = new Vector2(1, 1);
    }

    /// <summary>
    /// 인스턴스
    /// </summary>
    public static ChangePanelManager Instance
    {
        get
        {
            return g_changePanelManager;
        }
    }
}
