using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VolSoundTest : MonoBehaviour, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData = null)
    {
        //Test Sound Volume
        AudioManager.manager.Play("FireSound");
    }
}
