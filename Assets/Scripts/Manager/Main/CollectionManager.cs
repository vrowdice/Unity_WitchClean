using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    /// <summary>
    /// 수집품이 없을 경우 이미지
    /// </summary>
    public Sprite m_collectionIsNone = null;

    /// <summary>
    /// 돈 스프라이트
    /// </summary>
    public Sprite m_moneySprite = null;

    /// <summary>
    /// 수집품 패널
    /// </summary>
    public GameObject m_collectionPanel = null;

    /// <summary>
    /// 수집품 패널의 이미지
    /// </summary>
    public Image m_collectionImage = null;

    /// <summary>
    /// 현재 수집품 매뉴 정보 텍스트
    /// </summary>
    public Text m_collectionMenuInfoT = null;

    /// <summary>
    /// 선택한 수집품의 이름
    /// </summary>
    public Text m_collectionName = null;

    /// <summary>
    /// 선택한 수집품의 갯수
    /// </summary>
    public Text m_collectionCount = null;

    /// <summary>
    /// 수집품 패널의 설명
    /// </summary>
    public Text m_collectionExplain = null;

    /// <summary>
    /// 수집품 패널의 보상 내역
    /// </summary>
    public Image m_rewardImg = null;

    /// <summary>
    /// 수집품 패널의 콜랙션 버튼
    /// </summary>
    public Button[] m_collectionBtn = null;

    /// <summary>
    /// 현재 수집품 매뉴 인덱스
    /// </summary>
    int m_nowCollectionMenuIndex = 0;

    /// <summary>
    /// 선택한 콜렉션
    /// </summary>
    int m_nowCollection = 0;

    /// <summary>
    /// 콜랙션 매뉴 공개
    /// </summary>
    static CollectionManager g_collectionManager = null;

    /// <summary>
    /// start
    /// </summary>
    private void Awake()
    {
        g_collectionManager = this;
    }

    /// <summary>
    /// 수집품 매뉴 버튼 클릭시
    /// </summary>
    /// <param name="argIndex">수집품 매뉴 인덱스</param>
    public void CollectionMenuClick(int argIndex)
    {
        if(argIndex == 1)
        {
            CollectionBtnClick(0);
            m_rewardImg.sprite = GameDataManager.Instance.m_itemDic[10].m_itemImage;
            m_rewardImg.preserveAspect = true;
        }
        else
        {
            CollectionBtnClick(5);
            m_rewardImg.sprite = m_moneySprite;
            m_rewardImg.preserveAspect = true;
        }

        m_nowCollectionMenuIndex = argIndex;

        int _collectionCheck = 0;
        CollectionData _collectionData = null;
        int _btnCheck = 0;

        for (int i = 0; i < m_collectionBtn.Length; i++)
        {
            GameDataManager.Instance.m_collectionDataDic.TryGetValue(i, out _collectionData);
            GameDataManager.Instance.m_collectionAmountDic.TryGetValue(i, out _collectionCheck);

            if (GameDataManager.Instance.m_collectionDataDic.Count > i)
            {
                if (_collectionData.m_collectionMenu == m_nowCollectionMenuIndex)
                {
                    m_collectionBtn[_btnCheck].transform.Find("Image").gameObject.GetComponent<Image>().sprite = _collectionData.m_collectionSprite;
                    m_collectionBtn[_btnCheck].GetComponent<CollectionBtn>().m_collectionCode = _collectionData.m_collectionCode;
                    _btnCheck++;
                }
            }
            else
            {
                m_collectionBtn[_btnCheck].transform.Find("Image").gameObject.GetComponent<Image>().sprite = m_collectionIsNone;
                m_collectionBtn[_btnCheck].GetComponent<CollectionBtn>().m_collectionCode = -1;
            }
        }

        m_collectionMenuInfoT.text = string.Format("수집품 매뉴 {0}번", m_nowCollectionMenuIndex + 1);

        m_collectionPanel.SetActive(true);
    }

    /// <summary>
    /// 수집품 패널의 수집품 버튼 클릭시
    /// </summary>
    /// <param name="argCode">수집품의 코드</param>
    public void CollectionBtnClick(int argCode)
    {
        m_nowCollection = argCode;

        if (m_nowCollection <= -1)
        {
            WarningPanelManager.Instance.Warning("콜랙션이 없습니다!");
            return;
        }
        CollectionData _collectionData = null;
        GameDataManager.Instance.m_collectionDataDic.TryGetValue(m_nowCollection, out _collectionData);

        m_collectionImage.sprite = _collectionData.m_collectionSprite;
        m_collectionName.text = _collectionData.m_collectionName;
        m_collectionCount.text = GameDataManager.Instance.m_collectionAmountDic[m_nowCollection].ToString();
        m_collectionExplain.text = _collectionData.m_collectionExplain;

        // m_rewardMenu.text = 
    }

    /// <summary>
    /// 콜렉션 생성
    /// </summary>
    public void AddCollection(int argCode)
    {
        GameDataManager.Instance.m_collectionAmountDic[argCode] += 1;
        GameDataManager.Instance.Save();
    }

    /// <summary>
    /// 콜렉션 보상 받기
    /// </summary>
    public void GetReward()
    {
        List<int> _list = new List<int>();

        foreach(KeyValuePair<int, int> item in GameDataManager.Instance.m_collectionAmountDic)
        {
            CollectionData _data = GameDataManager.Instance.m_collectionDataDic[item.Key];
            if (_data.m_collectionMenu == m_nowCollectionMenuIndex)
            {
                if(item.Value < 1)
                {
                    WarningPanelManager.Instance.Warning("콜랙션이 부족합니다!");
                    return;
                }
                _list.Add(item.Key);
            }
        }

        for(int i = 0; i < _list.Count; i++)
        {
            GameDataManager.Instance.m_collectionAmountDic[_list[i]] -= 1 ;
        }

        switch (m_nowCollectionMenuIndex)
        {
            case 0:
                PlayerValueManager.Instance.IsMoney += 500;
                break;
            case 1:
                ItemManager.Instance.AddItem(10, 1);
                break;
            default:
                break;
        }

        CollectionBtnClick(m_nowCollection);
        GameDataManager.Instance.Save();
    }

    /// <summary>
    /// 인스턴스
    /// </summary>
    public static CollectionManager Instance
    {
        get
        {
            return g_collectionManager;
        }
    }
}
