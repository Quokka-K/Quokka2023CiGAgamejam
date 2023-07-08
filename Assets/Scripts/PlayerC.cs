using UnityEngine;

public class PlayerC : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [Header("���λ�ò���")]
    Vector3 screenPosition;//���������������ת��Ϊ��Ļ����
    Vector3 mousePositionOnScreen;//��ȡ�������Ļ����Ļ����
    Vector3 mousePositionInWorld;//�������Ļ����Ļ����ת��Ϊ��������

    [Header("���������߲���")]
    private Vector3 StartPos;//��ʼλ�ã�Ҳ������ҵ�λ��
    private Vector3 ControlPoint;//���Ƶ㣬�������ߵı仯
    private Vector3 TargetPos;//Ŀ��λ��
    private float t = 0f;
    public float speed = 0.12f;//�������ƶ����ٶ�
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        StartPos = transform.position;
        ControlPoint = transform.position;
        TargetPos = transform.position;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))//������ס����ı�����
        {
            MouseFollow();
        }
        if (Input.GetMouseButtonUp(1))//�ɿ��Ҽ����ƶ���������¿�ʼλ��Ϊ���ڵ�transformλ��
        {
            StartPos = transform.position;
            t = 0f;
        }
        if (Input.GetMouseButton(1)&&!Input.GetMouseButton(0))//������ס�Ҽ������ƶ�
        {
            MoveOnTheLine();
        }
        if (Input.GetMouseButton(0)&& Input.GetMouseButton(1))//ͬʱ��ס���Ҽ���������Ŀ��λ��
        {
            TargetPos = MouseFollow();
        }
        UpdateLineRenderer();
    }

    /// <summary>
    /// ��ȡ���������ķ���
    /// </summary>
    public Vector3 MouseFollow()
    {
        //��ȡ��Ϸ���������������е�λ�ã���ת��Ϊ��Ļ���ꣻ
        screenPosition = Camera.main.WorldToScreenPoint(ControlPoint);

        //��ȡ����ڳ���������
        mousePositionOnScreen = Input.mousePosition;

        //����������Z������ ���� ��������Ϸ�����Z������
        mousePositionOnScreen.z = screenPosition.z;

        //��������Ļ����ת��Ϊ��������
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

        //����Ϸ����������Ϊ�����������꣬�����������ƶ�
        ControlPoint = mousePositionInWorld;

        //����������X���ƶ�
        return new Vector3(mousePositionInWorld.x, mousePositionInWorld.y, mousePositionInWorld.z);
    }
    /// <summary>
    /// ������������ƶ��ķ���
    /// </summary>
    private void MoveOnTheLine()
    {
        // ���㱴���������ϵĵ�
        Vector3 point = CalculateBezierPoint(t, StartPos,
            ControlPoint, TargetPos);
        // �ƶ����嵽�����������ϵĵ�
        transform.position = point;
        // �����ٶȵ�����ֵ�����ı仯����
        t += Time.deltaTime * speed;
    }
    /// <summary>
    /// ���㱴���������ϵĵ�
    /// </summary>
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 point = uuu * p0;
        point += 3f * uu * t * p1;
        point += 3f * u * tt * p2;
        point += ttt * p2;

        return point;
    }
    /// <summary>
    /// ���±��������ߵ���ʾ
    /// </summary>
    private void UpdateLineRenderer()
    {
        // ���ñ��������ߵĵ���
        int numPoints = 100;
        lineRenderer.positionCount = numPoints;

        // ���㲢���ñ����������ϵĵ�
        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float)(numPoints - 1);
            Vector3 point = CalculateBezierPoint(t, StartPos, ControlPoint, TargetPos);
            lineRenderer.SetPosition(i, point);
        }
    }
}
