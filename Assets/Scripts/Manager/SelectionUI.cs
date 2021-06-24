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

    public float arrowScaleMulty;

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

    private Action<InputAction.CallbackContext> triggerSelectionAction;
    private Action<InputAction.CallbackContext> moveMapPositionAction;
    private Action<InputAction.CallbackContext> moveSliderAction;

    public Action onCompleteMove;
    
    void Awake()
    {
       
    }
    
    void OnDestroy()
    {
        if (Gears.gears.playerInput != null)
        {
            Gears.gears.playerInput.actions["Enter"].performed -= triggerSelectionAction;
            //Gears.gears.playerInput.actions["MoveMenu"].performed -= action2;
            Gears.gears.playerInput.actions["Move"].performed -= moveMapPositionAction;
            Gears.gears.playerInput.actions["Move"].performed -= moveSliderAction;
        }
    }

    void Start()
    {
        //Gears.gears.playerInput.actions["MoveMenu"].performed += context => MoveMapPosition(
        //new Vector2Int((int) context.ReadValue<Vector2>().x, (int) context.ReadValue<Vector2>().y));
        
        //Gears.gears.playerInput.actions["Enter"].performed += context => TriggerSelection();
        
        triggerSelectionAction = context => TriggerSelection();
        moveMapPositionAction = context => MoveMapPosition(new Vector2Int((int) context.ReadValue<Vector2>().x, (int) context.ReadValue<Vector2>().y));
        moveSliderAction = context => MoveSlider(menuManager.currentMap.map[posOnMap.x, posOnMap.y].gameObject, context.ReadValue<Vector2>().x, 0.1f);

        Gears.gears.playerInput.actions["Enter"].performed += triggerSelectionAction;
        //Gears.gears.playerInput.actions["MoveMenu"].performed += action2;
        Gears.gears.playerInput.actions["Move"].performed += moveMapPositionAction;
        Gears.gears.playerInput.actions["Move"].performed += moveSliderAction;

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
        //check if mouse is on a button of the menu map
        if (MenuManager.ObjectUnderCursor() != null)
        {
            for (int i = 0; i < menuManager.currentMap.map.GetLength(0); i++)
            {
                for (int j = 0; j < menuManager.currentMap.map.GetLength(1); j++)
                {
                    //if the object in the map is the one under cursor
                    if (menuManager.currentMap.map[i, j] != null && (MenuManager.ObjectUnderCursor() == menuManager.currentMap.map[i, j].gameObject || 
                                                                     MenuManager.ObjectUnderCursor().transform.parent.gameObject == menuManager.currentMap.map[i, j].gameObject))
                    {
                        // if (Input.GetButtonDown("Fire1"))
                        // {
                        //     RescaleObjectText(menuManager.currentMap.map[posOnMap.x, posOnMap.y].gameObject);
                        // }
                        
                        //if selection is not on the object under cursor set selection to the object under cursor
                        if (posOnMap != new Vector2Int(i, j))
                        {
                            RescaleObjectText(menuManager.currentMap.map[posOnMap.x, posOnMap.y].gameObject);
                            posOnMap = new Vector2Int(i, j);
                            UpdateDisplayScalePosition();
                        }
                    }
                }
            }
        }
    }

    public void MoveSlider(GameObject go, float addedValue, float multiply = 1)
    {
        List<Slider> sliders = MenuManager.GetAllComponentInChilds<Slider>(go, useParent: true);
        
        if (sliders.Capacity > 0 && Gears.gears.menuManager.pause)
        {
            sliders[0].value += addedValue * multiply;
            sliders[0].value = Mathf.Clamp(sliders[0].value, 0f, 1f);
        }
    }

    public void RescaleObjectText(GameObject go)
    {
        if (MenuManager.GetAllComponentInChilds<TextMeshProUGUI>(go, useParent: true).Capacity > 0)
        {
            //Debug.LogWarning("Rescale text");
            go.transform.localScale /= textScaleMulti;
            
            // Debug.LogWarning((menuManager.currentMap.map[posOnMap.x, posOnMap.y].gameObject.name + " Scale Down :  " + 
            //                   menuManager.currentMap.map[posOnMap.x, posOnMap.y].transform.localScale));
        }
    }

    public void TriggerSelection()
    {
        //Vector2Int v = posOnMap;

        if (menuManager.currentMap.map[posOnMap.x, posOnMap.y].TryGetComponent(out Button button) && button != null)
        {
            //Debug.Log(button.gameObject.name);
            button.onClick?.Invoke();
        }

        //mimic button click
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(SelectionColor());
        }

        // if (posOnMap != v)
        // {
        //     UpdateDisplayScalePosition();
        // }
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
            if (menuManager.currentMap.map[posOnMap.x, posOnMap.y] != null)
            {
                RescaleObjectText(menuManager.currentMap.map[posOnMap.x, posOnMap.y].gameObject);
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
                onCompleteMove?.Invoke();
                UpdateDisplayScalePosition();
            }
            else
            {
                MoveMapPosition(vector2I);
            }
        }
    }
    
    public void UpdateDisplayScalePosition(bool scaleSelection = true)
    {
        //Debug.Log(menuManager.currentMap.map[posOnMap.x, posOnMap.y]);
        //Debug.Log(transform.parent + " pos On Map : " + menuManager.currentMap.map[posOnMap.x, posOnMap.y]);
        RectTransform rectTransform = menuManager.currentMap.map[posOnMap.x, posOnMap.y];
        
        List<TextMeshProUGUI> textMeshPros = MenuManager.GetAllComponentInChilds<TextMeshProUGUI>(rectTransform.gameObject, useParent: true);
        List<Slider> sliders = MenuManager.GetAllComponentInChilds<Slider>(rectTransform.gameObject, useParent: true);

        if (rectTransform != null && textMeshPros.Capacity > 0 && scaleSelection)
        {
            rectTransform.transform.localScale *= textScaleMulti;
            
            // Debug.LogWarning((menuManager.currentMap.map[posOnMap.x, posOnMap.y].gameObject.name + " Scale Up:  " + 
            //                   menuManager.currentMap.map[posOnMap.x, posOnMap.y].transform.localScale));
        }

        Vector3 adaptScale = AdaptScale(rectTransform.gameObject, rectTransform.localScale);
        
        //adapt selection
        _rectTransform = GetComponent<RectTransform>();
        if (selectionUi != null)
        {
            selectionUi.position = rectTransform.position;
            selectionUi.sizeDelta = rectTransform.sizeDelta;
            
            selectionUi.localScale = new Vector3(adaptScale.x * scaleMultiplierX, adaptScale.y * scaleMultiplierY);
            
            selectionUiBaseScale = selectionUi.localScale;
            StopAllCoroutines();
            //StartCoroutine(RescaleOverTime(selectionUi.gameObject, selectionUiBaseScale, selectionUiBaseScale * scaleIncrease, increaseDuration, true));
        }

        if (arrow != null && arrow.gameObject.activeInHierarchy)
        {
            arrow.localScale = new Vector3(adaptScale.x, adaptScale.x) * arrowScaleMulty;

            float arrowPosX = 0;

            if (sliders.Capacity == 0)
            {
                if (placeArrowOnText && textMeshPros.Capacity > 0)
                {
                    TextMeshProUGUI textMeshPro = textMeshPros[0];
                    
                    arrowPosX = textMeshPro.bounds.size.x / 2f * adaptScale.x * Screen.width * 0.0008f;

                    //Debug.Log(arrowPosX + " = " + textMeshPro.bounds.size.x + " /2 * " + adaptScale.x + " * " + Screen.width * 0.0008f);
                }
                else
                {
                    arrowPosX = rectTransform.sizeDelta.x * adaptScale.x * scaleMultiplierX / 2f + arrow.sizeDelta.x * arrow.localScale.x / 2f + Screen.width * 0.01f;
                    // Debug.Log($"arrowPosX : {arrowPosX} = sizeDeltaX : {rectTransform.sizeDelta.x} * adaptScaleX : {adaptScale.x} * ScaleMultiplierX : {scaleMultiplierX} / 2 + " +
                    //           $"arrow size deltaX : {arrow.sizeDelta.x} * arrowLocalScaleX : {arrow.localScale.x} / 2 - ScreenWidth * 0.05 : {Screen.width * 0.05f}");
                }
            }
            else
            {
                arrowPosX = rectTransform.sizeDelta.x * adaptScale.x * scaleMultiplierX * 0.6f + arrow.sizeDelta.x * arrow.localScale.x / 2f;// + Screen.width * 0.015f; 1.3f, 0.5f
            }

            //position arrow
            arrow.position = rectTransform.position + new Vector3(-arrowPosX * Screen.width / 1920, 0, 0);
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
