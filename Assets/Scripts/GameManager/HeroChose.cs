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
    [SerializeField] private Material selectionMaterial;

    [SerializeField] private Material originalMaterial;
    private Material _originalMaterialEnemy;
    private Transform _highlight;
    private Transform _highlightEnemy;
    private Transform _selection;
    private Transform _enemySelection;
    private RaycastHit _raycastHit;
    private Knight _hero;
    public GameObject[] Enemies;
    public GameObject[] Heroes;
    public List<GameObject> EnemiesGroup;
    public List<GameObject> HeroesGroup;
    private List<GameObject> _queue;
    public List<GameObject> List;

    private bool _heroIsSelected = false;
    private float _coolDown = 1f;
    private float _timer;
    public int CurrentCharacter = 0;
    private int _count = 1;
    private bool _stageStarted = false;
    private GameObject _temp;
    public bool CanAttack { get; private set; }

    private void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("enemy");
        Heroes = GameObject.FindGameObjectsWithTag("hero");

        List.AddRange(Enemies);
        EnemiesGroup.AddRange(Enemies);
        List.AddRange(Heroes);
        HeroesGroup.AddRange(Heroes);

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

        foreach (var var in List)
        {
            Debug.Log(var.GetComponent<Knight>().Speed);
            Debug.Log(var);
        }
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
        
        if (_highlight != null)
        {
            _highlight.GetComponent<MeshRenderer>().material = originalMaterial;
            _highlight = null;
        }
        
        if (_highlightEnemy != null)
        {
            _highlightEnemy.GetComponent<MeshRenderer>().material = _originalMaterialEnemy;
            _highlightEnemy = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!_stageStarted)
        {
            _stageStarted = true;
            StartBattle();
        }

        if (CurrentCharacter < List.Count)
        {
            if (List[CurrentCharacter].CompareTag("hero"))
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
            CurrentCharacter = 0;
            StartBattle();
        }

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out _raycastHit) && _hero != null && List[CurrentCharacter].CompareTag("hero"))
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
        
        if (Input.GetKey(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject() && _hero != null && List[CurrentCharacter].CompareTag("hero"))
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
                    List[CurrentCharacter].GetComponent<MeshRenderer>().material = originalMaterial;
                    _stageStarted = false;
                    _heroIsSelected = false;
                    CurrentCharacter += 1;
                    if (CurrentCharacter >= List.Count)
                    {
                        CurrentCharacter = 0;
                    }
                }
                else
                {
                    _enemySelection = null;
                }
            }
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
        if (CurrentCharacter < List.Count)
        {
            if (List[CurrentCharacter].CompareTag("enemy"))
            {
                StartCoroutine(EnemyAttack());
            }
            else
            {
                List[CurrentCharacter].GetComponent<MeshRenderer>().material = highlightMaterial;
                _heroIsSelected = true;
                _hero = List[CurrentCharacter].GetComponent<Knight>();
            }
        }
        else
        {
            CurrentCharacter = 0;
            _stageStarted = false;
        }
    }

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(1);
        List[CurrentCharacter].GetComponent<MeshRenderer>().material = highlightMaterial;
        int rand = Random.Range(0, HeroesGroup.Count);
        HeroesGroup[rand].GetComponent<MeshRenderer>().material = highlightMaterial;
        yield return new WaitForSeconds(1);
        HeroesGroup[rand].GetComponent<Knight>().GetDamage(List[CurrentCharacter].GetComponent<Knight>().Damage);
        CurrentCharacter += 1;
        HeroesGroup[rand].GetComponent<MeshRenderer>().material = originalMaterial;
        _stageStarted = false;
    }
}
