using UnityEngine;

public class PlayerC : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [Header("鼠标位置参数")]
    Vector3 screenPosition;//将物体从世界坐标转换为屏幕坐标
    Vector3 mousePositionOnScreen;//获取到点击屏幕的屏幕坐标
    Vector3 mousePositionInWorld;//将点击屏幕的屏幕坐标转换为世界坐标

    [Header("贝塞尔曲线参数")]
    private Vector3 StartPos;//起始位置，也就是玩家的位置
    private Vector3 ControlPoint;//控制点，控制曲线的变化
    private Vector3 TargetPos;//目标位置
    private float t = 0f;
    public float speed = 0.12f;//曲线上移动的速度
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        StartPos = transform.position;
        ControlPoint = transform.position;
        TargetPos = transform.position;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))//单独按住左键改变曲线
        {
            MouseFollow();
        }
        if (Input.GetMouseButtonUp(1))//松开右键不移动后立马更新开始位置为现在的transform位置
        {
            StartPos = transform.position;
            t = 0f;
        }
        if (Input.GetMouseButton(1)&&!Input.GetMouseButton(0))//单独按住右键可以移动
        {
            MoveOnTheLine();
        }
        if (Input.GetMouseButton(0)&& Input.GetMouseButton(1))//同时按住左右键可以设置目标位置
        {
            TargetPos = MouseFollow();
        }
        UpdateLineRenderer();
    }

    /// <summary>
    /// 获取鼠标点击坐标的方法
    /// </summary>
    public Vector3 MouseFollow()
    {
        //获取游戏对象在世界坐标中的位置，并转换为屏幕坐标；
        screenPosition = Camera.main.WorldToScreenPoint(ControlPoint);

        //获取鼠标在场景中坐标
        mousePositionOnScreen = Input.mousePosition;

        //让鼠标坐标的Z轴坐标 等于 场景中游戏对象的Z轴坐标
        mousePositionOnScreen.z = screenPosition.z;

        //将鼠标的屏幕坐标转化为世界坐标
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

        //将游戏对象的坐标改为鼠标的世界坐标，物体跟随鼠标移动
        ControlPoint = mousePositionInWorld;

        //物体跟随鼠标X轴移动
        return new Vector3(mousePositionInWorld.x, mousePositionInWorld.y, mousePositionInWorld.z);
    }
    /// <summary>
    /// 让玩家在线上移动的方法
    /// </summary>
    private void MoveOnTheLine()
    {
        // 计算贝塞尔曲线上的点
        Vector3 point = CalculateBezierPoint(t, StartPos,
            ControlPoint, TargetPos);
        // 移动物体到贝塞尔曲线上的点
        transform.position = point;
        // 根据速度调整插值参数的变化速率
        t += Time.deltaTime * speed;
    }
    /// <summary>
    /// 计算贝塞尔曲线上的点
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
    /// 更新贝塞尔曲线的显示
    /// </summary>
    private void UpdateLineRenderer()
    {
        // 设置贝塞尔曲线的点数
        int numPoints = 100;
        lineRenderer.positionCount = numPoints;

        // 计算并设置贝塞尔曲线上的点
        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float)(numPoints - 1);
            Vector3 point = CalculateBezierPoint(t, StartPos, ControlPoint, TargetPos);
            lineRenderer.SetPosition(i, point);
        }
    }
}
