using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    /// <summary>
    /// 리셋 토글
    /// </summary>
    public bool m_reset = false;

    /// <summary>
    /// 제작품 데이터
    /// </summary>
    public ProductData[] m_productData = null;

    /// <summary>
    /// 재료 데이터
    /// </summary>
    public MaterialData[] m_materialData = null;

    /// <summary>
    /// 아이템 데이터
    /// </summary>
    public ItemData[] m_itemData = null;

    /// <summary>
    /// 콜렉션 데이터
    /// </summary>
    public CollectionData[] m_collectionData = null;

    /// <summary>
    /// 의뢰 데이터
    /// </summary>
    public RequestData[] m_requestData = null;

    /// <summary>
    /// 사람들 데이터
    /// </summary>
    public PeopleData[] m_peopleData = null;

    /// <summary>
    /// 스테이지 데이터
    /// </summary>
    public StageData[] m_stageData = null;

    /// <summary>
    /// 아이템 데이터 딕셔너리
    /// </summary>
    public Dictionary<int, ItemData> m_itemDic = new Dictionary<int, ItemData>();

    /// <summary>
    /// 아이템 갯수 딕셔너리
    /// </summary>
    public Dictionary<int, int> m_itemAmountDic = new Dictionary<int, int>();

    /// <summary>
    /// 제작품 딕셔너리
    /// </summary>
    public Dictionary<int, ProductData> m_productDic = new Dictionary<int, ProductData>();

    /// <summary>
    /// 제작품 갯수 딕셔너리
    /// </summary>
    public Dictionary<int, int> m_productAmountDic = new Dictionary<int, int>();

    /// <summary>
    /// 재료 딕셔너리
    /// </summary>
    public Dictionary<int, MaterialData> m_materialDic = new Dictionary<int, MaterialData>();

    /// <summary>
    /// 재료 갯수 딕셔너리
    /// </summary>
    public Dictionary<int, int> m_materialAmountDic = new Dictionary<int, int>();

    /// <summary>
    /// 콜랙션 데이터 딕셔너리
    /// </summary>
    public Dictionary<int, CollectionData> m_collectionDataDic = new Dictionary<int, CollectionData>();

    /// <summary>
    /// 콜랙션 존재 여부를 저장할 딕셔너리
    /// </summary>
    public Dictionary<int, int> m_collectionAmountDic = new Dictionary<int, int>();

    /// <summary>
    /// 의뢰 데이터 딕셔너리
    /// </summary>
    public Dictionary<int, RequestData> m_requestDic = new Dictionary<int, RequestData>();

    /// <summary>
    /// 사람들 데이터 딕셔너리
    /// </summary>
    public Dictionary<int, PeopleData> m_peopleDic = new Dictionary<int, PeopleData>();

    /// <summary>
    /// 스테이지 데이터 딕셔너리
    /// </summary>
    public Dictionary<int, StageData> m_stageDic = new Dictionary<int, StageData>();

    /// <summary>
    /// 스테이지 클리어 딕셔너리
    /// </summary>
    public Dictionary<int, bool> m_stageClearDic = new Dictionary<int, bool>();

    /// <summary>
    /// 의뢰 패널들의 아이템 코드
    /// </summary>
    public List<int> m_requestCode = new List<int>();

    /// <summary>
    /// 의뢰의 생성된 시간
    /// </summary>
    public List<int> m_requestTime = new List<int>();

    /// <summary>
    /// 클리어 플래그
    /// </summary>
    public bool m_clearFlag = false;

    /// <summary>
    /// 현재 층
    /// </summary>
    public int m_nowFloor = 0;

    /// <summary>
    /// 현재 스테이지 코드
    /// </summary>
    public int m_nowStageCode = 0;
    
    /// <summary>
    /// 스테이지 진행상황
    /// </summary>
    public int m_nowStageIndex = 0;

    /// <summary>
    /// 게임에서 추가될 돈
    /// </summary>
    public long m_toAddMoney = 0;

    /// <summary>
    /// 게임에서 추가될 경험치
    /// </summary>
    public long m_toAddExp = 0;

    /// <summary>
    /// 게임에서 추가될 재료
    /// </summary>
    public List<int> m_toAddMat = new List<int>();

    /// <summary>
    /// 클리어 시 나오는 텍스트
    /// </summary>
    public GameObject[] m_clearImg = null;

    /// <summary>
    /// 게임 시작 못함 오브젝트
    /// </summary>
    public GameObject m_cantStart = null;

    /// <summary>
    /// 자기 자신 글로벌화
    /// </summary>
    static GameDataManager g_gameDataManager = null;

    private void Awake()
    {
        if(Instance == null)
        {
            g_gameDataManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        for(int i = 0; i < m_itemData.Length; i++)
        {
            m_itemDic.Add(m_itemData[i].m_itemCode, m_itemData[i]);
            m_itemAmountDic.Add(m_itemData[i].m_itemCode, 0);
        }

        for (int i = 0; i < m_collectionData.Length; i++)
        {
            m_collectionDataDic.Add(m_collectionData[i].m_collectionCode, m_collectionData[i]);
            m_collectionAmountDic.Add(m_collectionData[i].m_collectionCode, 0);
        }

        for (int i = 0; i < m_productData.Length; i++)
        {
            m_productDic.Add(m_productData[i].m_productCode, m_productData[i]);
            m_productAmountDic.Add(m_productData[i].m_productCode, 0);
        }
        for (int i = 0; i < m_materialData.Length; i++)
        {
            m_materialDic.Add(m_materialData[i].m_materialCode, m_materialData[i]);
            m_materialAmountDic.Add(m_materialData[i].m_materialCode, 0);
        }
        for(int i = 0; i < m_requestData.Length; i++)
        {
            m_requestDic.Add(m_requestData[i].m_requestCode, m_requestData[i]);
        }
        for (int i = 0; i < m_peopleData.Length; i++)
        {
            m_peopleDic.Add(m_peopleData[i].m_personCode, m_peopleData[i]);
        }
        for (int i = 0; i < m_stageData.Length; i++)
        {
            m_stageDic.Add(m_stageData[i].m_stageCode, m_stageData[i]);
            m_stageClearDic.Add(m_stageData[i].m_stageCode, false);
        }

        m_productData = new ProductData[0];
        m_materialData = new MaterialData[0];
        m_itemData = new ItemData[0];
        m_collectionData = new CollectionData[0];
        m_requestData = new RequestData[0];
        m_peopleData = new PeopleData[0];
        m_stageData = new StageData[0];
        m_productData = null;
        m_materialData = null;
        m_itemData = null;
        m_collectionData = null;
        m_requestData = null;
        m_peopleData = null;
        m_stageData = null;

    }

    private void Start()
    {
        if(m_reset == true)
        {
            SaveReset();
        }

        if (PlayerValueManager.Instance.IsLevel <= 0 || PlayerValueManager.Instance.IsMaxHealth <= 0)
        {
            SaveReset();
        }
    }

    /// <summary>
    /// 게임 시작
    /// </summary>
    public void GameStart()
    {
        m_clearFlag = false;
        m_nowStageIndex = 0;
        
        StageData _stage = m_stageDic[m_nowStageCode];
        if(m_nowStageCode >= 11 && m_nowStageCode <= 13 || m_nowStageCode == 21)
        {
            if(PlayerValueManager.Instance.IsNowHealth < 5)
            {
                if (ScenManager.Instance.CheckMain())
                {
                    WarningPanelManager.Instance.Warning("게임을 시작할 수 없습니다");
                    return;
                }
                else
                {
                    GameObject _obj = Instantiate(m_cantStart);
                    _obj.transform.SetParent(ScenManager.Instance.m_canvas.transform);
                    _obj.transform.localPosition = new Vector3(0, 0, 0);
                    _obj.transform.localScale = new Vector3(1, 1, 1);
                }

            }
            PlayerValueManager.Instance.IsNowHealth -= 5;
        }
        else
        {
            if (PlayerValueManager.Instance.IsNowHealth < 10)
            {
                if (ScenManager.Instance.CheckMain())
                {
                    WarningPanelManager.Instance.Warning("게임을 시작할 수 없습니다");
                    return;
                }
                else
                {
                    GameObject _obj = Instantiate(m_cantStart);
                    _obj.transform.SetParent(ScenManager.Instance.m_canvas.transform);
                    _obj.transform.localPosition = new Vector3(0, 0, 0);
                    _obj.transform.localScale = new Vector3(1, 1, 1);
                }
            }
            PlayerValueManager.Instance.IsNowHealth -= 10;
        }
        Glober.curTime = Glober.maxTime;
        ManageGame(_stage.m_miniLevel[m_nowStageIndex], _stage.m_miniType[m_nowStageIndex]);
    }

    /// <summary>
    /// 게임 클리어시 호출
    /// </summary>
    public void GameClear()
    {
        AddValues();
        m_clearFlag = true;
        
        ++m_nowStageIndex;
        StageData _stage = m_stageDic[m_nowStageCode];
        if (_stage.m_miniLevel.Count <= m_nowStageIndex)
        {
            GameObject _obj = Instantiate(m_clearImg[1]);
            _obj.transform.SetParent(ScenManager.Instance.m_canvas.transform);
            _obj.transform.localPosition = new Vector3(0, 0, 0);
            _obj.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            GameObject _obj = Instantiate(m_clearImg[0]);
            _obj.transform.SetParent(ScenManager.Instance.m_canvas.transform);
            _obj.transform.localPosition = new Vector3(0, 0, 0);
            _obj.transform.localScale = new Vector3(1, 1, 1);
        }
        Invoke("NextGame", 3.0f);
    }

    /// <summary>
    /// 다음 게임 씬이나 메인으로
    /// </summary>
    void NextGame()
    {
        StageData _stage = m_stageDic[m_nowStageCode];
        if (_stage.m_miniLevel.Count <= m_nowStageIndex)
        {
            ScenManager.Instance.LoadScene("Main");

            m_stageClearDic[m_nowStageCode] = true;
            m_clearFlag = false;
            Save();
            return;
        }
        ManageGame(_stage.m_miniLevel[m_nowStageIndex], _stage.m_miniType[m_nowStageIndex]);
        m_clearFlag = false;
    }

    /// <summary>
    /// 게임 진입
    /// </summary>
    /// <param name="argLev">게임 레벨</param>
    /// <param name="argType">게임 타입</param>
    public void ManageGame(int argLev, MiniGameType.GAME_TYPE argType)
    {
        ScenManager _scenManager = ScenManager.Instance;

        switch (argType)
        {
            case MiniGameType.GAME_TYPE.spider_repelle:
                switch (argLev)
                {
                    case 0:
                    case 1:
                        Glober.stage = argLev;
                        Glober.spiderLevel = 1;
                        break;
                    case 2:
                        Glober.stage = argLev;
                        Glober.spiderLevel = 2;
                        break;
                    default:
                        return;
                }
                _scenManager.LoadScene("SpiderGame");
                break;
            case MiniGameType.GAME_TYPE.potion_arrangement:
                switch (argLev)
                {
                    case 0:
                    case 1:
                        Glober.stage = argLev;
                        Glober.maxPotionNum = 3;
                        Glober.PotionAnswerNum = 10;
                        break;
                    case 2:
                        Glober.stage = argLev;
                        Glober.maxPotionNum = 4;
                        Glober.PotionAnswerNum = 20;
                        break;
                    default:
                        return;
                }
                _scenManager.LoadScene("PotionGame");
                break;
            case MiniGameType.GAME_TYPE.material_classification:
                switch (argLev)
                {
                    case 0:
                    case 1:
                        Glober.stage = argLev;
                        Glober.maxStuffNum = 20;
                        break;
                    case 2:
                        Glober.stage = argLev;
                        Glober.maxStuffNum = 40;
                        break;
                    default:
                        return;
                }
                _scenManager.LoadScene("StuffGame");
                break;
            case MiniGameType.GAME_TYPE.bookcase_tidy:
                switch (argLev)
                {
                    case 0:
                    case 1:
                        Glober.stage = argLev;
                        Glober.maxShelfNum = 40;
                        break;
                    case 2:
                        Glober.stage = argLev;
                        Glober.maxShelfNum = 60;
                        break;
                    default:
                        return;
                }
                _scenManager.LoadScene("ShelfGame");
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 데이터 리셋
    /// </summary>
    public void SaveReset()
    {
        SaveData _saveData = new SaveData();

        _saveData.m_first = false;

        PlayerValueManager.Instance.ResetValue();

        _saveData.m_money = 5000;
        _saveData.m_maxHealth = 25;
        _saveData.m_nowHealth = 25;
        _saveData.m_maxExp = 200;
        _saveData.m_nowExp = 0;
        _saveData.m_level = 1;
        _saveData.m_time = DateTime.Now.ToString();
        _saveData.m_totalTime = 0;

        int _count = 0;
        foreach (KeyValuePair<int, int> item in m_itemAmountDic)
        {
            _saveData.m_itemCode.Add(item.Key);
            _saveData.m_itemAmount.Add(0);
            ++_count;
        }
        _count = 0;

        foreach (KeyValuePair<int, int> item in m_productAmountDic)
        {
            _saveData.m_productCode.Add(item.Key);
            _saveData.m_productAmount.Add(0);
            ++_count;
        }
        _count = 0;

        foreach (KeyValuePair<int, int> item in m_materialAmountDic)
        {
            _saveData.m_materialCode.Add(item.Key);
            _saveData.m_materialAmount.Add(0);
            ++_count;
        }
        _count = 0;

        foreach (KeyValuePair<int, int> item in m_collectionAmountDic)
        {
            _saveData.m_collectionCode.Add(item.Key);
            _saveData.m_collectionAmount.Add(0);
            ++_count;
        }
        _count = 0;
        
        foreach (KeyValuePair<int, bool> item in m_stageClearDic)
        {
            _saveData.m_stageCode.Add(item.Key);
            _saveData.m_stageClear.Add(false);
            ++_count;
        }
        _count = 0;

        _saveData.m_requestCode = new List<int>();
        _saveData.m_requestTime = new List<int>();

        SaveLoad.SaveLoadManager.Instance.Save("SaveData", _saveData.ToJsonString());
        Load();
    }

    /// <summary>
    /// 데이터 세이브
    /// </summary>
    public void Save()
    {
        SaveData _saveData = new SaveData();
        PlayerValueManager _playerValueManager = PlayerValueManager.Instance;

        _saveData.m_first = _playerValueManager.m_first;

        _saveData.m_money = _playerValueManager.IsMoney;
        _saveData.m_maxHealth = _playerValueManager.IsMaxHealth;
        _saveData.m_nowHealth = _playerValueManager.IsNowHealth;
        _saveData.m_maxExp = _playerValueManager.IsMaxExp;
        _saveData.m_nowExp = _playerValueManager.IsNowExp;
        _saveData.m_level = _playerValueManager.IsLevel;
        _saveData.m_time = DateTime.Now.ToString();
        _saveData.m_totalTime = _playerValueManager.m_totalTime;

        int _count = 0;
        foreach(KeyValuePair<int, int> item in m_itemAmountDic)
        {
            _saveData.m_itemCode.Add(item.Key);
            _saveData.m_itemAmount.Add(item.Value);
            ++_count;
        }
        _count = 0;
        
        foreach (KeyValuePair<int, int> item in m_productAmountDic)
        {
            _saveData.m_productCode.Add(item.Key);
            _saveData.m_productAmount.Add(item.Value);
            ++_count;
        }
        _count = 0;

        foreach (KeyValuePair<int, int> item in m_materialAmountDic)
        {
            _saveData.m_materialCode.Add(item.Key);
            _saveData.m_materialAmount.Add(item.Value);
            ++_count;
        }
        _count = 0;

        foreach (KeyValuePair<int, int> item in m_collectionAmountDic)
        {
            _saveData.m_collectionCode.Add(item.Key);
            _saveData.m_collectionAmount.Add(item.Value);
            ++_count;
        }
        _count = 0;

        foreach (KeyValuePair<int, bool> item in m_stageClearDic)
        {
            _saveData.m_stageCode.Add(item.Key);
            _saveData.m_stageClear.Add(item.Value);
            ++_count;
        }
        _count = 0;

        _saveData.m_requestCode = m_requestCode;
        _saveData.m_requestTime = m_requestTime;

        SaveLoad.SaveLoadManager.Instance.Save("SaveData", _saveData.ToJsonString());
    }

    /// <summary>
    /// 데이터 로드
    /// </summary>
    public void Load()
    {
        string _saveDataStr = SaveLoad.SaveLoadManager.Instance.Load("SaveData");
        SaveData _saveData = new SaveData(_saveDataStr);
        PlayerValueManager _playerValueManager = PlayerValueManager.Instance;

        if (_saveData == null)
        {
            SaveReset();
        }

        _playerValueManager.m_first = _saveData.m_first;

        _playerValueManager.IsMoney = _saveData.m_money;
        _playerValueManager.IsMaxHealth = _saveData.m_maxHealth;
        _playerValueManager.IsNowHealth = _saveData.m_nowHealth;
        _playerValueManager.IsMaxExp = _saveData.m_maxExp;
        _playerValueManager.IsNowExp = _saveData.m_nowExp;
        _playerValueManager.IsLevel = _saveData.m_level;
        _playerValueManager.m_time = _saveData.m_time;
        _playerValueManager.m_totalTime = _saveData.m_totalTime;

        for (int i = 0; i < m_itemAmountDic.Count; i++)
        {
            if (_saveData.m_itemCode.Count <= i)
            {
                break;
            }
            m_itemAmountDic[_saveData.m_itemCode[i]] = _saveData.m_itemAmount[i];
        }
        for(int i = 0; i < m_productAmountDic.Count; i++)
        {
            if (_saveData.m_productCode.Count <= i)
            {
                break;
            }
            m_productAmountDic[_saveData.m_productCode[i]] = _saveData.m_productAmount[i];
        }
        for (int i = 0; i < m_materialAmountDic.Count; i++)
        {
            if (_saveData.m_materialCode.Count <= i)
            {
                break;
            }
            m_materialAmountDic[_saveData.m_materialCode[i]] = _saveData.m_materialAmount[i];
        }
        for (int i = 0; i < m_collectionAmountDic.Count; i++)
        {
            if(_saveData.m_collectionCode.Count <= i)
            {
                break;
            }
            m_collectionAmountDic[_saveData.m_collectionCode[i]] = _saveData.m_collectionAmount[i];
        }
        for (int i = 0; i < m_stageClearDic.Count; i++)
        {
            if (_saveData.m_stageClear.Count <= i)
            {
                break;
            }
            m_stageClearDic[_saveData.m_stageCode[i]] = _saveData.m_stageClear[i];
        }
        m_requestCode = _saveData.m_requestCode;
        m_requestTime = _saveData.m_requestTime;
    }
    
    /// <summary>
    /// 값 추가
    /// </summary>
    public void AddValues()
    {
        m_toAddMat = new List<int>();
        MiniGameType.GAME_TYPE _type = new MiniGameType.GAME_TYPE();
        _type = m_stageDic[m_nowStageCode].m_miniType[m_nowStageIndex];
        System.Random _random = new System.Random();

        switch (_type)
        {
            case MiniGameType.GAME_TYPE.spider_repelle:
                for(int i = 0; i < UnityEngine.Random.Range(0, 5); i++)
                {
                    m_toAddMat.Add(0);
                }                    
                break;
            case MiniGameType.GAME_TYPE.potion_arrangement:
                for (int i = 0; i < UnityEngine.Random.Range(0, 5); i++)
                {
                    m_toAddMat.Add(1);
                }
                for (int i = 0; i < UnityEngine.Random.Range(0, 5); i++)
                {
                    m_toAddMat.Add(2);
                }
                break;
            case MiniGameType.GAME_TYPE.material_classification:
                for (int i = 0; i < UnityEngine.Random.Range(0, 5); i++)
                {
                    m_toAddMat.Add(3);
                }
                for (int i = 0; i < UnityEngine.Random.Range(0, 5); i++)
                {
                    m_toAddMat.Add(4);
                }
                break;
            case MiniGameType.GAME_TYPE.bookcase_tidy:
                for (int i = 0; i < UnityEngine.Random.Range(0, 5); i++)
                {
                    m_toAddMat.Add(5);
                }
                for (int i = 0; i < UnityEngine.Random.Range(0, 5); i++)
                {
                    m_toAddMat.Add(6);
                }
                break;
        }

        if (m_nowFloor == 1)
        {
            m_toAddMoney += UnityEngine.Random.Range(90, 110);
        }
        else if (m_nowFloor == 2)
        {
            m_toAddMoney += UnityEngine.Random.Range(120, 140);
        }
        else if (m_nowFloor == 3)
        {
            m_toAddMoney += UnityEngine.Random.Range(140, 170);
        }

        if(Glober.stage == 0)
        {
            m_toAddExp += 250;
        }
        else if (m_nowFloor == 1)
        {
            m_toAddExp += 200;
        }
        else if (m_nowFloor == 2)
        {
            m_toAddExp += 280;
        }
        else if (m_nowFloor == 3)
        {
            m_toAddExp += 300;
        }
    }

    /// <summary>
    /// 추가할 값 초기화
    /// </summary>
    public void ResetAddValue()
    {
        m_toAddExp = 0;
        m_toAddMoney = 0;
        m_toAddMat = new List<int>();
        m_toAddMat = null;
    }

    /// <summary>
    /// 종료 시
    /// </summary>
    void OnApplicationQuit()
    {
        Save();
    }

    /// <summary>
    /// 인스턴스 전달
    /// </summary>
    public static GameDataManager Instance
    {
        get
        {
            return g_gameDataManager;
        }
    }
}
