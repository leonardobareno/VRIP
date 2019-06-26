using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// Listbox UI Tool
/// Developed by Dibbie.
/// Website: http://www.simpleminded.x10host.com
/// Email: mailto:strongstrenth@hotmail.com
/// Discord Server: https://discord.gg/33PFeMv
/// </summary>

[AddComponentMenu("UI Tools/Listbox", 1), DisallowMultipleComponent]
public class ListBox : MonoBehaviour, IPointerClickHandler
{

    public RectTransform content;
    public RectTransform selectionBoxOrigen;

    [Space]

    public List<ListItem> items = new List<ListItem>();

    [Header("Text Preferences")]
    [Tooltip("If you supply text for a new ListItem, this will be the font used.")]
    public Font defaultFont;
    [Tooltip("If you supply text for a new ListItem, this will be the color used.")]
    public Color defaultColor = Color.black;

    /// <summary>
    /// The currently selected item in the listbox
    /// </summary>
    [System.NonSerialized]
    public ListItem selection;

    private bool bDebug = false;

    [System.Serializable]
    public class ListItem
    {
        [Tooltip("Creates a new UI Text element with the specified text to it. This is seperate from a provided object and will be displayed instead of a object, if the object is null.")]
        public string text;

        [Space]

        [Tooltip("Provide a object to be displayed and added to the ListBox. This is seperate from a provided text, and will be used instead of text. If this field is null, the provided text will be used instead.")]
        public GameObject _object;

        [Tooltip("Determines if the object (or text) will be given a fixed height while in the ListBox hierarchy, or if it will be controlled by a Layout Element.")]
        public bool useFixedHeight;
        [Tooltip("The fixed height to maintain for the object (or text) when added to the ListBox.")]
        public float fixedHeight;

        /// <summary>
        /// Create a new ListBox.ListItem with a specific object.
        /// </summary>
        /// <param name="gameObject">The actual GameObject to add to this ListItem ListBox.</param>
        public ListItem(GameObject gameObject)
        {
            _object = gameObject;
        }

        /// <summary>
        /// Create a new ListBox.ListItem with a specific object, and the objects height, that will be maintained when added to the ListBox.
        /// </summary>
        /// <param name="gameObject">The actual GameObject to add to this ListItem ListBox.</param>
        /// <param name="objectFixedHeight">The height of the object to maintain, when created and added to the ListBox.</param>
        public ListItem(GameObject gameObject, float objectFixedHeight)
        {
            _object = gameObject;
            useFixedHeight = true;
            fixedHeight = objectFixedHeight;
        }

        /// <summary>
        /// Create a new ListBox.ListItem with a specific object, that will also add a LayoutElement to it when added to the ListBox, to control height.
        /// </summary>
        /// <param name="gameObject">The actual GameObject to add to this ListItem ListBox.</param>
        /// <param name="includeLayoutElement">Should this GameObject have a LayoutElement added to it, when being created and added to the ListBox?</param>
        public ListItem(GameObject gameObject, bool includeLayoutElement)
        {
            _object = gameObject;
            if (!_object.GetComponent<LayoutElement>()) { _object.AddComponent<LayoutElement>().preferredHeight = GetObjectHeight(); }
            else
            {
                if (_object.AddComponent<LayoutElement>().preferredHeight <= 0f) { _object.AddComponent<LayoutElement>().preferredHeight = GetObjectHeight(); }
            }
        }

        /// <summary>
        /// Create a new ListBox.ListItem with specific text. This works seperately from a object.
        /// </summary>
        /// <param name="objectText">The string contents to add to the new UI Text element.</param>
        public ListItem(string objectText)
        {
            text = objectText;
            _object = null;
        }

        /// <summary>
        /// Create a new ListBox.ListItem with specific text, and a set fixed height for the new text. This works seperately from a object.
        /// </summary>
        /// <param name="objectText">The string contents to add to the new UI Text element.</param>
        /// <param name="objectFixedHeight">The height of the UI Text object to maintain, when created and added to the ListBox.</param>
        public ListItem(string objectText, float objectFixedHeight)
        {
            text = objectText;
            _object = null;
            useFixedHeight = true;
            fixedHeight = objectFixedHeight;
        }

