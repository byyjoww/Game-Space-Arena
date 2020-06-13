using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private MapPositions mapPositions;
    [SerializeField] private GameObject pfShip;
    [SerializeField] private Transform tShip;
    [SerializeField] private GameObject selectionUI;
    [SerializeField] private List<ScriptableShip> spaceshipList = new List<ScriptableShip>();
    [SerializeField] private GameObject selectedSpaceship;

    public static event Action OnMatchStart;

    private void Awake()
    {
        GetSpaceships();
        PopulateSpelectionUI();
        StopTime();
    }

    private void GetSpaceships()
    {
        Profile profile = Database.i.profile;
        List<ScriptableDatabase> databaselist = Database.i.databases;
        ScriptableDatabase correctDatabase = databaselist.First(x => x.sectionType == Shop.ShopSections.Ship);

        foreach (var spaceship in correctDatabase.databaseElements)
        {
            if (profile.CheckForItem(spaceship.itemName) == true)
            {
                spaceshipList.Add(spaceship as ScriptableShip);
            }            
        }
    }

    private void PopulateSpelectionUI()
    {
        foreach (var spaceship in spaceshipList)
        {
            GameObject g = Instantiate(pfShip, tShip);
            SetupShipItem setup = g.GetComponent<SetupShipItem>();
            setup.Setup(spaceship);
            setup.ButtonComponent.onClick.AddListener(delegate { SelectSpaceship(spaceship); });
        }
    }

    private void SelectSpaceship(ScriptableShip spaceship)
    {
        selectedSpaceship = spaceship.itemPrefab;
        ConfirmSelection();
    }

    public void DEBUGSelectSpaceship(ScriptableShip spaceship)
    {
        selectedSpaceship = spaceship.itemPrefab;
        ConfirmSelection();
    }

    private void ConfirmSelection()
    {
        Transform player = Instantiate(selectedSpaceship, mapPositions.PlayerParent).transform;
        mapPositions.SetPlayer(player);
        selectionUI.SetActive(false);
        OnMatchStart?.Invoke();
        StartTime();
    }

    private bool isPaused = false;

    private void StopTime()
    {
        if (isPaused == false)
        {
            Time.timeScale = 0f;
            isPaused = true;
        }
        else Debug.Log("Time is already stopped!");
    }

    private void StartTime()
    {
        if (isPaused == true)
        {
            Time.timeScale = 1f;
            isPaused = false;
        }
        else Debug.Log("Time is already ticking!");
    }

    private void OnValidate()
    {
        if (mapPositions == null) mapPositions = GetComponent<MapPositions>();
    }
}