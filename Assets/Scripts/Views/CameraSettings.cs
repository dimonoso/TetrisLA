using UnityEngine;

namespace Tetris.Views
{
    public class CameraSettings : MonoBehaviour
    {
        [SerializeField]
        private float _baseScreenHeight;
        [SerializeField]
        private float _baseScreenWidth;

        private Camera _camera;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            var baseOrthographicSize = _camera.orthographicSize;
            var currentHeight = (float)_camera.pixelHeight;
            var currentWidth = (float)_camera.pixelWidth;

            _camera.orthographicSize = baseOrthographicSize
                                       / (currentWidth / currentHeight)
                                       * (_baseScreenWidth / _baseScreenHeight);
        }
    }
}
