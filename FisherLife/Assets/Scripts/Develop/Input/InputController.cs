using UnityEngine;

public class InputContainer : MonoBehaviour 
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

    private void Start()
    {
        
    }
}
