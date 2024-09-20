using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Sokoban
{
    public class Box : MonoBehaviour, IInteractable
    {
        [Header("Settings")]
        [SerializeField] private LayerMask _layerDot;

        public Action<bool> onTrigger;

        private bool _isComplete;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2;

        private Transform _baseTransform;

        private void Awake()
        {
            _boxCollider2 = GetComponent<BoxCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _baseTransform = transform.parent;
        }

        public void AddParent(Transform parent)
        {
            //Lo vuelvo hijo para que lleve la caja
            transform.SetParent(parent);
        }

        public void RemoveParent()
        {
            //lo vuelvo hijo nuevamente de nadie para que se separe
            transform.SetParent(_baseTransform);
        }

        public void Trigger()
        {
            //TODO
            Collider2D collider = Physics2D.OverlapBox(transform.position, _boxCollider2.size, 0, _layerDot);

            if (_isComplete)
            {
                if (collider == null)
                {
                    onTrigger?.Invoke(false);
                    Debug.Log("FALSE");
                }
                else
                {
                    Debug.Log("RETURN");
                    return;
                }
            }

            _isComplete = collider != null;

            if (_isComplete)
            {
                onTrigger?.Invoke(true);
                Debug.Log("TRUE");
            }

            _spriteRenderer.color = _isComplete ? Color.yellow : Color.white;
        }
    }
}


