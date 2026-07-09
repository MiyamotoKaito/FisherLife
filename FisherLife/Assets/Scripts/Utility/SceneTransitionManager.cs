using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utility
{
    public class SceneTransitionManager : MonoBehaviour
    {
        public static SceneTransitionManager Instance { get; private set; }

        public Image Image => _image;

        [SerializeField]
        private Image _image;
        [SerializeField]
        private Ease _ease;

        [SerializeField]
        private GameObject _canvas;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _canvas.SetActive(false);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static async UniTask IrisIn(string sceneName)
        {
            Instance._canvas.SetActive(true);
            await Instance.Image.rectTransform
                .DOScale(0f, 2f).SetEase(Instance._ease)
                .ToUniTask();

            await SceneManager.LoadSceneAsync(sceneName);
            await IrisOut();
        }
        public static async UniTask IrisOut()
        {
            await Instance.Image.rectTransform
                 .DOScale(2.6f, 2f).SetEase(Instance._ease)
                 .ToUniTask();

            Instance._canvas.SetActive(false);
        }
    }
}
