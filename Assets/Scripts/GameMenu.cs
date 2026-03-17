using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private VideoPlayer _video;
    [SerializeField] private GameObject _videoImage;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private AudioClip _audios;
    [SerializeField] private AudioSource _source;
    [SerializeField] private bool _skipVideo;

    public static Action loose;
    public static Action win;

    private void Start()
    {
        if(_videoImage != null)
        {
            _videoImage.SetActive(false);
        }
        
        if (_winPanel != null && _losePanel != null)
        {
            _winPanel.SetActive(false);
            _losePanel.SetActive(false);

            win += ShowWin;
            loose += ShowLoose;
        }
    }

    private void ShowWin() 
    {
        if (_winPanel != null)
        {
            _winPanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void ShowLoose()
    {
        if (_losePanel != null)
        {
            _losePanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void StartGame()
    {
        if (!_skipVideo)
             StartCoroutine(PlayVideo());
        else
            SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator PlayVideo()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _videoImage.SetActive(true);
        _source.clip = _audios;
        _source.Play();
        _video.Play();
        yield return new WaitForSeconds((float)_video.clip.length);
        SceneManager.LoadScene(1);
    }
}
   
