using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValueManager : MonoBehaviour
{
    /// <summary>
    /// 의뢰 매니저
    /// </summary>
    public RequestManager m_reqestManager = null;

    /// <summary>
    /// 받아온 시간
    /// </summary>
    public string m_time = string.Empty;

    /// <summary>
    /// 게임 접속 시간
    /// </summary>
    public long m_totalTime = 0;

    /// <summary>
    /// 돈
    /// </summary>
    long m_money = 0;

    /// <summary>
    /// 최대 체력
    /// </summary>
    int m_maxHealth = 0;

    /// <summary>
    /// 현재 체력
    /// </summary>
    int m_nowHealth = 0;

    /// <summary>
    /// 최대 경험치
    /// </summary>
    long m_maxExp = 0;

    /// <summary>
    /// 현재 경험치
    /// </summary>
    long m_nowExp = 0;

    /// <summary>
    /// 레벨
    /// </summary>
    int m_level = 0;

    /// <summary>
    /// 돈을 보여줄 택스트
    /// </summary>
    Text m_goldText = null;

    /// <summary>
    /// 체력을 보여줄 택스트
    /// </summary>
    Text m_healthText = null;

    /// <summary>
    /// 레벨 텍스트
    /// </summary>
    Text m_levelText = null;

    /// <summary>
    /// 체력 슬라이더
    /// </summary>
    Slider m_healthSlider = null;

    /// <summary>
    /// 경험치 슬라이더
    /// </summary>
    Slider m_ExpSlider = null;

    /// <summary>
    /// 미니게임 끝났을 때 패널
    /// </summary>
    public GameObject m_endPanel = null;

    /// <summary>
    /// 경험치 텍스트
    /// </summary>
    Text m_endExpText = null;

    /// <summary>
    /// 골드 텍스트
    /// </summary>
    Text m_endGoldText = null;

    /// <summary>
    /// 경험치 슬라이더
    /// </summary>
    Slider m_endExpSlider = null;

    /// <summary>
    /// 첫 선물
    /// </summary>
    public bool m_first = false;

    /// <summary>
    /// 재료 콘텐츠
    /// </summary>
    public GameObject m_metContent = null;

    /// <summary>
    /// 아이콘 프리펍
    /// </summary>
    public GameObject m_icon = null;

    /// <summary>
    /// 자기 자신 글로벌화
    /// </summary>
    static PlayerValueManager g_playerValueManager = null;

    /// <summary>
    /// awake
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            g_playerValueManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        m_goldText = transform.Find("GoldText").gameObject.GetComponent<Text>();
        m_healthText = transform.Find("HealthText").gameObject.GetComponent<Text>();
        m_levelText = transform.Find("LevelText").gameObject.GetComponent<Text>();
        m_healthSlider = transform.Find("HealthSlider").gameObject.GetComponent<Slider>();
        m_ExpSlider = transform.Find("ExpSlider").gameObject.GetComponent<Slider>();

        m_endExpText = m_endPanel.transform.GetChild(0).Find("ExpText").gameObject.GetComponent<Text>();
        m_endGoldText = m_endPanel.transform.GetChild(0).Find("GoldText").gameObject.GetComponent<Text>();
        m_endExpSlider = m_endPanel.transform.GetChild(0).Find("ExpSlider").gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        TimeSpan _span = new TimeSpan();
        DateTime _now = DateTime.Now;
        DateTime _userIndate = Convert.ToDateTime(m_time);
        
        _span = _now - _userIndate;
        IsNowHealth += (int)_span.TotalSeconds / 180;

        for (int i = 0; i < GameDataManager.Instance.m_requestTime.Count; i++)
        {
            TimeSpan _span1 = new TimeSpan();
            DateTime _now1 = DateTime.Now;
            DateTime _userIndate1 = Convert.ToDateTime(m_time);

            _span1 = _now1 - _userIndate1;
            GameDataManager.Instance.m_requestTime[i] -= (int)_span.TotalSeconds;
        }

        GameDataManager.Instance.Save();
        
        m_reqestManager.InvRep();
        InvokeRepeating("AddHearth", 1.0f, 1.0f);
    }

    /// <summary>
    /// 체력 추가
    /// </summary>
    public void AddHearth()
    {
        m_totalTime += 1;
        if(m_totalTime % 180 == 0)
        {
            IsNowHealth += 3;
            GameDataManager.Instance.Save();
        }
                                     
        if (IsLevel <= 1)
        {
            if (!m_first)
            {
                GameDataManager.Instance.SaveReset();

                Debug.Log(0);
                m_first = true;
                IsMaxHealth = 25;
                IsNowHealth = 25;
                IsMoney = 5000;
                GameDataManager.Instance.Save();
            }
        }
    }

    /// <summary>
    /// 게임 끝났을 때 셋팅
    /// </summary>
    public void GameEndSetting()
    {
        GameDataManager.Instance.Load();

        int _level = IsLevel;

        IsMoney += GameDataManager.Instance.m_toAddMoney;
        IsNowExp += GameDataManager.Instance.m_toAddExp;

        if(_level < IsLevel)
        {
            IsNowHealth = IsMaxHealth;
        }

        m_endExpText.text = "+" + GameDataManager.Instance.m_toAddExp;
        m_endGoldText.text = "+" + GameDataManager.Instance.m_toAddMoney;
        m_endExpSlider.value = m_ExpSlider.value;

        for(int i = 0; i < m_metContent.transform.childCount; i++)
        {
            Destroy(m_metContent.transform.GetChild(i));
        }
 
        if(GameDataManager.Instance.m_toAddMat.Count != 0 || GameDataManager.Instance.m_toAddMat != null)
        {
            GameDataManager.Instance.m_toAddMat.Sort();
            GameObject _icon = null;
            int _before = -1;
            int _count = 0;
            foreach (int item in GameDataManager.Instance.m_toAddMat)
            {
                GameDataManager.Instance.m_materialAmountDic[item] += 1;

                Debug.Log("0: " + _before);
                Debug.Log("1: " + item);
                if (_before != item)
                {
                    _icon = Instantiate(m_icon);
                    _icon.transform.SetParent(m_metContent.transform);
                    _icon.transform.localScale = new Vector3(1, 1, 1);
                    _icon.transform.GetChild(0).GetComponent<Image>().sprite = GameDataManager.Instance.m_materialDic[item].m_materialSprite;
                    _icon.transform.GetChild(1).GetComponent<Text>().text = "1";
                    _count = 1;
                }
                else
                {
                    _count++;
                    _icon.transform.GetChild(1).GetComponent<Text>().text = _count.ToString();
                }
                _before = item;
            }
        }

        GameDataManager.Instance.ResetAddValue();

        GameDataManager.Instance.Save();
        m_endPanel.SetActive(true);
    }

    /// <summary>
    /// 돈 변경
    /// </summary>
    public long IsMoney
    {
        get
        {
            return m_money;
        }
        set
        {
            m_money = value;
            if (m_money < 0)
            {
                m_money = 0;
                return;
            }

            m_goldText.text = string.Format("금화 : {0:#,###}", m_money);

            if (m_money == 0)
            {
                m_goldText.text = string.Format("금화 : 0");
            }
        }
    }

    /// <summary>
    /// 최대 체력 변경
    /// </summary>
    public int IsMaxHealth
    {
        get
        {
            return m_maxHealth;
        }
        set
        {
            m_maxHealth = value;
            if (m_maxHealth < 25)
            {
                m_maxHealth = 25;
                return;
            }
            m_healthText.text = string.Format("체력 : " + m_nowHealth + " / " + m_maxHealth);
            m_healthSlider.maxValue = m_maxHealth;
        }
    }

    /// <summary>
    /// 현재 체력 변경
    /// </summary>
    public int IsNowHealth
    {
        get
        {
            return m_nowHealth;
        }
        set
        {
            m_nowHealth = value;
            if (m_nowHealth < 0)
            {
                m_nowHealth = 0;
            }
            if (m_nowHealth >= m_maxHealth)
            {
                m_nowHealth = m_maxHealth;
            }
            if(m_healthText != null)
            {
                m_healthText.text = string.Format("체력 : " + m_nowHealth + " / " + m_maxHealth);
            }
            m_healthSlider.value = m_nowHealth;
        }
    }

    /// <summary>
    /// 현재 경험치 변경
    /// </summary>
    public long IsNowExp
    {
        get
        {
            return m_nowExp;
        }
        set
        {
            bool _flag = false;
            long _exp = value;

            while (_flag == false)
            {
                IsMaxExp = 200 + 300 * (IsLevel - 1);
                if (IsMaxExp <= _exp)
                {
                    _exp -= 200 + 300 * (IsLevel - 1);
                    IsLevel++;
                }
                else
                {
                    m_nowExp = _exp;
                    _flag = true;
                }
            }
            if (m_nowExp <= 0)
            {
                m_ExpSlider.value = 0;
                m_nowExp = 0;
                return;
            }
            m_ExpSlider.value = (float)m_nowExp / IsMaxExp;
        }
    }

    /// <summary>
    /// 최대 경험치 변경
    /// </summary>
    public long IsMaxExp
    {
        get
        {
            return m_maxExp;
        }
        set
        {
            m_maxExp = value;
            if (m_maxExp < 200)
            {
                m_maxExp = 200;
                return;
            }
        }
    }

    /// <summary>
    /// 레벨 변경
    /// </summary>
    public int IsLevel
    {
        get
        {
            return m_level;
        }
        set
        {
            int _level = m_level;
            m_level = value;
            m_levelText.text = "LV." + value.ToString();
            IsMaxHealth = 25 + 15 * (m_level - 1);
            if (m_level < 1)
            {
                m_level = 1;
                return;
            }
        }
    }

    /// <summary>
    /// 플레이어 수치 초기화(세이브 초기화x)
    /// </summary>
    public void ResetValue()
    {
        IsMoney = 5000;
        IsMaxHealth = 25;
        IsNowHealth = 25;
        IsMaxExp = 200;
        IsNowExp = 0;
        IsLevel = 1;
        m_time = DateTime.Now.ToString();
    }

    /// <summary>
    /// 수치 매니저 인스턴스
    /// </summary>
    public static PlayerValueManager Instance
    {
        get
        {
            return g_playerValueManager;
        }
    }
}

