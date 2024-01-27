using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField]private Transform VM_transform_point;
    [Header("Parameters")]
    [SerializeField]private float max_x_rotation;
    [SerializeField]private float max_y_rotation;
    [SerializeField]private float sensivity;


    private PlayerController player;
    private void Start()
    {
        player = GetComponent<PlayerController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void RotateCamera(Vector2 _input)
    {
        
        if(VM_transform_point == null)return;
        if((_input.x > 0 && VM_transform_point.forward.x >= max_x_rotation) || (_input.x < 0 && VM_transform_point.forward.x <= -max_x_rotation))return;
        if((_input.y > 0 && VM_transform_point.forward .y >= max_y_rotation) || (_input.y < 0 && VM_transform_point.forward.y <= -max_y_rotation))return;
        Vector3 payload = Vector3.zero;
        payload.x = _input.x;
        payload.y =  _input.y;
        
        VM_transform_point.forward = Vector3.Lerp(VM_transform_point.forward,payload,Time.deltaTime*sensivity);

    }

    public float GetCameraEdgeValue()
    {
        if(player.GetMouseAxisInput().x > 0)
        {
            return player.GetMouseAxisInput().x ;
        }
        else if(player.GetMouseAxisInput().x < 0)
        {
            return player.GetMouseAxisInput().x ;
        }
        else
        { 
            return 0f; 
        }
    }

    public void ResetCameraByFrame()
    {
        Vector3 frw_player = VM_transform_point.forward;
        frw_player.x = player.transform.forward.x;
        VM_transform_point.forward = Vector3.Lerp(VM_transform_point.forward,frw_player,Time.deltaTime*sensivity*10f);
    }

}
