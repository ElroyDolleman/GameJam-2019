using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slpashscreen : MonoBehaviour
{
    public Image ggj;
    public Image drol;
    public Image unity;


    // Start is called before the first frame update
    void Start()
    {
        ggj.canvasRenderer.SetAlpha(0);
        drol.canvasRenderer.SetAlpha(0);
        unity.canvasRenderer.SetAlpha(0);
        StartCoroutine(Splash());
    }
    
    IEnumerator Splash()
    {
        yield return new WaitForSeconds(1);
        //StartCoroutine(Fade(ggj));
        ggj.CrossFadeAlpha(1,0.5f,false);
        yield return new WaitForSeconds(2);
        ggj.CrossFadeAlpha(0, 0.5f, false);

        yield return new WaitForSeconds(1);

        drol.CrossFadeAlpha(1, 0.5f, false);
        yield return new WaitForSeconds(2);
        drol.CrossFadeAlpha(0, 0.5f, false);

        yield return new WaitForSeconds(1);

        unity.CrossFadeAlpha(1, 0.5f, false);
        yield return new WaitForSeconds(2);
        unity.CrossFadeAlpha(0, 0.5f, false);

        yield return new WaitForSeconds(1);

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    //IEnumerator Fade(Image im)
    //{
    //    while (im.alpha)
    //    {

    //    }
    //}
}
