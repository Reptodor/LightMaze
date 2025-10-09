using LightMaze._Scripts.SceneLoader;
using UnityEngine;

public class ExitHandler : MonoBehaviour
{
    [SerializeField] private int _nextGameplaySceneNumber;
    private SceneLoader _sceneLoader;
    private QuestHandler _questHandler;

    private bool _isFinished;

    public void Initialize(SceneLoader sceneLoader, QuestHandler questHandler)
    {
        _sceneLoader = sceneLoader;
        _questHandler = questHandler;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isFinished)
            return;

        if (other.TryGetComponent(out Player player))
            {
                if (_questHandler.CurrentQuest.GetType() == typeof(ExitQuest))
                {
                    _questHandler.CurrentQuest.Complete();
                }
                if (player.BagHandler.IsEnoughKeys())
                {
                    _questHandler.CurrentQuest.Complete();
                    _isFinished = true;
                    FinishLevel();
                }
            }
    }

    private void FinishLevel()
    {
        _sceneLoader.LoadLevel(_nextGameplaySceneNumber);
    }
}
