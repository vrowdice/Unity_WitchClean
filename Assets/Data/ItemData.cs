using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", order = 1, menuName = "Create Item Data")]
public class ItemData : ScriptableObject
{
    /// <summary>
    /// 아이탬 코드
    /// </summary>
    public int m_itemCode = 0;

    /// <summary>
    /// 아이탬 이미지
    /// </summary>
    public Sprite m_itemImage = null;

    /// <summary>
    /// 아이탬 이름
    /// </summary>
    public string m_itemName = string.Empty;

    /// <summary>
    /// 아이탬 가격
    /// </summary>
    public long m_itemPrice = 0;

    /// <summary>
    /// 아이템이 메인화면 씬에서 사용 가능한지를 결정
    /// </summary>
    public bool m_itemMainUse = false;

    /// <summary>
    /// 아이템 설명
    /// </summary>
    [TextArea]
    public string m_itemExplain = string.Empty;
}
