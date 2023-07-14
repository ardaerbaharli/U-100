using System;
using Managers;
using UnityEngine;

namespace Collectables
{
    public abstract class Collectable : MonoBehaviour
    {
        public CollectableType Type;
        public PooledObject PooledObject;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _collider;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
        }

        public void SetCollectableProperty(CollectableProperty property)
        {
            Type = property.Type;
            _spriteRenderer.sprite = property.Sprite;
            _collider.size = property.ColliderSize;

            SetSpecificProperty(property);
        }


        public abstract void SetSpecificProperty(CollectableProperty property);

        public void ReturnToPool()
        {
            PooledObject.ReturnToPool();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GetCollected();
            }
        }

        public abstract void GetCollected();
    }
}