using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void EasyMode(){
        SceneManager.LoadSceneAsync("SampleScene"); 
    }

    public void MediumMode(){
        //add something
    }

    public void HardMode(){
        //add something
    }

    public void BackToMenu(){
        SceneManager.LoadSceneAsync("StartMenu");
    }
}
