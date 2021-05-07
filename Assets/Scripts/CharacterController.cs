using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    Animator animator;

    void Start()
    {
        animator= characterBody.GetComponent<Animator>();
    }

    void Update()
    {
        LookAround(); 
        Move();
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0; // moveInput = 0 이라면 이동이 없는 것, 0이 아니라면 이동이 있는것이다.

        // 걷는 애니메이션 재생


        if(isMove)
        {
            Vector3 lookForward  = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;

            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
            // lookForward * moveInput + LookRight * moveInput = 바라보고 있는 방향을 기준으로 이동 방향을 알 수 있다.


            characterBody.forward = lookForward; // 카메라 방향 = 캐릭터의 시선방향 
            // characterBody.forward = moveDir; // 캐릭터의 시선이 자유로움
            transform.position += moveDir * Time.deltaTime * 5f;
        }



        Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x , 0f, cameraArm.forward.z).normalized, Color.red);
    }
    private void LookAround() // 마우스의 움직임에 따라서 카메라 회전
    {
        Vector2 mouseDelta=  new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 cameraAngle  = cameraArm.rotation.eulerAngles;
        
        // 회전값 제한을 위한 작업
        float x =cameraAngle.x - mouseDelta.y;

        if(x < 170f)
        {
            x  = Mathf.Clamp(x, -1f,70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }



        cameraArm.rotation = Quaternion.Euler(x, cameraAngle.y +  mouseDelta.x, cameraAngle.z);
    }
}
