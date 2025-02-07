using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    /// <summary>
    /// 처음 시작 시
    /// </summary>
    public bool m_first = false;

    /// <summary>
    /// 저장할 돈
    /// </summary>
    public long m_money = 0;

    /// <summary>
    /// 저장할 최대 체력
    /// </summary>
    public int m_maxHealth = 0;

    /// <summary>
    /// 저장할 현재 체력
    /// </summary>
    public int m_nowHealth = 0;

    /// <summary>
    /// 저장할 최대 경험치
    /// </summary>
    public long m_maxExp = 0;

    /// <summary>
    /// 저장할 현재 경험치
    /// </summary>
    public long m_nowExp = 0;

    /// <summary>
    /// 저장할 레벨
    /// </summary>
    public int m_level = 0;

    /// <summary>
    /// 저장할 분
    /// </summary>
    public string m_time = String.Empty;

    /// <summary>
    /// 플래이 시간
    /// </summary>
    public long m_totalTime = 0;

    /// <summary>
    /// 아이템 코드
    /// </summary>
    public List<int> m_itemCode = new List<int>();

    /// <summary>
    /// 아이템 갯수
    /// </summary>
    public List<int> m_itemAmount = new List<int>();

    /// <summary>
    /// 제작품 코드
    /// </summary>
    public List<int> m_productCode = new List<int>();

    /// <summary>
    /// 제작품 갯수
    /// </summary>
    public List<int> m_productAmount = new List<int>();

    /// <summary>
    /// 재료의 코드
    /// </summary>
    public List<int> m_materialCode = new List<int>();

    /// <summary>
    /// 재료 갯수
    /// </summary>
    public List<int> m_materialAmount = new List<int>();

    /// <summary>
    /// 콜렉션 코드
    /// </summary>
    public List<int> m_collectionCode = new List<int>();

    /// <summary>
    /// 콜렉션 갯수
    /// </summary>
    public List<int> m_collectionAmount = new List<int>();

    /// <summary>
    /// 스테이지 코드
    /// </summary>
    public List<int> m_stageCode = new List<int>();

    /// <summary>
    /// 스테이지 클리어
    /// </summary>
    public List<bool> m_stageClear = new List<bool>();

    /// <summary>
    /// 의뢰 인덱스
    /// </summary>
    public List<int> m_requestCode = new List<int>();

    /// <summary>
    /// 의뢰 생성 시간
    /// </summary>
    public List<int> m_requestTime = new List<int>();

    /// <summary>
    /// 생성자
    /// </summary>
    public SaveData()
    {

    }

    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="argData"></param>
    public SaveData(string argData)
    {
        SaveData _data = JsonUtility.FromJson<SaveData>(argData);

        m_first = _data.m_first;

        m_money = _data.m_money;
        m_maxHealth = _data.m_maxHealth;
        m_nowHealth = _data.m_nowHealth;
        m_nowExp = _data.m_nowExp;
        m_maxExp = _data.m_maxExp;
        m_level = _data.m_level;
        m_time = _data.m_time;
        m_totalTime = _data.m_totalTime;

        m_itemCode = _data.m_itemCode;
        m_itemAmount = _data.m_itemAmount;
        m_productCode = _data.m_productCode;
        m_productAmount = _data.m_productAmount;
        m_materialCode = _data.m_materialCode;
        m_materialAmount = _data.m_materialAmount;
        m_collectionCode = _data.m_collectionCode;
        m_collectionAmount = _data.m_collectionAmount;
        m_stageCode = _data.m_stageCode;
        m_stageClear = _data.m_stageClear;
        m_requestCode = _data.m_requestCode;
        m_requestTime = _data.m_requestTime;
    }

    /// <summary>
    /// Json 문자열 데이터 변환
    /// </summary>
    /// <returns>문자열 데이터</returns>
    public string ToJsonString()
    {
        return JsonUtility.ToJson(this);
    }
}