using UnityEngine;

public class InputContainer : MonoBehaviour
{
    public InputActionMapSwitcher MapSwitcher => _mapSwitcher;

    private FisherLifeInputActions _actions;
    private InputActionMapSwitcher _mapSwitcher;

    private TypingInputHandler _handler;

    private void Awake()
    {
        if (this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }

        _actions = new FisherLifeInputActions();
    }

    private void Start()
    {
        _mapSwitcher = new InputActionMapSwitcher(_actions);
        _handler = new TypingInputHandler(_actions);
    }

    private void OnDestroy()
    {
        _mapSwitcher.Dispose();
        _handler.Dispose();
    }
}
