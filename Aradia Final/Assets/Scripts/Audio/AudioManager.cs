using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public static class AudioManager
{
    static bool initialized = false;
    static GameObject gameAudioSourceObj;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();
    static Dictionary<AudioClipName, AudioSource> uniqueAudioSources =
        new Dictionary<AudioClipName, AudioSource>();
    static Dictionary<AudioClipName, AudioSource> loopingAudioSources =
        new Dictionary<AudioClipName, AudioSource>();

    /// <summary>
    /// Gets whether or not the audio manager has been initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">audio source</param>
    public static void Initialize(AudioSource source, GameObject audioSourceObj)
    {
        initialized = true;
        audioSource = source;
        gameAudioSourceObj = audioSourceObj;

        audioClips.Add(AudioClipName.menuClick, Resources.Load<AudioClip>(@"Audio\menu_click"));
        audioClips.Add(AudioClipName.menuClickPlay, Resources.Load<AudioClip>(@"Audio\menu_click_jugar"));
        audioClips.Add(AudioClipName.menuMusic, Resources.Load<AudioClip>(@"Audio\menu_musica"));
        audioClips.Add(AudioClipName.menuNavigation, Resources.Load<AudioClip>(@"Audio\menu_navegacion"));
        audioClips.Add(AudioClipName.basicAttack, Resources.Load<AudioClip>(@"Audio\ataque_basico"));
        audioClips.Add(AudioClipName.basicAttack2, Resources.Load<AudioClip>(@"Audio\ataque_basico_2"));
        audioClips.Add(AudioClipName.basicAttack3, Resources.Load<AudioClip>(@"Audio\ataque_basico_3"));
        audioClips.Add(AudioClipName.specialAttackCharge, Resources.Load<AudioClip>(@"Audio\ataque_especial_carga2"));
        audioClips.Add(AudioClipName.specialAttackFire, Resources.Load<AudioClip>(@"Audio\ataque_especial_lanzamiento"));
        audioClips.Add(AudioClipName.specialAttackImpact, Resources.Load<AudioClip>(@"Audio\ataque_especial_impacto"));
        audioClips.Add(AudioClipName.playerLand, Resources.Load<AudioClip>(@"Audio\diana_aterrizaje"));
        audioClips.Add(AudioClipName.playerJump, Resources.Load<AudioClip>(@"Audio\diana_salto"));
        audioClips.Add(AudioClipName.spikesImpact, Resources.Load<AudioClip>(@"Audio\impacto_pinchos2"));
        audioClips.Add(AudioClipName.rockImpact, Resources.Load<AudioClip>(@"Audio\impacto_roca"));
        audioClips.Add(AudioClipName.caveAmbiance, Resources.Load<AudioClip>(@"Audio\ambiente_cueva"));
        audioClips.Add(AudioClipName.healthResource, Resources.Load<AudioClip>(@"Audio\recurso_vida"));
        audioClips.Add(AudioClipName.music, Resources.Load<AudioClip>(@"Audio\musica"));
        audioClips.Add(AudioClipName.theDevil, Resources.Load<AudioClip>(@"Audio\the_devil"));
        audioClips.Add(AudioClipName.theEmperorImpact, Resources.Load<AudioClip>(@"Audio\the_emperor_golpe2"));
        audioClips.Add(AudioClipName.theEmperorEarthquake, Resources.Load<AudioClip>(@"Audio\the_emperor_terremoto"));
        audioClips.Add(AudioClipName.theEmpressCharge, Resources.Load<AudioClip>(@"Audio\the_empress_carga3"));
        audioClips.Add(AudioClipName.theEmpressSmoke, Resources.Load<AudioClip>(@"Audio\the_empress_humo"));
        audioClips.Add(AudioClipName.theSunFire, Resources.Load<AudioClip>(@"Audio\the_sun2"));
        audioClips.Add(AudioClipName.theSunCharge, Resources.Load<AudioClip>(@"Audio\the_sun_carga"));
        audioClips.Add(AudioClipName.theSunImpact, Resources.Load<AudioClip>(@"Audio\the_sun_golpe2"));
        audioClips.Add(AudioClipName.theFool, Resources.Load<AudioClip>(@"Audio\the_fool_escudo2"));
        audioClips.Add(AudioClipName.tarotCardsMovement, Resources.Load<AudioClip>(@"Audio\cartas-movimiento"));
        audioClips.Add(AudioClipName.tarotCardsReadingDeal, Resources.Load<AudioClip>(@"Audio\cartas-tirada"));
        audioClips.Add(AudioClipName.playerAttacked1, Resources.Load<AudioClip>(@"Audio\diana_golpe"));
        audioClips.Add(AudioClipName.playerAttacked2, Resources.Load<AudioClip>(@"Audio\diana_golpe2"));
        audioClips.Add(AudioClipName.playerDeath, Resources.Load<AudioClip>(@"Audio\diana_muerte"));
        audioClips.Add(AudioClipName.enemyDeath, Resources.Load<AudioClip>(@"Audio\enemigo_muerte"));
        audioClips.Add(AudioClipName.groundedEnemyGrowl, Resources.Load<AudioClip>(@"Audio\enemigo_terrestre_rugido"));
        audioClips.Add(AudioClipName.flyingEnemyGrowl, Resources.Load<AudioClip>(@"Audio\enemigo_volador_rugido2"));
        audioClips.Add(AudioClipName.flyingEnemyMissile, Resources.Load<AudioClip>(@"Audio\enemigo_misil2"));
        audioClips.Add(AudioClipName.skillUnavailable, Resources.Load<AudioClip>(@"Audio\no_disponible2"));
        audioClips.Add(AudioClipName.roomChange, Resources.Load<AudioClip>(@"Audio\transicion_salas"));
        audioClips.Add(AudioClipName.levelCompleted, Resources.Load<AudioClip>(@"Audio\final_nivel"));
        audioClips.Add(AudioClipName.enemyAttacked, Resources.Load<AudioClip>(@"Audio\enemigo_golpe"));
        audioClips.Add(AudioClipName.groundedEnemyAttack, Resources.Load<AudioClip>(@"Audio\enemigo_terrestre_ataque"));
        // audioClips.Add(AudioClipName.music, Resources.Load<AudioClip>(@"Audio\musica"));

    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }

    /// <summary>
    /// Plays the audio clip with the given name, but checks if the same clip is already playing to avoid overlapping
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void PlayUnique(AudioClipName name)
    {
        if (!uniqueAudioSources.ContainsKey(name))
        {
            AudioSource audioSource = gameAudioSourceObj.AddComponent<AudioSource>();
            audioSource.clip = audioClips[name];
            uniqueAudioSources.Add(name, audioSource);
            audioSource.Play();
        }
        else if (!uniqueAudioSources[name].isPlaying) uniqueAudioSources[name].Play();
    }

    public static void StopUnique(AudioClipName name)
    {
        if (uniqueAudioSources.ContainsKey(name)) uniqueAudioSources[name].Stop();
    }

    public static void PlayLoop(AudioClipName name)
    {
        if (!loopingAudioSources.ContainsKey(name))
        {
            AudioSource audioSource = gameAudioSourceObj.AddComponent<AudioSource>();
            audioSource.clip = audioClips[name];
            audioSource.loop = true;
            loopingAudioSources.Add(name, audioSource);
            audioSource.Play();
        }
        else if (!loopingAudioSources[name].isPlaying)
        {
            loopingAudioSources[name].volume = 1;
            loopingAudioSources[name].Play();
        }
    }

    public static void StopLoop(AudioClipName name)
    {
        if (loopingAudioSources.ContainsKey(name)) loopingAudioSources[name].Stop();
    }

    // static IEnumerator doFade(AudioSource audioSource, float duration)
    // {
    //     float counter = 0f;

    //     Debug.Log("fade about to start");

    //     while (counter < duration)
    //     {
    //         counter += Time.deltaTime;
    //         audioSource.volume = Mathf.Lerp(1, 0, counter / duration);
    //         Debug.Log("disminuyendo volumen");

    //         yield return null; //Because we don't need a return value.
    //     }

    //     audioSource.Stop();
    // }

}
