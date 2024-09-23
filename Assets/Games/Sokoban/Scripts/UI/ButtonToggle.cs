using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Sokoban
{
    public class ButtonToggle : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Toggle _toggle;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _imageOn;
        [SerializeField] private Image _imageOff;

        public void ToggleValue(bool isActve)
        {
            _text.text = isActve ? "On" : "Off";

            _imageOn.gameObject.SetActive(isActve);
            _imageOff.gameObject.SetActive(!isActve);

            _toggle.isOn = isActve;
        }
    }
}


