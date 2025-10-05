using TMPro;

public class FinishQuest : IQuest
{
    private TextMeshProUGUI _text;
    private bool _isCompleted;

    public FinishQuest(TextMeshProUGUI text)
    {
        _text = text;
    }

    public void ChangeText()
    {
        _text.text = "Escape";
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
