using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaviEnt.Data;
using System;

namespace NaviEnt.Game
{
    public class EquipmentHandler : MonoBehaviour
    {
        [SerializeField]
        ActorSocketFinder _socketFinder = null;

        public string[] equipmentItemKey = new string[4];
        //Equip item will increse character state.
        CharacterState _equipableStateHead = new CharacterState();
        CharacterState _equipableStateBody = new CharacterState();
        CharacterState _equipableStateWeapon = new CharacterState();
        CharacterState _equipableStateMisc = new CharacterState();

        List<CharacterState> _equipStates = new List<CharacterState>();
        
        // Weapon has own state for Attack.
        WeaponState _weaponState = new WeaponState();
        public Weapon currentWeapon = null;

        ItemData currentItemData = new ItemData();

        public event Action onActorEquipmentChanged = delegate { };
        
        public WeaponState WeaponState { get => _weaponState; }
        
        public void EquipItem(string key)
        {
            currentItemData = GameManager.Instance.GetItemData(key);
            if(currentItemData.isEquipable)
            {
                switch(currentItemData.equipType)
                {
                    case EquipType.Head:
                        _equipableStateHead = currentItemData.equipableState;
                        equipmentItemKey[0] = key;
                        break;
                    case EquipType.Body:
                        _equipableStateBody = currentItemData.equipableState;
                        equipmentItemKey[1] = key;
                        break;
                    case EquipType.Weapon:
                        _equipableStateWeapon = currentItemData.equipableState;
                        _weaponState = currentItemData.weaponState;
                        currentWeapon = currentItemData.obj.GetComponent<Weapon>();
                        equipmentItemKey[2] = key;
                        SocketEquipItem(_socketFinder.RightHand, currentItemData.obj);
                        break;
                    case EquipType.Misc:
                        _equipableStateMisc = currentItemData.equipableState;
                        equipmentItemKey[3] = key;
                        break;
                    default:
                        Debug.Log("Equipment type is unknown.");
                        break;
                }
            }
            onActorEquipmentChanged?.Invoke();
        }

        public void UnEquipItem(string key)
        {
            currentItemData = GameManager.Instance.GetItemData(key);
            if (currentItemData.isEquipable)
            {
                switch (currentItemData.equipType)
                {
                    case EquipType.Head:
                        _equipableStateHead = new CharacterState();
                        equipmentItemKey[0] = "";
                        break;
                    case EquipType.Body:
                        _equipableStateBody = new CharacterState();
                        equipmentItemKey[1] = "";
                        break;
                    case EquipType.Weapon:
                        _equipableStateWeapon = new CharacterState();
                        equipmentItemKey[2] = "";
                        currentWeapon = null;
                        _weaponState = new WeaponState();
                        break;
                    case EquipType.Misc:
                        _equipableStateMisc = new CharacterState();
                        equipmentItemKey[3] = "";
                        break;
                    default:
                        Debug.Log("Equipment type is unknown.");
                        break;
                }
            }
            onActorEquipmentChanged?.Invoke();
        }

        void SocketEquipItem(Transform socket, GameObject item)
        {
            if (socket.childCount != 0)
                GameObject.Destroy(socket.GetChild(0).gameObject);

            GameObject obj = GameObject.Instantiate(item, socket);
            obj.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

        }

        public CharacterState UpdateModifiedCharacterState(CharacterState baseState)
        {
            _equipStates.Clear();
            _equipStates.Add(_equipableStateHead);
            _equipStates.Add(_equipableStateBody);
            _equipStates.Add(_equipableStateWeapon);
            _equipStates.Add(_equipableStateMisc);
            _equipStates.Add(baseState);

            int maxHealth = 0;        
            int damage = 0;
            int strength = 0;
            int agility = 0;
            int defance = 0;
            float moveSpeed = 0;
            float jumpSpeed = 0;

            foreach(CharacterState state in _equipStates)
            {
                maxHealth += state.maxHealth;
                damage += state.damage;
                strength += state.strength;
                agility += state.agility;
                defance += state.defance;
                moveSpeed += state.moveSpeed;
                jumpSpeed += state.jumpSpeed;
            }
            CharacterState newModifiedState = new CharacterState();
            newModifiedState.maxHealth = maxHealth;
            newModifiedState.damage = damage;
            newModifiedState.strength = strength;
            newModifiedState.agility = agility;
            newModifiedState.defance = defance;
            newModifiedState.moveSpeed = moveSpeed;
            newModifiedState.jumpSpeed = jumpSpeed;

            return newModifiedState;
        }

        public int GetWeaponIndex()
        {
            int result = 0;
            if (currentWeapon != null)
            { 
                result = currentWeapon.WeaponTypeIndex;
            }
            return result;
        }

        public float GetAttackAnimIndex()
        {
            float result = 0f;
            if (currentWeapon != null)
            {
                result = currentWeapon.GetAttackAnimIndex();
            }
            return result;
        }

        void initEquipments()
        {
            foreach(string key in equipmentItemKey)
            {
                if(key != string.Empty)
                {
                    EquipItem(key);
                }
            }
        }

        private void Start()
        {
            initEquipments();
        }
    }
}
