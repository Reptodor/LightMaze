using System.Collections;
using TMPro;
using UnityEngine;

namespace _LightMaze._Scripts.Prologue
{
    public class DialogSystem : MonoBehaviour
    {
        [SerializeField] private string[] _dialogs;
        [SerializeField] private TextMeshProUGUI _dialogText;

        private Player _player;
        private bool _isSpacePressed;

        public void Initialize(Player player)
        {
            _player = player;
            StartCoroutine(StartDialog());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isSpacePressed = true;
            }
        }

        private IEnumerator StartDialog()
        {
            _dialogText.text = _dialogs[0];

            for (int i = 1; i < _dialogs.Length; i++)
            {
                _isSpacePressed = false;

                yield return new WaitWhile(() => !_isSpacePressed);

                _dialogText.text = _dialogs[i];
            }

            _player.IsStopped(false);
            gameObject.SetActive(false);
        }
    }
}