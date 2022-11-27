using UnityEngine;
using UnityEngine.UI;

namespace FoggyWoods.Inventories.UI
{
    public class ItemDisplay : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        public void Display(IItem item)
        {
            _icon.sprite = item.GetProperty<Sprite>("icon");
        }
    }
}