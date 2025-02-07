using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequestManager : MonoBehaviour
{
    /// <summary>
    /// 사람들 이미지
    /// </summary>
    public Sprite[] m_peopleImage = null;

    /// <summary>
    /// 의뢰 패널 (프리펍)
    /// </summary>
    public GameObject m_requestPanelPrf = null;

    /// <summary>
    /// 스크롤뷰 콘텐츠
    /// </summary>
    public GameObject m_content = null;

    /// <summary>
    /// 최대 의뢰 수
    /// </summary>
    public int m_maxRequest = 0;

    /// <summary>
    /// 의뢰 패널들
    /// </summary>
    public List<GameObject> m_requestPanels = new List<GameObject>();

    /// <summary>
    /// 의뢰 패널들 시간 텍스트
    /// </summary>
    public List<Text> m_requestTimeText = new List<Text>();

    /// <summary>
    /// 의뢰 임시 저장
    /// </summary>
    List<int> m_requestTmp = new List<int>();

    /// <summary>
    /// 시간 임시 저장
    /// </summary>
    List<int> m_timeTmp = new List<int>();

    private void Start()
    {
        LoadRequest();
        if (GameDataManager.Instance.m_requestCode.Count > m_maxRequest)
        {
            ReloadRequest();
        }
    }

    /// <summary>
    /// 인보크
    /// </summary>
    public void InvRep()
    {
        InvokeRepeating("NextSeconds", 0.0f, 1.0f);
    }


    /// <summary>
    /// 저장된 데이터에서 의뢰 불러오기
    /// </summary>
    public void LoadRequest()
    {
        GameDataManager.Instance.Load();
        for (int i = 0; i < m_maxRequest; i++)
        {
            try
            {
                AddRequest(GameDataManager.Instance.m_requestCode[i]);
            }
            catch
            {
                ReloadRequest();
                return;
            }
        }
        GameDataManager.Instance.m_requestTime = m_timeTmp;
    }

    /// <summary>
    /// 정해진 데이터로 의뢰 랜덤 리셋
    /// </summary>
    public void ReloadRequest()
    {
        ResetRequest();

        List<int> _count = new List<int>();
        for(int i = 0; i < m_maxRequest; i++)
        {
            int _random = UnityEngine.Random.Range(0, GameDataManager.Instance.m_requestDic.Count);
            if (_count.Contains(_random) == false)
            {
                _count.Add(_random);
                AddRequest(_random);
            }
            else
            {
                --i;
            }
        }

        GameDataManager.Instance.Save();
    }

    /// <summary>
    /// 의뢰 전부 리셋
    /// </summary>
    public void ResetRequest()
    {
        for (int i = 0; i < m_requestPanels.Count; i++)
        {
            Destroy(m_requestPanels[i]);
        }

        m_requestPanels = new List<GameObject>();
        GameDataManager.Instance.m_requestCode.Clear();
        m_requestTimeText = new List<Text>();
        GameDataManager.Instance.m_requestTime.Clear();
    }

    /// <summary>
    /// 의뢰 생성
    /// </summary>
    /// <param name="argCode">의뢰 코드</param>
    public void AddRequest(int argCode)
    {
        if (m_requestPanels.Count > m_maxRequest)
        {
            return;
        }

        GameDataManager.Instance.m_requestCode.Add(argCode);
        GameDataManager.Instance.m_requestTime.Add(28800);
        GameObject _requestPanel = Instantiate(m_requestPanelPrf);
        m_requestPanels.Add(_requestPanel);
        m_requestTimeText.Add(_requestPanel.transform.Find("RemainTimeText").gameObject.GetComponent<Text>());

        Image _personImage = _requestPanel.transform.Find("RequestImage").gameObject.GetComponent<Image>();
        Text _personText = _requestPanel.transform.Find("PersonText").gameObject.GetComponent<Text>();
        Text _contentsText = _requestPanel.transform.Find("RequestContentText").gameObject.GetComponent<Text>();
        RequestBtn _btn = _requestPanel.transform.Find("GetReward").gameObject.GetComponent<RequestBtn>();
        _btn.m_requestManager = this;
        _btn.m_requestCode = argCode;

        _requestPanel.transform.SetParent(m_content.transform);
        _requestPanel.transform.localScale = new Vector3(1, 1, 1);

        RequestData _requestData = GameDataManager.Instance.m_requestDic[argCode];
        _personImage.sprite = m_peopleImage[_requestData.m_requestPerson];
        _personImage.preserveAspect = true;
        _personText.text = GameDataManager.Instance.m_peopleDic[_requestData.m_requestPerson].m_personName;
        _contentsText.text = string.Format("");
        _contentsText.text = string.Format(_requestData.m_explain + "\n\n(");
        for (int i = 0; i < _requestData.m_needProductCode.Length; i++)
        {
            if (_requestData.m_needProductAmount[i] != 0)
            {
                _contentsText.text += GameDataManager.Instance.m_productDic[_requestData.m_needProductCode[i]].m_productName + " " + _requestData.m_needProductAmount[i] + "개, ";
            }
        }
        _contentsText.text = _contentsText.text.Substring(0, _contentsText.text.Length - 2);
        _contentsText.text += ")\n\n보상 : " + GameDataManager.Instance.m_collectionDataDic[_requestData.m_rewardCollectionCode].m_collectionName;
    }

    /// <summary>
    /// 보상 받기
    /// </summary>
    /// <param name="argCode">의뢰 코드</param>
    public void GetReward(int argCode)
    {
        RequestData _request = GameDataManager.Instance.m_requestDic[argCode];
        for (int i = 0; i < _request.m_needProductCode.Length; i++)
        {
            int _proCode = _request.m_needProductCode[i];
            int _proAmount = _request.m_needProductAmount[i];
            if (_proAmount > GameDataManager.Instance.m_productAmountDic[_proCode])
            {
                WarningPanelManager.Instance.Warning("제작품이 부족합니다!");
                return;
            }
            GameDataManager.Instance.m_productAmountDic[_proCode] -= _proAmount;
        }

        DelRequest(argCode);

        GameDataManager.Instance.m_collectionAmountDic[_request.m_rewardCollectionCode]++;
        GameDataManager.Instance.Save();
        LoadRequest();
    }

    /// <summary>
    /// 의뢰 무조건 삭제 후 랜덤 생성
    /// </summary>
    /// <param name="argCode">의뢰 코드</param>
    public void DelRequest(int argCode)
    {
        RequestData _request = GameDataManager.Instance.m_requestDic[argCode];

        int _index = GameDataManager.Instance.m_requestCode.IndexOf(_request.m_requestCode);
        GameDataManager.Instance.m_requestCode.Remove(_request.m_requestCode);

        GameDataManager.Instance.m_requestTime.RemoveAt(_index);
        GameDataManager.Instance.m_requestTime.Add(28800);

        List<int> _list = new List<int>();
        foreach (KeyValuePair<int, RequestData> item in GameDataManager.Instance.m_requestDic)
        {
            if (GameDataManager.Instance.m_requestCode.Contains(item.Key) == false)
            {
                _list.Add(item.Key);
            }
        }
        int _random = UnityEngine.Random.Range(0, _list.Count + 1);
        AddRequest(_random);
    }

    /// <summary>
    /// 다음 초
    /// </summary>
    void NextSeconds()
    {
        if (GameDataManager.Instance.m_requestTime.Count > m_maxRequest)
        {
            for(int i = m_maxRequest - 1; i < GameDataManager.Instance.m_requestTime.Count; i++)
            {
                GameDataManager.Instance.m_requestTime.RemoveAt(i);
            }
            return;
        }

        for(int i = 0; i <  GameDataManager.Instance.m_requestTime.Count; i++)
        {
            if(GameDataManager.Instance.m_requestTime[i] <= 0)
            {
                DelRequest(GameDataManager.Instance.m_requestCode[i]);
            }

            GameDataManager.Instance.m_requestTime[i]--;

            string _string = string.Empty;
            _string = string.Format("{0}시간 {1}분", GameDataManager.Instance.m_requestTime[i] / 3600,
                GameDataManager.Instance.m_requestTime[i] % 3600 / 60);
            m_requestTimeText[i].text = _string;
        }
    }

    /*

    /// <summary>
    /// 의뢰 랜덤 생성
    /// </summary>
    public void RandomAddRequest()
    {
        if (GameDataManager.Instance.m_requestPanels.Count > m_maxRequest)
        {
            return;
        }

        ProductManager _productManager = ProductManager.g_productManager;
        GameDataManager _gameDataManager = GameDataManager.g_gameDataManager;

        GameObject _requestPanel = Instantiate(m_requestPanelPrf);
        Image _image = _requestPanel.transform.Find("RequestImage").gameObject.GetComponent<Image>();
        Text _text = _requestPanel.transform.Find("RequestContent").gameObject.GetComponent<Text>();
        RequestBtn _btn = _requestPanel.transform.Find("GetReward").gameObject.GetComponent<RequestBtn>();
        _btn.m_ownIndex = GameDataManager.Instance.m_requestPanels.Count;
        GameDataManager.Instance.m_requestPanels.Add(_requestPanel);
        _requestPanel.transform.SetParent(m_content.transform);
        _requestPanel.transform.localScale = new Vector3(1, 1, 1);

        _text.text = string.Format("");

        int[] _productCode = new int[m_maxNeedProduct];
        int[] _productAmount = new int[m_maxNeedProduct];
        int _random0 = Random.Range(1, m_maxNeedProduct);
        for (int i = 0; i < _random0; i++)
        {
            int _random1 = Random.Range(1, _gameDataManager.m_productData.Length);
            _productCode[i] = _random1;

            int _random2 = Random.Range(1, 16);
            _productAmount[i] = _random2;
        }

        ProductData _productDicData = null;
        for (int i = 0; i < _productCode.Length; i++)
        {
            if(_productAmount[i] != 0)
            {
                _gameDataManager.m_productDic.TryGetValue(_productCode[i], out _productDicData);
                _text.text += _productDicData.m_productName + " " + _productAmount[i] + "개, ";
            }
        }
        _text.text = _text.text.Substring(0, _text.text.Length - 2);
        _text.text += "가 필요하다.\n\n";

        CollectionData _collectionData = null;
        int _collectionFlag = 0;

        int _random3 = Random.Range(1, _gameDataManager.m_collectionDataDic.Count);
        _gameDataManager.m_collectionAmountDic.TryGetValue(_random3, out _collectionFlag);
        _gameDataManager.m_collectionDataDic.TryGetValue(_random3, out _collectionData);

        _text.text += "보상은 " + _collectionData.m_collectionName + "이다.";

        _btn.m_needProductCode = _productCode;
        _btn.m_needProductAmount = _productAmount;
        _btn.m_rewardCollectionCode = _collectionData.m_collectionCode;
    }

    /// <summary>
    /// 의뢰 랜덤 새로고침
    /// </summary>
    public void RandomReloadRequest()
    {
        if(GameDataManager.Instance.m_requestPanels.Count != 0)
        {
            for (int i = 0; i < GameDataManager.Instance.m_requestPanels.Count; i++)
            {
                Destroy(GameDataManager.Instance.m_requestPanels[i]);
            }
            for (int i = 0; i < GameDataManager.Instance.m_requestPanels.Count; i++)
            {
                GameDataManager.Instance.m_requestPanels = new List<GameObject>();
            }
        }

        for (int i = 0; i < m_maxRequest; i++)
        {
            RandomAddRequest();
        }
    }


    /// <summary>
    /// 랜덤 생성에서 보상 받기
    /// </summary>
    /// <param name="argProCode">제작품 코드 배열</param>
    /// <param name="argProAmount">제작품 필요 갯수 배열 </param>
    /// <param name="argCollectionCode">보상할 콜렉션 코드</param>
    /// <param name="argIndex">자기 자신의 인덱스</param>
    public void RandomGetReward(int[] argProCode, int[] argProAmount, int argCollectionCode, int argIndex)
    {
        bool _check = false;

        for (int i = 0; i < argProCode.Length; i++)
        {
            if(GameDataManager.g_gameDataManager.m_productAmountDic[argProCode[i]] >= argProAmount[i])
            {
                _check = true;
            }
            else
            {
                _check = false;
                WarningPanelManager.g_warningPanel.SetWarningPanel("재작품이 부족합니다!");
                break;
            }
        }

        if(_check == true)
        {
            Destroy(GameDataManager.Instance.m_requestPanels[argIndex]);
            GameDataManager.Instance.m_requestPanels.RemoveAt(argIndex);
            RandomAddRequest();
            CollectionManager.g_collectionManager.AddCollection(argCollectionCode);
        }
    }
    */
}