        float GetObjectHeight()
        {
            if (useFixedHeight)
            {
                return fixedHeight;
            }
            else
            {
                return _object.GetComponent<RectTransform>().rect.height;
            }
        }
    };

    void Awake()
    {
        //selectionBox.gameObject.SetActive(false);
        if (bDebug) Debug.Log("*** Awake");
        RefreshListBox();
    }

    #region Common Functions
    /// <summary>
    /// Add an existing ListItem to the ListBox.
    /// </summary>
    /// <param name="item">The actual ListItem to add to the ListBox.</param>
    public void AddItem(ListItem item)
    {
        items.Add(item);
        RefreshListBox();
    }

    /// <summary>
    /// Add an existing ListItem to the ListBox, with a LayoutElement, if it does not already have one.
    /// </summary>
    /// <param name="item">The actual ListItem to add to the ListBox.</param>
    /// <param name="includeLayoutElement">Should a LayoutElement be added to the ListItem being added?</param>
    public void AddItem(ListItem item, bool includeLayoutElement)
    {
        if (!item._object.GetComponent<LayoutElement>())
        {
            item._object.AddComponent<LayoutElement>().preferredHeight = GetPreference(item);
        }
        else
        {
            if (item._object.AddComponent<LayoutElement>().preferredHeight <= 0f)
            {
                item._object.AddComponent<LayoutElement>().preferredHeight = GetPreference(item);
            }
        }

        items.Add(item);
        RefreshListBox();
    }

    /// <summary>
    /// Remove an existing ListItem from the ListBox.
    /// </summary>
    /// <param name="item">The actual ListItem to remove from the ListBox.</param>
    public void RemoveItem(ListItem item)
    {
        if (item._object != null) { Destroy(item._object); }
        items.Remove(item);
        RefreshListBox();
    }

    /// <summary>
    /// Remove an existing ListItem from the ListBox.
    /// </summary>
    /// <param name="itemIndex">The index of the ListItem to remove from the ListBox.</param>
    public void RemoveItem(int itemIndex)
    {
        if (items[itemIndex]._object != null) { Destroy(items[itemIndex]._object); }
        items.RemoveAt(itemIndex);
        RefreshListBox();
    }

