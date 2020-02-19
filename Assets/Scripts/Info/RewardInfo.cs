using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _info = null;
    [SerializeField] Image _icon = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateInfo(Item item)
    {
        _info.text = item._itemDescription;
        _icon.sprite = item._itemIcon;
    }
}
