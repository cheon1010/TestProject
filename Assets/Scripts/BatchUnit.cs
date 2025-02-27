using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatchUnit : MonoBehaviour
{
    public static BatchUnit instance;
    public Text UName;
    public Image UnitIcon;
    public Image HPBar;
    public int BUC;
    public float BUEC;    // 유닛강화비용
    public float BUEP;    // 유닛강화확률
    public Sprite UnitIconSprite;
    GameObject UL;
    GameObject BU;
    public GameObject BannedPanel;
    public Button InfoButton;
    public Button EnchantButton;    // 강화버튼
    public int BUECost;

    private void Start()
    {
        instance = this;
        UL = GameObject.Find("UnitList");
        BU = UL.transform.GetChild(BUC - 1).gameObject;
        BannedPanel.SetActive(false);
        EnchantButton.onClick.AddListener(UnitEnchant);
    }
    private void Awake()
    {

    }
    private void Update()
    {
        BatchUnitData();
    }

    public void BatchUnitData()
    {
        UnitIcon.sprite = BU.GetComponent<UnitControl>().UnitSprite;
        UName.text = BU.GetComponent<UnitControl>().UName;
        HPBar.fillAmount = BU.GetComponent<UnitControl>().UHp / BU.GetComponent<UnitControl>().MaxUHp;
        BUEC = BU.GetComponent<UnitControl>().EnchantCost;
        BUEP = BU.GetComponent<UnitControl>().EnchantPer;
    }

    public void BatchInfoButton()
    {
        
        Debug.Log("클릭");
        GameManager.instance.UIT = true;
    }

    public void OnButtonDown()
    {
        if (GameManager.instance.UIT == false)
        {
            InfoButton.onClick.AddListener(() =>
            {
                Debug.Log(BU.ToString() + "의 상태를 불러옵니다.");
                GameManager.instance.UIT = true;
                GameManager.instance.UnitSprite.sprite = BU.GetComponent<UnitControl>().USP;
                GameManager.instance.UName.text = BU.GetComponent<UnitControl>().UName;
                GameManager.instance.UOccupation.text = BU.GetComponent<UnitControl>().UOccupation;
                GameManager.instance.UHP.text = BU.GetComponent<UnitControl>().UHp.ToString() + " / " + BU.GetComponent<UnitControl>().MaxUHp.ToString();
                GameManager.instance.UATK.text = BU.GetComponent<UnitControl>().UAtk.ToString();
                GameManager.instance.UnitCC.text = BU.GetComponent<UnitControl>().Max_CC.ToString();
                GameManager.instance.UCost.text = BU.GetComponent<UnitControl>().Cost.ToString();
                GameManager.instance.UenchantCost.text = BU.GetComponent<UnitControl>().EnchantCost.ToString();
                GameManager.instance.UEnchantPer.text = BU.GetComponent<UnitControl>().EnchantPer.ToString();
            });
        }
        else
        {
            InfoButton.onClick.AddListener(() =>
            {
                GameManager.instance.UIT = false;
            });
        }
    }

    public void BatchUnitInfo_Runtime()
    {
        GameManager.instance.UnitSprite.sprite = BU.GetComponent<UnitControl>().USP;
        GameManager.instance.UName.text = BU.GetComponent<UnitControl>().UName;
        GameManager.instance.UOccupation.text = BU.GetComponent<UnitControl>().UOccupation;
        GameManager.instance.UHP.text = BU.GetComponent<UnitControl>().UHp.ToString() + " / " + BU.GetComponent<UnitControl>().MaxUHp.ToString();
        GameManager.instance.UATK.text = BU.GetComponent<UnitControl>().UAtk.ToString();
        GameManager.instance.UnitCC.text = BU.GetComponent<UnitControl>().Max_CC.ToString();
        GameManager.instance.UCost.text = BU.GetComponent<UnitControl>().Cost.ToString();
        GameManager.instance.UenchantCost.text = BU.GetComponent<UnitControl>().EnchantCost.ToString();
        GameManager.instance.UEnchantPer.text = BU.GetComponent<UnitControl>().EnchantPer.ToString();
    }

    public void UnitEnchant()
    {
        if (BU.GetComponent<UnitControl>().Level==1)
        {
            BUECost = 5;
            if(GameManager.instance.Mana<5)
            {
                IEF();
                return;
            }
            else if(GameManager.instance.Mana>=5)
            {
                GameManager.instance.Mana -= 5;
                BU.GetComponent<UnitControl>().Level += 1;
                BU.GetComponent<UnitControl>().UAtk = Mathf.RoundToInt(BU.GetComponent<UnitControl>().UAtk * 1.5f);
                BU.GetComponent<UnitControl>().UHp = Mathf.RoundToInt(BU.GetComponent<UnitControl>().UHp * 1.25f);
                BU.GetComponent<UnitControl>().MaxUHp = Mathf.RoundToInt(BU.GetComponent<UnitControl>().MaxUHp * 1.25f);
                BU.transform.GetChild(1).gameObject.SetActive(true);
                IES();
            }
        }
        else if(BU.GetComponent<UnitControl>().Level == 2)
        {
            BUECost = 15;
            if(GameManager.instance.Mana<15)
            {
                IEF();
                BU.transform.GetChild(1).gameObject.GetComponent<Animator>().Play("NewBoomAnim", -1, 0);
                return;
            }
            else if(GameManager.instance.Mana>=15)
            {
                GameManager.instance.Mana -= 15;
                int chance = Random.Range(1, 100);
                if (chance <= 80)
                {
                    BU.GetComponent<UnitControl>().Level += 1;
                    BU.GetComponent<UnitControl>().UAtk = Mathf.RoundToInt(BU.GetComponent<UnitControl>().UAtk * 1.5f);
                    BU.GetComponent<UnitControl>().UHp = Mathf.RoundToInt(BU.GetComponent<UnitControl>().UHp * 1.25f);
                    BU.GetComponent<UnitControl>().MaxUHp = Mathf.RoundToInt(BU.GetComponent<UnitControl>().MaxUHp * 1.25f);
                    BU.transform.GetChild(1).gameObject.GetComponent<Animator>().Play("ESuccess", -1, 0);
                    IES();
                    return;
                }
                else
                {
                    IEF();
                    return;
                }
            }
        }
        else if(BU.GetComponent<UnitControl>().Level==3)
        {
            BUECost = 35;
            if(GameManager.instance.Mana<35)
            {
                IEF();
                BU.transform.GetChild(1).gameObject.GetComponent<Animator>().Play("NewBoomAnim", -1, 0);
                return;
            }
            else if(GameManager.instance.Mana>=35)
            {
                GameManager.instance.Mana -= 35;
                int chance = Random.Range(1, 100);
                if (chance <= 50)
                {
                    IES();
                    BU.GetComponent<UnitControl>().Level += 1;
                    BU.GetComponent<UnitControl>().UAtk = Mathf.RoundToInt(BU.GetComponent<UnitControl>().UAtk * 1.5f);
                    BU.GetComponent<UnitControl>().UHp = Mathf.RoundToInt(BU.GetComponent<UnitControl>().UHp * 1.25f);
                    BU.GetComponent<UnitControl>().MaxUHp = Mathf.RoundToInt(BU.GetComponent<UnitControl>().MaxUHp * 1.25f);
                    BU.transform.GetChild(1).gameObject.GetComponent<Animator>().Play("ESuccess", -1, 0);
                    return;
                }
                else
                {
                    IEF();
                    return;
                }
            }
        }
        else if(BU.GetComponent<UnitControl>().Level==4)
        {
            BUEC = 55;
            if(GameManager.instance.Mana<55)
            {
                IEF();
                BU.transform.GetChild(1).gameObject.GetComponent<Animator>().Play("NewBoomAnim", -1, 0);
                return;
            }
            else if(GameManager.instance.Mana>=55)
            {
                GameManager.instance.Mana -= 55;
                int chance = Random.Range(1, 100);
                if (chance <= 30)
                {
                    IES();
                    BU.GetComponent<UnitControl>().Level += 1;
                    BU.GetComponent<UnitControl>().UAtk = Mathf.RoundToInt(BU.GetComponent<UnitControl>().UAtk * 1.5f);
                    BU.GetComponent<UnitControl>().UHp = Mathf.RoundToInt(BU.GetComponent<UnitControl>().UHp * 1.25f);
                    BU.GetComponent<UnitControl>().MaxUHp = Mathf.RoundToInt(BU.GetComponent<UnitControl>().MaxUHp * 1.25f);
                    BU.transform.GetChild(1).gameObject.GetComponent<Animator>().Play("ESuccess", -1, 0);
                    return;
                }
                else
                {
                    IEF();
                    return;
                }
            }
        }
    }

    public void IES()   // InstantiateEnchantSuccess
    {
        GameObject ES = Instantiate(BU.GetComponent<UnitControl>().EnchantSucces);
        ES.transform.position = new Vector3(BU.transform.position.x, BU.transform.position.y - 0.5f, BU.transform.position.z);
    }
    public void IEF() // InstantiateEnchantFail
    {
        GameObject EF = Instantiate(BU.GetComponent<UnitControl>().EnchantFail);
        EF.transform.position = new Vector3(BU.transform.position.x, BU.transform.position.y - 0.5f, BU.transform.position.z);
    }

}
