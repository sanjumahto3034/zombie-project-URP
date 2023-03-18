using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadingScene : MonoBehaviour
{
    [SerializeField]
    private TMP_Text loadingText;
    [SerializeField]
    private float loadingDotDelayTime = 0.7f;

    [SerializeField]
    private bool isCustomDelayAdded = false;
    [SerializeField]
    private Slider _slider;
    private int counter = 1;

    private float destroyAfterDelayTime = 5f;

    private float counterToDestroy = 0f;
    private float timeRemainingToDestroy = 0f;

    void Awake()
    {
        StartCoroutine(Scheduler());
    }

    void Start()
    {

        if (isCustomDelayAdded){// Automatic destroy after 5 second
             StartCoroutine(DestroyAfterDelay());
             StartCoroutine(fillSliderContinue());
        } 
        counterToDestroy = destroyAfterDelayTime / 100f;
        timeRemainingToDestroy = counterToDestroy;
    }
    IEnumerator DestroyAfterDelay()
    {
        /*
        * @param int which show delay time
        */
        yield return new WaitForSeconds(destroyAfterDelayTime);
        setActive(false);
        
    }
    IEnumerator fillSliderContinue(){
        timeRemainingToDestroy+= counterToDestroy;
        _slider.value = timeRemainingToDestroy;
        yield return new WaitForSeconds(counterToDestroy);
        StartCoroutine(fillSliderContinue());

    }
    IEnumerator Scheduler()
    {
        /*
        * @param int which show delay time
        */
        yield return new WaitForSeconds(loadingDotDelayTime);
        updateLoadingText();

    }

    private void updateLoadingText()
    {
        switch (counter)
        {
            case 0:
                loadingText.text = "Loading";
                break;
            case 1:
                loadingText.text = "Loading .";
                break;

            case 2:
                loadingText.text = "Loading ..";
                break;

            case 3:
                loadingText.text = "Loading ...";
                break;

            default:

                break;
        }
        counter = counter == 3 ? 1 : counter + 1;
        StartCoroutine(Scheduler());
    }

    public void setSliderValues(float _value)
    {
        /*
        * @param float slider value
        * Fill the bar in loading screen
        */

        if (!isCustomDelayAdded)
        {
            _slider.value = _value;
            if(_value>=0.95f)setActive(false);
        }
    }
    public void setActive(bool _active){
        gameObject.SetActive(_active);
    }

    }
