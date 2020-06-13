using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerSpawnHandler : MonoBehaviour
{
    [SerializeField] private GameObject pfPowerIcon;
    [SerializeField] private MapPositions mapPositions;

    private void Start()
    {
        InvokeRepeating("EventStart", firstEventTime, recurringEventTime);
        StateMachine.OnGameEnd += GameOver;
    }

    private void OnDestroy()
    {
        StateMachine.OnGameEnd -= GameOver;
    }

    private void OnValidate()
    {
        if (mapPositions == null) mapPositions = GetComponent<MapPositions>();
    }

    //Spawn Regulators
    [SerializeField] private float firstEventTime;
    [SerializeField] private float recurringEventTime;

    //Coords
    [SerializeField] private float coordPadding = 0.3f;

    public void GameOver()
    {
        CancelInvoke();
    }

    private void EventStart()
    {
        var powerIcon = Instantiate(pfPowerIcon, GetPosition(), Quaternion.identity);
        ScriptablePower power = RandomPower();
        //Debug.LogError("Power: " + power);
        powerIcon.GetComponent<PowerObject>().SetPower(power);
        powerIcon.GetComponent<SpriteRenderer>().sprite = power.itemSprite;
    }

    private ScriptablePower RandomPower()
    {
        List<ScriptablePower> profilePowerList = new List<ScriptablePower>();
        ScriptableDatabase powerDatabase = Database.i.Databases[1];

        foreach (ScriptableElement power in powerDatabase.databaseElements)
        {
            if (Database.i.profile.CheckForItem(power.itemName) == true)
            {
                profilePowerList.Add(power as ScriptablePower);
                //Debug.LogError("Added " + power + " to list");
            }
        }

        int r = Random.Range(0, profilePowerList.Count);

        try
        {
            //Debug.LogError(profilePowerList[r].itemName);
            return profilePowerList[r];
        }
        catch
        {
            Debug.LogError("Out of range exception. Range: " + r);
            return profilePowerList[0];            
        }
        
    }

    private Vector2 GetPosition()
    {
        float x = Random.Range(mapPositions.X_min + coordPadding, mapPositions.X_max - coordPadding);
        float y = Random.Range(mapPositions.Y_min + coordPadding, mapPositions.Y_max - coordPadding);
        return new Vector2(x, y);
    }
}
