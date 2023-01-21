using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    private static SceneTransition _instance;
    private static bool _shouldPlayOpeningAnimation = false;
    private static float _musicVolume;

    private Animator _componentAnimator;
    private AsyncOperation _loadingSceneOperation;


    public static void SwitchToScene(int sceneIndex)
    {
        float volume;
        _instance._audioMixer.GetFloat("volumeMusic", out volume);
        _musicVolume = Mathf.Pow(10, volume / 20);

        _instance.StartCoroutine(FadeMixerGroup.StartFade(_instance._audioMixer, "volumeMusic", 1f, 0f));

        _instance._componentAnimator.SetTrigger("sceneEnding");

        _instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneIndex);

        _instance._loadingSceneOperation.allowSceneActivation = false;
    }

    private void Start()
    {
        _instance = this;

        _componentAnimator = GetComponent<Animator>();

        if (_shouldPlayOpeningAnimation)
        {
            _instance.StartCoroutine(FadeMixerGroup.StartFade(_instance._audioMixer, "volumeMusic", 1f, _musicVolume));

            _componentAnimator.SetTrigger("sceneOpening");

            _shouldPlayOpeningAnimation = false;
        }
    }

    public void OnAnimationOver()
    {
        _shouldPlayOpeningAnimation = true;

        _loadingSceneOperation.allowSceneActivation = true;
    }
}
