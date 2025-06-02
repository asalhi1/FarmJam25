using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NJG.Utilities
{
    [RequireComponent(typeof(Image))]
    public class UIImageSpriteAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Sprite[] _sprites;
        [SerializeField]
        private float _framesPerSecond = 10f;
        [SerializeField]
        private bool _playOnStart;
        [SerializeField]
        private bool _autoLoop;
        [SerializeField]
        private bool _isForButton;

        private Image _image;
        private int _currentFrame;
        private float _timer;
        private bool _isPlaying;

        private void Awake()
        {
            _image = GetComponent<Image>();
            FullReset();
        }

        private void Start()
        {
            if (_playOnStart)
                _isPlaying = true;
        }

        private void Update()
        {
            if (!_isPlaying || _sprites.Length == 0) return;

            _timer += Time.deltaTime;
            if (_timer >= 1f / _framesPerSecond)
            {
                _currentFrame = (_currentFrame + 1) % _sprites.Length;
                _image.sprite = _sprites[_currentFrame];
                _timer = 0f;
            }
            
            if (_currentFrame == _sprites.Length - 1 && !_autoLoop)
            {
                _isPlaying = false;
                FullReset();
            }
        }
        
        public void Play(bool loop = false)
        {
            _isPlaying = true;
            _autoLoop = loop;
        }
        
        public void Pause()
        {
            _isPlaying = false;
        }
        
        public void Stop()
        {
            _isPlaying = false;
            FullReset();
        }

        public void FullReset()
        {
            _currentFrame = 0;
            if (_sprites.Length > 0)
            {
                _image.sprite = _sprites[0];
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isForButton)
                return;
            
            _isPlaying = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isForButton)
                return;

            _isPlaying = false;
        }
    }
}