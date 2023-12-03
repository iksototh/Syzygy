using Cinemachine;
using GrandLine.Core;
using GrandLine.Overlays;
using GrandLine.World;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace GrandLine.Assets.Scripts.Hideout
{
    public class HideoutManager : MonoBehaviour
    {
        public HideoutManager Instance { get; private set; }

        public GameData SceneData;
        public Grid WorldGrid;
        public GameObject Player;

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
            Game.SceneData = SceneData;
            Game.WorldMap = new WorldMap(WorldGrid);

            var _player = Instantiate(Player, new Vector3(-0.5f, 0.5f), Quaternion.identity);
            var playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
            var cinematicScript = playerCamera.GetComponent<CinemachineVirtualCamera>();
            cinematicScript.Follow = _player.transform;
            cinematicScript.LookAt = _player.transform;

            _mouse = Mouse.current;
            _camera = Camera.main;

            PlaceDirtBtn.onClick.AddListener(OnPlaceDirt);
            ClearDirtBtn.onClick.AddListener(OnClearDirt);
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

        private Vector3Int _currentPosition;
        private bool _pressed = false;
        private void Update()
        {
            if(_mouse.leftButton.isPressed)
            {
                _pressed = true;
                if (_mouse.leftButton.wasPressedThisFrame)
                {
                    _startPosition = Game.WorldMap.WorldToCell(_camera.ScreenToWorldPoint(_mouse.position.value));
                }
                
                _currentPosition = Game.WorldMap.WorldToCell(_camera.ScreenToWorldPoint(_mouse.position.value));
            } else
            {
                if (_mouse.leftButton.wasReleasedThisFrame)
                {
                    AddDirtMounds();
                    _pressed = false;
                    OverlayManager.Instance.Clear();
                    //if(_placeDirt)
                    //{
                    //    AddDirtMounds();
                    //    _placeDirt = false;
                    //}
                    //if(_clearDirt)
                    //{
                    //    AddDirtMounds(true);
                    //    _clearDirt = false;
                    //}

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
            //Start painting
            for (var x = 0; x < xCols; x++)
            {
                for (var y = 0; y < yCols; y++)
                {
                    var tilePos = _startPosition + new Vector3Int(x * xDir, y * yDir, 0);
                    if(clear)
                    {
                        if(_dirtMounds.TryGetValue(tilePos, out var mound))
                        {
                            _dirtMounds.Remove(tilePos);
                            Destroy(mound);
                        }   
                    } else
                    {
                        var dirtMound = Instantiate(Dirt, tilePos, Quaternion.identity, Farm.transform);
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
            var tile = OverlayManager.Instance.OverlayGreenTile;

            //Determine directions on X and Y axis
            var xDir = _startPosition.x < _currentPosition.x ? 1 : -1;
            var yDir = _startPosition.y < _currentPosition.y ? 1 : -1;
            //How many tiles on each axis?
            int xCols = 1 + Mathf.Abs(_startPosition.x - _currentPosition.x);
            int yCols = 1 + Mathf.Abs(_startPosition.y - _currentPosition.y);
            //Start painting
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