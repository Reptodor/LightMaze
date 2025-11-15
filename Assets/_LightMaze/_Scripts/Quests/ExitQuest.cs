using TMPro;

public class ExitQuest : IQuest
{
    private readonly TextMeshProUGUI _text;
    private bool _isCompleted;

    public ExitQuest(TextMeshProUGUI text)
    {
        _text = text;
    }

    public void ChangeText()
    {
        _text.text = "Find the exit";
    }

    public void Complete()
    {
        _isCompleted = true;
    }

    public bool IsCompleted()
    {
        if(_isCompleted)
            return true;

        return false;
    }
}
