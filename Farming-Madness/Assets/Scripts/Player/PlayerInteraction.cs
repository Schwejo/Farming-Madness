using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public string player;
    
    [Header("Keys")]
    public KeyCode keyInteract;
    public KeyCode keyUp;
    public KeyCode keyDown;
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
        GetKeys();
        basicMovement.SetupKeys(keyUp, keyDown, keyLeft, keyRight);
    }

    private void Update()
    {
        if (Input.GetKeyUp(keyInteract))
        {          
            if (target != null)
            {
                switch (targetTag)
                {
                    case "Seedbag":
                        Seedbag seedbag = target.GetComponent<Seedbag>();
                        if (seedbag != null && product == null)
                        {
                            seedbag.Interact(crop, hasCan, this);
                        }
                        break;
                    case "Field":
                        Field field = target.GetComponent<Field>();
                        if (field != null && product == null)
                        {
                            field.Interact(crop, hasCan, this);
                        }
                        break;
                    case "Can":
                        Can can = target.GetComponent<Can>();
                        if (can != null && product == null)
                        {
                            can.Interact(crop, hasCan, this);
                        }
                        break;
                    case "Table":
                        Table table = target.GetComponent<Table>();
                        if (table != null)
                        {
                            table.Interact(crop, product, hasCan, this);
                        }
                        break;
                    case "Trash":
                        Trash trash = target.GetComponent<Trash>();
                        if (trash != null)
                        {
                            trash.Interact(crop, product, hasCan, this);
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
                            else if (product != null)
                            {
                                building.Interact(product, this);
                            }
                            else if (crop == null)
                            {
                                building.Interact(null, this);
                            }
                        }
                        break;
                    case "SpawnedItems":
                        SpawnItems items = target.GetComponent<SpawnItems>();
                        if (items != null && product == null)   
                        {
                            items.Interact(crop, hasCan, this);
                        }
                        break;
                    case "Well":
                        Well well = target.GetComponent<Well>();
                        if (well != null && product == null)
                        {
                            well.Interact(crop, hasCan, this);
                        }
                        break;
                    case "TargetPoint":
                        TargetPoint point = target.GetComponent<TargetPoint>();
                        if (point != null)
                        {
                            if (crop != null)
                            {
                                product = crop.MakeProductFromCrop();
                            }
                            if (product != null)
                            {
                                point.Interact(product, this);
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
        AudioManager.instance.Play("pick");
        ShowInventory(crop.GetSprite());
    }

    public void UnsetCrop(bool audio = true)
    {
        crop = null;
        product = null;
        if (audio)
            AudioManager.instance.Play("put");
        CloseInventory();
    }

    public Crop GetCrop()
    {
        return crop;
    }

    public void TakeCan(GameObject c)
    {
        hasCan = true;
        can = c;
        AudioManager.instance.Play("pick");
        ShowInventory(can.GetComponent<Can>().GetSprite());
    }

    public void PlaceCan()
    {
        hasCan = false;
        can = null;
        AudioManager.instance.Play("put");
        CloseInventory();
    }

    public void SetProduct(Product p) 
    {
        product = p;
        AudioManager.instance.Play("pick");
        ShowInventory(product.productSprite);
    }

    public void UnsetProduct(bool audio = true)
    {
        product = null;
        crop = null;
        if (audio)
            AudioManager.instance.Play("put");
        CloseInventory();
    }

    public Product GetProduct()
    {
        return product;
    }

    public void AllowMovement(bool isAllowed)
    {
        basicMovement.movementEnabled = isAllowed;
    }

    private void GetKeys()
    {
        switch (player)
        {
            case "p1":
                keyUp = GameManager.instance.p1Up;
                keyDown = GameManager.instance.p1Down;
                keyLeft = GameManager.instance.p1Left;
                keyRight = GameManager.instance.p1Right;
                keyInteract = GameManager.instance.p1Interact;
                break;
            case "p2":
                keyUp = GameManager.instance.p2Up;
                keyDown = GameManager.instance.p2Down;
                keyLeft = GameManager.instance.p2Left;
                keyRight = GameManager.instance.p2Right;
                keyInteract = GameManager.instance.p2Interact;
                break;
        }
    }
}
