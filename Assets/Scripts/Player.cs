using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip _moveClip, _loseClip, _pointClip;

    [SerializeField] private GameplayManager _gm;
    [SerializeField] private GameObject _explosionPrefab, _scoreParticlePrefab;
    public GameObject _deathpanel;
    public GameObject _player;
    public GameObject _dusman;
    public GameObject _score;
    public GameObject _cemberler;

    public int MaxCan, Can;
    public GameObject[] Hearts;

    private bool canClick;

    private void Awake()
    {
        canClick = true;
        level = 0;
        currentRadius = _startRadius;
    }
    void can_azalt()
    {
        Can--;
        cansistemi();
    }
    void cansistemi()
    {
        for (int i = 0; i < MaxCan; i++)
        {
            Hearts[i].SetActive(false);
        }
        for (int i = 0; i < Can; i++)
        {
            Hearts[i].SetActive(true);
        }
    }

    private void Update()
    {
        if(canClick && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ChangeRadius());
            SoundManager.Instance.PlaySound(_moveClip);
        }
    }

    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Transform _rotateTransform;

    private void FixedUpdate()
    {
        transform.localPosition = Vector3.up * currentRadius;
        float rotateValue = _rotateSpeed * Time.fixedDeltaTime * _startRadius / currentRadius;
        _rotateTransform.Rotate(0, 0, rotateValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))

        {
            can_azalt();
            SoundManager.Instance.PlaySound(_loseClip);


            if (Can <= 0)
            {

                //Instantiate(_carpisma, transform.position, Quaternion.identity);
                Time.timeScale = 0f;
                _deathpanel.SetActive(true);
                _player.SetActive(false);
                _dusman.SetActive(false);
                _cemberler.SetActive(false);
                _score.SetActive(false);
                //_gm.GameEnded();


            }
            return;
        }

        if (collision.CompareTag("Score"))
        {
            Destroy(Instantiate(_scoreParticlePrefab, transform.position, Quaternion.identity),1f);
            SoundManager.Instance.PlaySound(_pointClip);
            _gm.UpdateScore();
            collision.gameObject.GetComponent<Score>().ScoreAdded();
            return;
        }
    }


    [SerializeField] private float _startRadius;
    [SerializeField] private float _moveTime;

    [SerializeField] private List<float> _rotateRadius;
    private float currentRadius;

    private int level;


    private IEnumerator ChangeRadius()
    {
        canClick = false;
        float moveStartRadius = _rotateRadius[level];
        float moveEndRadius = _rotateRadius[(level + 1) % _rotateRadius.Count];
        float moveOffset = moveEndRadius - moveStartRadius;
        float speed = 1 / _moveTime;
        float timeElasped = 0f;
        while(timeElasped < 1f)
        {
            timeElasped += speed * Time.fixedDeltaTime;
            currentRadius = moveStartRadius + timeElasped * moveOffset;
            yield return new WaitForFixedUpdate();
        }

        canClick = true;
        level = (level + 1) % _rotateRadius.Count;
        currentRadius = _rotateRadius[level];
    }
}
