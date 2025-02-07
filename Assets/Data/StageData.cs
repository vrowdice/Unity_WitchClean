using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage Data", order = 1, menuName = "Create Stage Data")]
public class StageData : ScriptableObject
{
    /// <summary>
    /// 스테이지 코드
    /// </summary>
    public int m_stageCode = 0;

    /// <summary>
    /// 층
    /// </summary>
    public int m_floor;

    /// <summary>
    /// 미니게임 타입 리스트
    /// </summary>
    public List<MiniGameType.GAME_TYPE> m_miniType = new List<MiniGameType.GAME_TYPE>();

    /// <summary>
    /// 미니게임 레벨 리스트
    /// </summary>
    public List<int> m_miniLevel = new List<int>();
}
