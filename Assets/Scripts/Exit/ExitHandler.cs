using UnityEngine;

public class ExitHandler : MonoBehaviour
{    
    private ExitHandlerConfig _exitHandlerConfig;
    private SceneLoader _sceneLoader;
    private QuestHandler _questHandler;

    public void Initialize(ExitHandlerConfig exitHandlerConfig, SceneLoader sceneLoader, QuestHandler questHandler)
    {
        _exitHandlerConfig = exitHandlerConfig;
        _sceneLoader = sceneLoader;
        _questHandler = questHandler;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if(player != null)
        {
            if(_questHandler.CurrentQuest.GetType() == typeof(ExitQuest))
            {
                _questHandler.CurrentQuest.Complete();
            }
            if(player.BagHandler.IsEnoughKeys())
            {
                _questHandler.CurrentQuest.Complete();
                FinishLevel();
            }
        }
    }

    private void FinishLevel()
    {
        Coroutines.StartRoutine(_sceneLoader.LoadScene(_exitHandlerConfig.NextSceneName, _exitHandlerConfig.LoadingTime));
    }
}
