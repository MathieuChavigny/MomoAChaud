using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ChangerScene : MonoBehaviour
{


    void Start()
    {


        // Debug.Log(_currentSceneName);
    }
    void Update()
    {
        //fonction qui permet au joueur de quitter
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
#if (UNITY_EDITOR)
            if (EditorApplication.isPlaying)
            {
                EditorApplication.ExitPlaymode();
            }
#endif
        }

    }
    public string DonnerNomScene()
    {


        return SceneManager.GetActiveScene().name;
    }


    public void Instruction()
    {
        SceneManager.LoadScene("Instruction");

    }


    public void Intro()
    {
        SceneManager.LoadScene("Intro");
    }

    public void Mort()
    {
        SceneManager.LoadScene("Mort");
        PlayerPrefs.SetString("LastPlayedScene", "Mort");
    }
    public void Fin()
    {
        SceneManager.LoadScene("Fin");
    }
    public void PremierNiveau()
    {
        SceneManager.LoadScene("niveau1");
    }
    public void DeuxiemeNiveau()
    {
        SceneManager.LoadScene("niveau2");
    }
    public void TroisiemeNiveau()
    {
        SceneManager.LoadScene("niveau3");
    }
    public void QuatriemeNiveau()
    {
        SceneManager.LoadScene("niveau4");
    }
    public void CinquiemeNiveau()
    {
        SceneManager.LoadScene("niveau5");
    }

    public void ChargerScene()
    {
        if (PlayerPrefs.HasKey("LastPlayedScene"))
        {
            // Load the last played scene
            string lastPlayedScene = PlayerPrefs.GetString("LastPlayedScene");

            if (lastPlayedScene == "Fin" || lastPlayedScene == "Mort") lastPlayedScene = "niveau1";


            Debug.Log("test" + lastPlayedScene);

            SceneManager.LoadScene(lastPlayedScene);


        }
        else
        {
            Debug.Log("test2");
            PremierNiveau();
        }
    }

    void OnApplicationQuit()
    {

        // Save the current scene name to PlayerPrefs
        if (SceneManager.GetActiveScene().name == "Instruction" || SceneManager.GetActiveScene().name == "Intro")
        {

        }
        else
        {

            PlayerPrefs.SetString("LastPlayedScene", SceneManager.GetActiveScene().name);
        }

        PlayerPrefs.Save();
    }








}