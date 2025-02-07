using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material Data", order = 1, menuName = "Create Material Data")]
public class MaterialData : ScriptableObject
{
    /// <summary>
    /// 재료 코드
    /// </summary>
    public int m_materialCode = 0;

    /// <summary>
    /// 재료 스프라이트
    /// </summary>
    public Sprite m_materialSprite = null;

    /// <summary>
    /// 재료 이름
    /// </summary>
    public string m_materialName = string.Empty;

    /// <summary>
    /// 재료 설명
    /// </summary>
    [TextArea]
    public string m_materialExplain = string.Empty;
}
