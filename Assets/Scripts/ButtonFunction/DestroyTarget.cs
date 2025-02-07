using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTarget : MonoBehaviour
{
    /// <summary>
    /// 타겟
    /// </summary>
    public GameObject m_target = null;

    /// <summary>
    /// 클릭 시
    /// </summary>
    public void Click()
    {
        Destroy(m_target);
    }
}
