using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private int slotIndex = 1;
    private PlayerPowers playerPowers = null;
    [SerializeField] private MapPositions mapPositions;

    private void Start()
    {
        PlayerSpawner.OnMatchStart += Initialize;
    }

    private void OnDestroy()
    {
        PlayerSpawner.OnMatchStart -= Initialize;
    }

    private void Initialize()
    {
        playerPowers = mapPositions.Player.GetComponent<PlayerPowers>();
    }

    public void ActivateSlot()
    {
        if (slotIndex == 1) playerPowers.UsePower1();
        if (slotIndex == 2) playerPowers.UsePower2();
        if (slotIndex == 3) playerPowers.UsePower3();
        if (slotIndex == 4) playerPowers.UsePower4();
    }

    private void OnValidate()
    {
        if (mapPositions == null) mapPositions = FindObjectOfType<MapPositions>();
    }
}
