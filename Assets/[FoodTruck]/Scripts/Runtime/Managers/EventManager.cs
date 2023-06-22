using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent OnMoneyDataChange = new UnityEvent();
    public static UnityEvent OnSoundStateChange = new UnityEvent();

    public static UnityEvent OnNewOrder = new UnityEvent(); // yeni sipariş geldiğinde
    public static UnityEvent OnTakingOrderCompleted = new UnityEvent(); // sipariş alındığında yani sipariş alım süresi dolduğunda
    public static UnityEvent OnMakeOrder = new UnityEvent(); // makine siparişi tammaladığında
    public static UnityEvent OnSendOrder = new UnityEvent(); // siparis gönderildiğinde
    public static UnityEvent OnOrderCompleted = new UnityEvent(); // musteri siparisi aldıgında 
}
