using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : MonoBehaviour, ITitleScreenView
{
    [SerializeField] private List<Button> _buttons = new List<Button>();
    [SerializeField] private List<ITitleScreenButton> _titleScreenButtons = new List<ITitleScreenButton>();

    public void Setup()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            ITitleScreenButton titleScreenButton = _buttons[i].gameObject.GetComponent<ITitleScreenButton>();

            if (titleScreenButton == null)
            {
                ConsoleLog.Error(LogCategory.Initialisation, $"Could not find a button script on the {_buttons[i].gameObject.name} button");
            }

            _titleScreenButtons.Add(titleScreenButton);

            titleScreenButton.Setup();
        }
    }

    public void Initialise() { }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
