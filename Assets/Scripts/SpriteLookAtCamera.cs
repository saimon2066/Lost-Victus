using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class SpriteLookAtCamera : MonoBehaviour
{
    private Sprite _sprite;
    
    private void Awake()
    {
        _sprite = GetComponent<Sprite>();
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
    }
}
