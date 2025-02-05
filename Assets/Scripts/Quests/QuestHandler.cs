using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class QuestHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private List<IQuest> _quests = new List<IQuest>();
    private QuestAnimationHandler _questAnimationHandler;
    private int _currentQuestIndex = 0;
    private bool _isInitialized = false;

    public IQuest CurrentQuest => _quests[_currentQuestIndex];

    public void Initialize(QuestAnimationHandlerConfig questAnimationHandlerConfig, BagHandler bag)
    {
        Vector3 startPosition = transform.position;
        _questAnimationHandler = new QuestAnimationHandler(questAnimationHandlerConfig, _text, startPosition);

        InitializeQuestsList(bag);
        ChangeQuest();

        _isInitialized = true;
        OnEnable();
    }

    private void InitializeQuestsList(BagHandler bag)
    {
        _quests.Add(new ExitQuest(_text));
        _quests.Add(new KeyQuest(_text, bag));
        _quests.Add(new FinishQuest(_text));
    }

    private void OnEnable()
    {
        if(!_isInitialized)
            return;

        _questAnimationHandler.AppearingAnimationCompleted += ChangeQuest;
    }

    private void OnDisable()
    {
        _questAnimationHandler.AppearingAnimationCompleted -= ChangeQuest;
        _questAnimationHandler.OnDisable();
    }

    private void Update()
    {
        if(!_isInitialized)
            return;

        if(_quests[_currentQuestIndex].IsCompleted())
            _questAnimationHandler.AnimateCompleting();
    }

    private void ChangeQuest()
    {
        if(!TryGetNextQuestIndex())
            return;

        _quests[_currentQuestIndex].ChangeText();
        _questAnimationHandler.AnimateAppearing();
    }

    private bool TryGetNextQuestIndex()
    {
        for(int i = 0; i < _quests.Count; i++)
        {
            if(!_quests[i].IsCompleted())
            {
                _currentQuestIndex = i;
                return true;
            }
        }

        return false;
    }
}
