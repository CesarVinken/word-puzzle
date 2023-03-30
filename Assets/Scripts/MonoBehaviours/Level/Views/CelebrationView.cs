using System.Collections;
using TMPro;
using UnityEngine;

public class CelebrationView : MonoBehaviour, ILevelScreenView
{
    [SerializeField] private TextMeshProUGUI _highScoreText;

    public void Setup()
    {
        if(_highScoreText == null)
        {
            ConsoleLog.Error(LogCategory.Initialisation, $"Cannot find high score text");
        }
    }

    public void Initialise()
    {
    }

    public void Show()
    {
        SetHighScoreText();
        gameObject.SetActive(true);
        StartCelebration();
    }

    public void Hide()
    {

    }

    public void SetHighScoreText()
    {
        int levelNumber = GameManager.Instance.CurrentLevelData.LevelNumber;
        int highScore = GameManager.Instance.UserData.Levels[levelNumber - 1].HighScore;
        _highScoreText.text = $"High Score\n{highScore}";
    }

    public void StartCelebration()
    {
        IEnumerator celebrationRoutine = CelebrationRoutine();

        StartCoroutine(celebrationRoutine);

    }

    private IEnumerator CelebrationRoutine()
    {
        yield return new WaitForSeconds(3f);

        LevelUIController.Instance.ToLevelSelection();
    }
}
