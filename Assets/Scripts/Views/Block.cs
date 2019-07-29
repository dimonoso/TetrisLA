using System;
using System.Collections;
using UnityEngine;

namespace Tetris.Views
{
    public class Block : MonoBehaviour, IBlock
    {
        [SerializeField]
        private float _animationTime;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        public void Remove(Action onRemovedAction)
        {
            _animator.SetTrigger("Remove");
            StartCoroutine(WaitAnimation(onRemovedAction));
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        public Transform Transform
        {
            get { return transform; }
        }

        private IEnumerator WaitAnimation(Action onRemovedAction)
        {
            yield return new WaitForSeconds(_animationTime);

            onRemovedAction();
        }
    }
}