using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;

public class GameManager1 : MonoBehaviour
{

    [SerializeField]
    private GameObject karePrefab;

    [SerializeField]
    private Transform karelerPaneli;

    [SerializeField]
    private TMP_Text soruText;



    private GameObject[] karelerDizisi= new GameObject[24];

    [SerializeField]
    private Transform soruPaneli;

    [SerializeField]
    private Sprite[] kareSprites;

    List<int> bolumDegerleriListesi = new List<int>();

    int bolunenSayi, bolenSayi;

    int kacinciSoru;

    int butonDegeri;

    int dogruSonuc;

    bool butonaBasilsinmi;

    int kalancan;

    string sorununZorlukDerecesi;

    int carpan1, carpan2;   

    HealthManager HealthManager;

    PuanManager puanManager;

    GameObject gecerliKare;

    [SerializeField]
    private GameObject kazandýnPanel;

    [SerializeField]
    private GameObject kaybettinPanel;

    [SerializeField]
    AudioSource audioSource;

    public AudioClip butonSesi;


    private void Awake()
    {
        kalancan = 3;

        audioSource = GetComponent<AudioSource>();  

        kaybettinPanel.GetComponent<RectTransform>().localScale = Vector3.zero;

        kazandýnPanel.GetComponent<RectTransform>().localScale = Vector3.zero;



        HealthManager=Object.FindObjectOfType<HealthManager>();

        puanManager=Object.FindObjectOfType<PuanManager>();


        HealthManager.KalanHaklarýKontrolEt(kalancan);
    }


    void Start()
    {
       // SoruyuSorCarpma();
      //Debug.Log(carpan1 + "x" + carpan2);

        butonaBasilsinmi = false;

        soruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        
        kareleriOlustur();
    }

   
    public void kareleriOlustur()
    {
        for (int i = 0; i < 24; i++)
        {
            GameObject kare = Instantiate(karePrefab, karelerPaneli);
            kare.transform.GetChild(1).GetComponent<Image>().sprite = kareSprites[Random.Range(0, kareSprites.Length)];
            kare.transform.GetComponent<Button>().onClick.AddListener(() => ButonaBasildi());

            karelerDizisi[i] = kare;
        }

        BolumDegerleriniTexteYazdir();

        StartCoroutine(DoFadeRoutine());

        Invoke("SoruPaneliniAc", 2f);
    }
    void ButonaBasildi()
    {
        if(butonaBasilsinmi)
        {

            audioSource.PlayOneShot(butonSesi);

            butonDegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TMP_Text>().text);

            gecerliKare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

            SonucuKontrolet();
        }
    }

    void SonucuKontrolet()
    {
        if (butonDegeri == dogruSonuc)
        {
            gecerliKare.transform.GetChild(1).GetComponent<Image>().enabled = true;      
            
            gecerliKare.transform.GetChild(0).GetComponent<TMP_Text>().text = "";

            gecerliKare.transform.GetComponent<Button>().interactable = false;




            puanManager.PuaniArtir(sorununZorlukDerecesi);

            bolumDegerleriListesi.RemoveAt(kacinciSoru);

            if (bolumDegerleriListesi.Count>0)
            {
                SoruPaneliniAc();
            }
            else
            {
                oyunBitti();
            }

           
        }
        else
        {
            kalancan--;
            HealthManager.KalanHaklarýKontrolEt(kalancan);
        }

        if (kalancan<=0)
        {
            kaybettinPanel.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
            butonaBasilsinmi = false;
        }
    }
    void oyunBitti()
    {
        butonaBasilsinmi = false;
        kazandýnPanel.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }
    



   IEnumerator DoFadeRoutine()
    {
        foreach(var kare in karelerDizisi)
        {
            kare.GetComponent<CanvasGroup>().DOFade(1, .2f);

            yield return new WaitForSeconds(0.07f);
        }
    }
    void BolumDegerleriniTexteYazdir()
    {
        foreach (var kare in karelerDizisi)
        {
            int rastgeleDeger = Random.Range(1, 13);

            bolumDegerleriListesi.Add(rastgeleDeger);   

            kare.transform.GetChild(0).GetComponent<TMP_Text>().text = rastgeleDeger.ToString();
        }
    }

    void SoruPaneliniAc()
    {
        SoruyuSorBölme();
        butonaBasilsinmi = true;
        soruPaneli.GetComponent<RectTransform>().DOScale(4, 0.3f).SetEase(Ease.OutBack);
    }

    void SoruyuSorBölme()
    {
        bolenSayi = Random.Range(2, 11);

        kacinciSoru= Random.Range(0, bolumDegerleriListesi.Count);

        dogruSonuc = bolumDegerleriListesi[kacinciSoru];

        bolunenSayi = bolenSayi * dogruSonuc;

        if (bolunenSayi<=40)
        {
            sorununZorlukDerecesi = "kolay";          
        }
        else if (bolunenSayi > 40 && bolunenSayi<=80)
        {
            sorununZorlukDerecesi = "orta";
        }
        else
        {
            sorununZorlukDerecesi = "zor";
        }


        soruText.text=bolunenSayi.ToString() + " : " + bolenSayi.ToString();
    }

    void SoruyuSorCarpma()
    {
        carpan1 = Random.Range(2, 11);

        carpan2 = Random.Range(1, 21);

        if (carpan2<=50)
        {
            soruText.text = carpan1.ToString() + "x" + carpan2.ToString();
        }
        else
        {
            soruText.text = carpan2.ToString() + "x" + carpan1.ToString();  
        }
    }



}
