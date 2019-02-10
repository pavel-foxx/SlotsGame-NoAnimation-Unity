using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SoftCount : MonoBehaviour
{
    private static SoftCount _instance;

    public static SoftCount instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<SoftCount>();
            return _instance;
        }
    }
    [System.Serializable]
    public class SoftnerClass
    {
        public float Timer;

        public Text UI;
        public float ToCount;
        public float FromCount;
        public float CurrentNumber;
        public float Speed;

        public bool useComma, useSound;
    }
    private AudioSource AS;

    private List<SoftnerClass> Nums = new List<SoftnerClass>();

    private void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    public void Soft(float From, float To, Text UI_Text, bool useComma, bool useSound)
    {
        if (From == To)
            return;

        SoftnerClass temp = new SoftnerClass();
        temp.UI = UI_Text;
        temp.ToCount = To;
        temp.FromCount = From;
        temp.CurrentNumber = From;
        temp.useComma = useComma;
        temp.useSound = useSound;
        temp.Timer = 1.0f;
        temp.Speed = 8.0f;

        Nums.Add(temp);

        if (useSound)
        {
            if (AS.clip != null)
            {
                if (!AS.isPlaying)
                    AS.Play();
            }
        }
    }
    public void Soft(float From, float To, Text UI_Text, bool useComma)
    {
        if (From == To)
            return;

        SoftnerClass temp = new SoftnerClass();
        temp.UI = UI_Text;
        temp.ToCount = To;
        temp.FromCount = From;
        temp.CurrentNumber = From;
        temp.useComma = useComma;
        temp.useSound = true;
        temp.Timer = 1.0f;
        temp.Speed = 8.0f;

        Nums.Add(temp);

        if (AS.clip != null)
        {
            if (!AS.isPlaying)
                AS.Play();
        }

    }
    public void Soft(float From, float To, Text UI_Text, bool useComma, float Speed, float Timer)
    {
        if (From == To)
            return;

        SoftnerClass temp = new SoftnerClass();
        temp.UI = UI_Text;
        temp.ToCount = To;
        temp.FromCount = From;
        temp.CurrentNumber = From;
        temp.useComma = useComma;
        temp.useSound = true;
        temp.Timer = Timer;
        temp.Speed = Speed;

        Nums.Add(temp);

        if (AS.clip != null)
        {
            if (!AS.isPlaying)
                AS.Play();
        }

    }
    public void Soft(float From, float To, Text UI_Text)
    {
        if (From == To)
            return;

        SoftnerClass temp = new SoftnerClass();
        temp.UI = UI_Text;
        temp.ToCount = To;
        temp.FromCount = From;
        temp.CurrentNumber = From;
        temp.useComma = false;
        temp.useSound = true;
        temp.Timer = 1.0f;
        temp.Speed = 8.0f;

        Nums.Add(temp);


        if (AS.clip != null)
        {
            if (!AS.isPlaying)
                AS.Play();
        }

    }
    public void Soft(float To, Text UI_Text)
    {
        int From;
        System.Int32.TryParse(UI_Text.text, out From);

        if (From == To)
            return;

        SoftnerClass temp = new SoftnerClass();
        temp.UI = UI_Text;
        temp.ToCount = To;
        temp.FromCount = From;
        temp.CurrentNumber = From;
        temp.useComma = false;
        temp.useSound = true;
        temp.Timer = 1.0f;
        temp.Speed = 8.0f;

        Nums.Add(temp);


        if (AS.clip != null)
        {
            if (!AS.isPlaying)
                AS.Play();
        }

    }

    private void Update()
    {

        if (Nums.Count > 0)
        {
            for (int i = 0; i < Nums.Count; i++)
            {
                #region Check
                if (Nums[i].ToCount < 0)
                {
                    Nums[i].CurrentNumber = Nums[i].ToCount;
                    Nums[i].UI.text = "" + Nums[i].CurrentNumber;
                    Nums.RemoveAt(i);
                    continue;
                }
                #endregion

                #region Sound
                if (Nums[i].useSound)
                {
                    if (AS.clip != null)
                    {
                        if (!AS.isPlaying)
                        {
                            AS.Play();
                        }
                    }
                }
                else
                {
                    AS.Stop();
                }
                #endregion

                #region Timer
                Nums[i].Timer -= Time.deltaTime;
                if (Nums[i].Timer < 0)
                {
                    DoneCount(i);
                    continue;
                }
                #endregion

                #region Counting
                //Nums[i].CurrentNumber = Mathf.Lerp(Nums[i].CurrentNumber, Nums[i].ToCount, Time.deltaTime);
                
                if (Nums[i].FromCount < Nums[i].ToCount)
                {
                    Nums[i].CurrentNumber += (1.2f * Time.deltaTime * Nums[i].Speed) * (Nums[i].ToCount - Nums[i].FromCount);
                    if (Nums[i].CurrentNumber >= Nums[i].ToCount) { Nums[i].CurrentNumber = Nums[i].ToCount; }
                }
                else
                {
                    Nums[i].CurrentNumber -= (1.2f * Time.deltaTime * Nums[i].Speed) * (Nums[i].FromCount - Nums[i].ToCount);
                    if (Nums[i].CurrentNumber <= Nums[i].ToCount) { Nums[i].CurrentNumber = Nums[i].ToCount; }
                }
                
                
                Nums[i].CurrentNumber = Mathf.RoundToInt(Nums[i].CurrentNumber);

                if (Nums[i].useComma)
                    Nums[i].UI.text = "" + string.Format("{0:n0}", Nums[i].CurrentNumber);
                else
                    Nums[i].UI.text = "" + Nums[i].CurrentNumber;
                if (Nums[i].CurrentNumber == Nums[i].ToCount)
                {
                    DoneCount(i);
                }
                #endregion
            }

        }
        else
        {
            if (AS.isPlaying)
            {
                AS.Stop();
            }
        }
    }

    private void DoneCount(int ID)
    {
        Nums[ID].CurrentNumber = Nums[ID].ToCount;

        if (Nums[ID].useComma)
            Nums[ID].UI.text = "" + string.Format("{0:n0}", Nums[ID].CurrentNumber);
        else
            Nums[ID].UI.text = "" + Nums[ID].CurrentNumber;

        Nums.RemoveAt(ID);
    }
}