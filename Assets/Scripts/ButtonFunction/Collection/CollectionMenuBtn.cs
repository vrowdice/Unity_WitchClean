using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionMenuBtn : MonoBehaviour
{
    /// <summary>
    /// 수집품 매뉴 인덱스
    /// </summary>
    public int m_collectionMenuIndex = 0;

    public void Click()
    {
        CollectionManager.Instance.CollectionMenuClick(m_collectionMenuIndex);
    }
}
