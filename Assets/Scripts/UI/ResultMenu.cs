using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ResultMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private TextMeshProUGUI percentResultText;
        [SerializeField] private TextMeshProUGUI pointsResultText;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Color winColor;
        [SerializeField] private Color loseColor;
        
        public event Action OnMainMenuButtonPressed;
        public event Action OnRestartButtonPressed;
        
        public void Init()
        {
            mainMenuButton.onClick.AddListener(() => OnMainMenuButtonPressed?.Invoke());    
            restartButton.onClick.AddListener(() => OnRestartButtonPressed?.Invoke());    
        }

        public void Show(bool winResult, float currentPercent, float targetPercent, int points)
        {
            pointsResultText.text = points.ToString();
            if (winResult)
            {
                resultText.text = "You win";
                resultText.color = winColor;
                percentResultText.text = $"{ Mathf.Round(currentPercent * 100)}% >= {Mathf.Round(targetPercent * 100)}%";
            }
            else
            {
                resultText.text = "You lose";
                resultText.color = loseColor;
                percentResultText.text = $"{ Mathf.Round(currentPercent * 100)}% < {Mathf.Round(targetPercent * 100)}%";
            }
            
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}