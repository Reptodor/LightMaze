using TMPro;

public class KeyQuest : IQuest
{
    private TextMeshProUGUI _text;
    private BagHandler _bag;

    public KeyQuest(TextMeshProUGUI text, BagHandler bag)
    {
        _text = text;
        _bag = bag;
    }

    public void Complete()
    {
    }

    public void ChangeText()
    {
        if(_bag.RemainingKeysCount == 1)
        {
            _text.text = $"Find {_bag.RemainingKeysCount} key to exit";
        }
        else
        {
            _text.text = $"Find {_bag.RemainingKeysCount} keys to exit";
        }
    }

    public bool IsCompleted()
    {
        if(_bag.RemainingKeysCount == 0)
            return true;

        ChangeText();
        return false;
    }
}
