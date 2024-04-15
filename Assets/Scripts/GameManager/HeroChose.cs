using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class HeroChose : MonoBehaviour
{
    [SerializeField] private LoseScene loseScene;

    private Material _originalMaterialEnemy;
    private Transform _highlight;
    private Transform _highlightEnemy;
    private Transform _selection;
    private Transform _enemySelection;
    private RaycastHit _raycastHit;
    private Character _hero;
    private List<GameObject> _queue;
    private GameObject _temp;
    private UnitSetup _unit;
    private OurHand _ourHand;

    public List<GameObject> List;
    public GameObject[] Enemies;
    public GameObject[] Heroes;
    public List<GameObject> EnemiesGroup;
    public List<GameObject> HeroesGroup;

    private bool _heroIsSelected = false;
    private float _coolDown = 1f;
    private float _timer;
    private int _count = 1;
    private bool _stageStarted = false;
    private bool _teamIsReady = false;
    private bool _gameOver = false;
    public bool CanAttack { get; private set; }
    public int сurrentCharacter = 0;

    private void Start()
    {
        _ourHand = FindObjectOfType<OurHand>();
        _unit = FindObjectOfType<UnitSetup>();
        Enemies = GameObject.FindGameObjectsWithTag("enemy");
        _gameOver = false;
    }

    private void Update()
    {
        if (_gameOver == false)
        {
            if (_teamIsReady == false && _unit.IsStarted == true)
            {
                Heroes = GameObject.FindGameObjectsWithTag("hero");
                List.AddRange(Heroes);
                HeroesGroup.AddRange(Heroes);
                List.AddRange(Enemies);
                EnemiesGroup.AddRange(Enemies);
                SortMassive();
                _teamIsReady = true;
            }

            CDAttack();

            if (_ourHand.Army.Count == 0 && _unit.IsStarted == true && _gameOver == false)
            {
                _gameOver = true;
                _ourHand.Save();
                AudioManager.instance.PlayMusic("Lose");
                loseScene.LoseGame();
            }

            if (EnemiesGroup.Count == 0 && _unit.IsStarted == true && _gameOver == false)
            {
                _gameOver = true;
                _ourHand.Save();
                
                if (PlayerPrefs.GetInt("numOfLevel") == 1)
                {
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 4500);
                    PlayerPrefs.SetInt("Leadership", PlayerPrefs.GetInt("Leadership") + 80);
                }
                if (PlayerPrefs.GetInt("numOfLevel") == 2)
                {
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 4800);
                    PlayerPrefs.SetInt("Leadership", PlayerPrefs.GetInt("Leadership") + 85);
                }
                
                if (PlayerPrefs.GetInt("numOfLevel") == 3)
                {
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 5300);
                    PlayerPrefs.SetInt("Leadership", PlayerPrefs.GetInt("Leadership") + 90);
                }
                
                if (PlayerPrefs.GetInt("numOfLevel") == 4)
                {
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 7500);
                    PlayerPrefs.SetInt("Leadership", PlayerPrefs.GetInt("Leadership") + 110);
                }
                
                if (PlayerPrefs.GetInt("numOfLevel") == 5)
                {
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 12500);
                    PlayerPrefs.SetInt("Leadership", PlayerPrefs.GetInt("Leadership") + 150);
                }
                
                if (PlayerPrefs.GetInt("numOfLevel") == 6)
                {
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 7500);
                    PlayerPrefs.SetInt("Leadership", PlayerPrefs.GetInt("Leadership") + 80);
                }
                
                if (PlayerPrefs.GetInt("numOfLevel") == 7)
                {
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 7000);
                    PlayerPrefs.SetInt("Leadership", PlayerPrefs.GetInt("Leadership") + 100);
                }
                
                if (PlayerPrefs.GetInt("numOfLevel") == 8)
                {
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 7500);
                    PlayerPrefs.SetInt("Leadership", PlayerPrefs.GetInt("Leadership") + 105);
                }
                
                if (PlayerPrefs.GetInt("numOfLevel") == 9)
                {
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 8000);
                    PlayerPrefs.SetInt("Leadership", PlayerPrefs.GetInt("Leadership") + 110);
                }
                
                if (PlayerPrefs.GetInt("numOfLevel") == 10)
                {
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 15000);
                    PlayerPrefs.SetInt("Leadership", PlayerPrefs.GetInt("Leadership") + 150);
                }
                
                PlayerPrefs.SetInt("numOfLevel", PlayerPrefs.GetInt("numOfLevel") + 1);
                
                if (PlayerPrefs.GetInt("numOfLevel") == 6) PlayerPrefs.SetInt("numOfChapter", PlayerPrefs.GetInt("numOfChapter") + 1);
                
                AudioManager.instance.musicSource.Stop();
                AudioManager.instance.PlaySFX("Win");
                loseScene.WinGame();
            }

            if (_highlightEnemy != null)
            {
                _highlightEnemy.GetComponent<Character>().Circle.SetActive(false);
                _highlightEnemy = null;
            }

            if (!_stageStarted && _unit.IsStarted == true)
            {
                _stageStarted = true;
                StartBattle();
            }

            CheckForOutOfRange();

            HighlightEnemy();

            SelectEnemy();
        }
    }

    private void CDAttack()
    {
        if (CanAttack)
        {
            return;
        }

        _timer += Time.deltaTime;

        if (_timer < _coolDown)
        {
            return;
        }

        CanAttack = true;
        _timer = 0;
    }

    private void StartBattle()
    {
        if (сurrentCharacter < List.Count)
        {
            if (List[сurrentCharacter].CompareTag("enemy"))
            {
                StartCoroutine(EnemyAttack());
            }
            else
            {
                List[сurrentCharacter].GetComponent<Character>().Circle.SetActive(true);
                _heroIsSelected = true;
                _hero = List[сurrentCharacter].GetComponent<Character>();
            }
        }
        else
        {
            сurrentCharacter = 0;
            _stageStarted = false;
        }
    }

    IEnumerator EnemyAttack()
    {
        if (_gameOver == false)
        {
            yield return new WaitForSeconds(1);
            if (_gameOver == false) List[сurrentCharacter].GetComponent<Character>().Circle.SetActive(true);
            int rand = Random.Range(0, HeroesGroup.Count);
            if (_gameOver == false) HeroesGroup[rand].GetComponent<Character>().Circle.SetActive(true);
            yield return new WaitForSeconds(1);
            if (_gameOver == false)
            {
                string enemyAttack = List[сurrentCharacter].GetComponent<Character>().gameObject.name;
                Debug.Log(enemyAttack);
                AudioManager.instance.PlaySFX(enemyAttack);
                HeroesGroup[rand].GetComponent<Character>().GetDamage(
                    List[сurrentCharacter].GetComponent<Character>().Damage,
                    List[сurrentCharacter].GetComponent<Character>().Count);
                сurrentCharacter += 1;
                HeroesGroup[rand].GetComponent<Character>().Circle.SetActive(false);
                _stageStarted = false;
            }
        }
    }

    private void SortMassive()
    {
        while (_count != 0)
        {
            _count = 0;
            for (int i = 0; i < List.Count - 1; i++)
            {
                if (List[i].GetComponent<Character>().Speed == List[i + 1].GetComponent<Character>().Speed)
                {
                    if (List[i].CompareTag("enemy") && List[i + 1].CompareTag("hero"))
                    {
                        _temp = List[i];
                        List[i] = List[i + 1];
                        List[i + 1] = _temp;
                        _count = 1;
                    }
                }

                if (List[i].GetComponent<Character>().Speed < List[i + 1].GetComponent<Character>().Speed)
                {
                    _temp = List[i];
                    List[i] = List[i + 1];
                    List[i + 1] = _temp;
                    _count = 1;
                }
            }
        }
    }

    private void CheckForOutOfRange()
    {
        if (сurrentCharacter < List.Count)
        {
            if (List[сurrentCharacter].CompareTag("hero"))
            {
                foreach (var enemy in EnemiesGroup)
                {
                    enemy.GetComponent<Character>().Circle.SetActive(true);
                }
            }
            else
            {
                foreach (var enemy in EnemiesGroup)
                {
                    enemy.GetComponent<Character>().Circle.SetActive(false);
                }
            }
        }
        else
        {
            сurrentCharacter = 0;
            StartBattle();
        }
    }

    private void HighlightEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out _raycastHit) && _hero != null &&
            List[сurrentCharacter].CompareTag("hero"))
        {
            _highlightEnemy = _raycastHit.transform;
            if (_highlightEnemy.CompareTag("enemy") && _highlightEnemy != _selection && _heroIsSelected == true)
            {
                if (_highlightEnemy.GetComponent<Character>().Circle.transform.position == new Vector3(0, 0, 0))
                {
                    
                }
            }
            else
            {
                _highlightEnemy = null;
            }
        }
    }

    private void SelectEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKey(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject() && _hero != null &&
            List[сurrentCharacter].CompareTag("hero"))
        {
            if (_enemySelection != null)
            {
                _enemySelection.GetComponent<Character>().Circle.SetActive(false);
                _enemySelection = null;
            }

            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out _raycastHit))
            {
                _enemySelection = _raycastHit.transform;
                if (_enemySelection.CompareTag("enemy") && CanAttack == true)
                {
                    string heroesAttack = List[сurrentCharacter].GetComponent<Character>().gameObject.name;
                    Debug.Log(heroesAttack);
                    CanAttack = false;
                    AudioManager.instance.PlaySFX(heroesAttack);
                    _enemySelection.GetComponent<Character>().GetDamage(_hero.Damage, _hero.Count);
                    List[сurrentCharacter].GetComponent<Character>().Circle.SetActive(false);;
                    _stageStarted = false;
                    _heroIsSelected = false;
                    сurrentCharacter += 1;
                    if (сurrentCharacter >= List.Count)
                    {
                        сurrentCharacter = 0;
                    }
                }
                else
                {
                    _enemySelection = null;
                }
            }
        }
    }
}