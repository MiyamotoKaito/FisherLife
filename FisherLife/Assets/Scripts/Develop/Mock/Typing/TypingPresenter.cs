using Cysharp.Threading.Tasks;
using Miyamoto.FisherLife.Develop.Mock.Typing;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

public class TypingPresenter : MonoBehaviour
{
    [SerializeField] private AssetReference _questionListTextAsset;

    private TypingView _view;
    private TypingModel _model;
    private LoadWords _loadWords;

    [Inject]private readonly InputActionMapSwitcher _mapSwitcher;
    private void Awake()
    {
        _view = GetComponent<TypingView>();
        _model = new TypingModel();
        GetWordList().Forget();
    }
    private void Start()
    {
        _mapSwitcher.PushMap(ActionMapType.Typing);
        _mapSwitcher.ChangeMap(ActionMapType.Typing);
    }

    private async UniTask GetWordList()
    {
        var textAsset = Addressables.LoadAssetAsync<TextAsset>(_questionListTextAsset);

        await textAsset.Task;

        _loadWords = new LoadWords(textAsset.Result);
    }
}
