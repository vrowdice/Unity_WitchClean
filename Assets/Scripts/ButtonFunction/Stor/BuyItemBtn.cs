using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItemBtn : MonoBehaviour
{
    /// <summary>
    /// 받아올 아이탬 코드
    /// </summary>
    public int m_itemCode = 0;

    /// <summary>
    /// 클릭 시
    /// </summary>
    public void Click()
    {
        ItemManager.Instance.AddItem(m_itemCode, 1);
    }
}
