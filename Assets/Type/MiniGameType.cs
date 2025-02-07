using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameType : MonoBehaviour
{
    public enum GAME_TYPE
    {
        spider_repelle,
        potion_arrangement,
        material_classification,
        bookcase_tidy
    }

    public enum ACHIVE_TYPE
    {
        clear_sec,
        clear_no_item,
        clear_no_continue
    }
}
