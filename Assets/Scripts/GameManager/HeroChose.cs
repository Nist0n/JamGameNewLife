using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroChose : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material highlightMaterialForEnemy;
    [SerializeField] private Material selectionMaterial;

    private Material _originalMaterial;
    private Material _originalMaterialEnemy;
    private Transform _highlight;
    private Transform _highlightEnemy;
    private Transform _selection;
    private Transform _enemySelection;
    private RaycastHit _raycastHit;
    private Knight _hero;
    private GameObject[] _enemies;

    private bool _heroIsSelected = false;
    private float _coolDown = 1f;
    private float _timer;
    public bool CanAttack { get; private set; }

    private void Start()
    {
        _enemies = GameObject.FindGameObjectsWithTag("enemy");
    }

    private void Update()
    {
        CDAttack();
        
        if (_highlight != null)
        {
            _highlight.GetComponent<MeshRenderer>().material = _originalMaterial;
            _highlight = null;
        }
        
        if (_highlightEnemy != null)
        {
            _highlightEnemy.GetComponent<MeshRenderer>().material = _originalMaterialEnemy;
            _highlightEnemy = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out _raycastHit))
        {
            _highlight = _raycastHit.transform;
            if (_highlight.CompareTag("hero") && _highlight != _selection)
            {
                if (_highlight.GetComponent<MeshRenderer>().material != highlightMaterial)
                {
                    _originalMaterial = _highlight.GetComponent<MeshRenderer>().material;
                    _highlight.GetComponent<MeshRenderer>().material = highlightMaterial;
                }
            }
            else
            {
                _highlight = null;
            }
        }

        if (Input.GetKey(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (_selection != null)
            {
                _selection.GetComponent<MeshRenderer>().material = _originalMaterial;
                _selection = null;
                foreach (var enemy in _enemies)
                {
                    enemy.GetComponent<MeshRenderer>().material = _originalMaterial;
                }
            }

            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out _raycastHit))
            {
                _selection = _raycastHit.transform;
                if (_selection.CompareTag("hero"))
                {
                    _heroIsSelected = true;
                    _hero = _selection.GetComponent<Knight>();
                    _selection.GetComponent<MeshRenderer>().material = selectionMaterial;
                    foreach (var enemy in _enemies)
                    {
                        enemy.GetComponent<MeshRenderer>().material = highlightMaterial;
                    }
                }
                else
                {
                    _selection = null;
                    _heroIsSelected = false;
                }
            }

            if (_selection == null)
            {
                _heroIsSelected = false;
            }
        }
        
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out _raycastHit))
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
        
        if (Input.GetKey(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject() && _hero != null)
        {
            if (_enemySelection != null)
            {
                _enemySelection.GetComponent<MeshRenderer>().material = _originalMaterial;
                _enemySelection = null;
            }

            if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out _raycastHit))
            {
                _enemySelection = _raycastHit.transform;
                if (_enemySelection.CompareTag("enemy") && CanAttack == true)
                {
                    CanAttack = false;
                    _enemySelection.GetComponent<Knight>().GetDamage(_hero.Damage);
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
}
