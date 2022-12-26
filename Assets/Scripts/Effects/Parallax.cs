using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _layers;
    [SerializeField, Range(0, 1)] private float[] _parallaxOnLayerX;
    [SerializeField, Range(0, 1)] private float _parallaxY = 0;

    private Transform _mainCam;
    private List<float> _startX = new List<float>();
    private List<float> _startY = new List<float>();
    private List<float> _length = new List<float>();

    private void Start()
    {
        _mainCam = Camera.main.transform;

        for (int i = 0; i < _layers.Length; i++)
        {
            _startX.Add(_layers[i].transform.position.x);
            _startY.Add(_layers[i].transform.position.y);
            _length.Add(_layers[i].bounds.size.x);

            _layers[i].size = new Vector2(_layers[i].size.x * 2, _layers[i].size.y);
        }
    }

    private void Update()
    {
        float camDeltaY = _mainCam.position.y * _parallaxY;

        for (int i = 0; i < _layers.Length; i++)
        {
            float varX = _startX[i] + _mainCam.position.x * _parallaxOnLayerX[i];
            float varY = _startY[i] + camDeltaY;
            Vector2 newPos = new Vector2(varX, varY);

            _layers[i].transform.position = newPos;


            if (_layers[i].transform.position.x < _mainCam.position.x - _length[i]/2)
            {
                _startX[i] += _length[i];
            }
            else if (_layers[i].transform.position.x > _mainCam.position.x + _length[i]/2)
            {
                _startX[i] -= _length[i];
            }
        }
    }
}
