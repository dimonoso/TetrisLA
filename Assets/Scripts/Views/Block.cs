using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris.Views
{
    public class Block : MonoBehaviour, IBlock
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private float _animationTime;

        public void Remove(Action onRemovedAction)
        {
            _animator.SetTrigger("Remove");
            StartCoroutine(WaitAnimation(onRemovedAction));
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
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