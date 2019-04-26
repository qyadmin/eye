using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Occ : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        float[] distances = new float[32];

        distances[8] = 10.0f;

        Camera.main.layerCullDistances = distances;

    }


}
