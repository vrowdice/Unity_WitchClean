using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestIndexBtn : MonoBehaviour
{
    /// <summary>
    /// 상자 인덱스
    /// </summary>
    public int m_chestIndex = 0;

    public void Click()
    {
        ItemManager.Instance.m_nowChestIndex = m_chestIndex;
        ItemManager.Instance.UpdateChest();
    }
}
