using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collection Data", order = 1, menuName = "Create Collection Data")]
public class CollectionData : ScriptableObject
{
    /// <summary>
    /// 콜랙션 코드
    /// </summary>
    public int m_collectionCode = 0;

    /// <summary>
    /// 수집품이 표시될 매뉴 숫자
    /// </summary>
    public int m_collectionMenu = 0;

    /// <summary>
    /// 콜랙션 스프라이트
    /// </summary>
    public Sprite m_collectionSprite = null;

    /// <summary>
    /// 수집품 이름
    /// </summary>
    public string m_collectionName = string.Empty;

    /// <summary>
    /// 수집품 설명
    /// </summary>
    [TextArea]
    public string m_collectionExplain = string.Empty;
}
