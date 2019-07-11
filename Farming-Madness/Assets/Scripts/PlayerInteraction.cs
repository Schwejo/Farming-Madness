using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Keys")]
    public KeyCode interactKey;
    public KeyCode keyLeft;
    public KeyCode keyRight;
    
    [Header("UI")]
    public IconBox iconBox;

    private GameObject target;
    private string targetTag;
    
    private Crop crop;
    private GameObject can;
    private bool hasCan = false;

    private Product product;

    private BasicMovement basicMovement;


    private void Start()
    {
        basicMovement = GetComponent<BasicMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {          
            if (target != null)
            {
                switch (targetTag)
                {
                    case "Seedbag":
                        Seedbag seedbag = target.GetComponent<Seedbag>();
                        if (seedbag != null)
                        {
                            seedbag.Interact(crop, hasCan, this);
                        }
                        break;
                    case "Field":
                        Field field = target.GetComponent<Field>();
                        if (field != null)
                        {
                            field.Interact(crop, hasCan, this);
                        }
                        break;
                    case "Can":
                        Can can = target.GetComponent<Can>();
                        if (can != null)
                        {
                            can.Interact(crop, hasCan, this);
                        }
                        break;
                    case "Table":
                        Table table = target.GetComponent<Table>();
                        if (table != null)
                        {
                            table.Interact(crop, hasCan, this);
                        }
                        break;
                    case "Trash":
                        Trash trash = target.GetComponent<Trash>();
                        if (trash != null)
                        {
                            trash.Interact(crop, hasCan, this);
                        }
                        break;
                    case "Tree":
                        Trees tree = target.GetComponent<Trees>();
                        if (tree != null)
                        {
                            tree.Interact(crop, hasCan, this);
                        }
                        break;
                    case "ProductionB":
                        ProductionBuilding building = target.GetComponent<ProductionBuilding>();
                        if (building != null)
                        {
                            if (crop != null)
                            {
                                product = crop.MakeProductFromCrop();
                                if (product != null)
                                {
                                    building.Interact(product, this);
                                }  
                            }
                            else if (crop == null)
                            {
                                building.Interact(null, this);
                            }
                        }
                        break;  
                }
            }
            else if (hasCan) //for placing the can
            {
                can.GetComponent<Can>().Interact(crop, hasCan, this);
            } 
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        target = col.gameObject;
        targetTag = target.tag;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (target == col.gameObject)
        {
            target = null;
            targetTag = "";
        }
    }

    private void ShowInventory(Sprite s)
    {
        basicMovement.SetIsHolding(true);
        iconBox.SetIcon(s);
    }

    private void CloseInventory()
    {
        basicMovement.SetIsHolding(false);
        iconBox.UnsetIcon();
    }

    public void SetCrop(Crop c)
    {
        crop = c;
        ShowInventory(crop.GetSprite());
    }

    public void UnsetCrop()
    {
        crop = null;
        CloseInventory();
    }

    public void TakeCan(GameObject c)
    {
        hasCan = true;
        can = c;
        ShowInventory(can.GetComponent<Can>().GetSprite());
    }

    public void PlaceCan()
    {
        hasCan = false;
        can = null;
        CloseInventory();
    }

    public void SetProduct(Product p) 
    {
        product = p;
        ShowInventory(product.productSprite);
    }

    public void UnsetProduct()
    {
        product = null;
        CloseInventory();
    }

    public void AllowMovement(bool isAllowed)
    {
        basicMovement.movementEnabled = isAllowed;
    }
}
