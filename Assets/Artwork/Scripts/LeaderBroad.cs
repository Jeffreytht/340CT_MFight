using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBroad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         AudioController.getInstance().PlaySong(AudioController.GameState.Menu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
