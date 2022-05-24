using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentManager : MonoBehaviour
{
    private GameObject[] papers;

    public void EnablePaper(GameObject Document)
    {
        foreach (GameObject paper in papers)
        {
            if (paper.name == Document.name)
            {
                paper.SetActive(true);
            }
        }
    }

    public void DisablePaper()
    {
        foreach (GameObject paper in papers)
        {
            if (paper != null)
            {
                paper.SetActive(false);
            }
        }
    }
}
