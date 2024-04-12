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
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material highlightMaterialForEnemy;
    [SerializeField] private Material originalMaterial;

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
    public bool CanAttack { get; private set; }
    public int сurrentCharacter = 0;

    private void Start()
    {
        _ourHand = FindObjectOfType<OurHand>();
        _unit = FindObjectOfType<UnitSetup>();
        Enemies = GameObject.FindGameObjectsWithTag("enemy");
    }

    private void Update()
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

        if (_ourHand.Army.Count == 0 && _unit.IsStarted == true)
        {
            SceneManager.LoadScene(1);
            _ourHand.Save();
        }

        if (EnemiesGroup.Count == 0 && _unit.IsStarted == true)
        {
            SceneManager.LoadScene(1);
            _ourHand.Save();
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
        yield return new WaitForSeconds(1);
        List[сurrentCharacter].GetComponent<Character>().Circle.SetActive(true);
        int rand = Random.Range(0, HeroesGroup.Count);
        HeroesGroup[rand].GetComponent<Character>().Circle.SetActive(true);
        yield return new WaitForSeconds(1);
        HeroesGroup[rand].GetComponent<Character>().GetDamage(List[сurrentCharacter].GetComponent<Character>().Damage, List[сurrentCharacter].GetComponent<Character>().Count);
        сurrentCharacter += 1;
        HeroesGroup[rand].GetComponent<Character>().Circle.SetActive(false);
        _stageStarted = false;
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
                    CanAttack = false;
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