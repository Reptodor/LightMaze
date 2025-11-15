using UnityEngine;

namespace _LightMaze._Scripts.Prologue
{
    public class BossDoor : MonoBehaviour
    {
        [SerializeField] private DialogSystem _dialogSystem;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                player.IsStopped(true);
                _dialogSystem.gameObject.SetActive(true);
                _dialogSystem.Initialize(player);
            }
        }
    }
}