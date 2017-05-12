using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMng : MonoBehaviour
{
    private static GameMng instance = null;
    public static GameMng GetInstance
    {
        get
        {
            return instance;
        }
    }

    //Item Change
    public GameObject ItemBlockPar;
    public GameObject[] ItemWhiteBlock;
    public GameObject[] ItemBlock;

    Color b = new Color(255, 255, 255, 255);
    Color c = new Color(255, 255, 255, 0);
    ////////////////////////////////////////////////

    public Text HPText;
    public Text MoneyText;

    public AudioSource btnSound;
    public AudioSource costSound;

    public Text StageTime;
    public Text StageShow;

    //Shop Object
    public GameObject Shop;
    public GameObject ShopBt;
    public GameObject ShopSelect;
    public GameObject ShopImage;
    public GameObject[] ShopPoint;
    public GameObject[] ShopSlot;

    public GameObject[] SelectText;

    public GameObject blood;

    public float[,] GunPerform = new float[4, 6];
    public float[,] HandGunPerform = new float[4, 6];

    public float[] GranadePerform;
    public float[,] AmmoPerform = new float[2, 6];

    int shopPos;

    bool ShopIn;
    int NowPoint;       //0 = null;

    //총 Text
    public Text BulletText;
    public Text MaxBulletText;
    public Text StageText;

    //총 image 
    public Sprite[] Gun;
    public Sprite[] HandGun;
    public Sprite Boom;
    public Sprite[] BulletImage;

    bool startcheck;

    [SerializeField]
    private Camera uiCamera;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    // Use this for initialization
    void Start()
    {
        shopPos = 0;
        ShopIn = false;
        Player.GetInstance.cash = 13000;

        StageAni();

        setGunPerform();
        setHandGunPerform();
        setAmmoperform();

        startcheck = false;
    }

    void Update()
    {
        int HoldGun = -1;

        // 1번무기
        if (!ShopIn && Input.GetKeyDown(KeyCode.Alpha1))
            HoldGun = 0;
        else if (!ShopIn && Input.GetKeyDown(KeyCode.Alpha2))
            HoldGun = 1;
        else if (!ShopIn && Input.GetKeyDown(KeyCode.Alpha3))
            HoldGun = 2;

        if (HoldGun != -1)
        {
            if (!startcheck)
            {
                startcheck = true;
                ItemBlockActiveT();
                ItemBlockActiveF(HoldGun, true);
                for (int i = 0; i < 3; i++)
                    StartCoroutine(Fade(ItemBlock[i], false));
                StartCoroutine(Fade(ItemWhiteBlock[HoldGun], true));
            }
            else
            {
                StopAllCoroutines();
                startcheck = true;
                ItemBlockActiveT();
                ItemBlockActiveF(HoldGun, true);
                for (int i = 0; i < 3; i++)
                    StartCoroutine(Fade(ItemBlock[i], false));
                StartCoroutine(Fade(ItemWhiteBlock[HoldGun], true));
            }
        }

        //Shop In
        if (!ShopIn && Input.GetKeyDown(KeyCode.B))
        {
            btnSound.Play();

            ShopBt.SetActive(false);
            Shop.SetActive(true);
            ShopIn = true;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            Player.GetInstance.fpController.enabled = false;
            Player.GetInstance.armController.enabled = false;
            Player.GetInstance.enabled = false;
        }

        //총 발사 && 상점 버튼
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (ShopIn)
            {
                ShopSelect.gameObject.SetActive(true);

                Vector2 wp = uiCamera.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray = new Ray2D(wp, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Pistols_slot"))        //오른쪽 클릭
                    {
                        btnSound.Play();

                        if (shopPos.Equals(0))
                        {
                            shopPos = 1;
                            ShopSlotSetActive(1);
                        }
                        else
                        {
                            switch (shopPos)
                            {
                                case 1:
                                    if (HandGunPerform[0, 0] <= Player.GetInstance.cash)
                                    {
                                        costSound.Play();
                                        Player.GetInstance.cash -= (int)HandGunPerform[0, 0];
                                        Player.GetInstance.armController.HandGunChange(0, HandGunPerform);
                                    }
                                    break;
                                case 2:
                                    if (GunPerform[0, 0] <= Player.GetInstance.cash)
                                    {

                                        costSound.Play();
                                        Player.GetInstance.cash -= (int)GunPerform[0, 0];
                                        Player.GetInstance.armController.RifleChange(0, GunPerform);
                                    }
                                    break;
                                case 3:
                                    if (AmmoPerform[0, 0] <= Player.GetInstance.cash)
                                    {
                                        costSound.Play();
                                        Player.GetInstance.cash -= (int)AmmoPerform[0, 0];
                                        Player.GetInstance.armController.guns[1].gunSettings.maxAmmo += 10;
                                    }
                                    break;
                            }
                        }
                    }
                    else if (hit.collider.CompareTag("Rifles_slot"))    //위쪽 클릭
                    {
                        if (shopPos.Equals(0))
                        {
                            shopPos = 2;
                            ShopSlotSetActive(2);
                        }
                        else
                        {
                            switch (shopPos)
                            {
                                case 1:
                                    if (HandGunPerform[1, 0] <= Player.GetInstance.cash)
                                    {

                                        costSound.Play();
                                        Player.GetInstance.cash -= (int)HandGunPerform[1, 0];
                                        Player.GetInstance.armController.HandGunChange(1, HandGunPerform);
                                    }
                                    break;
                                case 2:
                                    if (GunPerform[1, 0] <= Player.GetInstance.cash)
                                    {

                                        costSound.Play();
                                        Player.GetInstance.cash -= (int)GunPerform[1, 0];
                                        Player.GetInstance.armController.RifleChange(1, GunPerform);
                                    }
                                    break;
                                case 3:
                                    if(AmmoPerform[1,0] <= Player.GetInstance.cash)
                                    {
                                        costSound.Play();
                                        Player.GetInstance.cash -= (int)AmmoPerform[1, 0];
                                        Player.GetInstance.armController.guns[0].gunSettings.maxAmmo += 15;
                                    }
                                    break;
                            }
                        }
                    }
                    else if (hit.collider.CompareTag("Granades_slot"))  //왼쪽 클릭
                    {
                        if (shopPos.Equals(0))      // 폭탄 구매
                        {
                            if ((int)GranadePerform[0] <= Player.GetInstance.cash)
                            {
                                costSound.Play();
                                Player.GetInstance.cash -= (int)GranadePerform[0];
                                Player.GetInstance.armController.grenade.cur += 1;
                                if (Player.GetInstance.armController.grenade.cur > 3)
                                {
                                    Player.GetInstance.armController.grenade.cur = 3;
                                    Player.GetInstance.cash += (int)GranadePerform[0];
                                }
                            }
                        }
                        else
                        {
                            switch (shopPos)
                            {
                                case 1:
                                    if (HandGunPerform[2, 0] <= Player.GetInstance.cash)
                                    {

                                        costSound.Play();
                                        Player.GetInstance.cash -= (int)HandGunPerform[2, 0];
                                        Player.GetInstance.armController.HandGunChange(2, HandGunPerform);
                                    }
                                    break;
                                case 2:
                                    if (GunPerform[2, 0] <= Player.GetInstance.cash)
                                    {

                                        costSound.Play();
                                        Player.GetInstance.cash -= (int)GunPerform[2, 0];
                                        Player.GetInstance.armController.RifleChange(2, GunPerform);
                                    }
                                    break;
                            }
                        }
                    }
                    else if (hit.collider.CompareTag("Ammo_slot"))//아래쪽 클릭
                    {
                        if (shopPos.Equals(0))
                        {
                            shopPos = 3;
                            ShopSlotSetActive(3);
                        }
                        else
                        {
                            switch (shopPos)
                            {
                                case 1:
                                    if (HandGunPerform[3, 0] <= Player.GetInstance.cash)
                                    {

                                        costSound.Play();
                                        Player.GetInstance.cash -= (int)HandGunPerform[3, 0];
                                        Player.GetInstance.armController.HandGunChange(3, HandGunPerform);
                                    }
                                    break;
                                case 2:
                                    if (GunPerform[3, 0] <= Player.GetInstance.cash)
                                    {

                                        costSound.Play();
                                        Player.GetInstance.cash -= (int)GunPerform[3, 0];
                                        Player.GetInstance.armController.RifleChange(3, GunPerform);
                                    }
                                    break;
                            }
                        }
                    }
                }

            }
            else
            {
                ShopSelect.gameObject.SetActive(false);
            }
        }

        if (ShopIn)
        {
            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(wp, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Pistols_slot"))
                {
                    if (NowPoint != 0)
                    {
                        NowPoint = 0;
                        shopwhiteslot(0);
                        ShopImage.GetComponent<Image>().sprite = null;
                        NullSelect();
                    }
                    if (shopPos.Equals(1))      //권총 오른쪽
                    {
                        ShopImage.GetComponent<Image>().sprite = HandGun[0];
                        ChangeSelect(HandGunPerform, 0);
                    }
                    else if (shopPos.Equals(2))      //총 오른쪽
                    {
                        ShopImage.GetComponent<Image>().sprite = Gun[0];
                        ChangeSelect(GunPerform, 0);
                    }
                    else if (shopPos.Equals(3))      //총알 오른쪽
                    {
                        ShopImage.GetComponent<Image>().sprite = BulletImage[0];
                        ChangeSelect(AmmoPerform, 0);
                    }

                }
                else if (hit.collider.CompareTag("Rifles_slot"))
                {
                    if (NowPoint != 1)
                    {
                        NowPoint = 1;
                        shopwhiteslot(1);
                        ShopImage.GetComponent<Image>().sprite = null;
                        NullSelect();

                    }
                    if (shopPos.Equals(1))      //권총 위쪽
                    {
                        ShopImage.GetComponent<Image>().sprite = HandGun[1];
                        ChangeSelect(HandGunPerform, 1);
                    }
                    else if (shopPos.Equals(2))      //총 위쪽
                    {
                        ShopImage.GetComponent<Image>().sprite = Gun[1];
                        ChangeSelect(GunPerform, 1);
                    }
                    else if (shopPos.Equals(3))      //총알 위쪽
                    {
                        ShopImage.GetComponent<Image>().sprite = BulletImage[1];
                        ChangeSelect(AmmoPerform, 1);
                    }
                }
                else if (hit.collider.CompareTag("Granades_slot"))
                {
                    if (NowPoint != 2)
                    {
                        NowPoint = 2;
                        shopwhiteslot(2);
                    }
                    if (shopPos.Equals(0))      //폭탄 왼쪽
                    {
                        ShopImage.GetComponent<Image>().sprite = Boom;
                        ChangeBoomSelect(GranadePerform);
                    }
                    if (shopPos.Equals(1))      //권총 왼쪽
                    {
                        ShopImage.GetComponent<Image>().sprite = HandGun[2];
                        ChangeSelect(HandGunPerform, 2);
                    }
                    else if (shopPos.Equals(2))      //총 왼쪽
                    {
                        ShopImage.GetComponent<Image>().sprite = Gun[2];
                        ChangeSelect(GunPerform, 2);
                    }
                    else if (shopPos.Equals(3))      //총알 위쪽
                    {
                        ShopImage.GetComponent<Image>().sprite = null;
                        NullSelect();
                    }
                }
                else if (hit.collider.CompareTag("Ammo_slot"))
                {
                    if (NowPoint != 3)
                    {
                        NowPoint = 3;
                        shopwhiteslot(3);
                        ShopImage.GetComponent<Image>().sprite = null;
                        NullSelect();

                    }
                    if (shopPos.Equals(1))      //권총 밑
                    {
                        ShopImage.GetComponent<Image>().sprite = HandGun[3];
                        ChangeSelect(HandGunPerform, 3);
                    }
                    else if (shopPos.Equals(2))      //총 밑
                    {
                        ShopImage.GetComponent<Image>().sprite = Gun[3];
                        ChangeSelect(GunPerform, 3);
                    }
                }
            }
            else
            {
                if (!(NowPoint.Equals(0)))
                {
                    NowPoint = 4;
                    shopwhiteslot(4);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (shopPos.Equals(0))       //상점 나가기
                {
                    ShopIn = false;
                    ShopBt.SetActive(true);
                    Shop.SetActive(false);

                    Player.GetInstance.fpController.enabled = true;
                    Player.GetInstance.armController.enabled = true;
                    Player.GetInstance.enabled = true;

                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else if (shopPos.Equals(1))
                {
                    shopPos = 0;
                    ShopSlotSetActive(0);

                    ShopSelect.gameObject.SetActive(false);
                }
                else if (shopPos.Equals(2))
                {
                    shopPos = 0;
                    ShopSlotSetActive(0);

                    ShopSelect.gameObject.SetActive(false);
                }
                else if (shopPos.Equals(3))
                {
                    shopPos = 0;
                    ShopSlotSetActive(0);

                    ShopSelect.gameObject.SetActive(false);
                }
            }
        }
    }

    void ItemBlockActiveF(int i, bool check)
    {

        for (int j = 0; j < 3; j++)
        {
            ItemWhiteBlock[j].GetComponent<Image>().color = c;
        }
        if (check)
            ItemWhiteBlock[i].GetComponent<Image>().color = b;
    }

    void ItemBlockActiveT()
    {
        Color a = new Color(255, 255, 255, 0.33f);
        for (int j = 0; j < 3; j++)
        {
            ItemBlock[j].GetComponent<Image>().color = a;
        }
    }

    public void AttackDamege()
    {
        StartCoroutine(FadeIn(blood, true));
    }

    IEnumerator FadeIn(GameObject Target, bool Full)
    {
        float f;
        float speed;
        Color c = new Color(255, 255, 255);
        if (Full)
        {
            f = 1f;
            speed = 0.03f;
        }
        else
        {
            f = 0.33f;
            speed = 0.01f;

        }
        for (; f >= 0; f -= speed)
        {
            c.a = f;
            Target.GetComponent<Image>().color = c;
            yield return null;
        }
        c.a = 0;
        Target.GetComponent<Image>().color = c;
        if (startcheck)
            startcheck = false;
    }

    IEnumerator Fade(GameObject Target, bool Full)
    {
        yield return StartCoroutine(Wait());
        float f;
        float speed;
        Color c = new Color(255, 255, 255);
        if (Full)
        {
            f = 1f;
            speed = 0.03f;
        }
        else
        {
            f = 0.33f;
            speed = 0.01f;

        }
        for (; f >= 0; f -= speed)
        {
            c.a = f;
            Target.GetComponent<Image>().color = c;
            yield return null;
        }
        c.a = 0;
        Target.GetComponent<Image>().color = c;
        if (startcheck)
            startcheck = false;

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }

    void shopwhiteslot(int i)
    {
        for (int j = 0; j < 4; j++)
        {
            ShopPoint[j].SetActive(false);
        }
        if (i != 4)
            ShopPoint[i].SetActive(true);
    }
    void ShopSlotSetActive(int i)
    {
        for (int j = 0; j < 4; j++)
        {
            ShopSlot[j].SetActive(false);
        }
        if (i != 4)
            ShopSlot[i].SetActive(true);
    }

    public void StageAni()
    {
        StageText.text = "Stage" + GameManager.GetInstance.stage.ToString();
        StageText.GetComponent<Animator>().SetTrigger("StageAni");
    }

    void setGunPerform()
    {
        GunPerform[0, 0] = 1000;
        GunPerform[0, 1] = 30;
        GunPerform[0, 2] = 120;
        GunPerform[0, 3] = 1;
        GunPerform[0, 4] = 0.15f;
        GunPerform[0, 5] = 10;

        GunPerform[1, 0] = 2000;
        GunPerform[1, 1] = 30;
        GunPerform[1, 2] = 150;
        GunPerform[1, 3] = 1;
        GunPerform[1, 4] = 0.1f;
        GunPerform[1, 5] = 10;

        GunPerform[2, 0] = 3500;
        GunPerform[2, 1] = 30;
        GunPerform[2, 2] = 180;
        GunPerform[2, 3] = 2;
        GunPerform[2, 4] = 0.1f;
        GunPerform[2, 5] = 10;

        GunPerform[3, 0] = 5000;
        GunPerform[3, 1] = 30;
        GunPerform[3, 2] = 240;
        GunPerform[3, 3] = 3;
        GunPerform[3, 4] = 0.65f;
        GunPerform[3, 5] = 10;
    }

    //0 = Cost , 1 = Ammo , 2 = MaxAmmo , 3 = Damage , 4 = Firerate , 5 = Rebound
    void setHandGunPerform()
    {
        HandGunPerform[0, 0] = 1000;
        HandGunPerform[0, 1] = 7;
        HandGunPerform[0, 2] = 50;
        HandGunPerform[0, 3] = 1;
        HandGunPerform[0, 4] = 0.15f;
        HandGunPerform[0, 5] = 10;

        HandGunPerform[1, 0] = 2000;
        HandGunPerform[1, 1] = 9;
        HandGunPerform[1, 2] = 50;
        HandGunPerform[1, 3] = 1;
        HandGunPerform[1, 4] = 0.1f;
        HandGunPerform[1, 5] = 10;

        HandGunPerform[2, 0] = 3500;
        HandGunPerform[2, 1] = 11;
        HandGunPerform[2, 2] = 50;
        HandGunPerform[2, 3] = 2;
        HandGunPerform[2, 4] = 0.1f;
        HandGunPerform[2, 5] = 10;

        HandGunPerform[3, 0] = 5000;
        HandGunPerform[3, 1] = 13;
        HandGunPerform[3, 2] = 60;
        HandGunPerform[3, 3] = 3;
        HandGunPerform[3, 4] = 0.08f;
        HandGunPerform[3, 5] = 10;
    }
    void setGranadeperform()
    {
        GranadePerform[0] = 1000;

        GranadePerform[1] = 100;
        GranadePerform[2] = 300;
        GranadePerform[3] = 5;
        GranadePerform[4] = 100;
        GranadePerform[5] = 10;
    }
    void setAmmoperform()
    {
        AmmoPerform[0, 0] = 100;
        AmmoPerform[0, 1] = 10;
        AmmoPerform[0, 2] = 10;

        AmmoPerform[0, 3] = 15;
        AmmoPerform[0, 4] = 42;
        AmmoPerform[0, 5] = 33;

        AmmoPerform[1, 0] = 900;
        AmmoPerform[1, 1] = 15;
        AmmoPerform[1, 2] = 15;

        AmmoPerform[1, 3] = 8;
        AmmoPerform[1, 4] = 85;
        AmmoPerform[1, 5] = 15;
    }

    void ChangeSelect(float[,] i, int j)
    {
        SelectText[0].GetComponent<CostTextMng>().CostChange(i[j, 0].ToString());
        SelectText[1].GetComponent<AmmoTextMng>().AmmoChange(i[j, 1].ToString(), i[j, 2].ToString());
        SelectText[2].GetComponent<DamageTextMng>().DamageChange(i[j, 3].ToString());
        SelectText[3].GetComponent<FirerateTextMng>().FirerateChange(i[j, 4].ToString());
        SelectText[4].GetComponent<ReboundTextMng>().ReboundChange(i[j, 5].ToString());
    }

    void ChangeBoomSelect(float[] i)
    {
        SelectText[0].GetComponent<CostTextMng>().CostChange(i[0].ToString());
        SelectText[1].GetComponent<AmmoTextMng>().AmmoChange(i[1].ToString(), i[2].ToString());
        SelectText[2].GetComponent<DamageTextMng>().DamageChange(i[3].ToString());
        SelectText[3].GetComponent<FirerateTextMng>().FirerateChange(i[4].ToString());
        SelectText[4].GetComponent<ReboundTextMng>().ReboundChange(i[5].ToString());

    }

    void NullSelect()
    {
        SelectText[0].GetComponent<CostTextMng>().NullCost();
        SelectText[1].GetComponent<AmmoTextMng>().NullAmmo();
        SelectText[2].GetComponent<DamageTextMng>().NullDamage();
        SelectText[3].GetComponent<FirerateTextMng>().NullFirerate();
        SelectText[4].GetComponent<ReboundTextMng>().NullRebound();
    }
}
