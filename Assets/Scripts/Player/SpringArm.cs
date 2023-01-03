using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    public LayerMask CrashMask;
    public LayerMask CrashMask2;
    public Transform myCam = null;
    public float rotSpeed = 5.0f;
    public float zoomSpeed = 5.0f;
    public Vector2 RotateRange = new Vector2(-70, 80); // Clamp
    // Vector3 curRot = Vector3.zero;
    float curRotX = 0.0f; // Version2
    float curRotY = 0.0f; // Version2
    Vector2 curRot = Vector2.zero;
    Vector2 desireRot = Vector2.zero;
    public float SmoothRotSpeed = 3.0f;
    public Vector2 ZoomRange = new Vector2(1.5f, 10.0f);
    public float SmoothDistSpeed = 3.0f;
    float curCamDist = 0.0f;
    float desirDist = 0.0f; // 희망 거리
    float OffsetDist = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        desireRot.x = curRot.x = transform.localRotation.eulerAngles.x;
        desirDist = curCamDist = -myCam.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(!UI.inventoryActivated)
        {
            desireRot.x += -Input.GetAxisRaw("Mouse Y") * rotSpeed;
            desireRot.x = Mathf.Clamp(desireRot.x, RotateRange.x, RotateRange.y);

            if (Input.GetKey(KeyCode.LeftAlt))
            {
                desireRot.y += Input.GetAxisRaw("Mouse X") * rotSpeed;
            }
            else
            {
                curRot.y = 0.0f;
                transform.parent.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * rotSpeed);
            }

            curRot = Vector2.Lerp(curRot, desireRot, Time.deltaTime * SmoothDistSpeed);

            Quaternion x = Quaternion.Euler(new Vector3(curRot.x, 0, 0));
            Quaternion y = Quaternion.Euler(new Vector3(0, curRot.y, 0));
            transform.localRotation = y * x; // 먼저 곱한 값의 이동을 한후 뒤에 곱한 값을 이동한다.(곱하는 순서가 중요함)
                                             // x를 먼저 곱하면 x축 이동 후 y축 이동을 하여 축이 틀어져 캐릭터의 전방이 아닌 다른곳을 볼수도 있다.
            if (Input.GetAxisRaw("Mouse ScrollWheel") > Mathf.Epsilon || Input.GetAxisRaw("Mouse ScrollWheel") < -Mathf.Epsilon)
            {
                desirDist -= Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed;
                desirDist = Mathf.Clamp(desirDist, ZoomRange.x, ZoomRange.y);
            }
            curCamDist = Mathf.Lerp(curCamDist, desirDist, Time.deltaTime * SmoothDistSpeed);

            Ray ray = new Ray();
            ray.origin = transform.position; // 월드 공간에서 ray의 현재 위치
            ray.direction = -transform.forward; // ray의 진행 방향
            float checkDist = Mathf.Min(curCamDist, desirDist);
            if (Physics.Raycast(ray, out RaycastHit hit, checkDist + OffsetDist + 0.01f, CrashMask)
                || Physics.Raycast(ray, out hit, checkDist + OffsetDist + 0.01f, CrashMask2))
            {
                curCamDist = Vector3.Distance(transform.position, hit.point + myCam.forward * OffsetDist);
                // 카메라 땅에 박힘 방지
            }
            myCam.transform.localPosition = new Vector3(0, 0, -curCamDist); // 원래 거리로 복구
        }
    }
}
