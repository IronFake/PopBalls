using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class BallsSpawner : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private float spawnYPos;
        [SerializeField] private float spawnXRange;
        [SerializeField] private float spawnSpeed;
        [SerializeField] private float minBallSpeed;
        [SerializeField] private float maxBallSpeed;
        [SerializeField] private Ball ballPrefab;
        [SerializeField] private Transform ballsContent;

        private bool _isSpawning;
        private IBallDamageDealer _ballDamageDealer;
        private List<Ball> _activeBalls = new List<Ball>();
        
        public int SpawnedBallsCount { get; private set; }
        public event Action OnSpawnedBallCountChanged;

        public void Init(IBallDamageDealer ballDamageDealer)
        {
            _ballDamageDealer = ballDamageDealer;
        }

        public void StartSpawn()
        {
            DestroyAllBalls();
            SpawnedBallsCount = 0;
            StartCoroutine(SpawnBalls());
        }

        public void StopSpawn()
        {
            _isSpawning = false;
            StopAllCoroutines();
        }

        IEnumerator SpawnBalls()
        {
            _isSpawning = true;
            while (_isSpawning)
            {
                yield return new WaitForSeconds(spawnSpeed);
                var ball = Instantiate(ballPrefab, GetRandomPosition(), Quaternion.identity, ballsContent);
                ball.Init(_ballDamageDealer, Random.ColorHSV(), Random.Range(minBallSpeed, maxBallSpeed));
                _activeBalls.Add(ball);
                SpawnedBallsCount++;
                OnSpawnedBallCountChanged?.Invoke();
            }
        }

        private Vector3 GetRandomPosition()
        {
            return new Vector3(Random.Range(-spawnXRange, spawnXRange), spawnYPos, 0);
        }
        
        public void DestroyBall(Ball ball)
        {
            if (_activeBalls.Contains(ball))
            {
                Destroy(ball.gameObject);
                _activeBalls.Remove(ball);
            }
        }

        public void DestroyAllBalls()
        {
            for (int i = 0; i < _activeBalls.Count; i++)
            {
                Destroy(_activeBalls[i].gameObject);
            }
            
            _activeBalls.Clear();
        }

        public void SetPause(bool pause)
        {
            for (int i = 0; i < _activeBalls.Count; i++)
            {
                _activeBalls[i].SetPause(pause);
            }

            if (pause)
            {
                StopSpawn();
            }
            else
            {
                StartCoroutine(SpawnBalls());
            }
        }
    }
}