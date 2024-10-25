using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int sceneBuildIndex;
    private void OnTriggerEnter(Collider other) {
        if( other.gameObject.tag == "Player"){
            Debug.Log("Collidiu");
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);

        }
    }
}
