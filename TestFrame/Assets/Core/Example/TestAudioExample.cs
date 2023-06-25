using SumBorn.Core;
using SumBorn.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioType = SumBorn.Manager.AudioType;

public class TestAudioExample : MonoBehaviour
{
    private string music1 = "Audio/Sound/Уќжа-жэ";
    private string music2 = "";
    private string sound1 = "Audio/Sound/Уќжа-Ъї";
    private string sound2 = "";

    private int id1, id2, id3, id4;

    // Start is called before the first frame update
    void Start()
    {
        //id1 = AudioMgr.Instance.Play(music1, AudioType.Music,true);
        id2 = AudioMgr.Instance.Play(sound1, AudioType.Sound);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioMgr.Instance.Stop(id1);
        }
    }
}
