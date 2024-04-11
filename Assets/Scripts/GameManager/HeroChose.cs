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
    private Knight _hero;
    private List<GameObject> _queue;
    private GameObject _temp;

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
    public bool CanAttack { get; private set; }
    public int сurrentCharacter = 0;

    private void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("enemy");
        Heroes = GameObject.FindGameObjectsWithTag("hero");

        List.AddRange(Enemies);
        EnemiesGroup.AddRange(Enemies);
        List.AddRange(Heroes);
        HeroesGroup.AddRange(Heroes);

        SortMassive();
    }

    private void Update()
    {
        CDAttack();

        if (HeroesGroup.Count == 0)
        {
            SceneManager.LoadScene(1);
        }

        if (EnemiesGroup.Count == 0)
        {
            SceneManager.LoadScene(1);
        }

        if (_highlightEnemy != null)
        {
            _highlightEnemy.GetComponent<MeshRenderer>().material = _originalMaterialEnemy;
            _highlightEnemy = null;
        }

        if (!_stageStarted)
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
                List[сurrentCharacter].GetComponent<MeshRenderer>().material = highlightMaterial;
                _heroIsSelected = true;
                _hero = List[сurrentCharacter].GetComponent<Knight>();
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
        List[сurrentCharacter].GetComponent<MeshRenderer>().material = highlightMaterial;
        int rand = Random.Range(0, HeroesGroup.Count);
        HeroesGroup[rand].GetComponent<MeshRenderer>().material = highlightMaterial;
        yield return new WaitForSeconds(1);
        HeroesGroup[rand].GetComponent<Knight>().GetDamage(List[сurrentCharacter].GetComponent<Knight>().Damage);
        сurrentCharacter += 1;
        HeroesGroup[rand].GetComponent<MeshRenderer>().material = originalMaterial;
        _stageStarted = false;
    }

    private void SortMassive()
    {
        while (_count != 0)
        {
            _count = 0;
            for (int i = 0; i < List.Count - 1; i++)
            {
                if (List[i].GetComponent<Knight>().Speed == List[i + 1].GetComponent<Knight>().Speed)
                {
                    if (List[i].CompareTag("enemy") && List[i + 1].CompareTag("hero"))
                    {
                        _temp = List[i];
                        List[i] = List[i + 1];
                        List[i + 1] = _temp;
                        _count = 1;
                    }
                }

                if (List[i].GetComponent<Knight>().Speed < List[i + 1].GetComponent<Knight>().Speed)
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
                    enemy.GetComponent<MeshRenderer>().material = highlightMaterial;
                }
            }
            else
            {
                foreach (var enemy in EnemiesGroup)
                {
                    enemy.GetComponent<MeshRenderer>().material = originalMaterial;
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
                if (_highlightEnemy.GetComponent<MeshRenderer>().material != highlightMaterialForEnemy)
                {
                    _originalMaterialEnemy = _highlightEnemy.GetComponent<MeshRenderer>().material;
                    _highlightEnemy.GetComponent<MeshRenderer>().material = highlightMaterialForEnemy;
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
                _enemySelection.GetComponent<MeshRenderer>().material = originalMaterial;
                _enemySelection = null;
            }

            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out _raycastHit))
            {
                _enemySelection = _raycastHit.transform;
                if (_enemySelection.CompareTag("enemy") && CanAttack == true)
                {
                    CanAttack = false;
                    _enemySelection.GetComponent<Knight>().GetDamage(_hero.Damage);
                    List[сurrentCharacter].GetComponent<MeshRenderer>().material = originalMaterial;
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