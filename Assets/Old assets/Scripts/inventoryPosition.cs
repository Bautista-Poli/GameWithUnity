using UnityEngine;
using UnityEngine.UI;

public class InventoryPositioner : MonoBehaviour
{
    public RectTransform slot1, slot2, slot3, slot4;

    void Start()
    {
        PositionSlot(slot1, -180);
        PositionSlot(slot2, -60);
        PositionSlot(slot3, 60);
        PositionSlot(slot4, 180);
    }

    void PositionSlot(RectTransform slot, float xPos)
    {
        slot.anchorMin = new Vector2(0.5f, 0f);
        slot.anchorMax = new Vector2(0.5f, 0f);
        slot.pivot = new Vector2(0.5f, 0.5f);
        slot.anchoredPosition = new Vector2(xPos, 80);
        slot.sizeDelta = new Vector2(60, 60);
    }
}
