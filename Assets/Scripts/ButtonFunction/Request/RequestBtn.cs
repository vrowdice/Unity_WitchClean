using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestBtn : MonoBehaviour
{
    /// <summary>
    /// 의뢰 매니저
    /// </summary>
    public RequestManager m_requestManager = null;

    /// <summary>
    /// 의뢰 코드
    /// </summary>
    public int m_requestCode = 0;

    /*
    /// <summary>
    /// 필요한 제작품 코드
    /// </summary>
    public int[] m_needProductCode = new int[5];

    /// <summary>
    /// 필요한 제작품 갯수
    /// </summary>
    public int[] m_needProductAmount = new int[5];

    /// <summary>
    /// 보상할 콜랙션 코드
    /// </summary>
    public int m_rewardCollectionCode = 0;

    /// <summary>
    /// 자기 자신의 순서
    /// </summary>
    public int m_ownIndex = 0;
    */

    /// <summary>
    /// 클릭 시
    /// </summary>
    public void Click()
    {
        m_requestManager.GetReward(m_requestCode);
    }
}
