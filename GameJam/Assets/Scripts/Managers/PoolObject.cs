using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private GameObjectPool m_Pool;

    public GameObjectPool Pool
    {
        get { return m_Pool; }
    }

    public void SetObjectPool(GameObjectPool pool)
    {
        m_Pool = pool;
    }

    public virtual void PoolRecycle()
    {
        m_Pool.RecycleGameObject(this);
    }

    public Dictionary<string, List<AudioSource>> AllAttachedAudioSources = new Dictionary<string, List<AudioSource>>();

    public void SoundPlay(string path, float volume = 1.0f)
    {
        AudioSource source = AudioManager.Instance.SoundPlay(path, volume);
        if (source != null)
        {
            if (!AllAttachedAudioSources.ContainsKey(source.clip.name))
            {
                AllAttachedAudioSources.Add(source.clip.name, new List<AudioSource>());
            }

            AllAttachedAudioSources[source.clip.name].Add(source);
        }
    }

    public void StopSoundPlay(string sourceName)
    {
        if (AllAttachedAudioSources.ContainsKey(sourceName))
        {
            List<AudioSource> sources = AllAttachedAudioSources[sourceName];
            foreach (AudioSource source in sources)
            {
                source.Stop();
            }

            sources.Clear();
        }
    }
}