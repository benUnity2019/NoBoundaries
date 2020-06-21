using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float size = 0.01f;

    public float Size { get { return size; } }

    public Vector3 GetNearestPoint(Vector3 position)
    {

        position -= transform.position;
        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3((float)xCount * size, (float)yCount * size, (float)zCount * size);
        result += transform.position;
        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for(float x = 0; x < 8; x += size)
        {
            for(float y =0; y <8; y += size)
            {
                var point = GetNearestPoint(new Vector3(x, y, 0f));
                Gizmos.DrawSphere(point, 0.01f);
            }

        }
        
    }
}
