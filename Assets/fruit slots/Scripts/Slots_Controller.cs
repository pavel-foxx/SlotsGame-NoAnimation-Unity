using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slots_Controller : MonoBehaviour
{

    private int bet;
    private int credit = 500;
    private int[] combination;

    private bool run;
    private float delay_timer = 1f;
    private float game_timer;

    [Header("Links")]
    public Text bet_text;
    public Text credit_text;
    public Text win_text;
    public Image[] item_image;
    
    public GameObject[] betone_button;
    public GameObject[] betmax_button;
    public GameObject[] spin_button;

    [Space]
    [Header("Settings")]
    public int[] bet_list;

    [Range(0, 100)]
    public int win_chanсe;

    [System.Serializable]
    public class item_class
    {
        public Sprite icon;
        public int[] value;
        public int receive_priority;
    }
    public List<item_class> item_list = new List<item_class>();
    public float spin_time;
    [Range(0, 1)]
    public float spin_speed;

    private void Update()
    {
        if (run)
        {
            game_timer -= Time.deltaTime;
            delay_timer -= Time.deltaTime;
            if(delay_timer <= spin_speed)
            {
                delay_timer = 1f;
                if (game_timer <= 0)
                    finish();
                else
                    update_line();
            }
        }
    }

    public void spin() //метод клика на spin_button
    {
        if (credit < bet_list[bet])
            return;

        credit -= bet_list[bet];
        SoftCount.instance.Soft(credit, credit_text);

        disable_buttons();
        win_text.text = "0";
        delay_timer = 0;
        game_timer = spin_time;

        int win_id = random_item();
        bool win = false;
        if (Random.Range(0, 100) <= win_chanсe)
            win = true;

        combination = new int[item_image.Length];
        for (int i = 0; i < combination.Length; i++)
        {
            if (win)
                combination[i] = win_id;
            else
                combination[i] = random_item();
        }

        run = true;
    }

    private int random_item() //получение случайного номера, с учетом выставленных приоритетов в настройках
    {
        int total_priority = 0;
        for (int i = 0; i < item_list.Count; i++)
        {
            total_priority += item_list[i].receive_priority;
        }
        int win_value = Random.Range(total_priority, 0);
        int current_priority = 0;
        for (int i = 0; i < item_list.Count - 1; i++)
        {
            current_priority += item_list[i].receive_priority;
            if (win_value < current_priority)
                return i;
        }
        return 0;
    }

    private void update_line()
    {
        for (int i = 0; i < item_image.Length; i++)
            item_image[i].sprite = item_list[Random.Range(0, item_list.Count)].icon;
    }

    private void finish()
    {
        run = false;
        for (int i = 0; i < item_image.Length; i++)
            item_image[i].sprite = item_list[combination[i]].icon;

        bool win = true;
        for (int i = 0; i < combination.Length - 1; i++)
            if (combination[i + 1] != combination[0])
                win = false;

        if (win)
        {
            credit += item_list[combination[0]].value[bet];
            SoftCount.instance.Soft(credit, credit_text);
            SoftCount.instance.Soft(0, item_list[combination[0]].value[bet], win_text);
        }

        activate_buttons();
    }

    public void change_bet(int x) // x = 0 Уменшить на 1, x = 1 увеличить на 1, x = 2 макс. ставка
    {
        if (x == 0)
        {
            bet--;
            if (bet < 0)
                bet = bet_list.Length - 1;
        }

        if (x == 1)
        {
            bet++;
            if (bet >= bet_list.Length)
                bet = 0;
        }

        if (x == 2)
            bet = bet_list.Length - 1;

        SoftCount.instance.Soft(bet_list[bet], bet_text);
    }

    private void activate_buttons() //сделать кнопки активными
    {

        betone_button[0].SetActive(true);
        betone_button[1].SetActive(false);

        betmax_button[0].SetActive(true);
        betmax_button[1].SetActive(false);

        spin_button[0].SetActive(true);
        spin_button[1].SetActive(false);
    }
    private void disable_buttons() //сделать кнопки не активными
    {

        betone_button[0].SetActive(false);
        betone_button[1].SetActive(true);

        betmax_button[0].SetActive(false);
        betmax_button[1].SetActive(true);

        spin_button[0].SetActive(false);
        spin_button[1].SetActive(true);
    }
}
