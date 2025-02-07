using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCheat : MonoBehaviour
{

    /// <summary>
    /// 아이템 100씩 추가
    /// </summary>
    public bool m_item100 = false;

    /// <summary>
    /// 재료 500씩 추가
    /// </summary>
    public bool m_material500 = false;

    /// <summary>
    /// 재작품 100씩 추가
    /// </summary>
    public bool m_product100 = false;

    /// <summary>
    /// 모든 콜랙션 추가
    /// </summary>
    public bool m_allCollection = false;

    private void Start()
    {
        Invoke("SetCheat", 0.5f);
    }

    /// <summary>
    /// 치트 적용
    /// </summary>
    public void SetCheat()
    {
        if(m_item100 == true)
        {
            for (int i = 0; i < GameDataManager.Instance.m_itemAmountDic.Count; i++)
            {
                GameDataManager.Instance.m_itemAmountDic[i] += 100;
            }
        }
        if(m_material500 == true)
        {
            for(int i = 0; i < GameDataManager.Instance.m_materialAmountDic.Count; i++)
            {
                GameDataManager.Instance.m_materialAmountDic[i] += 500;
            }
        }
        if(m_product100 == true)
        {
            for(int i = 0; i < GameDataManager.Instance.m_productAmountDic.Count; i++)
            {
                GameDataManager.Instance.m_productAmountDic[i] += 100;
            }
        }
        if (m_allCollection == true)
        {
            for (int i = 0; i < GameDataManager.Instance.m_collectionAmountDic.Count; i++)
            {
                GameDataManager.Instance.m_collectionAmountDic[i] += 1;
            }
        }
    }
}
