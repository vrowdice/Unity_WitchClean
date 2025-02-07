using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItemBtn : MonoBehaviour
{
    /// <summary>
    /// 아이탬 코드
    /// </summary>
    public int m_code = 0;

    /// <summary>
    /// 버튼 클릭 시
    /// </summary>
    public void Click()
    {
        ItemManager.Instance.SetChestPanel(m_code);
    }
}
