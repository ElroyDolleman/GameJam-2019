using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadSceneAdditive(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Additive);
    }

    public void LoadSceneAdditive(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }

    public void LoadMultiScene(params int[] indexes)
    {
        for(int i = 0; i < indexes.Length; i++)
        {
            if (i == 0)
            {
                SceneManager.LoadScene(indexes[i]);
            } else
            {
                SceneManager.LoadScene(indexes[i], LoadSceneMode.Additive);
            }
        }
    }

    public void LoadMultiScene(params string[] names)
    {
        for (int i = 0; i < names.Length; i++)
        {
            if (i == 0)
            {
                SceneManager.LoadScene(names[i]);
            }
            else
            {
                SceneManager.LoadScene(names[i], LoadSceneMode.Additive);
            }
        }
    }
}
