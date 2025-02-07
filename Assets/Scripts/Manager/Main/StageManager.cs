//스테이지 쪽 버튼이나 다른 메소드는 이곳에서 관리

using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    /// <summary>
    /// 미니게임 샘플 이미지
    /// </summary>
    public Sprite[] m_miniGameImage = null;

    /// <summary>
    /// 엘리베이터에 들어갈 추가될 버튼들
    /// </summary>
    public GameObject[] m_plusBtn = null;
    
    /// <summary>
    /// 현재 스테이지를 나타내는 택스트
    /// </summary>
    public Text m_nowStageText = null;

    /// <summary>
    /// 현재 세부 스테이지를 나타내는 택스트
    /// </summary>
    public Text m_nowDetailStageText = null;

    /// <summary>
    /// 이번 스테이지 게임 알려주는 이미지
    /// </summary>
    public Image m_stageGameImage = null;

    /// <summary>
    /// 이번 스테이지 게임 알려주는 텍스트
    /// </summary>
    public Text m_stageGameText = null;

    /// <summary>
    /// 층 패널
    /// </summary>
    public GameObject m_floorPanel = null;

    /// <summary>
    /// 스테이지로 버튼 (프리펍)
    /// </summary>
    public GameObject m_stageBtn = null;

    /// <summary>
    /// 세부 스테이지로 버튼 (프리펍)
    /// </summary>
    public GameObject m_detailStageBtn = null;

    /// <summary>
    /// 버튼이 추가될 메인의 스크롤 뷰 콘덴츠
    /// </summary>
    public GameObject m_stageContent = null;

    /// <summary>
    /// 버튼이 추가될 디테일 스테이지의 스크롤 뷰 콘덴츠
    /// </summary>
    public GameObject m_detailStageContent = null;

    /// <summary>
    /// 층 인덱스
    /// </summary>
    List<int> m_floor = new List<int>();

    /// <summary>
    /// 스테이지 인덱스
    /// </summary>
    List<int> m_stageCode = new List<int>();

    /// <summary>
    /// 유저에게 보여지는 스테이지 코드들
    /// </summary>
    public List<int> m_viewStageCode = new List<int>();

    /// <summary>
    /// 현재 스테이지 코드
    /// </summary>
    int m_nowStageCode = 0;

    /// <summary>
    /// 현재 층 수
    /// </summary>
    int m_nowFloor = 0;

    /// <summary>
    /// 시작 플래그
    /// </summary>
    bool m_startFlag = false;

    /// <summary>
    /// 현재 미니게임 정보
    /// </summary>
    int m_nowMiniGameImfo = 0;

    /// <summary>
    /// 자기 자신 글로벌화
    /// </summary>
    static public StageManager g_stageManager = null;
    
    // Start is called before the first frame update
    void Awake()
    {
        g_stageManager = this;
    }

    /// <summary>
    /// start
    /// </summary>
    private void Start()
    {
        foreach (KeyValuePair<int, bool> item in GameDataManager.Instance.m_stageClearDic)
        {
            if (m_floor.Contains(GameDataManager.Instance.m_stageDic[item.Key].m_floor) == false)
            {
                m_floor.Add(GameDataManager.Instance.m_stageDic[item.Key].m_floor);
            }
            m_stageCode.Add(item.Key);
        }
        m_floor.Sort();
        m_stageCode.Sort();

        UpdateStage();
    }

    /// <summary>
    /// 게임 시작
    /// </summary>
    public void ClickGameStart()
    {
        Glober.m_nowStageCode = m_nowStageCode;
        m_startFlag = true;
        GameDataManager.Instance.m_nowFloor = m_nowFloor;
        GameDataManager.Instance.m_nowStageCode = m_nowStageCode;
        GameDataManager.Instance.GameStart();
    }

    /// <summary>
    /// 스테이지 업데이트
    /// </summary>
    public void UpdateStage()
    {
        SetViewStageCode();

        List<int> _floor = new List<int>();
        
        for (int i = 0; i < m_stageContent.transform.childCount; i++)
        {
            Destroy(m_stageContent.transform.GetChild(i).gameObject);
        }

        foreach(int item in m_viewStageCode)
        {
            int _floorint = GameDataManager.Instance.m_stageDic[item].m_floor;
            if (!_floor.Contains(_floorint) && m_floor.Contains(_floorint))
            {
                _floor.Add(_floorint);
            }
        }

        GameObject _addBtn0 = Instantiate(m_plusBtn[0]);
        _addBtn0.transform.SetParent(m_stageContent.transform);
        _addBtn0.transform.localScale = new Vector3(1, 1, 1);
        _addBtn0.transform.SetSiblingIndex(0);
        _addBtn0.GetComponent<Button>().onClick.AddListener(delegate { ChangePanelManager.Instance.ManagePanel(7); });

        GameObject _addBtn1 = Instantiate(m_plusBtn[1]);
        _addBtn1.transform.SetParent(m_stageContent.transform);
        _addBtn1.transform.localScale = new Vector3(1, 1, 1);
        _addBtn1.transform.SetSiblingIndex(0);
        _addBtn1.GetComponent<OnOffTargetButton>().m_target = m_floorPanel;


        for (int i = 0; i < _floor.Count; i++)
        {
            GameObject _btn = Instantiate(m_stageBtn);
            _btn.transform.SetParent(m_stageContent.transform);
            _btn.transform.localScale = new Vector3(1, 1, 1);
            _btn.transform.SetSiblingIndex(0);

            _btn.GetComponent<StageButton>().m_stageIndex = _floor[i];
            _btn.transform.Find("Text").gameObject.GetComponent<Text>().text = _floor[i].ToString();
        }
    }

    /// <summary>
    /// 세부 스테이지 업데이트
    /// </summary>
    public void UpdateDetailStage()
    {
        SetViewStageCode();
        List<int> _stage = new List<int>();
        m_startFlag = false;

        for (int i = 0; i < m_detailStageContent.transform.childCount; i++)
        {
            Destroy(m_detailStageContent.transform.GetChild(i).gameObject);
        }

        foreach (int item in m_viewStageCode)
        {
            if(GameDataManager.Instance.m_stageDic[item].m_floor == m_nowFloor)
            {
                _stage.Add(item);
            }
        }

        for (int i = 0; i < _stage.Count; i++)
        {
            GameObject _btn = Instantiate(m_detailStageBtn);
            Text _btnText = _btn.transform.Find("Image").GetChild(0).gameObject.GetComponent<Text>();
            _btn.transform.SetParent(m_detailStageContent.transform);
            _btn.transform.localScale = new Vector3(1, 1, 1);

            _btn.GetComponent<DetailStageButton>().m_detailStageIndex = _stage[i];
            _btnText.text = string.Format("{0:} - {1:}", m_nowFloor, _stage[i].ToString().Substring(m_nowFloor.ToString().Length));
        }
    }

    /// <summary>
    /// 보이는 스테이지 정렬
    /// </summary>
    void SetViewStageCode()
    {
        m_floor.Clear();
        m_stageCode.Clear();
        m_viewStageCode.Clear();

        foreach (KeyValuePair<int, StageData> item in GameDataManager.Instance.m_stageDic)
        {
            if (!m_floor.Contains(item.Value.m_floor))
            {
                m_floor.Add(item.Value.m_floor);
            }

            m_stageCode.Add(item.Key);

            if (GameDataManager.Instance.m_stageClearDic[item.Key] == true)
            {
                m_viewStageCode.Add(item.Key);
            }
        }

        m_viewStageCode.Add(m_stageCode[m_viewStageCode.Count]);
    }

    /// <summary>
    /// 스테이지 버튼 클릭 시
    /// </summary>
    public void ClickStageBtn(int argIndex)
    {
        m_floorPanel.SetActive(false);
        m_nowFloor = argIndex;
        UpdateDetailStage();

        ChangePanelManager.Instance.ManagePanel(5);
        m_nowStageText.text = string.Format(m_nowFloor.ToString() + " 층");
    }

    /// <summary>
    /// 세부 스테이지 버튼 클릭 시
    /// </summary>
    /// <param name="argCode"></param>
    public void ClickDetailStageBtn(int argCode)
    {
        m_nowStageCode = argCode;
        ChangePanelManager.Instance.ManagePanel(6);

        string _stage = string.Empty;
        _stage = argCode.ToString();
        _stage = _stage.Substring(m_nowFloor.ToString().Length);
        m_nowDetailStageText.text = string.Format("{0:} - {1:}", m_nowFloor, _stage);

        m_nowMiniGameImfo = 0;
        InvokeRepeating("NextMiniImfo", 0.0f, 1.5f);
    }

    /// <summary>
    /// 미니게임에 대한 이미지와 텍스트 변경
    /// </summary>
    public void NextMiniImfo()
    {
        if (m_startFlag)
        {
            CancelInvoke();
        }
        if (m_nowMiniGameImfo > GameDataManager.Instance.m_stageDic[m_nowStageCode].m_miniType.Count - 1)
        {
            m_nowMiniGameImfo = 0;
        }

        MiniGameType.GAME_TYPE _type = new MiniGameType.GAME_TYPE();
        _type = GameDataManager.Instance.m_stageDic[m_nowStageCode].m_miniType[m_nowMiniGameImfo];
        
        switch (_type)
        {
            case MiniGameType.GAME_TYPE.spider_repelle:
                m_stageGameImage.sprite = m_miniGameImage[0];
                m_stageGameText.text = string.Format("{0}번째\n거미퇴치 게임", m_nowMiniGameImfo + 1);
                break;
            case MiniGameType.GAME_TYPE.potion_arrangement:
                m_stageGameImage.sprite = m_miniGameImage[1];
                m_stageGameText.text = string.Format("{0}번째\n포션정리 게임", m_nowMiniGameImfo + 1);
                break;
            case MiniGameType.GAME_TYPE.material_classification:
                m_stageGameImage.sprite = m_miniGameImage[2];
                m_stageGameText.text = string.Format("{0}번째\n재료분류 게임", m_nowMiniGameImfo + 1);
                break;
            case MiniGameType.GAME_TYPE.bookcase_tidy:
                m_stageGameImage.sprite = m_miniGameImage[3];
                m_stageGameText.text = string.Format("{0}번째\n책장정리 게임", m_nowMiniGameImfo + 1);
                break;
            default:
                break;
        }

        m_nowMiniGameImfo++;
    }

    /// <summary>
    /// 스테이지로 복귀
    /// </summary>
    public void BackToStage()
    {
        ChangePanelManager.Instance.ManagePanel(5);
        CancelInvoke();
    }

    /// <summary>
    /// 인스턴스
    /// </summary>
    public static StageManager Instance
    {
        get
        {
            return g_stageManager;
        }
    }
}
