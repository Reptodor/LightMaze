using UnityEngine;

public class ExitHandler : MonoBehaviour
{
    [SerializeField] private int _nextGameplaySceneNumber;
    private SceneLoader _sceneLoader;
    private QuestHandler _questHandler;

    public void Initialize(SceneLoader sceneLoader, QuestHandler questHandler)
    {
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
        _sceneLoader.LoadSceneWithLoadingScreen(_sceneLoader.SceneNamesConfig.GameplayScenesNames[_nextGameplaySceneNumber],
                                                _sceneLoader.ScenesLoadingTimeConfig.GameplayScenesLoadingTime);
    }
}
