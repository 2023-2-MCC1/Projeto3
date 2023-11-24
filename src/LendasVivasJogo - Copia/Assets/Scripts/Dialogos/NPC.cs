using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] DialogController dia;
    [TextArea(5, 8)]
    [SerializeField] string[] pages;
    void Start()
    {
     dia?.OpenDialog(pages);
    }

}