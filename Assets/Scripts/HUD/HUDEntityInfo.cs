using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DG.Tweening;



namespace NaviEnt.Game
{
    public class HUDEntityInfo : MonoBehaviour
    {
        [SerializeField]
        DOTweenAnimation _entityInfoOpenAnim = null;
        [SerializeField]
        DOTweenAnimation _entityInfoCloseAnim = null;

        [SerializeField]
        Image _entityValueSlider = null;
        [SerializeField]
        TextMeshProUGUI _entityName = null;
        [SerializeField]
        TextMeshProUGUI _entityInfo = null;


        // Start is called before the first frame update
        void Start()
        {
            GameEventManager.onSelectedEntityChangedCallback += OpenEntitiyInfo;
            transform.localScale = Vector3.zero;
        }
        private void OnDestroy()
        {
            GameEventManager.onSelectedEntityChangedCallback -= OpenEntitiyInfo;
        }

        private void OpenEntitiyInfo(IEntity obj, Transform trans)
        {
            _entityValueSlider.fillAmount = obj.EntityValue;
            _entityName.text = obj.EntityName;
            _entityInfo.text = obj.EntityInfo;


            _entityInfoOpenAnim.DOPlay();

            StopAllCoroutines();
            StartCoroutine(CloseTimerRoutine());
        }

        void CloseEntityInfo()
        {
            _entityInfoCloseAnim.DORestart();
            _entityInfoOpenAnim.DORewind();
        }

        IEnumerator CloseTimerRoutine()
        {
            yield return new WaitForSeconds(5f);
            CloseEntityInfo();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}