using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class Ball : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private SpriteRenderer rend;

        private IBallDamageDealer _ballDamageDealer;
        private float _speed;

        private bool isPause;
        
        public void Init(IBallDamageDealer ballDamageDealer, Color color, float initSpeed)
        {
            rend.color = color;
            _ballDamageDealer = ballDamageDealer;
            _speed = initSpeed;
        }

        private void Update()
        {
            if(isPause)
                return;
            
            transform.Translate(Vector3.up * Time.deltaTime * _speed, Space.World);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _ballDamageDealer.TakeDamage(this);
        }

        public void SetPause(bool pause)
        {
            isPause = pause;
        }
    }
}