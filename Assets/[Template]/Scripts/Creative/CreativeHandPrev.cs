using UnityEngine;

namespace Template
{
    public class CreativeHandPrev : MonoBehaviour
    {
        [SerializeField] private Vector2 _offset;
        [SerializeField] private float _distanceFromCamera = 10f;
        private bool _isOn = true;
        private UnityEngine.Camera _mainCamera;
        private Vector3 _pos;
        private Vector3 _mousePos;
        
        public GameObject rend1;
        public GameObject rend2;

        private void Start() {
            Init();
        }

        private void Init()
        {
            _offset = Vector2.zero;
            _mainCamera = Camera.main;
        }

        private void Update() {
            
            //if(Input.GetKeyDown("a"))
            //{
            //    _isOn = !_isOn;
            //}

            if(Input.GetMouseButtonDown(0))
            {
                rend1.SetActive(false);
                rend2.SetActive(true);
            }

            if(Input.GetMouseButtonUp(0))
            {
                rend1.SetActive(true);
                rend2.SetActive(false);
            }

            if(_isOn)
            {
                _mousePos = Input.mousePosition;

                _mousePos.z = _distanceFromCamera;

                _pos = _mainCamera.ScreenToWorldPoint(_mousePos);

                _pos += new Vector3(_offset.x, _offset.y, 0f);

                transform.position = _pos;
            }

        }
    }
}
