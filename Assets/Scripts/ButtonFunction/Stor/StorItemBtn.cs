using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorItemBtn : MonoBehaviour
{
    /// <summary>
    /// 아이템 코드
    /// </summary>
    public int m_ownItemCode = 0;

    /// <summary>
    /// 클릭 시
    /// </summary>
    public void Click()
    {
        ItemManager.Instance.SetBuyPanel(m_ownItemCode);
    }
}
