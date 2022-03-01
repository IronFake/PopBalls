using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Collider2D))]
    public class DestroyLine : MonoBehaviour
    {
        private IBallDestroyer _ballDestroyer;
        
        public void Init(IBallDestroyer ballDestroyer)
        {
            _ballDestroyer = ballDestroyer;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Ball ball = other.gameObject.GetComponent<Ball>();
            if (ball)
            { 
                _ballDestroyer.DestroyBall(ball);
            }
        }
    }
}