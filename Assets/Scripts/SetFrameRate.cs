using UnityEngine;

public class SetFrameRate : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 120;
    }
}