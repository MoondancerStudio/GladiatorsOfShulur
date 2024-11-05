using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DragItem : MonoBehaviour
{
    public bool isDraggable;

    private Plane daggingPlane;
    private Vector3 offset;
    private Camera mainCamera;
    private Vector3 itemPos;
    private Transform parent;

    [SerializeField]
    private GameObject inventoryCells;

    [SerializeField]
    private GameObject equipCells;

    [SerializeField]
    private RawImage currentWeapon;

    private Transform playerCurrentWeapon;

    void Start()
    {
        isDraggable = false;
    }

    void Update()
    {
         mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        if (isDraggable)
        {
            daggingPlane = new Plane(mainCamera.transform.forward,
                                  transform.position);
            Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(camRay.origin, camRay.direction * 10, Color.green);

            float planeDistance;
            daggingPlane.Raycast(camRay, out planeDistance);
            offset = transform.position - camRay.GetPoint(planeDistance);
            itemPos = transform.position;
            parent = transform.parent;
        }
    }

    private void OnMouseUp()
    {
        if (isDraggable)
        {
            for (int i = 0; i < 12; i++)
            {
                Transform sr = inventoryCells.transform.GetChild(i);
                if (Vector2.Distance(transform.position, sr.position) < 0.7f)
                {
                    if (sr.childCount > 0 && parent != sr)
                    {
                        if (currentWeapon.texture == transform.GetComponent<SpriteRenderer>().sprite.texture)
                        {
                            currentWeapon.texture = sr.GetChild(0).GetComponent<SpriteRenderer>().sprite.texture;
                            GameObject.Find("player").transform.Find("weapon/current_weapon_eqiup").GetComponent<RawImage>().texture = sr.GetChild(0).GetComponent<SpriteRenderer>().sprite.texture;
                        }

                        transform.position = addNewPos(sr.position);
                        sr.GetChild(0).position = addNewPos(itemPos);

                        sr.GetChild(0).parent = parent;
                        transform.parent = sr;
                    }
                    else
                    {
                        transform.position = addNewPos(sr.position);
                        transform.parent = sr;

                        if (currentWeapon.texture == transform.GetComponent<SpriteRenderer>().sprite.texture &&
                            GameObject.Find("player").transform.Find("weapon/current_weapon_eqiup") != null)
                        {
                            if (currentWeapon.texture != null)
                                currentWeapon.texture = null;

                            Destroy(GameObject.Find("player").transform.Find("weapon/current_weapon_eqiup").gameObject);
                        }
                    }
                    break;
                }
            }

            for (int i = 0; i < 1; i++)
            {
                Transform sr = equipCells.transform.GetChild(i);
                if (Vector2.Distance(transform.position, sr.position) < 0.7f)
                {
                    if (sr.childCount > 0 && parent != sr)
                    {
                       
                        currentWeapon.texture = transform.GetComponent<SpriteRenderer>().sprite.texture;
                        GameObject.Find("player").transform.Find("weapon/current_weapon_eqiup").GetComponent<RawImage>().texture = transform.GetComponent<SpriteRenderer>().sprite.texture;
                     
                        transform.position = addNewPos(sr.position);

                        sr.GetChild(0).parent = parent;
                        transform.parent = sr;
                        parent.GetChild(0).position = addNewPos(itemPos);
                    }
                    else
                    {
                        transform.position = addNewPos(sr.position);
                        transform.parent = sr;

                        if (GameObject.Find("player").transform.Find("weapon/current_weapon_eqiup") == null)
                        {
                            currentWeapon.texture = transform.GetComponent<SpriteRenderer>().sprite.texture;
                            playerCurrentWeapon = Instantiate(currentWeapon.transform, new Vector3(GameObject.Find("player").transform.position.x, GameObject.Find("player").transform.position.y, -0.5f), Quaternion.identity);
                            playerCurrentWeapon.SetParent(GameObject.Find("player").transform.Find("weapon"));
                            playerCurrentWeapon.name = "current_weapon_eqiup";
                            playerCurrentWeapon.localScale = new Vector3(0.007f, 0.007f);
                        }
                    }
                    break;
                }
            }

            if (transform.parent == null)
            {
                transform.position = new Vector3(itemPos.x, itemPos.y, -2.0f);
                transform.parent = parent;
            }
        }
    }

    Vector3 addNewPos(Vector2 pos)
    {
        return new Vector3(pos.x, pos.y, -2.0f);    
    }

    void OnMouseDrag()
    {
        if (isDraggable)
        {
            transform.parent = null;
            Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            float planeDistance;
            daggingPlane.Raycast(camRay, out planeDistance);
            transform.position = camRay.GetPoint(planeDistance) + offset;
        }
    }
}
