using Battle;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI winnerText;
    public UnityEvent onPlacementStart;
    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("Battle Controller")
            .GetComponent<BattleController>().onGameOverEvent.AddListener(SetWinner);
    }

    public void OnStartGameButtonClicked()
    {
        onPlacementStart.Invoke();
    }
    public void OnExitGameButtonClicked()
    {
        Application.Quit();
    }

    public void OnRestartGameButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void SetWinner(BattleController.Winner winner)
    {
        if (winner.IsPlayerWinner)
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