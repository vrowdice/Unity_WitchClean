using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionBtn : MonoBehaviour
{
    /// <summary>
    /// 수집품 코드
    /// </summary>
    public int m_collectionCode = 0;

    public void Click()
    {
        CollectionManager.Instance.CollectionBtnClick(m_collectionCode);
    }
}
