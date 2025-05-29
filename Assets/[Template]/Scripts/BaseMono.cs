using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Video;

public abstract class BaseMono : MonoBehaviour
{
    private Transform _transform;
    private VideoPlayer _videoPlayer;
    private Light _light;
    private Animator _animator;
    private Rigidbody _rigidBody;
    private Rigidbody2D _physics2D;
    private Collider _collider;
    private Collider2D _collider2D;
    private BoxCollider _boxCollider;
    private BoxCollider2D _boxCollider2D;
    private SphereCollider _sphereCollider;
    private CapsuleCollider _capsuleCollider;
    private MeshCollider _meshCollider;
    private Renderer _renderer;
    private MeshRenderer _meshRenderer;
    private SpriteRenderer _spriteRenderer;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private MeshFilter _meshFilter;
    private Camera _camera;
    private AudioSource _audioSource;
    private TrailRenderer _trailRenderer;
    private Image _image;
    private NavMeshAgent _navMeshAgent;
    private NavMeshObstacle _navMeshObstacle;
    private RectTransform _rectTransform;
    private Button _button;
    private CanvasGroup _canvasGroup;


    public CanvasGroup CanvasGroup
    {
        get
        {
            if (_canvasGroup == null)
                _canvasGroup = GetComponent<CanvasGroup>();

            return _canvasGroup;
        }
    }
    public Button Button
    {
        get
        {
            if (_button == null)
                _button = GetComponent<Button>();

            return _button;
        }
    }

    public RectTransform RectTransform
    {
        get
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();

            return _rectTransform;
        }
    }
    public Transform Transform
    {
        get
        {
            if (_transform == null)
                _transform = transform;

            return _transform;
        }
    }


    public VideoPlayer VideoPlayer
    {
        get
        {
            if (_videoPlayer == null)
                _videoPlayer = GetComponent<VideoPlayer>();

            return _videoPlayer;
        }
    }


    public Light Light
    {
        get
        {
            if (_light == null)
            {
                _light = GetComponent<Light>();

                if (_light == null)
                {
                    foreach (Transform child in transform)
                        if (child.GetComponent<Light>())
                        {
                            _light = child.GetComponent<Light>();

                            break;
                        }
                }
            }

            return _light;
        }
    }


    public Animator Animator
    {
        get
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();

                if(_animator == null)
                {
                    foreach (Transform child in transform)
                    {
                        if(child.gameObject.activeSelf &&
                           child.GetComponent<Animator>())
                        {
                            _animator = child.GetComponent<Animator>();

                            break;
                        }
                        
                        foreach (Transform child1 in child)
                        {
                            if(child1.gameObject.activeSelf &&
                               child1.GetComponent<Animator>())
                            {
                                _animator = child1.GetComponent<Animator>();

                                break;
                            }
                        }
                    }
                }
            }

            return _animator;
        }
    }


    public Rigidbody Rigidbody
    {
        get
        {
            if (_rigidBody == null)
                _rigidBody = GetComponent<Rigidbody>();

            return _rigidBody;
        }
    }


    public Rigidbody2D Rigidbody2D
    {
        get
        {
            if (_physics2D == null)
                _physics2D = GetComponent<Rigidbody2D>();

            return _physics2D;
        }
    }


    public Collider Collider
    {
        get
        {
            if (_collider == null)
                _collider = GetComponent<Collider>();

            return _collider;
        }
    }

    public Collider2D Collider2D
    {
        get
        {
            if (_collider2D == null)
                _collider2D = GetComponent<Collider2D>();

            return _collider2D;
        }
    }


    public BoxCollider BoxCollider
    {
        get
        {
            if (_boxCollider == null)
                _boxCollider = GetComponent<BoxCollider>();

            return _boxCollider;
        }
    }


    public BoxCollider2D BoxCollider2D
    {
        get
        {
            if (_boxCollider2D == null)
                _boxCollider2D = GetComponent<BoxCollider2D>();

            return _boxCollider2D;
        }
    }


    public SphereCollider SphereCollider
    {
        get
        {
            if (_sphereCollider == null)
                _sphereCollider = GetComponent<SphereCollider>();

            return _sphereCollider;
        }
    }


    public CapsuleCollider CapsuleCollider
    {
        get
        {
            if (_capsuleCollider == null)
                _capsuleCollider = GetComponent<CapsuleCollider>();

            return _capsuleCollider;
        }
    }


    public MeshCollider MeshCollider
    {
        get
        {
            if (_meshCollider == null)
                _meshCollider = GetComponent<MeshCollider>();

            return _meshCollider;
        }
    }


    public Renderer Renderer
    {
        get
        {
            if (_renderer == null)
                _renderer = GetComponent<Renderer>();

            return _renderer;
        }
    }


    public MeshRenderer MeshRenderer
    {
        get
        {
            if (_meshRenderer == null)
                _meshRenderer = GetComponent<MeshRenderer>();

            return _meshRenderer;
        }
    }


    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            return _spriteRenderer;
        }
    }


    public SkinnedMeshRenderer SkinnedMeshRenderer
    {
        get
        {
            if (_skinnedMeshRenderer == null)
                _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

            return _skinnedMeshRenderer;
        }
    }


    public MeshFilter MeshFilter
    {
        get
        {
            if (_meshFilter == null)
                _meshFilter = GetComponent<MeshFilter>();

            return _meshFilter;
        }
    }


    public Camera Camera
    {
        get
        {
            if (_camera == null)
                _camera = GetComponent<Camera>();

            return _camera;
        }
    }


    public AudioSource AudioSource
    {
        get
        {
            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();

            return _audioSource;
        }
    }


    public TrailRenderer TrailRenderer
    {
        get
        {
            if (_trailRenderer == null)
                _trailRenderer = GetComponent<TrailRenderer>();

            return _trailRenderer;
        }
    }


    public Image Image
    {
        get
        {
            if (_image == null)
                _image = GetComponent<Image>();

            return _image;
        }
    }


    public NavMeshAgent NavMeshAgent
    {
        get
        {
            if (_navMeshAgent == null)
                _navMeshAgent = GetComponent<NavMeshAgent>();

            return _navMeshAgent;
        }
    }


    public NavMeshObstacle NavMeshObstacle
    {
        get
        {
            if (_navMeshObstacle == null)
                _navMeshObstacle = GetComponent<NavMeshObstacle>();

            return _navMeshObstacle;
        }
    }
}