    /// <summary>
    /// Clear the visible displayed ListBox to appear empty. The items array containing the data will still remain.
    /// </summary>
    public void ClearListBox()
    {
        if (bDebug) Debug.Log("*** ClearListBox content.childCount=" + content.childCount);
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
            items.RemoveAt(i);
        }
    }
    #endregion

    #region Public Functions
    /// <summary>
    /// Reload the entire listbox to account for newly added or removed items and their hierarchical positioning.
    /// This function is automatically called on Add/Remove events.
    /// </summary>
    public void RefreshListBox()
    {
        if (bDebug) Debug.Log("*** RefreshListBox itemsCount=" + items.Count);
        ClearListBox();

        float masterHeight = 0f;
        SetToDefaultFont();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i]._object != null)
            {
                if (bDebug) Debug.Log("*********items object != null");
                //move obj to listbox
                items[i]._object.transform.SetParent(content, false);
                if (items[i]._object.GetComponent<LayoutElement>() == null) {
                    items[i]._object.AddComponent<LayoutElement>().preferredHeight = GetPreference(items[i]);
                    items[i]._object.GetComponent<LayoutElement>().layoutPriority = 1;
                }
                masterHeight += items[i]._object.GetComponent<RectTransform>().rect.height;
            }
            else
            {
                //make text, set it, move it to listbox
                Text label = new GameObject("Text: " + items[i].text).AddComponent<Text>().GetComponent<Text>();

                if (bDebug) Debug.Log("*********items object SI NULL |" + items[i].text + "|" + items[i].GetType() + "|" + label.GetComponent<RectTransform>().position + "|");

                label.font = defaultFont;
                label.color = defaultColor;
                label.alignment = TextAnchor.MiddleLeft;
                label.GetComponent<RectTransform>().sizeDelta = new Vector2(0, label.preferredHeight + 2f);
                items[i]._object = label.gameObject;

                label.gameObject.AddComponent<LayoutElement>().preferredHeight = GetPreference(items[i]);
                label.gameObject.GetComponent<LayoutElement>().layoutPriority = 1;
                label.text = items[i].text;
                label.transform.SetParent(content, false);
                masterHeight += label.GetComponent<RectTransform>().rect.height;
                if (bDebug) Debug.Log("*********items object FINAL |" + label.GetComponent<RectTransform>().position + "|");
            }
        }
    }

    /// <summary>
    /// Checks if a font is provided. If not, the default font "Arial" will be used.
    /// </summary>
    public void SetToDefaultFont()
    {
        if (bDebug) Debug.Log("*** SetToDefaultFont |" + defaultFont + "|");
        if (defaultFont == null) { defaultFont = Resources.GetBuiltinResource<Font>("Arial.ttf"); }
    }
    #endregion

    #region Helper Functions
    float GetPreference(ListItem item)
    {
        if (item.useFixedHeight)
        {
            return item.fixedHeight;
        }
        else
        {
            return item._object.GetComponent<RectTransform>().rect.height;
        }
    }

    bool ClickedItemExistsInList(GameObject obj)
    {
        foreach (ListItem item in items)
        {
            if (obj == item._object) { return true; }
        }

        return false;
    }

    public string[] GetClickedItems() {
        List<string> temp = new List<string>();

        if(bDebug) Debug.Log("***** items LEN=" + items.Count);

        foreach (ListItem item in items) {
           if (item._object.transform.Find("Selection Box(Clone)") != null) {

                string cleanstr = item._object.GetComponent<Text>().text;
                cleanstr = cleanstr.Replace("\u000D", "");
                cleanstr = cleanstr.Replace("\u000A", "");
                cleanstr = cleanstr.Replace("\u0009", "");

                temp.Add(cleanstr);
           }
        }

        if (bDebug) Debug.Log("***** temp LEN=" + temp.Count);

        return temp.ToArray();
    }

    public bool SelectItemByName(string sBusq, bool bUnselectRest) {
        bool sal = false;

        foreach (ListItem item in items) {

            if (item._object.GetComponent<Text>().text.IndexOf(sBusq, 0) != -1) {
                if (item._object.transform.Find("Selection Box(Clone)") != null) continue;

                RectTransform selBox_inst = Instantiate<RectTransform>(selectionBoxOrigen);
                selBox_inst.SetParent(item._object.transform, false);
                selBox_inst.gameObject.SetActive(true);
                selBox_inst.sizeDelta = item._object.GetComponent<RectTransform>().sizeDelta;
                selBox_inst.position = item._object.GetComponent<RectTransform>().position;
                sal = true;
                break;
            }
            else if (bUnselectRest) {
                Transform selectionBoxTransform = null;

                try {
                    selectionBoxTransform = item._object.transform.GetChild(0);
                }
                catch (UnityException ex) {
                    continue;
                }

                Destroy(selectionBoxTransform.gameObject);
            }

        }

        return sal;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //selectionBox.gameObject.SetActive(false);
        GameObject clickedObj = eventData.pointerCurrentRaycast.gameObject;
        RectTransform objRect = clickedObj.GetComponent<RectTransform>();

        if (bDebug) Debug.Log("**** OnPointerClick clickedObj=" + clickedObj.name + "|");


        if (ClickedItemExistsInList(clickedObj))
        {
            Transform selectionBoxTransform = null;

            try {
                selectionBoxTransform = clickedObj.transform.GetChild(0);
            }
            catch (UnityException ex) {
                if (bDebug) Debug.Log("**** " + ex.GetType() + " | ");
                RectTransform selBox_inst = Instantiate<RectTransform>(selectionBoxOrigen);
                selBox_inst.SetParent(clickedObj.transform, false);
                selBox_inst.gameObject.SetActive(true);
                selBox_inst.sizeDelta = objRect.sizeDelta;
                selBox_inst.position = objRect.position;
                return;
            }

            Destroy(selectionBoxTransform.gameObject);

            if (bDebug) Debug.Log("**** Exists TRUE");
        }
        else
        {
            if (bDebug) Debug.Log("**** Exists false");
        }
    }
    #endregion
}
