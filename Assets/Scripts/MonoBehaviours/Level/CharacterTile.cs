using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTile : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _characterText;

    public List<CharacterTile> TileParents;
    public List<CharacterTile> TileChildren;

    public CharacterTileDataModel CharacterTileData { get; private set; }

    public int Id { get; private set; }
    public CharacterTileState State = CharacterTileState.Open;

    public void Setup(CharacterTileDataModel characterTileData)
    {
        if (_button == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find button on {gameObject.name}");
        }
        if (_image == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find image on {gameObject.name}");
        }
        if (_characterText == null)
        {
            ConsoleLog.Error(LogCategory.General, $"Cannot find _character on {gameObject.name}");
        }

        CharacterTileData = characterTileData;
        Id = CharacterTileData.Id;
        gameObject.name = $"CharacterTile {Id} '{CharacterTileData.Character}'";

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(delegate { OnClick(); });
    }

    public void Initialise(List<CharacterTile> tileChildren)
    {
        _characterText.text = CharacterTileData.Character;

        TileChildren = tileChildren;

        for (int i = 0; i < TileChildren.Count; i++)
        {
            TileChildren[i].AddParentTile(this);
        }
    }

    public void SetInitialState()
    {
        if(TileParents.Count == 0)
        {
            Open();
            State = CharacterTileState.Open;
        }
        else
        {
            Block();
            State = CharacterTileState.Blocked;
        }
    }

    public void AddParentTile(CharacterTile characterTile)
    {
        TileParents.Add(characterTile);
    }

    public void SetCharacterTileState(CharacterTileState newState)
    {
        State = newState;

        switch (State)
        {
            case CharacterTileState.Blocked:
                Block();
                BlockChildren(); 
                break;
            case CharacterTileState.Open:
                Open();
                BlockChildren(); 
                break;
            case CharacterTileState.Used:
                Use();
                OpenChildren();
                break;
            default:
                throw new NotImplementedException("Character tile state", State.ToString());
        }

    }

    private void BlockChildren()
    {
        for (int i = 0; i < TileChildren.Count; i++)
        {
            if(TileChildren[i].State != CharacterTileState.Blocked)
            {
                CharacterTile childTile = TileChildren[i];
                childTile.Block();
                childTile.State = CharacterTileState.Blocked;
            }
        }
    }

    private void OpenChildren()
    {
        for (int i = 0; i < TileChildren.Count; i++)
        {
            if (TileChildren[i].State == CharacterTileState.Open) continue;

            bool hasBlockingParent = false;
            CharacterTile childTile = TileChildren[i];
            for (int j = 0; j < childTile.TileParents.Count; j++)
            {
                if(childTile.TileParents[j].State != CharacterTileState.Used)
                {
                    hasBlockingParent = true;
                }
            }

            if (hasBlockingParent) continue;

            childTile.Open();
            childTile.State = CharacterTileState.Open;
        }
    }

    private void Block()
    {
        _image.color = ColourUtility.GetColour(ColourType.DisabledGray);

        gameObject.SetActive(true);
    }

    private void Open()
    {
        _image.color = ColourUtility.GetColour(ColourType.Empty);

        gameObject.SetActive(true);
    }

    private void Use()
    {
        gameObject.SetActive(false);
    }

    public void OnClick()
    {
        if (State != CharacterTileState.Open) return;

        if (GameFlowManager.Instance.LetterPickActions.Count >= 7) return; // the player cannot do more moves if the number of moves equals the maximum word length

        ConsoleLog.Log(LogCategory.General, $"Add {_characterText.text} to word", LogPriority.Normal);
        GameFlowManager.Instance.MoveHandler.UseTile(this);
    }
}



