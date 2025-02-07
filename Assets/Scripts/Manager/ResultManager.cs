using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    int _count = 0;
    long addold = 0;
    long adddExp = 0;

    public Slider expBar;
    public Text GoldText;
    public Text EXPText;
    public GameObject resultItemSlot;
    public GameObject[] resultItem;

    GameDataManager m_gameDataManager = null;

    void Start()
    {
        m_gameDataManager = GameDataManager.Instance;
        addold = GameDataManager.Instance.m_toAddMoney;
        adddExp = GameDataManager.Instance.m_toAddExp;
        Debug.Log("money" + GameDataManager.Instance.m_toAddMoney);
        Debug.Log("exp" + GameDataManager.Instance.m_toAddExp);
        Debug.Log("maxexp" + PlayerValueManager.Instance.IsMaxExp);
        Debug.Log("nowexp" + PlayerValueManager.Instance.IsNowExp);
    }

    void Update()
    {
        expBar.maxValue = PlayerValueManager.Instance.IsMaxExp;
        expBar.value = PlayerValueManager.Instance.IsNowExp;
        GoldText.text = addold.ToString();
        GoldText.text = string.Format("+{0:n0}", addold);
        EXPText.text = adddExp.ToString();
        EXPText.text = string.Format("X{0:n0}", adddExp);



        if (Glober.gameState == 0)
        {
            
            ++_count;
            if (_count > 1)
            {
                return;
            }
            ResultItem();
        }else if(Glober.gameState == 1)
        {
            GoldText.text = "0";
            EXPText.text = "0";
        }  
    }

    void ResultItem()
    {
        switch (Glober.gameValue)
        {
            case 0:

                Instantiate(resultItem[0], resultItemSlot.transform);
                m_gameDataManager.m_materialAmountDic[0] += 1;
                break;
            case 1:
                Instantiate(resultItem[1], resultItemSlot.transform);
                Instantiate(resultItem[2], resultItemSlot.transform);
                GameDataManager.Instance.m_materialAmountDic[1] += 1;
                GameDataManager.Instance.m_materialAmountDic[2] += 1;
                break;
            case 2:
                Instantiate(resultItem[3], resultItemSlot.transform);
                Instantiate(resultItem[4], resultItemSlot.transform);
                GameDataManager.Instance.m_materialAmountDic[3] += 1;
                GameDataManager.Instance.m_materialAmountDic[4] += 1;
                break;
            case 3:
                Instantiate(resultItem[5], resultItemSlot.transform);
                Instantiate(resultItem[6], resultItemSlot.transform);
                GameDataManager.Instance.m_materialAmountDic[5] += 1;
                GameDataManager.Instance.m_materialAmountDic[6] += 1;
                break;
        }
    }
    public void Clear()
    {
        ScenManager.Instance.LoadScene("Main");
    }
}
