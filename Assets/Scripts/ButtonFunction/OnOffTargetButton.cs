/*
 타겟이 엑티브 상태이면 엑티브 false 아니면 true
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffTargetButton : MonoBehaviour
{
    /// <summary>
    /// 게임오브젝트 타겟
    /// </summary>
    public GameObject m_target = null;

    /// <summary>
    /// 클릭 시
    /// </summary>
    public void Click()
    {
        if(m_target.activeSelf == false)
        {
            m_target.SetActive(true);
        }
        else
        {
            m_target.SetActive(false);
        }
    }
}
