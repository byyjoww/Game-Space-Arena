using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPositions : MonoBehaviour
{
    //Notice
    [SerializeField] private GameObject waveNoticePopup;
    [SerializeField] private GameObject getReadyPopup;

    public GameObject WaveNoticePopup => waveNoticePopup;
    public GameObject GetReadyPopup => getReadyPopup;

    [SerializeField] private Transform waveNoticePopupParent;
    public Transform WaveNoticePopupParent => waveNoticePopupParent;

    [SerializeField] private Transform playerParent;
    public Transform PlayerParent => playerParent;

    //Transforms
    [SerializeField] private Transform maxValues; //y = yMax | x = xMax
    [SerializeField] private Transform minValues; //y = yMin | x = xMin
    [SerializeField] private Transform player;
    public Transform Player => player;
    public void SetPlayer(Transform t) { player = t; }

    public float X_max => maxValues.position.x;
    public float X_min => minValues.position.x;
    public float Y_max => maxValues.position.y;
    public float Y_min => minValues.position.y;
}
