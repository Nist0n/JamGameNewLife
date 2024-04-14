using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;

namespace UI
{
    public class UnitInfoTooltipManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text leadershipText;
        
        public TextMeshProUGUI textComponent;
        private int _leadership;
        
        private void Start()
        {
            Cursor.visible = true;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            transform.position = Input.mousePosition;
        }

        public void SetAndShowTooltip(GameObject obj)
        {
            Debug.Log("a");
            string selectedUnit = obj.GetComponent<Image>().sprite.name;
            Debug.Log(obj);
            _leadership = Convert.ToInt32(leadershipText.text);

            transform.position = Input.mousePosition;
            gameObject.SetActive(true);
            
            int hp = 0;
            int speed = 0;
            int damage = 0;
            int leadershipCost = 1;
            int quantity = 0;
            int maxQuantity = 0;
            
            foreach (var unit in OurHand.instance.Army)
            {
                if (unit.name == selectedUnit)
                {
                    hp = unit.GetComponent<Class>().Health;
                    damage = unit.GetComponent<Class>().Damage;
                    speed = unit.GetComponent<Class>().Speed;
                    quantity = unit.GetComponent<Class>().Count;
                }
            }

            switch (selectedUnit)
            {
                case "Рыцарь":
                    hp = 32;
                    damage = 10;
                    speed = 3;
                    leadershipCost = 35;
                    break;
                case "Лучник":
                    hp = 20;
                    damage = 15;
                    speed = 2;
                    leadershipCost = 50;
                    break;
                case "Маг":
                    hp = 23;
                    damage = 18;
                    speed = 2;
                    leadershipCost = 50;
                    break;
                case "Всадник":
                    hp = 130;
                    damage = 29;
                    speed = 5;
                    leadershipCost = 180;
                    break;
                case "Некромант":
                    hp = 140;
                    damage = 30;
                    speed = 2;
                    leadershipCost = 200;
                    break;
                case "Скелет":
                    hp = 12;
                    damage = 4;
                    speed = 2;
                    leadershipCost = 12;
                    break;
                case "Зомби":
                    hp = 31;
                    damage = 9;
                    speed = 2;
                    leadershipCost = 30;
                    break;
                case "Тёмный Рыцарь":
                    hp = 180;
                    damage = 33;
                    speed = 2;
                    leadershipCost = 150;
                    break;
                default:
                    hp = 5;
                    damage = 1;
                    speed = 2;
                    leadershipCost = 5;
                    break;
            }
            
            maxQuantity = _leadership / leadershipCost;

            textComponent.text = "ХП: " + hp + "\n" + "Урон: " + damage + "\n" + "Скорость: " + speed + "\n" +
                                 "Количество: " + quantity + "/" + maxQuantity;
        }
        
        public void HideTooltip()
        {
            Debug.Log("b");
            gameObject.SetActive(false);
            textComponent.text = string.Empty;
        }
    }
}