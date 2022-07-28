using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenButtons : MonoBehaviour
{
    [SerializeField] AudioClip mouseDown;
    [SerializeField] AudioClip mouseEnter;

    private void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(mouseDown, Camera.main.transform.position, 1f);
    }

    private void OnMouseEnter()
    {
        AudioSource.PlayClipAtPoint(mouseEnter, Camera.main.transform.position, 1f);
    }
}
