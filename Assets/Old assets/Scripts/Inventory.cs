using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // ← AÑADIR

public class Inventory : MonoBehaviour
{
    int[] matCounts = new int[] { 0, 0, 0, 0 };

    public BlockType[] matTypes;
    public Image[] invImgs;
    public Image[] matImgs;

    int curMat;

    void Start()
    {
        foreach(Image img in matImgs)
        {
            img.gameObject.SetActive(false);
        }
        
        // AÑADIR - Bloques iniciales para testing
        for(int i = 0; i < 10; i++)
        {
            AddToInventory(BlockType.Grass);
            AddToInventory(BlockType.Stone);
            AddToInventory(BlockType.Trunk);
            AddToInventory(BlockType.Leaves);
        }
        
        Debug.Log("Inventario inicializado con bloques");
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        if(keyboard.digit1Key.wasPressedThisFrame)
            SetCur(0);
        else if(keyboard.digit2Key.wasPressedThisFrame)
            SetCur(1);
        else if(keyboard.digit3Key.wasPressedThisFrame)
            SetCur(2);
        else if(keyboard.digit4Key.wasPressedThisFrame)
            SetCur(3);
    }

    void SetCur(int i)
    {
        invImgs[curMat].color = new Color(0, 0, 0, 43/255f);

        curMat = i;
        invImgs[i].color = new Color(0, 0, 0, 80/255f);
    }

    public bool CanPlaceCur()
    {
        return matCounts[curMat] > 0;
    }

    public BlockType GetCurBlock()
    {
        return matTypes[curMat];
    }

    public void ReduceCur()
    {
        matCounts[curMat]--;

        if(matCounts[curMat] == 0)
            matImgs[curMat].gameObject.SetActive(false);
    }

    public void AddToInventory(BlockType block)
    {
        int i = 0;
        if(block == BlockType.Stone)
            i = 1;
        else if(block == BlockType.Trunk)
            i = 2;
        else if(block == BlockType.Leaves)
            i = 3;

        matCounts[i]++;
        if(matCounts[i] == 1)
            matImgs[i].gameObject.SetActive(true);
    }
}
