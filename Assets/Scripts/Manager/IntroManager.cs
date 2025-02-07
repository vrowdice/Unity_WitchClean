using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    /// <summary>
    /// 인트로 스프라이트
    /// </summary>
    public Sprite[] m_introSprite = null;

    /// <summary>
    /// 다음 인트로 인덱스 넘어가는 시간
    /// </summary>
    public float m_introTime = 0.0f;

    /// <summary>
    /// 인트로 이미지
    /// </summary>
    Image m_intro = null;

    /// <summary>
    /// 인트로 인덱스
    /// </summary>
    int m_introindex = 0;

    private void Start()
    {
        m_intro = gameObject.GetComponent<Image>();
        m_introindex = 0;
        m_intro.sprite = m_introSprite[m_introindex];
        InvokeRepeating("NextIntro", m_introTime, m_introTime);
    }

    void NextIntro()
    {
        if(m_introSprite.Length - 1 <= m_introindex)
        {
            CancelInvoke();
            gameObject.SetActive(false);
            return;
        }
        m_introindex++;
        m_intro.sprite = m_introSprite[m_introindex];
    }
}
