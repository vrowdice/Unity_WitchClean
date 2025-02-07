using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "People Data", order = 1, menuName = "Create People Data")]
public class PeopleData : ScriptableObject
{
    /// <summary>
    /// 사람 인덱스
    /// </summary>
    public int m_personCode = 0;

    /// <summary>
    /// 사람 이름
    /// </summary>
    public string m_personName = string.Empty;

    /// <summary>
    /// 사람 이미지
    /// </summary>
    public Sprite m_personImage = null;
}
