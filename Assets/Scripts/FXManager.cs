using UnityEngine;
using System.Collections;

namespace My
{
    public class FXManager : MonoBehaviour
    {
        public AudioClip gameOverSound;
        public AudioClip gameOverVoice;
        public AudioClip bulletSound;
        public AudioClip jumpSound;
        public AudioClip explodeSound;

        private AudioSource musicSource;

        private AudioSource effectSource;


        void Start()
        {
            effectSource = GetComponent<AudioSource>();
            musicSource = GameObject.Find("StageMusic").GetComponent<AudioSource>();
        }

        public void PlayBulletSound()
        {
            effectSource.PlayOneShot(bulletSound, PlayerPrefs.GetFloat("MusicVolume") + 2f);


        }

        public void PlayExplodeSound()
        {
            effectSource.PlayOneShot(explodeSound, PlayerPrefs.GetFloat("MusicVolume") + 10f);


        }

        public void PlayJumpSound()
        {
            effectSource.PlayOneShot(jumpSound, PlayerPrefs.GetFloat("MusicVolume")+2f);


        }


        public void PlayGameOverSound(){
            musicSource.Stop();
            effectSource.PlayOneShot(gameOverSound, PlayerPrefs.GetFloat("MusicVolume") + 2f);
            effectSource.PlayOneShot(gameOverVoice, PlayerPrefs.GetFloat("MusicVolume") + 5f);

        }

        //static AudioClip GetSoundEffect() {


        //}

    }
}