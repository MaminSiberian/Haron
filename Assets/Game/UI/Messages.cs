using System.Collections.Generic;

public static class Messages
{
    // когда получаешь квест забрать новую душу
    public const string sounIsWaiting = "Новая душа ждет!";
    // когда забрал душу с причала
    public const string deliverSoul = "Сопроводи душу в ее последний путь";
    // когда тебя схватили руки
    public const string handsAttacking = "Неупокоенные души пытаются затянуть тебя!";
    // когда руки отпустили
    public const string handsStopped = "Ты еле вырвался из их цепких лап";
    // когда попал в воронку
    public const string whirlAttacking = "Водоворот людских судеб тянет тебя ко дну";
    // когда ушел из воронки
    public const string whirlStopped = "На сей раз ты ускользнул";
    // когда похилился
    public const string healing = "Ты чувствуешь себя лучше";
    // когда тебя заметил враг
    public const string spotted = "Кто-то приближается к тебе из темноты!";
    // когда открыл дверь
    public const string doorOpened = "Дверь тяжело распахнулась";
    // когда нет ключа чтобы открыть дверь
    public const string doorNotOpened = "Эта дорога не для тебя";
    // когда хп ниже определенной отметки
    public const string boatLow = "Лодка трещит по швам";



    // просто на карте
    public static List<string> randomLines = new List<string>()
    {
        "От воды веет могильным холодом",
        "Лодка покачивается на воде",
        "Темнота сгущается, поспеши",
        "Причала все нет. Уж не сбился ли ты с пути?",
    };
}
