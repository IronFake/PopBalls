using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class Game : MonoBehaviour, IBallDamageDealer, IBallDestroyer
    {
        [SerializeField] private int pointsForPopBalls;
        
        [SerializeField] [Range(0, 1)]
        private float percentPoppedBallsForWin;
        
        [SerializeField] private int roundTimeLimit;
        [SerializeField] private BallsSpawner ballsSpawner;
        [SerializeField] private HUDView hudView;
        [SerializeField] private PauseMenu pauseMenu;
        [SerializeField] private ResultMenu resultMenu;
        [SerializeField] private DestroyLine destroyLine;
        [SerializeField] private ParticleSystem winEffect;

        private PointsHandler _pointsHandler;
        private float _remainingRoundTime;
        private bool _roundInProgress;
        private int _lostBalls;
        private bool _isInit;
        
        public bool IsPaused { get; private set; }

        [ContextMenu("Init")]
        public void Init()
        {
            ballsSpawner.Init(this);
            ballsSpawner.OnSpawnedBallCountChanged += UpdatePercentOfPoppedBalls;
            _pointsHandler = new PointsHandler();
            destroyLine.Init(this);
            
            hudView.Init(_pointsHandler);
            pauseMenu.Init();
            pauseMenu.OnContinueButtonPressed += ResumeGame;
            pauseMenu.OnRestartButtonPressed += StartGame;
            pauseMenu.OnQuitButtonPressed += GoToMainMenu;
            resultMenu.Init();
            resultMenu.OnRestartButtonPressed += StartGame;
            resultMenu.OnMainMenuButtonPressed += GoToMainMenu;
        }

        private void Update()
        {
            if (_roundInProgress && Input.GetKeyDown(KeyCode.Escape))
            {
                if (IsPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
            
            if (IsPaused)
                return;
            

            if (_roundInProgress)
            {
                _remainingRoundTime -= Time.deltaTime;
                hudView.UpdateRemainingTime(_remainingRoundTime);
                if (_remainingRoundTime <= 0)
                {
                    StopGame();
                }
            }
        }

        [ContextMenu("StartGame")]
        public void StartGame()
        {
            _pointsHandler.CurrentPoints = 0;
            _lostBalls = 0;
            _remainingRoundTime = roundTimeLimit;
            hudView.UpdateRemainingTime(_remainingRoundTime);
            pauseMenu.HidePauseMenu();
            resultMenu.Hide();
            _roundInProgress = true;
            ballsSpawner.StartSpawn();
            hudView.UpdatePercentOfPoppedBallsText(0);
            IsPaused = false;
        }

        private void PauseGame()
        {
            SetPause(true);
            pauseMenu.ShowPauseMenu();
        }

        private void ResumeGame()
        {
            SetPause(false);
            pauseMenu.HidePauseMenu();
        }
        
        [ContextMenu("StopGame")]
        public void StopGame()
        {
            _roundInProgress = false;
            ballsSpawner.StopSpawn();
            SetPause(true);

            float percent = CalcPercentOfPoppedBalls();
            bool winResult = percent >= percentPoppedBallsForWin;
            resultMenu.Show(
                winResult, 
                percent, 
                percentPoppedBallsForWin,
                _pointsHandler.CurrentPoints);

            if (winResult)
            {
                winEffect.Play();
            }
            
        }

        public void GoToMainMenu()
        {
            var operations = new Queue<ILoadingOperation>();
            operations.Enqueue(new MainMenuLoadingOperation());
            LoadingScreen.Instance.Load(operations);
        }
        
        public void TakeDamage(Ball ball)
        {
            _pointsHandler.CurrentPoints += pointsForPopBalls;
            ballsSpawner.DestroyBall(ball);
        }

        public void DestroyBall(Ball ball)
        {
            _lostBalls++;
            ballsSpawner.DestroyBall(ball);
            UpdatePercentOfPoppedBalls();
        }

        private void UpdatePercentOfPoppedBalls()
        {
            hudView.UpdatePercentOfPoppedBallsText(CalcPercentOfPoppedBalls());
        }
        
        private float CalcPercentOfPoppedBalls()
        {
            return 1 - (float) _lostBalls / ballsSpawner.SpawnedBallsCount;
        }

        private void SetPause(bool pause)
        {
            IsPaused = pause;
            ballsSpawner.SetPause(pause);
        }
    }
}