using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CustomScrollable : MonoBehaviour
{
    public Scrollbar scrollbar;
    public RectTransform scrollable;
    public float sensibility = 1f;
    private Vector2 startPos;
    public Vector2 maxPos;
    private float progress;
    
    [Header("Menu parameters")]
    public int menuIndex;
    public int columnIndex;
    private Menu refMenu;
    
    public enum scrollControl {MouseWheel, Keys, Menu, ScrollBar}

    public scrollControl[] ScrollControls;
    
    void Start()
    {
        if (scrollable != null)
        {
            startPos = scrollable.position;
        }
        maxPos = startPos + maxPos;
        
        if (scrollbar != null && ScrollControls.Contains(scrollControl.ScrollBar))
        {
            scrollbar.onValueChanged.AddListener(f => progress = f);
        }

        if (ScrollControls.Contains(scrollControl.Menu))
        {
            Gears.gears.playerInput.actions["Move"].performed += context => UpdateProgressMenu();
        }

        refMenu = Gears.gears.menuManager.menus.Find(menu => menu.index == menuIndex);
    }
    
    void Update()
    {
        for (int i = 0; i < ScrollControls.Count(); i++)
        {
            switch (ScrollControls[i])
            {
                case scrollControl.Keys :
                    progress -= Gears.gears.playerInput.actions["Move"].ReadValue<Vector2>().y * 0.01f * sensibility;
                    break;
                case scrollControl.MouseWheel :
                    progress -= Mouse.current.scroll.ReadValue().y * 0.001f * sensibility;
                    break;
                case scrollControl.ScrollBar :
                    if (scrollbar != null)
                    {
                        scrollbar.value = progress;
                    }
                    break;
            }
        }

        progress = Mathf.Clamp(progress, 0f, 1f);

        if (scrollable != null)
        {
            scrollable.position = Vector2.Lerp(startPos, maxPos, progress);
        }
    }

    public void UpdateProgressMenu()
    {
        if (Gears.gears.menuManager.selection.posOnMap.x == columnIndex)
        {
            progress = Gears.gears.menuManager.selection.posOnMap.y / (float) (refMenu.menuMap[columnIndex].list.Count - 1);
        }
    }
}