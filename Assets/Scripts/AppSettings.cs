using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AppSettings : MonoBehaviour
{
    public GameObject _Canvas;
    public GameObject rawImgGO;
    private RectTransform img;
    private RawImage raw;

    void Awake()
    {
        img = rawImgGO.GetComponent<RectTransform>();
        raw = rawImgGO.GetComponent<RawImage>();

        Screen.SetResolution((int)img.rect.width - 245, (int)img.rect.height, false);

        Application.targetFrameRate = 60;
        Application.runInBackground = true;
        Application.backgroundLoadingPriority = ThreadPriority.Low;

        raw.enabled = true;

        /*
        if (GameObject.FindGameObjectWithTag("Manager"))
        {
            this.gameObject.transform.parent = GameObject.FindGameObjectWithTag("Manager").transform;
        }
        */
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            raw.enabled = true;
            StartCoroutine(LoadScene());
        }
        
    }

    IEnumerator LoadScene()
    {
       
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(_Canvas);
        

        yield return new WaitForSeconds(1.5f);

        AsyncOperation operation = SceneManager.LoadSceneAsync("MeatAndPotatoes");

        while (!operation.isDone)
        {
            yield return operation.isDone;
        }
        raw.enabled = false;

        yield return new WaitForSeconds(0.5f);

        raw.enabled = true;
      //  Debug.Log("Get into the Meat and Potatoes");
    }

    private void OnApplicationQuit()
    {
        Destroy(GameObject.Find("Video Player"));
        Destroy(GameObject.Find("Canvas"));
        if (GameObject.Find("_Managers"))
        {
            Destroy(GameObject.Find("_Managers"));
        }
        
    }
}
