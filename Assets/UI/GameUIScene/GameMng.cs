using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMng : MonoBehaviour
{
    //Item Change
    public GameObject ItemBlockPar;
    public GameObject[] ItemWhiteBlock;
    public GameObject[] ItemBlock;

    Color b = new Color(255, 255, 255, 255);
    Color c = new Color(255, 255, 255, 0);
    ////////////////////////////////////////////////

    public GameObject HPTextManage;

    public GameObject MoneyTextManage;

    //Shop Object
    public GameObject Shop;
    public GameObject ShopBt;
    public GameObject ShopImage;
    public GameObject[] ShopPoint;
    public GameObject[] ShopSlot;

    public GameObject[] SelectText;     //0 = Cost , 1 = Ammo , 2 = MaxAmmo , 3 = Damage , 4 = Firerate , 5 = Rebound

    public int[,] GunPerform = new int[4, 6];
    public int[,] HandGunPerform = new int[4, 6];
    public int[] GranadePerform;
    public int[,] AmmoPerform = new int[2,6];

    int shopPos;

    bool ShopIn;
    int NowPoint;       //0 = null;

    //총 Text
    public GameObject[] BulletText;
    public GameObject[] MaxBulletText;

    public GameObject StageText;


    //총 image 
    public Sprite[] Gun;
    public Sprite[] HandGun;
    public Sprite Boom;
    public Sprite[] BulletImage;

    public GameObject StageAni;


    // 예시 변수
    int Hp;     //HP
    int Money;      //Money
    int[] Bullet = new int[3];  // 총알

    int[] MaxBullet = new int[3];  // 총총알

    int[] GunFull = new int[3];

    int HoldGun;        //들고있는총

    int stage;

    bool startcheck;

    [SerializeField]
    private Camera uiCamera;

    // Use this for initialization
    void Start()
    {
        stage = 1;
        HoldGun = 0;
        Hp = 100;
        Money = 0;
        shopPos = 0;
        ShopIn = false;

        // StartAni();

        setGunPerform();
        setHandGunPerform();
        setAmmoperform();

        GunFullReset();
        BulletReset();
        MaxBulletReset();


        StageText.GetComponent<StageTextMng>().StageReset(stage.ToString());
        HPTextManage.GetComponent<HPTextMng>().HPReset(Hp.ToString());
        //MoneyTextManage.GetComponent<MoneyTextMng>().MoneyReset(Money.ToString());
        BulletResetting();
        MaxBulletResetting();
        startcheck = false;
    }

    void Update()
    {
        // 1번무기
        if (!ShopIn && Input.GetKeyDown(KeyCode.Alpha1))
        {
            HoldGun = 0;
            BulletResetting();
            MaxBulletResetting();
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
        else if (!ShopIn && Input.GetKeyDown(KeyCode.Alpha2))          //2번무기
        {
            HoldGun = 1;
            BulletResetting();
            MaxBulletResetting();
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
        else if (!ShopIn && Input.GetKeyDown(KeyCode.Alpha3))      //3번무기
        {
            HoldGun = 2;
            BulletResetting();
            MaxBulletResetting();
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
        // Hp Down
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hp -= 10;
            HPTextManage.GetComponent<HPTextMng>().HPChange(Hp.ToString());
        }
        // Money Up
        if (Input.GetKeyDown(KeyCode.A))
        {
            Money += 111;
            MoneyTextManage.GetComponent<MoneyTextMng>().MoneyChange(Money.ToString());
        }

        //Shop In
        if (!ShopIn && Input.GetKeyDown(KeyCode.B))
        {
            ShopBt.SetActive(false);
            Shop.SetActive(true);
            ShopIn = true;


            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        //총 발사 && 상점 버튼
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!ShopIn)
            {
                if (HoldGun.Equals(0))
                {
                    if (Bullet[HoldGun] > 0)
                    {
                        Bullet[HoldGun] -= 1;
                        BulletText[0].GetComponent<GunBulletMng>().GunBulletChange(Bullet[HoldGun].ToString());

                    }
                }

                if (HoldGun.Equals(1))
                {
                    if (Bullet[HoldGun] > 0)
                    {
                        Bullet[HoldGun] -= 1;
                        BulletText[1].GetComponent<HandGunBulletMng>().HandGunBulletChange(Bullet[HoldGun].ToString());
                    }
                }

                if (HoldGun.Equals(2))
                {
                    if (Bullet[HoldGun] > 0)
                    {
                        Bullet[HoldGun] -= 1;
                        BulletText[2].GetComponent<GrenadeBulletMng>().GrenadeBulletChange(Bullet[HoldGun].ToString());
                    }
                }
            }
            else
            {

                Vector2 wp = uiCamera.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray = new Ray2D(wp, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Pistols_slot"))        //오른쪽 클릭
                    {
                        if (shopPos.Equals(0))
                        {
                            shopPos = 1;
                            ShopSlotSetActive(1);
                        }
                    }
                    else if (hit.collider.CompareTag("Rifles_slot"))//위쪽 클릭
                    {
                        if (shopPos.Equals(0))
                        {
                            shopPos = 2;
                            ShopSlotSetActive(2);

                        }
                    }
                    else if (hit.collider.CompareTag("Granades_slot"))//왼쪽 클릭
                    {
                        if (shopPos.Equals(0))      // 폭탄 구매
                        {


                        }
                    }
                    else if (hit.collider.CompareTag("Ammo_slot"))//아래쪽 클릭
                    {
                        if (shopPos.Equals(0))
                        {
                            shopPos = 3;
                            ShopSlotSetActive(3);
                        }
                    }
                    Debug.Log("Complete" + hit.collider.name);
                }

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

                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;

                }
                else if (shopPos.Equals(1))
                {
                    shopPos = 0;
                    ShopSlotSetActive(0);
                }
                else if (shopPos.Equals(2))
                {
                    shopPos = 0;
                    ShopSlotSetActive(0);
                }
                else if (shopPos.Equals(3))
                {
                    shopPos = 0;
                    ShopSlotSetActive(0);
                }
            }
        }

        //재장전
        if (!ShopIn && Input.GetKeyDown(KeyCode.R))
        {
            if (MaxBullet[HoldGun] >= GunFull[HoldGun])
            {
                MaxBullet[HoldGun] = MaxBullet[HoldGun] - (GunFull[HoldGun] - Bullet[HoldGun]);
                Bullet[HoldGun] = GunFull[HoldGun];
            }
            else
            {
                Bullet[HoldGun] = MaxBullet[HoldGun];
                MaxBullet[HoldGun] = 0;

            }
            if (HoldGun.Equals(0))
            {
                BulletText[HoldGun].GetComponent<GunBulletMng>().GunBulletChange(Bullet[HoldGun].ToString());
                MaxBulletText[HoldGun].GetComponent<GunMaxBulletMng>().GunMaxBulletChange(MaxBullet[HoldGun].ToString());
            }

            if (HoldGun.Equals(1))
            {
                if (Bullet[HoldGun] > 0)
                {
                    BulletText[HoldGun].GetComponent<HandGunBulletMng>().HandGunBulletChange(Bullet[HoldGun].ToString());
                    MaxBulletText[HoldGun].GetComponent<HandGunMaxBulletMng>().HandGunMaxBulletChange(MaxBullet[HoldGun].ToString());
                }
            }

            if (HoldGun.Equals(2))
            {
                if (Bullet[HoldGun] > 0)
                {
                    BulletText[HoldGun].GetComponent<GrenadeBulletMng>().GrenadeBulletChange(Bullet[HoldGun].ToString());
                    MaxBulletText[HoldGun].GetComponent<GrenadeMaxBulletMng>().GrenadeMaxBulletChange(MaxBullet[HoldGun].ToString());
                }
            }
        }

        //startAni
        if (Input.GetKeyDown(KeyCode.F1))
        {
            stage++;
            StageText.GetComponent<StageTextMng>().StageChange(stage.ToString());
            StartAni();
        }

        //게임 종료
        if (Hp <= 0)
        {
            //GameOver();
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

    void BulletReset()
    {
        Bullet[0] = 30;
        Bullet[1] = 7;
        Bullet[2] = 1;
    }

    void MaxBulletReset()
    {
        MaxBullet[0] = 120;
        MaxBullet[1] = 28;
        MaxBullet[2] = 4;
    }

    void GunFullReset()
    {

        GunFull[0] = 30;
        GunFull[1] = 7;
        GunFull[2] = 1;
    }

    void BulletResetting()
    {
        for (int i = 0; i < 3; i++)
            BulletText[i].SetActive(false);


        BulletText[HoldGun].SetActive(true);
        if (HoldGun.Equals(0))
        {
            BulletText[0].GetComponent<GunBulletMng>().GunBulletReset(Bullet[HoldGun].ToString());
        }

        if (HoldGun.Equals(1))
        {
            BulletText[1].GetComponent<HandGunBulletMng>().HandGunBulletReset(Bullet[HoldGun].ToString());
        }

        if (HoldGun.Equals(2))
        {
            BulletText[2].GetComponent<GrenadeBulletMng>().GrenadeBulletReset(Bullet[HoldGun].ToString());
        }
    }

    void MaxBulletResetting()
    {
        for (int i = 0; i < 3; i++)
            MaxBulletText[i].SetActive(false);


        MaxBulletText[HoldGun].SetActive(true);
        if (HoldGun.Equals(0))
        {
            MaxBulletText[0].GetComponent<GunMaxBulletMng>().GunMaxBulletReset(MaxBullet[HoldGun].ToString());
        }

        if (HoldGun.Equals(1))
        {
            MaxBulletText[1].GetComponent<HandGunMaxBulletMng>().HandGunMaxBulletReset(MaxBullet[HoldGun].ToString());
        }

        if (HoldGun.Equals(2))
        {
            MaxBulletText[2].GetComponent<GrenadeMaxBulletMng>().GrenadeMaxBulletReset(MaxBullet[HoldGun].ToString());
        }
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

    void StartAni()
    {
        StageAni.GetComponent<Animator>().SetTrigger("StageAni");
    }

    void setGunPerform()
    {
        GunPerform[0, 0] = 1000;
        GunPerform[0, 1] = 30;
        GunPerform[0, 2] = 120;
        GunPerform[0, 3] = 15;
        GunPerform[0, 4] = 42;
        GunPerform[0, 5] = 33;

        GunPerform[1, 0] = 2000;
        GunPerform[1, 1] = 25;
        GunPerform[1, 2] = 125;
        GunPerform[1, 3] = 8;
        GunPerform[1, 4] = 85;
        GunPerform[1, 5] = 15;

        GunPerform[2, 0] = 3500;
        GunPerform[2, 1] = 40;
        GunPerform[2, 2] = 160;
        GunPerform[2, 3] = 10;
        GunPerform[2, 4] = 55;
        GunPerform[2, 5] = 20;

        GunPerform[3, 0] = 9999;
        GunPerform[3, 1] = 100;
        GunPerform[3, 2] = 300;
        GunPerform[3, 3] = 5;
        GunPerform[3, 4] = 100;
        GunPerform[3, 5] = 10;


    }
    void setHandGunPerform()
    {
        HandGunPerform[0, 0] = 1000;
        HandGunPerform[0, 1] = 30;
        HandGunPerform[0, 2] = 120;
        HandGunPerform[0, 3] = 15;
        HandGunPerform[0, 4] = 42;
        HandGunPerform[0, 5] = 33;

        HandGunPerform[1, 0] = 2000;
        HandGunPerform[1, 1] = 25;
        HandGunPerform[1, 2] = 125;
        HandGunPerform[1, 3] = 8;
        HandGunPerform[1, 4] = 85;
        HandGunPerform[1, 5] = 15;

        HandGunPerform[2, 0] = 3500;
        HandGunPerform[2, 1] = 40;
        HandGunPerform[2, 2] = 160;
        HandGunPerform[2, 3] = 10;
        HandGunPerform[2, 4] = 55;
        HandGunPerform[2, 5] = 20;

        HandGunPerform[3, 0] = 9999;
        HandGunPerform[3, 1] = 100;
        HandGunPerform[3, 2] = 300;
        HandGunPerform[3, 3] = 5;
        HandGunPerform[3, 4] = 100;
        HandGunPerform[3, 5] = 10;
    }
    void setGranadeperform()
    {
        GranadePerform[0] = 9999;
        GranadePerform[1] = 100;
        GranadePerform[2] = 300;
        GranadePerform[3] = 5;
        GranadePerform[4] = 100;
        GranadePerform[5] = 10;
    }
    void setAmmoperform()
    {
        AmmoPerform[0, 0] = 1000;
        AmmoPerform[0, 1] = 30;
        AmmoPerform[0, 2] = 120;
        AmmoPerform[0, 3] = 15;
        AmmoPerform[0, 4] = 42;
        AmmoPerform[0, 5] = 33;

        AmmoPerform[1, 0] = 2000;
        AmmoPerform[1, 1] = 25;
        AmmoPerform[1, 2] = 125;
        AmmoPerform[1, 3] = 8;
        AmmoPerform[1, 4] = 85;
        AmmoPerform[1, 5] = 15;
    }

    void ChangeSelect(int[,] i, int j)
    {
        SelectText[0].GetComponent<CostTextMng>().CostChange(i[j, 0].ToString());
        SelectText[1].GetComponent<AmmoTextMng>().AmmoChange(i[j, 1].ToString(), i[j, 2].ToString());
        SelectText[2].GetComponent<DamageTextMng>().DamageChange(i[j, 3].ToString());
        SelectText[3].GetComponent<FirerateTextMng>().FirerateChange(i[j, 4].ToString());
        SelectText[4].GetComponent<ReboundTextMng>().ReboundChange(i[j, 5].ToString());
    }

    void ChangeBoomSelect(int[] i)
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
