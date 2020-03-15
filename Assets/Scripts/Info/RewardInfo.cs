using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using NaviEnt.Data;

public class RewardInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _info = null;
    [SerializeField] Image _icon = null;
    [SerializeField] RectTransform _bubleTransform = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateInfo(ItemData item)
    {
        Vector2 bubleSize = new Vector2(_bubleTransform.rect.width, _bubleTransform.rect.height);
        int descSize = item.description.Length;


        _bubleTransform.sizeDelta= new Vector2(bubleSize.x + (( Mathf.FloorToInt(descSize / 10)*10)) , bubleSize.y + (Mathf.FloorToInt(descSize / 40)*10));

        _info.text = item.description;
        _icon.sprite = item.icon;
    }
}
