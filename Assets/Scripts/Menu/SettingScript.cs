using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace My
{
    public class SettingScript : MonoBehaviour
    {

        public Text musicValue;
        public Slider musicSlider;

        void Awake()
        {
        }

        void Start()
        {
            // musicValue = GameObject.Find("MusicValueText").GetComponent<Text>();

            // get the saved value and set it to slider
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume") * 10f;

        }

        void Update()
        {
            musicValue.text = musicSlider.value.ToString("0");
        }


        public void OnValueChanged()
        {
            // slider change music volume
            Debug.LogFormat("{0} , {1} , MusicSlider Value: {2}", this.GetType(), this.name, (musicSlider.value / 10));
            AudioListener.volume = musicSlider.value / 10;
        }
  

        public void Save()
        {
            // save setting
            PlayerPrefs.SetFloat("MusicVolume", musicSlider.value / 10f); 
            SceneManager.LoadScene("MainMenu");
        }

        public void Cancel()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
