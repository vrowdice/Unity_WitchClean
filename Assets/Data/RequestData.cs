using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Request Data", order = 1, menuName = "Create Request Data")]
public class RequestData : ScriptableObject
{
    /// <summary>
    /// 자신의 의뢰 코드
    /// </summary>
    public int m_requestCode = 0;

    /// <summary>
    /// 의뢰하는 사람
    /// </summary>
    public int m_requestPerson = 0;

    /// <summary>
    /// 필요한 제작품 코드
    /// </summary>
    public int[] m_needProductCode = new int[0];

    /// <summary>
    /// 필요한 제작품 갯수
    /// </summary>
    public int[] m_needProductAmount = new int[0];

    /// <summary>
    /// 보상할 콜랙션 코드
    /// </summary>
    public int m_rewardCollectionCode = 0;

    /// <summary>
    /// 의뢰 설명
    /// </summary>
    [TextArea]
    public string m_explain = string.Empty;
}
