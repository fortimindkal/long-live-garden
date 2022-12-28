using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject _creditsMenu;
    public GameObject _quitMenu;
    public GameObject _musicIcon;
    public GameObject _sfxIcon;

    public AudioMixer _musicMixer;
    public AudioMixer _sfxMixer;

    public Sprite _defaultMusicSprite;
    public Sprite _unactiveMusicSprite;
    public Sprite _defaultSfxSprite;
    public Sprite _unactiveSfxSprite;

    // Start is called before the first frame update
    void Start()
    {
        _creditsMenu.SetActive(false);
        _quitMenu.SetActive(false);
        _musicIcon.GetComponent<Image>().sprite = _defaultMusicSprite;
        _sfxIcon.GetComponent<Image>().sprite = _defaultSfxSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMenu()
    {
        SceneManager.LoadScene("Intro");
    }

    public void CreditsMenu()
    {
        FindObjectOfType<AudioManager>().Play("UI Pop Up");
        _creditsMenu.SetActive(true);
    }

    public void CloseCreditsMenu()
    {
        FindObjectOfType<AudioManager>().Play("UI Clicks");
        _creditsMenu.SetActive(false);
    }

    public void QuitMenu()
    {
        FindObjectOfType<AudioManager>().Play("UI Pop Up");
        _quitMenu.SetActive(true);
    }

    public void ConfirmQuitMenu()
    {
        FindObjectOfType<AudioManager>().Play("UI Clicks");
        Application.Quit();
    }

    public void CancelQuitMenu()
    {
        FindObjectOfType<AudioManager>().Play("UI Clicks");
        _quitMenu.SetActive(false);
    }

    public void MusicOnOff()
    {
        FindObjectOfType<AudioManager>().Play("UI Clicks");
        if (_musicIcon.GetComponent<Image>().sprite == _defaultMusicSprite)
        {
            _musicMixer.SetFloat("volume", -80f);
            _musicIcon.GetComponent<Image>().sprite = _unactiveMusicSprite;
        }
        else
        {
            _musicMixer.SetFloat("volume", 0);
            _musicIcon.GetComponent<Image>().sprite = _defaultMusicSprite;
        }
    }

    public void SfxOnOff()
    {
        FindObjectOfType<AudioManager>().Play("UI Clicks");
        if (_sfxIcon.GetComponent<Image>().sprite == _defaultSfxSprite)
        {
            _sfxMixer.SetFloat("volume", -80f);
            _sfxIcon.GetComponent<Image>().sprite = _unactiveSfxSprite;
        }
        else
        {
            _sfxMixer.SetFloat("volume", 0f);
            _sfxIcon.GetComponent<Image>().sprite = _defaultSfxSprite;
        }
    }
}
