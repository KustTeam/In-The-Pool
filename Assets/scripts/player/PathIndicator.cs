using UnityEngine;
using UnityEngine.AI;

public class PathIndicator : MonoBehaviour
{
    public GameObject player;
    public GameObject target;
    public GameObject roadSegmentPrefab;
    
    public int segmentCount = 10;
    private GameObject[] roadSegments;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("predmet");
        roadSegments = new GameObject[segmentCount];
        for (int i = 0; i < segmentCount; i++)
        {
            roadSegments[i] = Instantiate(roadSegmentPrefab);
        }
    }

    void Update()
    {
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
        UnityEngine.AI.NavMesh.CalculatePath(player.transform.position, target.transform.position, UnityEngine.AI.NavMesh.AllAreas, path);

        if (path.corners.Length < 2)
            return;

        // Динамически обновляем сегменты дороги по маршруту
        for (int i = 0; i < segmentCount; i++)
        {
            // Определяем прогресс по маршруту
            float t = (float)i / (segmentCount - 1);

            // Находим позицию вдоль всего пути
            Vector3 pointOnPath = GetPointAlongPath(path, t);
            roadSegments[i].transform.position = pointOnPath;
        }
    }

    // Получить позицию на пути по доле t
    Vector3 GetPointAlongPath(UnityEngine.AI.NavMeshPath path, float t)
    {
        float totalLength = 0f;
        float[] segmentLengths = new float[path.corners.Length - 1];

        // Расчёт длин сегментов
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            float segmentLength = Vector3.Distance(path.corners[i], path.corners[i + 1]);
            segmentLengths[i] = segmentLength;
            totalLength += segmentLength;
        }

        float distanceAlong = t * totalLength;
        float accumulated = 0f;

        for (int i = 0; i < segmentLengths.Length; i++)
        {
            if (accumulated + segmentLengths[i] >= distanceAlong)
            {
                float remaining = distanceAlong - accumulated;
                return Vector3.Lerp(path.corners[i], path.corners[i + 1], remaining / segmentLengths[i]);
            }
            accumulated += segmentLengths[i];
        }

        return path.corners[path.corners.Length - 1];
    }
}