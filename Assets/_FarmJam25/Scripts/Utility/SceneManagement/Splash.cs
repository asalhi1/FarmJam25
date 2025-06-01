using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace NJG.Utilities.SceneManagement
{
    public class Splash : MonoBehaviour
    {
        [FoldoutGroup("References"), SerializeField]
        private Image _logo;

        [FoldoutGroup("Settings"), SerializeField]
        private float _fadeDuration = 2f;
        // [FoldoutGroup("Settings"), SerializeField]
        // private string _nextSceneName = "2_MainMenu";

        private Tween _fadeTween;

        private void Start()
        {
            _logo.color = new Color(_logo.color.r, _logo.color.g, _logo.color.b, 0f);
            FadeIn();
        }

        private void FadeIn()
        {
            _fadeTween?.Kill();
            _fadeTween = _logo.DOFade(1f, _fadeDuration).OnComplete(OnFadeInCompleted);
        }

        private void OnFadeInCompleted() => FadeOut();

        private void FadeOut()
        {
            _fadeTween?.Kill();
            _fadeTween = _logo.DOFade(0f, _fadeDuration).OnComplete(OnFadeOutCompleted);
        }

        private void OnFadeOutCompleted()
        {
            //bl_SceneLoaderManager.LoadScene(_nextSceneName);
        }
    }
}