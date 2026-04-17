using UnityEngine;

public class InputContaine : MonoBehaviour
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
