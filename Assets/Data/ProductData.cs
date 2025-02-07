using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Product Data", order = 1, menuName = "Create Product Data")]
public class ProductData : ScriptableObject
{
    /// <summary>
    /// 제작품 코드
    /// </summary>
    public int m_productCode = 0;

    /// <summary>
    /// 제작품 스프라이트
    /// </summary>
    public Sprite m_productSprite = null;

    /// <summary>
    /// 제작품 이름
    /// </summary>
    public string m_productName = string.Empty;

    /// <summary>
    /// 제작품 설명
    /// </summary>
    [TextArea]
    public string m_productExplain = string.Empty;

    /// <summary>
    /// 필요한 재료0
    /// </summary>
    public int m_materialCode0 = 0;

    /// <summary>
    /// 필요한 재료0
    /// </summary>
    public int m_materialAmount0 = 0;

    /// <summary>
    /// 필요한 재료1
    /// </summary>
    public int m_materialCode1 = 0;

    /// <summary>
    /// 필요한 재료양1
    /// </summary>
    public int m_materialAmount1 = 0;

    /// <summary>
    /// 필요한 재료2
    /// </summary>
    public int m_materialCode2 = 0;

    /// <summary>
    /// 필요한 재료양2
    /// </summary>
    public int m_materialAmount2 = 0;

    /// <summary>
    /// 필요한 재료3
    /// </summary>
    public int m_materialCode3 = 0;

    /// <summary>
    /// 필요한 재료양3
    /// </summary>
    public int m_materialAmount3 = 0;

    /// <summary>
    /// 필요한 재료4
    /// </summary>
    public int m_materialCode4 = 0;

    /// <summary>
    /// 필요한 재료양4
    /// </summary>
    public int m_materialAmount4 = 0;

    /// <summary>
    /// 필요한 재료5
    /// </summary>
    public int m_materialCode5 = 0;

    /// <summary>
    /// 필요한 재료양5
    /// </summary>
    public int m_materialAmount5 = 0;
}
