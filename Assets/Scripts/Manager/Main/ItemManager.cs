//아이탬 관련 처리는 모두 이곳에서 담당

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    /// <summary>
    /// 아이템 없을 때 공간
    /// </summary>
    public Sprite m_itemIsNone = null;

    /// <summary>
    /// 아이템 패널
    /// </summary>
    public GameObject m_itemPanel = null;

    /// <summary>
    /// 창고 셀들 배열
    /// </summary>
    public GameObject[] m_chestCell = null;

    /// <summary>
    /// 상점 셋들 배열
    /// </summary>
    public Button[] m_storCell = null;

    /// <summary>
    /// 아이탬 구매 버튼
    /// </summary>
    public BuyItemBtn m_buyItemBtn = null;

    /// <summary>
    /// 상점 아이탬 이미지
    /// </summary>
    public Image m_itemImage = null;

    /// <summary>
    /// 상점 아이탬 이름
    /// </summary>
    public Text m_itemName = null;

    /// <summary>
    /// 상점 아이탬 설명
    /// </summary>
    public Text m_itemExplain = null;

    /// <summary>
    /// 상점 아이탬 가격
    /// </summary>
    public Text m_itemPrice = null;

    /// <summary>
    /// 상점 아이탬 갯수
    /// </summary>
    public Text m_itemAmount = null;

    /// <summary>
    /// 현재 상자 인덱스
    /// 0 = itme, 1 = mat, 2 = pro
    /// </summary>
    public int m_nowChestIndex = 0;

    /// <summary>
    /// 패널 아이탬 이미지
    /// </summary>
    Image m_itemPanelImage = null;

    /// <summary>
    /// 패널 아이탬 이름
    /// </summary>
    Text m_itemPanelName = null;

    /// <summary>
    /// 패널 아이탬 설명
    /// </summary>
    Text m_itemPanelExplain = null;

    /// <summary>
    /// 사용 버튼
    /// </summary>
    GameObject m_itemPanelUseBtn = null;

    /// <summary>
    /// 지금 가방 아이템 코드
    /// </summary>
    int m_nowChestCode = 0;

    /// <summary>
    /// 자기 자신 글로벌화
    /// </summary>
    static ItemManager g_itemManager = null;

    void Awake()
    {
        if (Instance == null)
        {
            g_itemManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        m_nowChestIndex = 0;
        UpdateChest();
        SetBuyPanel(0);

        m_itemPanelImage = m_itemPanel.transform.GetChild(0).Find("ItemImage").gameObject.GetComponent<Image>();
        m_itemPanelName = m_itemPanel.transform.GetChild(0).Find("ItemText").gameObject.GetComponent<Text>();
        m_itemPanelExplain = m_itemPanel.transform.GetChild(0).Find("ItemExplainText").gameObject.GetComponent<Text>();
        m_itemPanelUseBtn = m_itemPanel.transform.GetChild(0).Find("ItemUseBtn").gameObject;

        int _count = 0;
        foreach(KeyValuePair<int, ItemData> item in GameDataManager.Instance.m_itemDic)
        {
            if(item.Key < 7)
            {
                Button _cell = m_storCell[_count];
                _cell.GetComponent<StorItemBtn>().m_ownItemCode = item.Key;
                _cell.transform.Find("Image").gameObject.GetComponent<Image>().sprite = item.Value.m_itemImage;
                _cell.transform.Find("Text").gameObject.GetComponent<Text>().text = item.Value.m_itemName;
                _count++;
            }
        }
    }

    /// <summary>
    /// 아이템 구매 패널 셋팅
    /// </summary>
    /// <param name="argItemCode"></param>
    public void SetBuyPanel(int argItemCode)
    {
        if (argItemCode < 0)
        {
            WarningPanelManager.Instance.Warning("아이템이 존재하지 않습니다!");
            return;
        }

        if (GameDataManager.Instance.m_itemDic.Count < argItemCode)
        {
            return;
        }

        ItemData _data = GameDataManager.Instance.m_itemDic[argItemCode];

        ResetPanel();

        m_buyItemBtn.m_itemCode = argItemCode;
        m_itemImage.sprite = _data.m_itemImage;
        m_itemName.text = _data.m_itemName;
        m_itemExplain.text = _data.m_itemExplain;
        m_itemPrice.text = "금화 " + _data.m_itemPrice.ToString() + "개";

        ChangePanelItemAmount(argItemCode);

        m_itemImage.preserveAspect = true;
    }

    /// <summary>
    /// 가방 패널 셋팅
    /// </summary>
    /// <param name="argCode">아이템 코드</param>
    public void SetChestPanel(int argCode)
    {
        if (argCode < 0)
        {
            WarningPanelManager.Instance.Warning("아이템이 존재하지 않습니다!");
            return;
        }

        m_nowChestCode = argCode;
        if (m_nowChestIndex == 0)
        {
            ItemData _data = GameDataManager.Instance.m_itemDic[argCode];

            m_itemPanelImage.sprite = _data.m_itemImage;
            m_itemPanelName.text = _data.m_itemName;
            m_itemPanelExplain.text = _data.m_itemExplain;
            m_itemPanelUseBtn.SetActive(true);
        }
        else if (m_nowChestIndex == 1)
        {
            MaterialData _data = GameDataManager.Instance.m_materialDic[argCode];

            m_itemPanelImage.sprite = _data.m_materialSprite;
            m_itemPanelName.text = _data.m_materialName;
            m_itemPanelExplain.text = _data.m_materialExplain;
            m_itemPanelUseBtn.SetActive(false);
        }
        else
        {
            ProductData _data = GameDataManager.Instance.m_productDic[argCode];

            m_itemPanelImage.sprite = _data.m_productSprite;
            m_itemPanelName.text = _data.m_productName;
            m_itemPanelExplain.text = _data.m_productExplain;
            m_itemPanelUseBtn.SetActive(false); ;
        }
        m_itemPanelImage.preserveAspect = true;

        m_itemPanel.SetActive(true);

    }

    /// <summary>
    /// 패널 안의 정보들을 리셋
    /// </summary>
    void ResetPanel()
    {
        m_buyItemBtn.m_itemCode = 0;
        m_itemImage.sprite = null;
        m_itemName.text = string.Empty;
        m_itemExplain.text = string.Empty;
        m_itemPrice.text = string.Empty;
        m_itemAmount.text = string.Empty;
    }

    /// <summary>
    /// 아이탬 패널의 아이탬 갯수 표시기 변경
    /// </summary>
    /// <param name="argItemCode">변경할 아이탬의 아이탬 코드</param>
    void ChangePanelItemAmount(int argItemCode)
    {
        if (GameDataManager.Instance.m_itemAmountDic[argItemCode] >= 0)
        {
            m_itemAmount.text = GameDataManager.Instance.m_itemAmountDic[argItemCode].ToString() + " 개";
        }
        else
        {
            m_itemAmount.text = "0 개";
        }
    }

    /// <summary>
    /// 아이탬 생성
    /// </summary>
    /// <param name="argItemCode">아이탬 코드</param>
    public void AddItem(int argItemCode, int argAmount)
    {
        ItemData _itemData = GameDataManager.Instance.m_itemDic[argItemCode];

        if(PlayerValueManager.Instance.IsMoney < _itemData.m_itemPrice)
        {
            WarningPanelManager.Instance.Warning("골드가 없습니다!");
            return;
        }

        PlayerValueManager.Instance.IsMoney -= _itemData.m_itemPrice;
        GameDataManager.Instance.m_itemAmountDic[argItemCode] += argAmount;

        UpdateChest();
        ChangePanelItemAmount(argItemCode);

        GameDataManager.Instance.Save();
    }

    /// <summary>
    /// 가방 업데이트
    /// </summary>
    public void UpdateChest()
    {
        if(m_nowChestIndex == 0)
        {
            UpdateChestItem();
        }
        else if(m_nowChestIndex == 1)
        {
            UpdateChestMat();
        }
        else
        {
            UpdateChestPro();
        }
    }

    /// <summary>
    /// 가방 안의 가진 아이템 새로고침
    /// </summary>
    void UpdateChestItem()
    {
        int _count = 0;

        foreach (KeyValuePair<int, int> item in GameDataManager.Instance.m_itemAmountDic)
        {
            if (GameDataManager.Instance.m_itemAmountDic[item.Key] > 0)
            {
                GameObject _cell = m_chestCell[_count];

                _cell.transform.Find("Image").gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.m_itemDic[item.Key].m_itemImage;
                _cell.transform.Find("Image").gameObject.GetComponent<Image>().preserveAspect = true;
                _cell.transform.Find("Text").gameObject.GetComponent<Text>().text = GameDataManager.Instance.m_itemAmountDic[item.Key].ToString();
                _cell.GetComponent<ChestItemBtn>().m_code = item.Key;
                ++_count;
            }
        }
        for(int i = _count; i < m_chestCell.Length; i++)
        {
            GameObject _cell = m_chestCell[i];

            _cell.transform.Find("Image").gameObject.GetComponent<Image>().sprite = m_itemIsNone;
            _cell.transform.Find("Image").gameObject.GetComponent<Image>().preserveAspect = true;
            _cell.transform.Find("Text").gameObject.GetComponent<Text>().text = string.Empty;
            _cell.GetComponent<ChestItemBtn>().m_code = -1;
        }
    }

    /// <summary>
    /// 가방 안의 가진 재료 새로고침
    /// </summary>
    void UpdateChestMat()
    {
        int _count = 0;

        foreach (KeyValuePair<int, int> item in GameDataManager.Instance.m_materialAmountDic)
        {
            if (GameDataManager.Instance.m_materialAmountDic[item.Key] > 0)
            {
                GameObject _cell = m_chestCell[_count];

                _cell.transform.Find("Image").gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.m_materialDic[item.Key].m_materialSprite;
                _cell.transform.Find("Image").gameObject.GetComponent<Image>().preserveAspect = true;
                _cell.transform.Find("Text").gameObject.GetComponent<Text>().text = GameDataManager.Instance.m_materialAmountDic[item.Key].ToString();
                _cell.GetComponent<ChestItemBtn>().m_code = item.Key;
                ++_count;
            }
        }
        for (int i = _count; i < m_chestCell.Length; i++)
        {
            GameObject _cell = m_chestCell[i];

            _cell.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Instance.m_itemIsNone;
            _cell.transform.Find("Image").gameObject.GetComponent<Image>().preserveAspect = true;
            _cell.transform.Find("Text").gameObject.GetComponent<Text>().text = string.Empty;
            _cell.GetComponent<ChestItemBtn>().m_code = -1;
        }
    }

    /// <summary>
    /// 가방 안의 가진 재료 새로고침
    /// </summary>
    void UpdateChestPro()
    {
        int _count = 0;

        foreach (KeyValuePair<int, int> item in GameDataManager.Instance.m_productAmountDic)
        {
            if (GameDataManager.Instance.m_productAmountDic[item.Key] > 0)
            {
                GameObject _cell = m_chestCell[_count];

                _cell.transform.Find("Image").gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.m_productDic[item.Key].m_productSprite;
                _cell.transform.Find("Image").gameObject.GetComponent<Image>().preserveAspect = true;
                _cell.transform.Find("Text").gameObject.GetComponent<Text>().text = GameDataManager.Instance.m_productAmountDic[item.Key].ToString();
                _cell.GetComponent<ChestItemBtn>().m_code = item.Key;
                ++_count;
            }
        }
        for (int i = _count; i < m_chestCell.Length; i++)
        {
            GameObject _cell = m_chestCell[i];

            _cell.transform.Find("Image").gameObject.GetComponent<Image>().sprite = Instance.m_itemIsNone;
            _cell.transform.Find("Image").gameObject.GetComponent<Image>().preserveAspect = true;
            _cell.transform.Find("Text").gameObject.GetComponent<Text>().text = string.Empty;
            _cell.GetComponent<ChestItemBtn>().m_code = -1;
        }
    }

    /// <summary>
    /// 아이템 사용하기
    /// </summary>
    public void UseItem()
    {
        if(m_nowChestIndex != 0)
        {
            return;
        }
        if(m_nowChestCode < 5 || GameDataManager.Instance.m_itemAmountDic[m_nowChestCode] < 1)
        {
            WarningPanelManager.Instance.Warning("사용이 불가능합니다!");
            return;
        }

        switch (m_nowChestCode)
        {
            case 5:
                PlayerValueManager.Instance.IsNowHealth += PlayerValueManager.Instance.IsMaxHealth / 2;
                break;
            case 6:
                break;
            case 10:
                PlayerValueManager.Instance.IsNowHealth += PlayerValueManager.Instance.IsMaxHealth;
                break;
            default:
                break;
        }

        GameDataManager.Instance.m_itemAmountDic[m_nowChestCode] -= 1;
        UpdateChest();
        m_itemPanel.SetActive(false);
        GameDataManager.Instance.Save();
    }

    /// <summary>
    /// 아이템 매니저 인스턴스
    /// </summary>
    public static ItemManager Instance
    {
        get
        {
            return g_itemManager;
        }
    }
}
