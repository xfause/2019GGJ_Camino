using UnityEngine;
using System;
using System.Collections.Generic;

public enum _eSoundLayer
{
    Background,
    Effect,
    EffectUI
}

public class KMSoundManager : MonoBehaviour
{
    SoundServer mSoundServer = new SoundServer();
    Dictionary<_eSoundLayer, ISoundLayer> mSoundLayers = new Dictionary<_eSoundLayer, ISoundLayer>();

    private static KMSoundManager _sInstance;
    public static KMSoundManager Instace
    {
        get
        {
            if (_sInstance == null)
            {
                _sInstance = FindObjectOfType(typeof(KMSoundManager)) as KMSoundManager;
                DontDestroyOnLoad(_sInstance);
            }

            if (_sInstance == null)
            {
                var obj = new GameObject("SoundManager");
                _sInstance = obj.AddComponent<KMSoundManager>();
                DontDestroyOnLoad(_sInstance);
                Debug.Log("SoundManager object not exist. will Generate Automatically.");
            }

            return _sInstance;
        }
    }

    public void OnApplicationQuit()
    {
        _sInstance = null;
    }

    void Awake()
    {
        mSoundServer.Create(KMGlobalDef.ResPath_Sound);
        mSoundLayers.Add(_eSoundLayer.Background, new SoundLayerBackground(mSoundServer));
        mSoundLayers.Add(_eSoundLayer.Effect, new SoundLayerEffect(mSoundServer, 5));
        mSoundLayers.Add(_eSoundLayer.EffectUI, new SoundLayerUI(mSoundServer, 3));
    }


    void Destroy()
    {
        foreach (var sound_layer in mSoundLayers)
        {
            sound_layer.Value.Destroy();
        }

        mSoundLayers.Clear();
    }

    public void SetVolume(_eSoundLayer layer, float volume)
    {
        mSoundLayers[layer].SetVolume(volume);
    }

    public void Play(_eSoundLayer layer, string sound_name)
    {
        if (string.IsNullOrEmpty(sound_name))
        {
            return;
        }
        mSoundLayers[layer].Play(sound_name);
    }

    public void Stop(_eSoundLayer layer)
    {
        mSoundLayers[layer].Stop();
    }
}

public class SoundServer
{
    GameObject mPlayer;
    string mSoundFileRootPath;

    public void Create(string sound_file_root_path)
    {
        mSoundFileRootPath = sound_file_root_path;
        mPlayer = new GameObject("SoundPlayer");
        mPlayer.transform.SetParent(KMSoundManager.Instace.transform);
    }

    AudioClip LoadSound(string sound_name)
    {
        return Resources.Load<AudioClip>(mSoundFileRootPath + sound_name);
    }

    public AudioSource CreateSoundPlayer()
    {
        return mPlayer.AddComponent<AudioSource>();
    }

    public void Play(AudioSource sound_player, string sound_name, bool is_loop)
    {
        if (sound_player == null) return;
        //sound_player.Stop();
        sound_player.loop = is_loop;
        sound_player.clip = LoadSound(sound_name);
        sound_player.Play();
    }

    public void Pause(AudioSource sound_player)
    {
        if (sound_player == null) return;
        sound_player.Pause();
    }

    public void Stop(AudioSource sound_player)
    {
        if (sound_player == null) return;
        sound_player.Stop();
    }

    public void DestroySoundPlayer(AudioSource sound_player)
    {
        if (sound_player == null) return;
        sound_player.Stop();
        UnityEngine.Object.Destroy(sound_player);
    }
}

public interface ISoundLayer
{
    void Play(string sound_name);
    void Stop();
    void SetVolume(float volume);

    void Destroy();
}

public class SoundLayerBackground : ISoundLayer
{
    SoundServer mSoundServer;
    AudioSource mSoundPlayer;

    public SoundLayerBackground(SoundServer sound_server)
    {
        mSoundServer = sound_server;
        mSoundPlayer = mSoundServer.CreateSoundPlayer();
    }

    public void Play(string sound_name)
    {
        if (IsSameSound(sound_name)) return;
        mSoundServer.Play(mSoundPlayer, sound_name, true);
    }

    public void Stop()
    {
        mSoundServer.Stop(mSoundPlayer);
    }

    public void SetVolume(float volume)
    {
        mSoundPlayer.volume = volume;
    }

    public void Destroy()
    {
        mSoundServer.DestroySoundPlayer(mSoundPlayer);
    }

    bool IsSameSound(string sound_name)
    {
        return mSoundPlayer.clip != null && mSoundPlayer.clip.name == sound_name;
    }
}

public class SoundLayerEffect : ISoundLayer
{
    SoundServer mSoundServer;
    Queue<AudioSource> mSoundPlayers = new Queue<AudioSource>();

    public SoundLayerEffect(SoundServer sound_server, int track_number)
    {
        mSoundServer = sound_server;
        mSoundPlayers = new Queue<AudioSource>(track_number);
        while (track_number > 0)
        {
            mSoundPlayers.Enqueue(mSoundServer.CreateSoundPlayer());
            track_number--;
        }
    }

    public void Play(string sound_name)
    {
        AudioSource current_player = mSoundPlayers.Dequeue();
        mSoundServer.Play(current_player, sound_name, false);
        mSoundPlayers.Enqueue(current_player);
    }

    public void Stop()
    {
        foreach (var sound_player in mSoundPlayers)
        {
            mSoundServer.Stop(sound_player);
        }
    }

    public void SetVolume(float volume)
    {
        foreach (var sound_player in mSoundPlayers)
        {
            sound_player.volume = volume;
        }
    }

    public void Destroy()
    {
        foreach (var sound_player in mSoundPlayers)
        {
            mSoundServer.DestroySoundPlayer(sound_player);
        }
    }
}
public class SoundLayerUI : ISoundLayer
{
    SoundServer mSoundServer;
    Queue<AudioSource> mSoundPlayers = new Queue<AudioSource>();

    public SoundLayerUI(SoundServer sound_server, int track_number)
    {
        mSoundServer = sound_server;
        mSoundPlayers = new Queue<AudioSource>(track_number);
        while (track_number > 0)
        {
            mSoundPlayers.Enqueue(mSoundServer.CreateSoundPlayer());
            track_number--;
        }
    }

    public void Play(string sound_name)
    {
        AudioSource current_player = mSoundPlayers.Dequeue();
        mSoundServer.Play(current_player, sound_name, false);
        mSoundPlayers.Enqueue(current_player);
    }

    public void Stop()
    {
        foreach (var sound_player in mSoundPlayers)
        {
            mSoundServer.Stop(sound_player);
        }
    }

    public void SetVolume(float volume)
    {
        foreach (var sound_player in mSoundPlayers)
        {
            sound_player.volume = volume;
        }
    }

    public void Destroy()
    {
        foreach (var sound_player in mSoundPlayers)
        {
            mSoundServer.DestroySoundPlayer(sound_player);
        }
    }
}