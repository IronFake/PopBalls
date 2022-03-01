using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI pointsText;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI percentOfPoppedBallsText;
        
        public void Init(PointsHandler pointsHandler)
        {
            pointsHandler.OnPointsChanged += UpdateText;
            UpdateText(pointsHandler.CurrentPoints);
        }

        private void UpdateText(int pointsValue)
        {
            pointsText.text = pointsValue.ToString();
        }

        public void UpdateRemainingTime(float remainingTime)
        {
            timeText.text = "Time:" + Mathf.Round(remainingTime).ToString();
        }
        
        public void UpdatePercentOfPoppedBallsText(float percent)
        {
            percentOfPoppedBallsText.text = "%:" + (Mathf.Round(percent * 100)).ToString();
        }
    }
}