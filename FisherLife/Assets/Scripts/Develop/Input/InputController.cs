using UnityEngine;

public class InputController : MonoBehaviour
{
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
    }
}
