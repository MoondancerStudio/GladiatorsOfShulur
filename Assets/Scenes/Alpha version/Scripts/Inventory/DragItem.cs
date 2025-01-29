using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class DragItem : MonoBehaviour
{
    public bool isDraggable;

    private Plane daggingPlane;
    private Vector3 itemPos;
    private Vector3 offset; 

    private Camera mainCamera;
    private Transform parent;

    [SerializeField]
    private GameObject inventoryCells;

    [SerializeField]
    private GameObject equipCells;

    [SerializeField]
    private RawImage currentWeapon;

    private Transform playerCurrentWeapon;

    private GameObject _itemOptionMenu;

    private GameObject _player;

    void Start()
    {
        isDraggable = false;
        mainCamera = Camera.main;
        _player = GameObject.Find("player");
        _itemOptionMenu = GameObject.Find("Canvas/inventory_system/item_options");
    }

    void Update()
    {
        if (isDraggable && Input.GetMouseButton(1)) 
        {
            _itemOptionMenu.SetActive(true);
            _itemOptionMenu.transform.position = new Vector3(transform.position.x, transform.position.y - 2.0f, transform.position.z);
        }
    }

    public void onItemDrop()
    {
        transform.SetParent(null);
        transform.localScale = new Vector3(0.5f,0.5f,1);
        transform.position = addNewPos(_player.transform.position);
        transform.Translate(new Vector3(1.0f, 0, 0));
        //Debug.Log(transform.position);
        isDraggable = false;
        _itemOptionMenu.SetActive(false);
    }

    public void onItemEqiup()
    {
        transform.position = addNewPos(equipCells.transform.GetChild(1).transform.position);
        transform.SetParent(equipCells.transform.GetChild(1));
        _itemOptionMenu.SetActive(false);

    }

    void OnMouseDown()
    {
        if (isDraggable)
        {
            daggingPlane = new Plane(mainCamera.transform.forward,
                                  transform.position);
            Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
           // Debug.DrawRay(camRay.origin, camRay.direction * 10, Color.green);

            float planeDistance;
            daggingPlane.Raycast(camRay, out planeDistance);
            offset = transform.position - camRay.GetPoint(planeDistance);
            itemPos = transform.position;
            parent = transform.parent;
        }
    }

    void OnMouseDrag()
    {
        if (isDraggable)
        {
            transform.SetParent(null);
            Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            float planeDistance;
            daggingPlane.Raycast(camRay, out planeDistance);
            transform.position = camRay.GetPoint(planeDistance) + offset;
        }
    }

    private void OnMouseUp()
    {
        if (isDraggable)
        {
            for (int i = 0; i < 12; i++)
            {
                Transform sr = inventoryCells.transform.GetChild(i);
       
                if (Vector2.Distance(transform.position, sr.position) < 65f)
                {
                    if (sr.childCount > 0 && parent != sr)
                    {
                        if (currentWeapon.texture == transform.GetComponent<SpriteRenderer>().sprite.texture)
                        {
                            currentWeapon.texture = sr.GetChild(0).GetComponent<SpriteRenderer>().sprite.texture;
                            _player.transform.Find("weapon/current_weapon_eqiup").GetComponent<RawImage>().texture = sr.GetChild(0).GetComponent<SpriteRenderer>().sprite.texture;
                        }

                        transform.position = addNewPos(sr.position);
                        sr.GetChild(0).position = addNewPos(itemPos);

                        sr.GetChild(0).SetParent(parent);
                        transform.SetParent(sr);
                    }
                    else
                    {
                        transform.position = addNewPos(sr.position);
                        transform.SetParent(sr);

                        if (currentWeapon.texture == transform.GetComponent<SpriteRenderer>().sprite.texture &&
                            _player.transform.Find("weapon/current_weapon_eqiup") != null)
                        {
                            if (currentWeapon.texture != null)
                                currentWeapon.texture = null;

                            Destroy(_player.transform.Find("weapon/current_weapon_eqiup").gameObject);
                        }
                    }
                    break;
                }
            }

            for (int i = 1; i < 2; i++)
            {
                Transform sr = equipCells.transform.GetChild(i);

                if (Vector2.Distance(transform.position, sr.position) < 70f)
                {
                    if (sr.childCount > 0 && parent != sr)
                    {
                       
                        currentWeapon.texture = transform.GetComponent<SpriteRenderer>().sprite.texture;
                        _player.transform.Find("weapon/current_weapon_eqiup").GetComponent<RawImage>().texture = transform.GetComponent<SpriteRenderer>().sprite.texture;
                     
                        transform.position = addNewPos(sr.position);

                        sr.GetChild(0).SetParent(parent);
                        transform.SetParent(sr);
                        parent.GetChild(0).position = addNewPos(itemPos);
                    }
                    else
                    {
                        transform.position = addNewPos(sr.position);
                        transform.SetParent(sr);
                        
                        if (_player.transform.Find("weapon/current_weapon_eqiup") == null)
                        {
                            currentWeapon.texture = transform.GetComponent<SpriteRenderer>().sprite.texture;
                            playerCurrentWeapon = Instantiate(currentWeapon.transform, new Vector3(_player.transform.position.x, _player.transform.position.y, -0.5f), Quaternion.identity);
                            playerCurrentWeapon.SetParent(_player.transform.Find("weapon"));
                            playerCurrentWeapon.name = "current_weapon_eqiup";
                            playerCurrentWeapon.localScale = new Vector3(0.007f, 0.007f);
                         
                            _player.GetComponent<Unit>().stat.weaponData = GetComponent<InstanceItemContainer>().item.itemType;
                        }
                    }
                    break;
                }
            }

            if (transform.parent == null)
            {
                transform.position = new Vector3(itemPos.x, itemPos.y, 0);
             //   Debug.Log(transform.position);
                transform.SetParent(parent);
            }
        }
    }

    Vector3 addNewPos(Vector2 pos)
    {
        return new Vector3(pos.x, pos.y, 0);    
    }
}
