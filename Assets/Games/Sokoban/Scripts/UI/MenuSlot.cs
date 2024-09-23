using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Sokoban
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuSlot : MonoBehaviour
    {
        [SerializeField] private bool _startVisible;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            if(_startVisible)
            {
                Show();
            }
            //ChangeVisibility(_startVisible);
        }

        //para apagar o mostrar un menu
        public void Show()
        {
            ChangeVisibility(true);
        }
        public void Hide()
        {
            ChangeVisibility(false);
        }

        private void ChangeVisibility(bool visible)
        {
            _canvasGroup.alpha = visible ? 1 : 0;
            _canvasGroup.blocksRaycasts = visible;
        }

    }



}
