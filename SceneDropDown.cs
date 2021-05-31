using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneDropDown : MonoBehaviour
{
    private Dropdown _DropDown;
    
    private void Start()
    {
        _DropDown = GetComponent<Dropdown>();
        _DropDown.value = Int32.Parse(SceneManager.GetActiveScene().name.Replace("Scene","")) - 1;
        _DropDown.onValueChanged.AddListener(delegate 
                                               {
                                                   ChangeScene(_DropDown);
                                               }
                                             );
    }
    private void ChangeScene(Dropdown change)
    {
        SceneManager.LoadScene("Scene" + (change.value + 1));
    }
}
