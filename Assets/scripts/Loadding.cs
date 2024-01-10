using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Unity.VisualScripting;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

public class Loadding : MonoBehaviour
{
    [SerializeField] GameObject loaddingpanel;
    [SerializeField] Image loadding;
    [SerializeField] Slider SliderLoading;
    public static Loadding load;
    public float Time;
    bool playing;
    private void Start()
    {
        /*SceneManager.LoadScene(0, LoadSceneMode.Single);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);*/
        SliderLoading.value = 0;

        
        
        loadding.fillAmount = 0;
        StartCoroutine(DoLoading(6));

        //StartCoroutine(loadscene());
    }

    IEnumerator DoLoading(float time)
    {
        SliderLoading.value = 0f;
        float deltaTime = time / 200;
        do
        {

            SliderLoading.value += deltaTime;
            yield return new WaitForSeconds(deltaTime);
        }
        while (SliderLoading.value < 6);

        
        //yield return new WaitForSeconds(0.2f);
        SliderLoading.value = 6;
        SceneManager.LoadScene(1);
    }
}
