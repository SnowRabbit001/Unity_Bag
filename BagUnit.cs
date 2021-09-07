using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUnit : MonoBehaviour
{
    public Image m_Icon = null;
    public Text m_CountText = null;
    public Text m_EquipText = null;

    public ItemData m_TempData = null; //暫存物品資料

    //更新物品狀態
    public void Refresh(ItemData itemData)
    {
        //初始化(空)
        if (itemData == null)
        {
            m_Icon.sprite = null;
            m_CountText.text = "";
            m_EquipText.text = "";
            return;
        }
        //更新
        m_TempData = itemData;
        m_Icon.sprite = itemData.m_Icon;
        m_CountText.text = itemData.m_Count.ToString();
        m_EquipText.text = "";
    }

    //裝備物品
    public void OnEquip(bool equip)
    {
        m_TempData.m_Equip = equip;
        RefreshEquip();
    }

    //更新裝備狀態文字
    public void RefreshEquip()
    {
        if(m_TempData.m_Equip)
        {
            m_EquipText.text = "裝備中";
        }
        else
        {
            m_EquipText.text = "";
        }
    }
}
