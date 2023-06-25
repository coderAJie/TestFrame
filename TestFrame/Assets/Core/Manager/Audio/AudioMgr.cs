using SumBorn.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SumBorn.Manager
{
    /// <summary>
    /// ������play���رգ�stop�����ã�replay
    /// 1.��������play��stop
    /// 2.Music��play��stop
    /// 3.Sound��play��stop
    /// 
    /// 1.ÿһ��item��һ��Ψһid�����Կ����ر�
    /// 2.ÿһ����ͬpath�Ķ��item������ͬ��path������һ�����ر�
    /// 3.ÿһ����ͬaudiotype�Ķ��item������һ�����ر�
    /// 4.musicĬ��ֻ��һ��
    /// </summary>

    public enum AudioType
    {
        Music,
        Sound
    }

    public class AudioMgr : Singleton<AudioMgr>
    {
        private AudioMixer _mixer;
        private readonly string _path="Audio/MasterMixer";

        private Dictionary<AudioType, AudioGroup> _groupDic;
        private Dictionary<string, AudioClip> _clipDic;

        private AudioItemPool _audioItemPool;

        public override void Initialize()
        {
            base.Initialize();

            _groupDic = new Dictionary<AudioType, AudioGroup>();
            _clipDic = new Dictionary<string, AudioClip>();

            //pool
            GameObject o = new GameObject(typeof(AudioItemPool).ToString());
            o.transform.SetParent(SingletonTrans);
            _audioItemPool = o.AddComponent<AudioItemPool>();

            LoadMixer();
        }

        public override void Release()
        {
            base.Release();
        }

        private void LoadMixer()
        {
            _mixer = Resources.Load<AudioMixer>(_path);

            Array array = Enum.GetValues(typeof(AudioType));
            foreach (var item in array)
            {
                AudioMixerGroup[] groups = _mixer.FindMatchingGroups(((AudioType)item).ToString());
                if (groups.Length > 0)
                {
                    AudioMixerGroup group = groups[0];
                    _groupDic.Add((AudioType)item, new AudioGroup(group));
                }
            }
        }

        public int Play(string path, AudioType type, bool isLoop = false)
        {
            AudioItem item = _audioItemPool.Get();
            item.Init(path, _groupDic[type], isLoop);
            item.Play();
            return item.Id;
        }

        public int Play(int id)
        {
            AudioItem item = _audioItemPool.FindById(id);
            if (item != null)
            {
                item.Play();
                return item.Id;
            }
            return -1;
        }

        public void Stop(int id)
        {
            AudioItem item = _audioItemPool.FindById(id);
            if (item != null)
            {
                item.Stop();
                _audioItemPool.Push(item);
            }
        }

        public void Pause(int id)
        {
            _audioItemPool.FindById(id)?.Pause();
        }
    }
}


