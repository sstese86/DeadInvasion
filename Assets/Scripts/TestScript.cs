using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

using NaviEnt.Data;
using NaviEnt.Game;


public class TestScript : MonoBehaviour
{

    void Start()
    {

        GameEventManager.onSelectedEntityChangedCallback += TestMethod_1;
    }

    private void OnGUI()
    {

    }
    
    void Update()
    {
        
    }
    [Button(ButtonSizes.Medium)]
    public void TestMethod_0()
    {
    }

    public void TestMethod_1(IEntity info)
    {
        Debug.Log(info.EntityInfo);
        Debug.Log(info.EntityName);
        
    }
}

#region UPDATE TEXTDATA WIT KEY IN EDITOR
//[ExecuteInEditMode]
//public class TestScript : MonoBehaviour
//{
//    [SerializeField]
//    string _key = string.Empty;

//    public string text = string.Empty;
//    ItemDatabase _database;
//    // Start is called before the first frame update
//    void Start()
//    {


//    }

//    private void OnGUI()
//    {
//        _database = Resources.Load<ItemDatabase>("ScriptableObject/ItemDatabase");
//        if (_database.data.ContainsKey(_key))
//            text = _database.data[_key].description;
//        //GUILayout.Label("NaviEnt Tools", EditorStyles.boldLabel);
//    }
//    // Update is called once per frame
//}
#endregion