using UnityEngine;
using System.Collections;

public class MenuMusicScript : MonoBehaviour {
    // singleton pattern
    // prevent more then one music instance created
    private static MenuMusicScript instance = null;
    public static MenuMusicScript Instance
    {
        get
        {
            return instance;
        }
    }

	void Awake () {
	    if(instance != null && instance != this)
        {
            Destroy(this.gameObject); // destroy self if other instance is running
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
	}
}
