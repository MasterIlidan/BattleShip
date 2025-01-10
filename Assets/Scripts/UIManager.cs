using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI winnerText;
        public UnityEvent OnPlacementStart;
        private void OnEnable()
        {
            GameObject.FindGameObjectWithTag("Battle Controller")
                .GetComponent<BattleController>().OnGameOverEvent.AddListener(SetWinner);
        }

        public void OnStartGameButtonClicked()
        {
            OnPlacementStart.Invoke();
        }

        private void SetWinner(BattleController.Winner winner)
        {
            if (winner.isPlayerWinner)
            {
                winnerText.text = "Вы выиграли!";
                winnerText.color = Color.green;
            }
            else
            {
                winnerText.text = "Вы проиграли!";
                winnerText.color = Color.red;
            }
        }
        
    }
}