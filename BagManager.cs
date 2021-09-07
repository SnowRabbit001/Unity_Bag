using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    public BagUnit m_CloneUnit = null; //複製的物件
    public Transform m_BagNode = null; //複製到哪裡

    public Sprite[] m_TotalItemSprite = null; //圖片列表

    public List<ItemData> m_TotalItemData = new List<ItemData>(); //物品資料儲存列表
    public List<BagUnit> m_TotalItemBagUnit = new List<BagUnit>(); //物品狀態儲存列表

    public bool m_IsLeft = true; //默認左裝備欄優先

    public BagUnit m_LeftUnit = null; //左裝備欄
    public BagUnit m_RightUnit = null; //右裝備欄

    //裝備欄初始化
    private void Awake()
    {
        m_LeftUnit.Refresh(null);
        m_RightUnit.Refresh(null);
    }

    private void Start()
    {
        for (int i = 0; i < 40; i++)
        {
            ItemData id = new ItemData(); //生成物品資料
            //隨機取圖片
            int r = Random.Range(0, m_TotalItemSprite.Length);
            Sprite s = m_TotalItemSprite[r];
            id.m_Icon = s; //設定圖片
            id.m_Count = 1; //設定數量
            id.m_ItemTag = r; //設定Tag(ID)
            m_TotalItemData.Add(id); //存進物品資料列表
        }

        m_TotalItemData.Sort(SortItem); //物品排序

        for (int i = 0; i < m_TotalItemData.Count; i++)
        {
            BagUnit unit = Instantiate<BagUnit>(m_CloneUnit, m_BagNode); //生成物品
            unit.Refresh(m_TotalItemData[i]); //刷新物品顯示
            m_TotalItemBagUnit.Add(unit); //存進物品狀態列表
        }
    }

    //更新物品狀態
    private void RefreshList()
    {
        for (int i = 0; i < m_TotalItemBagUnit.Count; i++)
        {
            m_TotalItemBagUnit[i].RefreshEquip();
        }
    }

    //排序
    public int SortItem(object a, object b)
    {
        ItemData i1 = a as ItemData;
        ItemData i2 = b as ItemData;

        if(i1.m_ItemTag > i2.m_ItemTag)
        {
            return 1;
        }
        else if (i1.m_ItemTag < i2.m_ItemTag)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    //點擊物品
    public void OnClickUnit(BagUnit unit)
    {
        unit.OnEquip(true); //裝備物品
        if(m_IsLeft)
        {
            //卸下左裝備欄原物品
            if (m_LeftUnit.m_TempData != null)
            {
                m_LeftUnit.OnEquip(false);
            }
            m_LeftUnit.Refresh(unit.m_TempData); //刷新左裝備欄
        }
        else
        {
            //卸下右裝備欄原物品
            if (m_RightUnit.m_TempData != null)
            {
                m_RightUnit.OnEquip(false);
            }
            m_RightUnit.Refresh(unit.m_TempData); //刷新右裝備欄
        }
        RefreshList(); //刷新物品狀態
    }

    //左裝備欄優先(點擊)
    public void OnClickLeft()
    {
        m_IsLeft = true;
    }

    //右裝備欄優先(點擊)
    public void OnClickRight()
    {
        m_IsLeft = false;
    }
}
