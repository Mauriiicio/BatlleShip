using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public float tempoDoJogo;
    public Slider tempoSlider;
    public Slider sliderRespawn;
    public TextMeshProUGUI txt_tempoJogo;
    public TextMeshProUGUI txt_tempoRespawn;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if(instance == null)
            instance = this;
    }
    private void Update()
    {
        txt_tempoJogo.text = tempoSlider.value.ToString("F1");
        txt_tempoRespawn.text = sliderRespawn.value.ToString("F1");
    }
    public void NextScene()
    {
        SceneManager.LoadScene(1);
        tempoDoJogo = tempoSlider.value * 60;
    }
}
