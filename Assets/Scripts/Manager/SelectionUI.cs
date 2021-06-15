using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class SelectionUI : MonoBehaviour
{
    public MenuManager menuManager;

    public float scaleMultiplierX = 1.1f;
    public float scaleMultiplierY = 1.1f;

    public float textScaleMulti = 1.1f;

    public Vector2Int posOnMap;

    public Color idleColor;
    public Color pressedColor;

    private Image _image;
    private RectTransform _rectTransform;
    
    public RectTransform selectionUi;
    private Vector3 selectionUiBaseScale;
    public float scaleIncrease = 1.1f;
    public float increaseDuration = 0;
    public RectTransform arrow;
    public bool placeArrowOnText;

    private Action<InputAction.CallbackContext> action1;
    private Action<InputAction.CallbackContext> action2;

    void Awake()
    {
       
    }
    
    void OnDestroy()
    {
        if (Gears.gears.playerInput != null)
        {
            Gears.gears.playerInput.actions["Enter"].performed -= action1;
            //Gears.gears.playerInput.actions["MoveMenu"].performed -= action2;
            Gears.gears.playerInput.actions["Move"].performed -= action2;
        }
    }

    void Start()
    {
        //Gears.gears.playerInput.actions["MoveMenu"].performed += context => MoveMapPosition(
        //new Vector2Int((int) context.ReadValue<Vector2>().x, (int) context.ReadValue<Vector2>().y));
        
        //Gears.gears.playerInput.actions["Enter"].performed += context => TriggerSelection();
        
        action1 = context => TriggerSelection();
        action2 = context => MoveMapPosition(new Vector2Int((int) context.ReadValue<Vector2>().x, (int) context.ReadValue<Vector2>().y));

        Gears.gears.playerInput.actions["Enter"].performed += action1;
        //Gears.gears.playerInput.actions["MoveMenu"].performed += action2;
        Gears.gears.playerInput.actions["Move"].performed += action2;

        //LevelManager.preLoadingScene += () => Gears.gears.playerInput.actions["Enter"].performed -= action1;
        //LevelManager.preLoadingScene += () => Gears.gears.playerInput.actions["MoveMenu"].performed -= action2;
        
        _rectTransform = GetComponent<RectTransform>();
        menuManager = Gears.gears.menuManager;

        if (selectionUi != null)
        {
            _image = selectionUi?.GetComponent<Image>();
            selectionUiBaseScale = selectionUi.localScale;
            //StartCoroutine(RescaleOverTime(selectionUi.gameObject, selectionUiBaseScale, selectionUiBaseScale * scaleIncrease, increaseDuration, true));
        }
    }
    
    void Update()
    {
        if (MenuManager.ObjectUnderCursor() != null)
        {
            for (int i = 0; i < menuManager.currentMap.map.GetLength(0); i++)
            {
                for (int j = 0; j < menuManager.currentMap.map.GetLength(1); j++)
                {
                    if (menuManager.currentMap.map[i, j] != null && (MenuManager.ObjectUnderCursor() == menuManager.currentMap.map[i, j].gameObject || 
                                                                     MenuManager.ObjectUnderCursor().transform.parent.gameObject == menuManager.currentMap.map[i, j].gameObject))
                    {
                        if (posOnMap != new Vector2Int(i, j))
                        {
                            if (MenuManager.GetAllComponentInChilds<TextMeshProUGUI>(menuManager.currentMap.map[posOnMap.x, posOnMap.y].gameObject, useParent: true).Capacity > 0)
                            {
                                menuManager.currentMap.map[posOnMap.x, posOnMap.y].transform.localScale /= textScaleMulti;
                            }
                            posOnMap = new Vector2Int(i, j);
                            UpdateDisplayScalePosition();
                        }
                    }
                }
            }
        }
    }

    public void TriggerSelection()
    {
        Vector2Int v = posOnMap;
        
        if (MenuManager.GetAllComponentInChilds<TextMeshProUGUI>(menuManager.currentMap.map[posOnMap.x, posOnMap.y].gameObject, useParent: true).Capacity > 0)
        {
            menuManager.currentMap.map[posOnMap.x, posOnMap.y].transform.localScale /= textScaleMulti;
            //Debug.Log(menuManager.currentMap.map[posOnMap.x, posOnMap.y].transform.localScale);
        }
        
        if (menuManager.currentMap.map[posOnMap.x, posOnMap.y].TryGetComponent(out Button button) && button != null)
        {
            //Debug.Log(button.gameObject.name);
            button.onClick?.Invoke();
        }

        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(SelectionColor());
        }

        if (posOnMap != v)
        {
            UpdateDisplayScalePosition();
        }
    }

    public IEnumerator RescaleOverTime(GameObject go, Vector3 startScale, Vector3 endScale, float duration, bool loop = false)
    {
        if (!go.activeSelf)
        {
            go.SetActive(true);
        }

        go.transform.localScale = startScale;
        
        for (float t = 0f; t < duration; t += Time.deltaTime) 
        {
            float normalizedTime = t/duration;

            if (go)
            {
                go.transform.localScale = Vector3.Lerp(startScale, endScale, normalizedTime);
            }
            else
            {
                break;
            }
         
            yield return null;
        }

        if (go)
        {
            go.transform.localScale = endScale;

            if (loop)
            {
                StartCoroutine(RescaleOverTime(go, endScale, startScale, duration, true));
            }
        }
    }

    public IEnumerator SelectionColor()
    {
        _image.color = pressedColor;
        
        yield return new WaitForSecondsRealtime(0.1f);
        
        //Debug.Log("set color to idle");
        _image.color = idleColor;
    }

    public void MoveMapPosition(Vector2Int vector2I)
    {
        if (Gears.gears.playerInput.currentActionMap.name == "Menu")
        {
            if (menuManager.currentMap.map[posOnMap.x, posOnMap.y] != null 
                && (menuManager.currentMap.map[posOnMap.x, posOnMap.y].GetComponent<TextMeshProUGUI>() != null || 
                    MenuManager.GetAllComponentInChilds<TextMeshProUGUI>(menuManager.currentMap.map[posOnMap.x, posOnMap.y].gameObject).Capacity > 0))
            {
                menuManager.currentMap.map[posOnMap.x, posOnMap.y].transform.localScale /= textScaleMulti;
            }
            //Debug.Log("MoveMapPos : " + vector2I);
            Vector2Int vector2Int = new Vector2Int(vector2I.x, -vector2I.y);
        
            if (menuManager.currentMap.map.GetLength(0) > posOnMap.x + vector2Int.x)
            {
                if (posOnMap.x + vector2Int.x >= 0)
                {
                    posOnMap.x += vector2Int.x;
                }
                else
                {
                    posOnMap.x = menuManager.currentMap.map.GetLength(0) - 1;
                }
            }
            else
            {
                posOnMap.x = 0;
            }

            if (menuManager.currentMap.map.GetLength(1) > posOnMap.y + vector2Int.y)
            {
                if (posOnMap.y + vector2Int.y >= 0)
                {
                    posOnMap.y += vector2Int.y;
                }
                else
                {
                    posOnMap.y = menuManager.currentMap.map.GetLength(1) - 1;
                }
            }
            else
            {
                posOnMap.y = 0;
            }
        
            if (menuManager.currentMap.map[posOnMap.x, posOnMap.y] != null && menuManager.currentMap.map[posOnMap.x, posOnMap.y].gameObject.activeSelf)
            {
                UpdateDisplayScalePosition();
            }
            else
            {
                MoveMapPosition(vector2I);
            }
        }
    }
    
    public void UpdateDisplayScalePosition()
    {
        //Debug.Log(menuManager.currentMap.map[posOnMap.x, posOnMap.y]);
        //Debug.Log(transform.parent + " pos On Map : " + menuManager.currentMap.map[posOnMap.x, posOnMap.y]);
        if (menuManager.currentMap.map[posOnMap.x, posOnMap.y] != null && (menuManager.currentMap.map[posOnMap.x, posOnMap.y].GetComponent<TextMeshProUGUI>() != null || 
            MenuManager.GetAllComponentInChilds<TextMeshProUGUI>(menuManager.currentMap.map[posOnMap.x, posOnMap.y].gameObject).Capacity > 0))
        {
            menuManager.currentMap.map[posOnMap.x, posOnMap.y].transform.localScale *= textScaleMulti;
        }

        Vector3 adaptScale = AdaptScale(menuManager.currentMap.map[posOnMap.x, posOnMap.y].gameObject, menuManager.currentMap.map[posOnMap.x, posOnMap.y].localScale);
        
        //adapt selection
        _rectTransform = GetComponent<RectTransform>();
        if (selectionUi != null)
        {
            selectionUi.position = menuManager.currentMap.map[posOnMap.x, posOnMap.y].position;
            selectionUi.sizeDelta = menuManager.currentMap.map[posOnMap.x, posOnMap.y].sizeDelta;
            
            selectionUi.localScale = new Vector3(adaptScale.x * scaleMultiplierX, adaptScale.y * scaleMultiplierY);
            
            selectionUiBaseScale = selectionUi.localScale;
            StopAllCoroutines();
            //StartCoroutine(RescaleOverTime(selectionUi.gameObject, selectionUiBaseScale, selectionUiBaseScale * scaleIncrease, increaseDuration, true));
        }

        if (arrow != null && arrow.gameObject.activeInHierarchy)
        {
            arrow.localScale = new Vector3(adaptScale.x * scaleMultiplierX, adaptScale.x * scaleMultiplierX) * 0.2f;

            float sizeDeltaX = 0;
            
            if (placeArrowOnText)
            {
                TextMeshProUGUI textMeshPro = MenuManager.GetAllComponentInChilds<TextMeshProUGUI>(menuManager.currentMap.map[posOnMap.x, posOnMap.y].gameObject, useParent: true)[0];

                sizeDeltaX = textMeshPro.text.ToCharArray().Count() * textMeshPro.fontSize / 2f * adaptScale.x;
                
                /*Debug.Log(textMeshPro.text.ToCharArray().Count() + " X " + textMeshPro.fontSize + " = " + textMeshPro.text.ToCharArray().Count() * textMeshPro.fontSize + " /2 = " 
                          + textMeshPro.text.ToCharArray().Count() * textMeshPro.fontSize / 2f + " * " + AdaptScale(textMeshPro.gameObject, textMeshPro.gameObject.transform.localScale).x + 
                          " = " + sizeDeltaX);*/
            }
            else
            {
                sizeDeltaX = menuManager.currentMap.map[posOnMap.x, posOnMap.y].sizeDelta.x * adaptScale.x * 
                             scaleMultiplierX / 2f + Screen.width * 0.03f + arrow.sizeDelta.x * arrow.localScale.x / 2f;
            }
            
            //position arrow
            arrow.position = menuManager.currentMap.map[posOnMap.x, posOnMap.y].position + new Vector3(-sizeDeltaX * Screen.width / 1920, 0, 0);
        }

        //Debug.Log(vector2Int + " -> " + posOnMap);
    }

    public Vector3 AdaptScale(GameObject go, Vector3 scale)
    {
        if (go.transform.parent != transform.parent)
        {
            Vector3 childScale = new Vector3(scale.x * go.transform.parent.localScale.x, scale.y * go.transform.parent.localScale.y);
            //Debug.Log($"ChildScale : {childScale}");
            return AdaptScale(go.transform.parent.gameObject, childScale);
        }
        
        //Debug.Log($"finale scale : {scale}");
        return scale;
    }
}
