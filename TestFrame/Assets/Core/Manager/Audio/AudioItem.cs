using SumBorn.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SumBorn.Core
{
    public class AudioItem
    {
        public int Id { get { return _id; } }
        
        private int _id;
        private AudioClip _clip;
        private AudioSource _source;
        private GameObject _obj;
        private string _path;
        private AudioGroup _group;
        private bool _isLoop;

        IEnumerator _iPlay;

        public AudioItem(Transform parent)
        {
            _obj = new GameObject();
            _obj.transform.SetParent(parent);
            _obj.SetActive(false);
            _id = _obj.GetInstanceID();
            _source = _obj.AddComponent<AudioSource>();
            _iPlay = IPlay();
        }

        public void Init(string path, AudioGroup group, bool isLoop = false)
        {
            _obj.SetActive(true);
            _path = path;
            _group = group;
            _isLoop = isLoop;
            _source.outputAudioMixerGroup = _group.Mixer;
            _source.loop = _isLoop;

            _clip = Resources.Load<AudioClip>(_path);
            _source.clip = _clip;
        }

        public void Play()
        {
            if (_source.isPlaying) return;
            MonoLifeMgr.Instance.AddCoroutine(_iPlay);
        }

        IEnumerator IPlay()
        {
            _source.Play();
            bool b = true;
            while (b)
            {
                float f = _clip.length - _source.time;
                if (f.Equals(0)) b = false;
                yield return null;
            }
            Stop();
        }

        public void Stop()
        {
                _source.Stop();
                MonoLifeMgr.Instance.RemoveCoroutine(_iPlay);
            OnPush();
            if (_source.isPlaying)
            {
            }
        }

        public void Pause()
        {
            if (_source.isPlaying)
            {
                _source.Pause();
            }
        }

        public void OnPush()
        {
            _obj.SetActive(false);
            _source.Stop();
        }
    }

    public class AudioItemPool:MonoBehaviour
    {
        private Pool<AudioItem> _audioPool;

        private Dictionary<int, AudioItem> _audioItemDic;

        private void Awake()
        {
            _audioPool = new Pool<AudioItem>(OnCreateAudioItem,null,OnPushAudioItem);

            _audioItemDic = new Dictionary<int, AudioItem>();
        }

        private AudioItem OnCreateAudioItem()
        {
            AudioItem item = new AudioItem(transform);
            return item;
        }

        private void OnPushAudioItem(AudioItem item)
        {
            item.OnPush();
        }

        public AudioItem Get()
        {
            AudioItem item = _audioPool.Get();
            _audioItemDic.Add(item.Id,item);
            return item;
        }

        public void Push(AudioItem item)
        {
            _audioItemDic.Remove(item.Id);
            _audioPool.Push(item);
        }

        public AudioItem FindById(int id)
        {
            return _audioItemDic[id];
        }
    }
}