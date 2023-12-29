using GrandLine.Overlays;
using GrandLine.ResourceSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GrandLine.Farming
{
    public class FarmingManager : MonoBehaviour
    {
        public static FarmingManager Instance { get; private set; }

        public GameObject Farm;
        public GameObject Dirt;

        public Button PlaceDirtBtn;
        public Button ClearDirtBtn;

        private Mouse _mouse;
        private Camera _camera;

        private Vector3Int _startPosition;

        private void Awake()
        {
            Instance = this;

            _mouse = Mouse.current;
            _camera = Camera.main;

            PlaceDirtBtn.onClick.AddListener(OnPlaceDirt);
            ClearDirtBtn.onClick.AddListener(OnClearDirt);
        }

        private void Start()
        {
            // ResourceManager.Instance.AddResourceAmount("dirt", 20);
        }

        private bool _placeDirt = false;
        private bool _clearDirt = false;

        private void OnPlaceDirt()
        {
            _placeDirt = true;
            _clearDirt = false;
        }

        private void OnClearDirt()
        {
            _placeDirt = false;
            _clearDirt = true;
        }

        public bool PlayerCanMove()
        {
            return !_placeDirt && !_clearDirt && !EventSystem.current.IsPointerOverGameObject();
        }

        private Vector3Int _currentPosition;
        private bool _pressed = false;
        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (_mouse.leftButton.isPressed && (_placeDirt || _clearDirt))
            {
                _pressed = true;
                if (_mouse.leftButton.wasPressedThisFrame)
                {
                    _startPosition = Game.Instance.WorldMap.WorldToCell(_camera.ScreenToWorldPoint(_mouse.position.value));
                }

                _currentPosition = Game.Instance.WorldMap.WorldToCell(_camera.ScreenToWorldPoint(_mouse.position.value));
            }
            else
            {
                if (_mouse.leftButton.wasReleasedThisFrame)
                {
                    // AddDirtMounds();
                    _pressed = false;
                    OverlayManager.Instance.Clear();
                    if (_placeDirt)
                    {
                        AddDirtMounds();
                        _placeDirt = false;
                    }
                    if (_clearDirt)
                    {
                        AddDirtMounds(true);
                        _clearDirt = false;
                    }

                }
            }
        }

        private Dictionary<Vector3Int, GameObject> _dirtMounds = new Dictionary<Vector3Int, GameObject>();
        private void AddDirtMounds(bool clear = false)
        {
            //Determine directions on X and Y axis
            var xDir = _startPosition.x < _currentPosition.x ? 1 : -1;
            var yDir = _startPosition.y < _currentPosition.y ? 1 : -1;
            //How many tiles on each axis?
            int xCols = 1 + Mathf.Abs(_startPosition.x - _currentPosition.x);
            int yCols = 1 + Mathf.Abs(_startPosition.y - _currentPosition.y);

            var canPlaceDirt = xCols * yCols <= ResourceManager.Instance.GetResourceAmount("dirt");
            if (!canPlaceDirt && !clear) return;

            //Start painting
            for (var x = 0; x < xCols; x++)
            {
                for (var y = 0; y < yCols; y++)
                {
                    var tilePos = _startPosition + new Vector3Int(x * xDir, y * yDir, 0);
                    if (clear)
                    {
                        if (_dirtMounds.TryGetValue(tilePos, out var mound))
                        {
                            _dirtMounds.Remove(tilePos);
                            Destroy(mound);
                        }
                    }
                    else
                    {
                        if (_dirtMounds.ContainsKey(tilePos)) { continue; }

                        var dirtMound = Instantiate(Dirt, new Vector3(tilePos.x + 0.5f, tilePos.y + .5f), Quaternion.identity, Farm.transform);

                        _dirtMounds.Add(tilePos, dirtMound);
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (!_pressed) return;

            OverlayManager.Instance.Clear();

            var map = OverlayManager.Instance.OverlayMap;
            
            //Determine directions on X and Y axis
            var xDir = _startPosition.x < _currentPosition.x ? 1 : -1;
            var yDir = _startPosition.y < _currentPosition.y ? 1 : -1;
            //How many tiles on each axis?
            int xCols = 1 + Mathf.Abs(_startPosition.x - _currentPosition.x);
            int yCols = 1 + Mathf.Abs(_startPosition.y - _currentPosition.y);
            
            var canPlaceDirt = xCols * yCols <= ResourceManager.Instance.GetResourceAmount("dirt");

            var tile = canPlaceDirt ? OverlayManager.Instance.OverlayGreenTile : OverlayManager.Instance.OverlayRedTile;

            for (var x = 0; x < xCols; x++)
            {
                for (var y = 0; y < yCols; y++)
                {
                    var tilePos = _startPosition + new Vector3Int(x * xDir, y * yDir, 0);
                    map.SetTile(tilePos, tile);
                }
            }
        }
    }
}