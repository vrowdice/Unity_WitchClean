using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductManager : MonoBehaviour
{
    /// <summary>
    /// 연구실 패널
    /// </summary>
    public GameObject m_labPanel = null;

    /// <summary>
    /// 제작품 이미지
    /// </summary>
    public Image m_productImage = null;

    /// <summary>
    /// 제작품 이름 텍스트
    /// </summary>
    public Text m_productName = null;

    /// <summary>
    /// 제작품 설명 텍스트
    /// </summary>
    public Text m_productExplain = null;

    /// <summary>
    /// 제작품 갯수 텍스트
    /// </summary>
    public Text m_productAmount = null;

    /// <summary>
    /// 재료 패널을 넣을 패널
    /// </summary>
    public GameObject m_materialSetPanel = null;

    /// <summary>
    /// 패널의 제작품 이미지
    /// </summary>
    public Image m_panelProductImage = null;

    /// <summary>
    /// 패널의 제작품 이름 텍스트
    /// </summary>
    public Text m_panelProductName = null;

    /// <summary>
    /// 패널의 제작품 갯수 텍스트
    /// </summary>
    public Text m_panelProductAmount = null;

    /// <summary>
    /// 패널의 재료 패널을 넣을 패널
    /// </summary>
    public GameObject m_panelMaterialSetPanel = null;

    /// <summary>
    /// 재료 내용이 들어갈 패널
    /// </summary>
    public GameObject m_materialPanel = null;

    /// <summary>
    /// 임시 제작품 코드
    /// </summary>
    int m_productCode = 0;

    /// <summary>
    /// 아이템이 선택 되었는지 플래그
    /// </summary>
    bool m_selectFlag = false;

    /// <summary>
    /// 임시 재료 데이터
    /// </summary>
    MaterialData m_tmpMaterialData = null;

    /// <summary>
    /// 임시 제작품 데이터
    /// </summary>
    ProductData m_tmpProductData = null;

    /// <summary>
    /// start
    /// </summary>
    private void Start()
    {
        ClickProductBtn(0);
        SelectProductBtn();
    }

    /// <summary>
    /// 제작품 버튼 클릭 시
    /// </summary>
    public void ClickProductBtn(int argProductCode)
    {
        m_productCode = argProductCode;

        UiSet(1);
    }

    /// <summary>
    /// 제작품 선택
    /// </summary>
    public void SelectProductBtn()
    {
        UiSet(0);
        m_selectFlag = true;

        m_productImage.sprite = m_panelProductImage.sprite;
        m_productName.text = m_panelProductName.text;
        m_productExplain.text = m_tmpProductData.m_productExplain;
        m_productAmount.text = m_panelProductAmount.text + " 개";

        m_labPanel.SetActive(false);
    }

    /// <summary>
    /// 재료 정보 설정
    /// </summary>
    /// <param name="argMaterialData">재료 데이터</param>
    void SetPanelProductInfo(int argIndex, int argVar)
    {
        GameObject _materialPanel = Instantiate(m_materialPanel);

        if(argVar == 0)
        {
            _materialPanel.transform.SetParent(m_materialSetPanel.transform);
            AddMaterialPanel(argIndex, _materialPanel);
        }
        else if(argVar == 1)
        {
            _materialPanel.transform.SetParent(m_panelMaterialSetPanel.transform);
            AddMaterialPanel(argIndex, _materialPanel);
        }
        else
        {
            _materialPanel.transform.SetParent(m_panelMaterialSetPanel.transform);
            AddMaterialPanel(argIndex, _materialPanel);
            _materialPanel.transform.SetParent(m_materialSetPanel.transform);
            AddMaterialPanel(argIndex, _materialPanel);
        }
    }

    /// <summary>
    /// 재료 정보 패널 생성
    /// </summary>
    /// <param name="argIndex">재작품의 필요 재료 인덱스</param>
    /// <param name="argMaterialObj">타겟 재료 정보 패널 오브젝트</param>
    void AddMaterialPanel(int argIndex, GameObject argMaterialObj)
    {
        argMaterialObj.transform.localScale = new Vector3(1, 1, 1);
        argMaterialObj.transform.Find("ProductImage").gameObject.GetComponent<Image>().sprite = m_tmpMaterialData.m_materialSprite;

        int _materialAmount = 0;
        GameDataManager.Instance.m_materialAmountDic.TryGetValue(m_tmpMaterialData.m_materialCode, out _materialAmount);
        switch (argIndex)
        {
            case 0:
                argMaterialObj.transform.Find("ProductMaterial").gameObject.GetComponent<Text>().text =
                    string.Format(m_tmpMaterialData.m_materialName + " {0}/{1}", m_tmpProductData.m_materialAmount0, _materialAmount);
                break;
            case 1:
                argMaterialObj.transform.Find("ProductMaterial").gameObject.GetComponent<Text>().text =
                    string.Format(m_tmpMaterialData.m_materialName + " {0}/{1}", m_tmpProductData.m_materialAmount1, _materialAmount);
                break;
            case 2:
                argMaterialObj.transform.Find("ProductMaterial").gameObject.GetComponent<Text>().text =
                    string.Format(m_tmpMaterialData.m_materialName + " {0}/{1}", m_tmpProductData.m_materialAmount2, _materialAmount); 
                break;
            case 3:
                argMaterialObj.transform.Find("ProductMaterial").gameObject.GetComponent<Text>().text =
                    string.Format(m_tmpMaterialData.m_materialName + " {0}/{1}", m_tmpProductData.m_materialAmount3, _materialAmount);
                break;
            case 4:
                argMaterialObj.transform.Find("ProductMaterial").gameObject.GetComponent<Text>().text =
                    string.Format(m_tmpMaterialData.m_materialName + " {0}/{1}", m_tmpProductData.m_materialAmount4, _materialAmount);
                break;
            case 5:
                argMaterialObj.transform.Find("ProductMaterial").gameObject.GetComponent<Text>().text =
                    string.Format(m_tmpMaterialData.m_materialName + " {0}/{1}", m_tmpProductData.m_materialAmount5, _materialAmount);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 제작 버튼 클릭
    /// </summary>
    public void ClickMakeBtn()
    {
        if (m_selectFlag == false)
        {
            WarningPanelManager.Instance.Warning("제작품이 선택되지 않았습니다.");
            return;
        }

        bool _isMake = false;
        int _materialAmount = 0;

        GameDataManager.Instance.m_materialAmountDic.TryGetValue(m_tmpProductData.m_materialCode0, out _materialAmount);
        if (m_tmpProductData.m_materialAmount0 <= _materialAmount)
        {
            _isMake = true;
        }
        else
        {
            _isMake = false;
        }
        GameDataManager.Instance.m_materialAmountDic.TryGetValue(m_tmpProductData.m_materialCode1, out _materialAmount);
        if (m_tmpProductData.m_materialAmount1 <= _materialAmount && _isMake)
        {
            _isMake = true;
        }
        else
        {
            _isMake = false;
        }
        GameDataManager.Instance.m_materialAmountDic.TryGetValue(m_tmpProductData.m_materialCode2, out _materialAmount);
        if (m_tmpProductData.m_materialAmount2 <= _materialAmount && _isMake)
        {
            _isMake = true;
        }
        else
        {
            _isMake = false;
        }
        GameDataManager.Instance.m_materialAmountDic.TryGetValue(m_tmpProductData.m_materialCode3, out _materialAmount);
        if (m_tmpProductData.m_materialAmount3 <= _materialAmount && _isMake)
        {
            _isMake = true;
        }
        else
        {
            _isMake = false;
        }
        GameDataManager.Instance.m_materialAmountDic.TryGetValue(m_tmpProductData.m_materialCode4, out _materialAmount);
        if (m_tmpProductData.m_materialAmount4 <= _materialAmount && _isMake)
        {
            _isMake = true;
        }
        else
        {
            _isMake = false;
        }
        GameDataManager.Instance.m_materialAmountDic.TryGetValue(m_tmpProductData.m_materialCode5, out _materialAmount);
        if (m_tmpProductData.m_materialAmount5 <= _materialAmount && _isMake)
        {
            _isMake = true;
        }
        else
        {
            _isMake = false;
        }

        if (_isMake)
        {
            GameDataManager.Instance.m_materialAmountDic[m_tmpProductData.m_materialCode0] -= m_tmpProductData.m_materialAmount0;
            GameDataManager.Instance.m_materialAmountDic[m_tmpProductData.m_materialCode1] -= m_tmpProductData.m_materialAmount1;
            GameDataManager.Instance.m_materialAmountDic[m_tmpProductData.m_materialCode2] -= m_tmpProductData.m_materialAmount2;
            GameDataManager.Instance.m_materialAmountDic[m_tmpProductData.m_materialCode3] -= m_tmpProductData.m_materialAmount3;
            GameDataManager.Instance.m_materialAmountDic[m_tmpProductData.m_materialCode4] -= m_tmpProductData.m_materialAmount4;
            GameDataManager.Instance.m_materialAmountDic[m_tmpProductData.m_materialCode5] -= m_tmpProductData.m_materialAmount5;

            GameDataManager.Instance.m_productAmountDic[m_productCode] += 1;

            m_productAmount.text = GameDataManager.Instance.m_productAmountDic[m_productCode].ToString() + "개";
            UiSet(2);
        }
        else
        {
            WarningPanelManager.Instance.Warning("만들 수 없습니다.");
        }

        GameDataManager.Instance.Save();
    }

    /// <summary>
    /// Ui 정보들 셋팅
    /// </summary>
    void UiSet(int argVar)
    {
        GameDataManager.Instance.m_productDic.TryGetValue(m_productCode, out m_tmpProductData);
        m_panelProductImage.sprite = m_tmpProductData.m_productSprite;
        m_panelProductName.text = m_tmpProductData.m_productName;
        m_panelProductAmount.text = GameDataManager.Instance.m_productAmountDic[m_productCode].ToString();
        
        if(argVar == 1)
        {
            int _child = m_panelMaterialSetPanel.transform.childCount;
            for (int i = 0; i < _child; i++)
            {
                Destroy(m_panelMaterialSetPanel.transform.GetChild(i).gameObject);
            }
        }
        else if(argVar == 0)
        {
            int _child = m_materialSetPanel.transform.childCount;
            for (int i = 0; i < _child; i++)
            {
                Destroy(m_materialSetPanel.transform.GetChild(i).gameObject);
            }
        }
        else
        {
            int _child0 = m_panelMaterialSetPanel.transform.childCount;
            for (int i = 0; i < _child0; i++)
            {
                Destroy(m_panelMaterialSetPanel.transform.GetChild(i).gameObject);
            }

            int _child1 = m_materialSetPanel.transform.childCount;
            for (int i = 0; i < _child1; i++)
            {
                Destroy(m_materialSetPanel.transform.GetChild(i).gameObject);
            }
        }

        if (m_tmpProductData.m_materialAmount0 != 0)
        {
            GameDataManager.Instance.m_materialDic.TryGetValue(m_tmpProductData.m_materialCode0, out m_tmpMaterialData);
            SetPanelProductInfo(0, argVar);
        }
        if (m_tmpProductData.m_materialAmount1 != 0)
        {
            GameDataManager.Instance.m_materialDic.TryGetValue(m_tmpProductData.m_materialCode1, out m_tmpMaterialData);
            SetPanelProductInfo(1, argVar);
        }
        if (m_tmpProductData.m_materialAmount2 != 0)
        {
            GameDataManager.Instance.m_materialDic.TryGetValue(m_tmpProductData.m_materialCode2, out m_tmpMaterialData);
            SetPanelProductInfo(2, argVar);
        }
        if (m_tmpProductData.m_materialAmount3 != 0)
        {
            GameDataManager.Instance.m_materialDic.TryGetValue(m_tmpProductData.m_materialCode3, out m_tmpMaterialData);
            SetPanelProductInfo(3, argVar);
        }
        if (m_tmpProductData.m_materialAmount4 != 0)
        {
            GameDataManager.Instance.m_materialDic.TryGetValue(m_tmpProductData.m_materialCode4, out m_tmpMaterialData);
            SetPanelProductInfo(4, argVar);
        }
        if (m_tmpProductData.m_materialAmount5 != 0)
        {
            GameDataManager.Instance.m_materialDic.TryGetValue(m_tmpProductData.m_materialCode5, out m_tmpMaterialData);
            SetPanelProductInfo(5, argVar);
        }
    }
}
