using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Scrollable : MonoBehaviour
{
    public Scrollbar scrollbar;
    public RectTransform scrollable;
    public float sensibility = 1f;
    private Vector2 startPos;
    public Vector2 maxPos;
    private float progress;
    
    void Start()
    {
        startPos = scrollable.position;
        maxPos = startPos + maxPos;
        
        if (scrollbar != null)
        {
            scrollbar.onValueChanged.AddListener(f => progress = f);
        }
    }
    
    void Update()
    {
        progress -= Mouse.current.scroll.ReadValue().y * 0.001f * sensibility;
        progress -= Gears.gears.playerInput.actions["Move"].ReadValue<Vector2>().y * 0.01f * sensibility;
        
        if (scrollbar != null)
        {
            scrollbar.value = progress;
        }

        progress = Mathf.Clamp(progress, 0f, 1f);
        
        scrollable.position = Vector2.Lerp(startPos, maxPos, progress);
    }
}
