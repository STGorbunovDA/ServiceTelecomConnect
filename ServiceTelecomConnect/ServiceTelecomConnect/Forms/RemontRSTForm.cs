using MySql.Data.MySqlClient;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TextBox = System.Windows.Forms.TextBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ServiceTelecomConnect
{
    public partial class RemontRSTForm : Form
    {
        private delegate DialogResult ShowOpenFileDialogInvoker(); // делаг для invoke

        public List<string> list = new List<string>();
        public List<string> list2 = new List<string>();

        public Dictionary<string, string> dictMotorolaGP300 = new Dictionary<string, string>()
        {
            ["Замена аксессуарного разъёма"] = "0180417C01 Аксессуарный разъем для GP-300",
            ["Замена держателя боковой клавиатуры"] = "1380159S01 Держатель боковой клавиатуры GP-300",
            ["Замена защёлки к корпусу"] = "4280190R04 Защелка к корпусу GP-300",
            ["Замена клавиши РТТ"] = "7580437C01 Клавиша РТТ для GP-300",
            ["Замена клеммы питания"] = "3980188R01 Клемма питания для GP-300/P110",
            ["Замена кнопки"] = "4080447U05 Кнопка для GP300",
            ["Замена наклейки"] = "1380507B04 Наклейка на корп. р/ст.Motorola GP300",
            ["Замена предохранителя"] = "6505663R04 Предохранитель для GP-300",
            ["Замена ручки переключателя каналов"] = "3680147S07 Ручка переключателя каналов GP-300",
            ["Замена ручки регулятора громкости"] = "3680146S03 Ручка регулятора громкости GP-300",
            ["Замена уплотнителя корпуса"] = "3280545C01 Уплотнитель корпуса GP 300",
            ["Замена уплотнителя рег. громкости"] = "3280960Y01 Уплотнитель регулятора громкости GP-300",
            ["Замена фронтальной наклейки"] = "1380992Z03 Фронтальная наклейка для GP-300",
        };

        public Dictionary<string, string> dictAltavia = new Dictionary<string, string>()
        {
            ["Замена фильтра"] = "Фильтр",
            ["Замена микросхемы"] = "Микросхема",
            ["Замена резонатора"] = "Резонатор",
            ["Замена антенны"] = "Антенна В-242 /154МГц/",
            ["Замена антенного разъема"] = "Антенный разъем",
            ["Замена генератора"] = "Генератор GEN1 (9.6 МГц VCTCXO-001)",
            ["Замена гнезда"] = "Гнездо",
            ["Замена диода"] = "Диоды HSMS-2822-TR1G",
            ["Замена защёлки"] = "Защелка аккумуляторная",
            ["Замена контакта"] = "Клемма",
            ["Замена прокладки"] = "Прокладка периметр. АЛЬТ.-301 /Кит./ ",
            ["Замена рамы"] = "Рама",
            ["Замена регулятора громкости"] = "Регулятор громкости",
            ["Замена резистора"] = "Резистор",
            ["Замена элемента питания с выводом из режима хранения и проведением 3-х кратного КТЦ"] = "Элемент питания NBP-15A2 ля р/с Альтавия-301",
            ["Замена ручки"] = "Ручка",
            ["Замена световода"] = "Световод",
            ["Замена транзистора"] = "Транзистор",
            ["Замена фильтра"] = "Фильтр",
            ["Замена энкодера"] = "Энкодер (переключатель каналов)",
        };

        public Dictionary<string, string> dictMotorolaGP340 = new Dictionary<string, string>()
        {
            ["Замена антенны"] = "Антенна NAD6502 146-174Mгц",
            ["Замена антенного разъёма"] = "0180117S05 Антенный разъем для GP-340",
            ["Замена батарейных контактов"] = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS",
            ["Замена войлока GP-340"] = "3586057А02 Войлок GP340",
            ["Замена герметика верхней панели"] = "3280533Z05 Герметик верхней панели",
            ["Замена держателя боковой клавиатуры"] = "1380528Z01 Держатель боковой клавиатуры для GP-340",
            ["Замена динамика"] = "5005589U05 Динамик для GP-300/600",
            ["Замена заглушки"] = "HLN9820 Заглушка для GP-серии",
            ["Замена катушки индуктивности"] = "2462587Q42 Индуктивность L410, L411 для GP-340",
            ["Замена кварца"] = "Кварц",
            ["Замена клавиши РТТ"] = "4080523Z02 Клавиша РТТ",
            ["Замена клавиатуры РТТ"] = "7580532Z01 Клавиатура РТТ для GP-340/640",
            ["Замена липкой ленты"] = "3385681Z01 Липкая лента для 1380525Z01",
            ["Замена липучки интерфейсного разъёма"] = "1386058A01 Липучка интерфейсного разъема",
            ["Замена микропереключателя каналов"] = "4086470Z01 Микропереключатель РТТ",
            ["Замена микросхемы"] = "Микросхема",
            ["Замена микрофона"] = "5015027H01 Микрофон для GP-340",
            ["Замена передней панели"] = "1580666Z03 Передняя панель для GP-340",
            ["Замена переключателя каналов"] = "4080710Z21 Переключатель GP340",
            ["Замена регулятора громкости"] = "1880619Z06 Регулятор громкости для GP-340",
            ["Замена ручки переключателя каналов"] = "3680530Z02 Ручка переключения каналов GP-340",
            ["Замена ручки регулятора громкости"] = "3680529Z01 Ручка регулятора громкости GP-340",
            ["Замена транзистора"] = "Транзистор",
            ["Замена уплотнителя бат. контактов"] = "3280534Z01 Уплотнит.резин.батар.контактов Karizma",
            ["Замена уплотнителя О-кольца"] = "3280536Z01 Уплотнитель О-кольца GP340",
            ["Замена фронтальной наклейки"] = "1364279В03 Фронтальная наклейка для GP-340",
            ["Замена шлейфа"] = "8415169Н01 Шлейф динамика и микрофона для GP-серии",
            ["Замена эл. ключа"] = "5102463J40 Электронный ключ U400",
            ["Замена элементов питания с выводом из режима хранения и проведением 3-х кратного КТЦ"] = "Аккумулятор HNN 9009 NIMH повышенной емкости",
        };

        public Dictionary<string, string> dictMotorolaDP2400 = new Dictionary<string, string>()
        {
            ["Замена технологического РЧ разъема"] = "MS-147 РЧ разъем технологический",
            ["Замена кронштейна РТТ"] = "75012087001 Кронштейн РТТ",
            ["Замена держателя РТТ (Клавиатура программирования)"] = "42012035001 Держатель РТТ (Клавиатура программирования)",
            ["Замена антенны"] = "PMAD4120 Антенна PMAD4120 (146-160мГц)",
            ["Замена гибкого шлейфа динамика"] = "PF001006A02 Гибкий шлейф динамика",
            ["Замена заглушки аксессуарного разъема"] = "0104058J40 Заглушка аксессуарного разъема",
            ["Замена уплотнителя регулятора громкости и перключателя каналов"] = "32012269001 Уплотнитель регулятора громкости и перключателя каналов",
            ["Замена основания клавиши РТТ (клавиатура)"] = "KP000086A01 Основание клавиши РТТ (клавиатура)",
            ["Замена накладки РТТ"] = "HN000696A01 Накладка РТТ",
            ["Замена О кольца"] = "32012111001 О кольцо",
            ["Замена заглушки PЧ"] = "38012018001 Заглушка PЧ",
            ["Замена таблички передней крышки"] = "33012026001 Табличка передней крышки",
            ["Замена ручки переключения каналов"] = "36012017001 Ручка переключения каналов",
            ["Замена  ручки регулятора громкости"] = "36012016001 Ручка регулятора громкости",
            ["Замена кнопки РТТ"] = "44070354A01 Кнопка РТТ",
            ["Замена сетки динамика"] = "35012060001 Сетка динамика",
            ["Замена уплотнителя контактов АКБ"] = "32012110001 Уплотнитель контактов АКБ",
            ["Замена контактов АКБ"] = "0915184H01 Контакты АКБ",
            ["Замена верхнего уплотнителя"] = "32012089001 Верхний уплотнитель",
            ["Замена регулятора громкости"] = "1875103С04 Регулятор громкости",
            ["Замена переключателя каналов 16 позиций"] = "40012029001 Переключатель каналов 16 позиций",
            ["Замена наклейки аксессуарного разъёма"] = "33012020001 Наклейка аксессуарного разъёма",
            ["Замена резиновой клавиши PTT"] = "75012081001 Резиновая клавиша РТТ",
            ["Замена накладки РТТ"] = "38012011001 Накладка РТТ",
            ["Замена гибкого соединительного шлейфа"] = "0104045J62 Гибкий соединительный шлейф",
            ["Замена динамика"] = "50012013001 Динамик",
            ["Замена корпуса"] = "Корпус Motorola DP2400",
            ["Замена транзистора"] = "Выходной ВЧ-Транзистор",
            ["Замена антенного разъёма"] = "Антенный разъём для DP2400",
        };

        public Dictionary<string, string> dictICOM = new Dictionary<string, string>()
        {
            ["Замена антенного разъема"] = "Антенный",
            ["Замена антенны"] = "Антенна FA-SC55V для IC-F3S/IC-F3GS",
            ["Замена болта"] = "БОЛТ",
            ["Замена элемента питания с выводом из режима хранения и проведением 3-х кратного КТЦ"] = "Элемент питания BP-210 (BP-209)",
            ["Замена динамика"] = "K036NA500-66 Динамик для IC-44088",
            ["Замена заглушки"] = "Заглушка",
            ["Замена защёлки"] = "Защелка MP5",
            ["Замена защелки АКБ"] = "2251 RELESE BUTTON Защелка для АКБ",
            ["Замена клавиатуры"] = "Клавиатура",
            ["Замена клейкой ленты"] = "2251 WINDOW sheet клейкая лента для IC-F3/4/GT/GS",
            ["Замена кнопки"] = "Кнопка",
            ["Замена контакта \"-\""] = "2251 MINUS TERMINAL Контакт \" - \"",
            ["Замена контакта \"+\""] = "2251 PLUS TERMINAL Контакт \"+\"",
            ["Замена корпуса"] = "Корпус",
            ["Замена микросхемы"] = "Микросхема",
            ["Замена микрофона"] = "EM9445P-45-LF Микрофон",
            ["Замена панели защелки"] = "Панель для защелки АКБ 2251 REAL PANEL",
            ["Замена прокладки"] = "Прокладка",
            ["Замена пружины"] = "SPRING(Y) - 1 Пружина",
            ["Замена разъёма"] = "Разъем",
            ["Замена регулятора громкости"] = "Регулятор громкости TP76NOON-15F-A103-2251",
            ["Замена резины"] = "Резина МР12",
            ["Замена ручки"] = "KNOB N-276 Ручка регулятора громкости",
            ["Замена стекла"] = "2251 WINDOW PLATE стекло ЖКИ для IC-F3/4/GT/GS",
            ["Замена транзистора"] = "Транзистор",
            ["Замена уплотнителя"] = "Уплотнитель",
            ["Замена фильтра"] = "Фильтр S.XTRAL CR-664A 15.300 MHz",
            ["Замена уплотнителя"] = "Уплотнитель",
            ["Замена шасси"] = "Шасси",

        };

        public Dictionary<string, string> dictComradeR5 = new Dictionary<string, string>()
        {
            ["Замена антенны"] = "Антенна Comrade PAC-R5",
            ["Замена разъёма"] = "Разъем гарнитуры и микрофона",
            ["Замена ручки переключения каналов"] = "Ручка переключения каналов",
            ["Замена  ручки регулятора громкости"] = "Ручка регулятора громкости",
            ["Замена кнопки РТТ"] = "Кнопка РТТ",
            ["Замена войлока динамика"] = "Войлок динамика",
            ["Замена уплотнителя контактов АКБ"] = "Уплотнитель контактов АКБ",
            ["Замена контактов АКБ"] = "Контакты АКБ",
            ["Замена регулятора громкости"] = "Регулятор громкости",
            ["Замена переключателя каналов"] = "Переключатель каналов",
            ["Замена резиновой клавиши PTT"] = "Резиновая клавиша РТТ",
            ["Замена динамика"] = "Динамик",
            ["Замена корпуса"] = "Корпус Comrade-R5",
            ["Замена транзистора"] = "Выходной ВЧ-Транзистор",
            ["Замена антенного разъёма"] = "Антенный разъём для Comrade R5",
        };


        TextBox focusedTB;
        TextBox focusedTB2;

        string[] motorolaGP340_work = { "Замена антенны", "Замена антенного разъёма", "Замена батарейных контактов",
            "Замена войлока GP-340", "Замена герметика верхней панели", "Замена держателя боковой клавиатуры",
            "Замена динамика", "Замена заглушки", "Замена катушки индуктивности",
            "Замена кварца", "Замена клавиатуры РТТ", "Замена клавиши РТТ", "Замена липкой ленты",
            "Замена липучки интерфейсного разъёма", "Замена микропереключателя каналов",
            "Замена микросхемы", "Замена микрофона", "Замена передней панели", "Замена переключателя каналов",
            "Замена регулятора громкости", "Замена ручки переключателя каналов", "Замена ручки регулятора громкости",
            "Замена транзистора", "Замена уплотнителя бат. контактов", "Замена уплотнителя О-кольца", "Замена фронтальной наклейки",
            "Замена шлейфа", "Замена эл. ключа", "Замена элементов питания с выводом из режима хранения и проведением 3-х кратного КТЦ"  };

        string[] motorolaGP340_part = { "Антенна NAD6502 146-174Mгц", "0180117S05 Антенный разъем для GP-340",
            "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS", "3586057А02 Войлок GP340", "3280533Z05 Герметик верхней панели",
            "1380528Z01 Держатель боковой клавиатуры для GP-340", "5005589U05 Динамик для GP-300/600", "HLN9820 Заглушка для GP-серии",
            "2462587Q42 Индуктивность L410, L411 для GP-340", "4802245J49 Кварц 16.8МГц", "4805875Z04 Кварц плоский 16,8 МГц",
            "7580532Z01 Клавиатура РТТ для GP-340/640", "4080523Z02 Клавиша РТТ", "3385681Z01 Липкая лента для 1380525Z01",
            "1386058A01 Липучка интерфейсного разъема", "4086470Z01 Микропереключатель РТТ", "5102463J44 Микросхема УНЧ для GP-340",
            "5102463J58 Микросхема U3201", "5105739X05 Микросхема U3711 для GP340",
            "5185130С53 Микросхема", "5185765В26 Микросхема регулятора мощности", "5185963A27 Микросхема синтезатора WARIS",
            "5015027H01 Микрофон для GP-340", "1580666Z03 Передняя панель для GP-340", "4080710Z21 Переключатель GP340",
            "1880619Z06 Регулятор громкости для GP-340", "3680530Z02 Ручка переключения каналов GP-340",
            "3680529Z01 Ручка регулятора громкости GP-340", "4802245J50 Транзистор Q3721", "4813976А01 Выходной ВЧ-Транзистор Q3501 (GM-300)",
            "5105109Z67 Транзистор предв.усиления мощности", "3280534Z01 Уплотнит.резин.батар.контактов Karizma", "3280536Z01 Уплотнитель О-кольца GP340",
            "1364279В03 Фронтальная наклейка для GP-340", "8415169Н01 Шлейф динамика и микрофона для GP-серии", "5102463J40 Электронный ключ U400",
            "Аккумулятор HNN 9009 NIMH повышенной емкости"};

        string[] motorolaGP300_work = { "Замена аксессуарного разъёма", "Замена держателя боковой клавиатуры", "Замена защёлки к корпусу",
            "Замена клавиши РТТ", "Замена клеммы питания", "Замена кнопки", "Замена наклейки", "Замена предохранителя",
            "Замена ручки переключателя каналов", "Замена ручки регулятора громкости", "Замена уплотнителя корпуса",
            "Замена уплотнителя рег. громкости", "Замена фронтальной наклейки" };

        string[] motorolaGP300_part = {"0180417C01 Аксессуарный разъем для GP-300", "1380159S01 Держатель боковой клавиатуры GP-300",
            "4280190R04 Защелка к корпусу GP-300", "7580437C01 Клавиша РТТ для GP-300", "3980188R01 Клемма питания для GP-300/P110",
            "4080447U05 Кнопка для GP300", "1380507B04 Наклейка на корп. р/ст.Motorola GP300", "6505663R04 Предохранитель для GP-300",
            "3680147S07 Ручка переключателя каналов GP-300", "3680146S03 Ручка регулятора громкости GP-300",
            "3280545C01 Уплотнитель корпуса GP 300", "3280960Y01 Уплотнитель регулятора громкости GP-300",
            "1380992Z03 Фронтальная наклейка для GP-300"  };

        string[] motorolaDP2400_work = { "Замена технологического РЧ разъема", "Замена кронштейна РТТ",
            "Замена держателя РТТ (Клавиатура программирования)", "Замена антенны", "Замена гибкого шлейфа динамика",
            "Замена заглушки аксессуарного разъема", "Замена уплотнителя регулятора громкости и перключателя каналов",
            "Замена основания клавиши РТТ (клавиатура)", "Замена накладки РТТ", "Замена О кольца", "Замена заглушки PЧ",
            "Замена таблички передней крышки", "Замена ручки переключения каналов", "Замена  ручки регулятора громкости",
            "Замена кнопки РТТ", "Замена сетки динамика", "Замена уплотнителя контактов АКБ", "Замена контактов АКБ",
            "Замена верхнего уплотнителя", "Замена регулятора громкости", "Замена переключателя каналов 16 позиций",
            "Замена наклейки аксессуарного разъёма", "Замена резиновой клавиши PTT", "Замена накладки РТТ",
            "Замена гибкого соединительного шлейфа", "Замена динамика", "Замена корпуса", "Замена транзистора", "Замена антенного разъёма"};

        string[] motorolaDP2400_part = { "MS-147 РЧ разъем технологический", "75012087001 Кронштейн РТТ",
            "42012035001 Держатель РТТ (Клавиатура программирования)", "PMAD4120 Антенна PMAD4120 (146-160мГц)",
            "PF001006A02 Гибкий шлейф динамика", "0104058J40 Заглушка аксессуарного разъема ",
            "32012269001 Уплотнитель регулятора громкости и перключателя каналов", "KP000086A01 Основание клавиши РТТ (клавиатура)",
            "HN000696A01 Накладка РТТ", "32012111001 О кольцо", "38012018001 Заглушка PЧ", "33012026001 Табличка передней крышки",
            "36012017001 Ручка переключения каналов", "36012016001 Ручка регулятора громкости", "4070354A01 Кнопка РТТ",
            "35012060001 Сетка динамика", "32012110001 Уплотнитель контактов АКБ", "0915184H01 Контакты АКБ",
            "32012089001 Верхний уплотнитель", "1875103С04 Регулятор громкости", "40012029001 Переключатель каналов 16 позиций",
            "33012020001 Наклейка аксессуарного разъёма", "75012081001 Резиновая клавиша РТТ", "38012011001 Накладка РТТ",
            "0104045J62 Гибкий соединительный шлейф", "50012013001 Динамик", "Корпус Motorola DP2400", "Выходной ВЧ-Транзистор", "Антенный разъём для DP2400" };

        string[] Comrade_R5_work = { "Замена антенны", "Замена разъёма", "Замена ручки переключения каналов", 
            "Замена  ручки регулятора громкости", "Замена кнопки РТТ", "Замена войлока динамика", 
            "Замена уплотнителя контактов АКБ", "Замена контактов АКБ", "Замена регулятора громкости", 
            "Замена переключателя каналов", "Замена резиновой клавиши PTT", "Замена динамика", 
            "Замена корпуса", "Замена транзистора", "Замена антенного разъёма" };

        string[] Comrade_R5_part = {"Антенна Comrade PAC-R5", "Разъем гарнитуры и микрофона", "Ручка переключения каналов", 
            "Ручка регулятора громкости", "Кнопка РТТ", "Войлок динамика", "Уплотнитель контактов АКБ", "Контакты АКБ",
            "Регулятор громкости", "Переключатель каналов", "Резиновая клавиша РТТ", "Динамик", 
            "Корпус Comrade-R5", "Выходной ВЧ-Транзистор", "Антенный разъём для Comrade R5" };

        string[] Icom_work = {"Замена антенного разъема", "Замена антенны", "Замена элемента питания с выводом из режима хранения и проведением 3-х кратного КТЦ",
            "Замена болта", "Замена динамика", "Замена заглушки", "Замена защёлки", "Замена защелки АКБ", "Замена клавиатуры", "Замена клейкой ленты",
            "Замена кнопки", "Замена кнопки РТТ", "Замена контакта \"-\"", "Замена контакта \"+\"", "Замена корпуса", "Замена микросхемы",
            "Замена микрофона", "Замена панели защелки", "Замена прокладки", "Замена пружины", "Замена разъёма", "Замена регулятора громкости",
            "Замена резины", "Замена ручки", "Замена стекла", "Замена транзистора", "Замена уплотнителя", "Замена фильтра", "Замена шасси" };

        string[] Icom_part = {"Антенный разъем для F16 (6910015910)", "Антенный разъем для F3G (8950005260)", "Антенна FA-SC55V для IC-F3S/IC-F3GS",
            "SCREW PH BO M2X6 NI-ZK3 БОЛТ", "Элемент питания BP-210 (BP-209)", "SCREW PH BT M2X4 NI-ZC3 БОЛТ", "K036NA500-66 Динамик для IC-44088",
            "2251 JACK CAP Резиновая заглушка", "Заглушка МР37", "Защелка MP5", "2251 RELESE BUTTON Защелка для АКБ", "2251 6-KEY Клавиатура",
            "Клавиатура 2251 MAIN SEAL", "2251 WINDOW sheet клейкая лента для IC-F3/4/GT/GS", "Кнопка РТТ для F3G (22300001070)",
            "Кнопка РТТ для IC-F16 (2260002840)", "JPM1990-2711R Кнопка РТТ", "2251 MINUS TERMINAL Контакт \" - \"", "2251 PLUS TERMINAL Контакт \"+\"",
            "2251-S FRONT PANEL-1 Корпус", "Корпус для IC-F16 (с вклееным динамиком защелкой АКБ линзой)", "Микросхема  BU4066BCFV-E1 IC",
            "Микросхема CDBCA450CX24 микросборка дискриминатор", "Микросхема HD6473877H (Z-TAT) IC", "Микросхема  S.IC HN 58X2432TI", "Микросхема  UPD3140GS-E1 (DS8) IC",
            "Микросхема BU4066BCFV-E1 IC", "Микросхема IC NJM2902V-TE1", "Микросхема IC NJM2904V-TE1", "Микросхема LM2902D", "Микросхема M62363FP-650C IC",
            "Микросхема S.IC 11-S-80942ANMP-DD6-T2", "Микросхема TA31136FN8 EL IC",  "Микросхема TA31136FNG (D,EL)", "Микросхема TK11250BM IC",
            "Микросхема ТА7368F (5,ER)", "EM9445P-45-LF Микрофон", "Панель для защелки АКБ 2251 REAL PANEL", "2251 JACK PANEL Прокладка",
            "2251 PLUS TERMINAL Прокладка", "SPRING(Y) - 1 Пружина", "HSJ1122-010010 Разъем гарнитуры и микрофона", "HSJ1456-010320 JASK Разъем гарнитуры и микрофона",
            "Разъем антенный корпусной", "Регулятор громкости TP76NOON-15F-A103-2251", "Резина МР12", "KNOB N-276 Ручка регулятора громкости",
            "2251 WINDOW PLATE стекло ЖКИ для IC-F3/4/GT/GS", "DTA144EUA T106 Транзистор", "UNR911HJ (TX) Транзистор", "Транзистор 2SA1577 T106 Q",
            "Транзистор 2SB1132 T100 R", "Транзистор 2SC4116GR", "Транзистор 2SC4215-O (TE85R)", "Транзистор 2SC5085 YF", "Транзистор 2SK1069-4-TL-E S.FET",
            "Транзистор 2SK2973 (MTS101P)", "Транзистор 2SK2974", "Транзистор 2SK3019TL", "Транзистор 2SK974A-T112 S.FET", "Транзистор 2SК1120",
            "Транзистор 3SC239 XR-TL", "Транзистор 3SK293", "Транзистор BSC1736B", "Транзистор DTA144EUA T106", "Транзистор IRF1378C",
            "Транзистор UNR911HJ (TX)", "Транзистор XP6501-(TX).AB", "2251 JACK RUBBER Уплотнитель резиновый разъема гарнитуры IC-F3/4/GT/GS",
            "2251 MAIN SEAL Уплотнительная резинка", "Фильтр S.XTRAL CR-664A 15.300 MHz", "Шасси 2251 CHASSIS-7", "Шасси 2251 CHASSIS-3",
            "Шасси для F3G (2251)  (8010017990)", "Шасси для IC-F16 (8010019695)", "Шасси для ICOM IC-F11" };

        string[] Altavia_work = {"Замена фильтра", "Замена микросхемы", "Замена резонатора", "Замена антенны", "Замена антенного разъема",
            "Замена генератора", "Замена гнезда", "Замена гнезда", "Замена диода", "Замена защёлки", "Замена контакта", "Замена микросхемы",
            "Замена прокладки", "Замена рамы", "Замена регулятора громкости", "Замена резистора", "Замена резонатора",
            "Замена элемента питания с выводом из режима хранения и проведением 3-х кратного КТЦ", "Замена ручки",
            "Замена световода", "Замена транзистора", "Замена фильтра", "Замена энкодера" };

        string[] Altavia_part = { "45Т151AF/UF-5.3S SMD 45MHz Фильтр", "Микросхема DA 10 (S-812C50AUA (S1F78101Y3BO )", "ZQ1 (44.545 МГц-UM-5S Резонатор",
            "Антенна В-242 /154МГц/", "Антенный разъем", "Генератор GEN1 (9.6 МГц VCTCXO-001)", "Гнездо XP1 (Ф3.5мм (AJ306B-5B)",
            "Гнездо XP2 (Ф2.5мм (AJ405B-5B)", "Диоды HSMS-2822-TR1G", "Защелка аккумуляторная", "Клемма МИНУС", "Клемма ПЛЮС",
            "Микросхема DA10 (S-812C50AUA (S1F78101Y3B0)", "Микросхема LM386N-1", "Микросхема SA607DK", "Прокладка периметр. АЛЬТ.-301 /Кит./",
            "Рама", "Регулятор громкости", "Резистор 0805 0,1 кОм 5%", "Резистор 1218 5%  0,1 Ом", "Резонатор ZQ1 (44.545 МГц-UM-5S",
            "Элемент питания NBP-15A2 ля р/с Альтавия-301", "Ручка регулятора громкости", "Ручка энкодера", "Световод",
            "Транзистор BFS17A-SOT23", "Транзистор 2SC3019-SC75", "Транзистор BC807-40", "Транзистор BC817-40", "Транзистор BC817А",
            "Транзистор BFR93A", "Транзистор BLT50", "Транзистор RD07MVS1", "Фильтр 45T151AF/UF-5.3S SMD 45MHz", "Фильтр F4.F7 (CFUCG455E(CFUCG455RE4A)",
            "Фильтр F5 (CDBC455CLX21(CDBC455KCL.Y21)", "Фильтр SAFCH154MAL0N00(SAFC154MC70N)[MuRata]" };

        public RemontRSTForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            var myCulture = new CultureInfo("ru-RU");
            myCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = myCulture;
            foreach (TextBox textBox in panel1.Controls.OfType<TextBox>()) //перебираем текстбоксы
                textBox.GotFocus += new EventHandler(textBox_GotFocus); //подписываем обработчик к событию получения фокуса

            foreach (TextBox textBox in panel2.Controls.OfType<TextBox>())
                //перебираем текстбоксы
                textBox.GotFocus += new EventHandler(textBox_GotFocus2); //подписываем обработчик к событию получения фокуса
        }

        void textBox_GotFocus(object sender, EventArgs e)
        {
            focusedTB = (sender as TextBox); //передаем в переменную focusedTB ссылку на текстбокс, получивший фокус
        }

        void textBox_GotFocus2(object sender, EventArgs e)
        {
            focusedTB2 = (sender as TextBox); //передаем в переменную focusedTB ссылку на текстбокс, получивший фокус
        }

        void RemontRSTForm_Load(object sender, EventArgs e)
        {
            txB_сompleted_works_1.Select();

            if (txB_model.Text == "Motorola GP-340" || txB_model.Text == "Motorola GP-360")
            {
                listBox1.Items.AddRange(motorolaGP340_work);
                listBox2.Items.AddRange(motorolaGP340_part);
            }

            else if (txB_model.Text == "Motorola GP-300")
            {
                listBox1.Items.AddRange(motorolaGP300_work);
                listBox2.Items.AddRange(motorolaGP300_part);
            }

            else if (txB_model.Text == "Motorola DP-2400е" || txB_model.Text == "Motorola DP-2400")
            {
                listBox1.Items.AddRange(motorolaDP2400_work);
                listBox2.Items.AddRange(motorolaDP2400_part);
            }

            else if (txB_model.Text == "Icom IC-F3GS" || txB_model.Text == "Icom IC-F3GT"
                || txB_model.Text == "Icom IC-F16" || txB_model.Text == "Icom IC-F11")
            {
                listBox1.Items.AddRange(Icom_work);
                listBox2.Items.AddRange(Icom_part);
            }

            else if (txB_model.Text == "Альтавия-301М")
            {
                listBox1.Items.AddRange(Altavia_work);
                listBox2.Items.AddRange(Altavia_part);
            }

            else if (txB_model.Text == "Comrade R5")
            {
                listBox1.Items.AddRange(Comrade_R5_work);
                listBox2.Items.AddRange(Comrade_R5_part);
            }

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                list.Add(listBox1.Items[i].ToString());
            }

            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                list2.Add(listBox2.Items[i].ToString());
            }

            QuerySettingDataBase.LoadingLastNumberActRemont(lbL_last_act_remont);
            txB_MainMeans.Text = QuerySettingDataBase.Loading_OC_6_values(txB_serialNumber.Text).Item1;
            txB_NameProductRepaired.Text = QuerySettingDataBase.Loading_OC_6_values(txB_serialNumber.Text).Item2;
            if (txB_numberActRemont.Text != "53/")
            {
                lbL_AddRemontRST.Text = "Изменение ремонта";
                btn_save_add_rst_remont.Text = "Изменить";
            }
            else
            {
                lbL_AddRemontRST.Text = "Добавление ремонта";
                btn_save_add_rst_remont.Text = "Добавить";
            }
        }

        void Button_save_add_rst_remont_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                if (!String.IsNullOrEmpty(txB_сompleted_works_1.Text) && !String.IsNullOrEmpty(txB_parts_1.Text))
                {
                    string Mesage;
                    Mesage = "Вы действительно хотите добавить ремонт?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }

                    try
                    {
                        foreach (Control control in panel1.Controls)
                        {
                            if (control is TextBox && !String.IsNullOrEmpty(control.Text))
                            {
                                var regex = new Regex(Environment.NewLine);
                                control.Text = regex.Replace(control.Text, "");
                                control.Text.Trim();
                            }
                        }
                        foreach (Control control in panel2.Controls)
                        {
                            if (control is TextBox && !String.IsNullOrEmpty(control.Text))
                            {
                                var regex2 = new Regex(Environment.NewLine);
                                control.Text = regex2.Replace(control.Text, "");
                                control.Text.Trim();
                            }
                        }

                        var numberActRemont = txB_numberActRemont.Text;


                        if (!Regex.IsMatch(numberActRemont, @"[0-9]{2,2}/([0-9]+([A-Z]?[А-Я]?)*[.\-]?[0-9]?[0-9]?[0-9]?[A-Z]?[А-Я]?)$"))
                        {
                            MessageBox.Show("Введите корректно № Акта Ремонта", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txB_numberActRemont.Select();
                            return;
                        }
                        var сategory = cmB_сategory.Text;
                        if (String.IsNullOrEmpty(сategory))
                        {
                            MessageBox.Show("Заполните поле категория ремонта");
                            return;
                        }
                        var priceRemont = txB_priceRemont.Text;
                        var сompleted_works_1 = txB_сompleted_works_1.Text;
                        var сompleted_works_2 = txB_сompleted_works_2.Text;
                        var сompleted_works_3 = txB_сompleted_works_3.Text;
                        var сompleted_works_4 = txB_сompleted_works_4.Text;
                        var сompleted_works_5 = txB_сompleted_works_5.Text;
                        var сompleted_works_6 = txB_сompleted_works_6.Text;
                        var сompleted_works_7 = txB_сompleted_works_7.Text;
                        var parts_1 = txB_parts_1.Text;
                        var parts_2 = txB_parts_2.Text;
                        var parts_3 = txB_parts_3.Text;
                        var parts_4 = txB_parts_4.Text;
                        var parts_5 = txB_parts_5.Text;
                        var parts_6 = txB_parts_6.Text;
                        var parts_7 = txB_parts_7.Text;
                        var serialNumber = txB_serialNumber.Text;

                        var regex3 = new Regex(Environment.NewLine);
                        txB_MainMeans.Text = regex3.Replace(txB_MainMeans.Text, "");
                        var mainMeans = txB_MainMeans.Text;

                        var regex4 = new Regex(Environment.NewLine);
                        txB_NameProductRepaired.Text = regex4.Replace(txB_NameProductRepaired.Text, "");
                        var nameProductRepaired = txB_NameProductRepaired.Text;


                        if (!(numberActRemont == "") && !(сategory == "") && !(priceRemont == "") && !(сompleted_works_1 == "") && !(parts_1 == ""))
                        {
                            var changeQuery = $"UPDATE radiostantion SET numberActRemont = '{numberActRemont.Trim()}', category = '{сategory}', " +
                                $"priceRemont = '{priceRemont}', completed_works_1 = '{сompleted_works_1}', completed_works_2 = '{сompleted_works_2}', " +
                                $"completed_works_3 = '{сompleted_works_3}', completed_works_4 = '{сompleted_works_4}', " +
                                $"completed_works_5 = '{сompleted_works_5}', completed_works_6 = '{сompleted_works_6}', " +
                                $"completed_works_7 = '{сompleted_works_7}', parts_1 = '{parts_1}', parts_2 = '{parts_2}', " +
                                $"parts_3 = '{parts_3}', parts_4 = '{parts_4}', parts_5 = '{parts_5}', parts_6 = '{parts_6}', parts_7 = '{parts_7}'" +
                                $"WHERE serialNumber = '{serialNumber}'";
                            using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                            {
                                DB.GetInstance.OpenConnection();
                                command.ExecuteNonQuery();
                                DB.GetInstance.CloseConnection();
                            }
                            if (CheacSerialNumber.GetInstance.CheacSerialNumber_OC6(serialNumber))
                            {
                                var changeQueryOC = $"UPDATE OC6 SET mainMeans = '{mainMeans}', nameProductRepaired = '{nameProductRepaired}' WHERE serialNumber = '{serialNumber}'";
                                using (MySqlCommand command2 = new MySqlCommand(changeQueryOC, DB.GetInstance.GetConnection()))
                                {
                                    DB.GetInstance.OpenConnection();
                                    command2.ExecuteNonQuery();
                                    DB.GetInstance.CloseConnection();
                                }
                            }
                            else
                            {
                                var addQueryOC = $"INSERT INTO OC6 (serialNumber, mainMeans, nameProductRepaired) " +
                                    $"VALUES ('{serialNumber.Trim()}','{mainMeans.Trim()}','{nameProductRepaired.Trim()}')";

                                using (MySqlCommand command3 = new MySqlCommand(addQueryOC, DB.GetInstance.GetConnection()))
                                {
                                    DB.GetInstance.OpenConnection();
                                    command3.ExecuteNonQuery();
                                    DB.GetInstance.CloseConnection();
                                }

                            }
                            MessageBox.Show("Ремонт успешно добавлен!");
                            this.Close();

                        }
                        else
                        {
                            MessageBox.Show("Вы не заполнили нужные поля со (*)!");
                        }


                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка! Ремонт не добавлен!(Button_save_add_rst_Click)");
                    }
                }
                else { MessageBox.Show("Невозможно добавить ремонт без выполненных работ и запчастей"); }
            }
        }
        void PictureBox4_Click(object sender, EventArgs e)
        {
            string Mesage;
            Mesage = "Вы действительно хотите очистить все введенные вами поля?";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            txB_numberActRemont.Text = "";

            foreach (Control control in panel1.Controls)
            {
                if (control is TextBox)
                {
                    control.Text = "";
                }
            }
            foreach (Control control in panel2.Controls)
            {
                if (control is TextBox)
                {
                    control.Text = "";
                }
            }
        }

        void ComboBox_сategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmB_сategory.Text == "3")
            {
                if (txB_model.Text == "Icom IC-F3GT"
                || txB_model.Text == "Icom IC-F11" || txB_model.Text == "Icom IC-F16" || txB_model.Text == "Icom IC-F3GS"
                || txB_model.Text == "Motorola P040" || txB_model.Text == "Motorola P080" || txB_model.Text == "Motorola GP-300"
                || txB_model.Text == "Motorola GP-320" || txB_model.Text == "Motorola GP-340" || txB_model.Text == "Motorola GP-360"
                || txB_model.Text == "Альтавия-301М" || txB_model.Text == "Comrade R5" || txB_model.Text == "Гранит Р33П-1"
                || txB_model.Text == "Гранит Р-43" || txB_model.Text == "Радий-301" || txB_model.Text == "Kenwood ТК-2107"
                || txB_model.Text == "Vertex - 261")
                {
                    txB_priceRemont.Text = "887.94";
                }
                else
                {
                    txB_priceRemont.Text = "895.86";
                }
            }
            if (cmB_сategory.Text == "4")
            {
                if (txB_model.Text == "Icom IC-F3GT"
                || txB_model.Text == "Icom IC-F11" || txB_model.Text == "Icom IC-F16" || txB_model.Text == "Icom IC-F3GS"
                || txB_model.Text == "Motorola P040" || txB_model.Text == "Motorola P080" || txB_model.Text == "Motorola GP-300"
                || txB_model.Text == "Motorola GP-320" || txB_model.Text == "Motorola GP-340" || txB_model.Text == "Motorola GP-360"
                || txB_model.Text == "Альтавия-301М" || txB_model.Text == "Comrade R5" || txB_model.Text == "Гранит Р33П-1"
                || txB_model.Text == "Гранит Р-43" || txB_model.Text == "Радий-301" || txB_model.Text == "Kenwood ТК-2107"
                || txB_model.Text == "Vertex - 261")
                {
                    txB_priceRemont.Text = "1267.49";
                }
                else
                {
                    txB_priceRemont.Text = "1280.37";
                }
            }
            if (cmB_сategory.Text == "5")
            {
                if (txB_model.Text == "Icom IC-F3GT"
                || txB_model.Text == "Icom IC-F11" || txB_model.Text == "Icom IC-F16" || txB_model.Text == "Icom IC-F3GS"
                || txB_model.Text == "Motorola P040" || txB_model.Text == "Motorola P080" || txB_model.Text == "Motorola GP-300"
                || txB_model.Text == "Motorola GP-320" || txB_model.Text == "Motorola GP-340" || txB_model.Text == "Motorola GP-360"
                || txB_model.Text == "Альтавия-301М" || txB_model.Text == "Comrade R5" || txB_model.Text == "Гранит Р33П-1"
                || txB_model.Text == "Гранит Р-43" || txB_model.Text == "Радий-301" || txB_model.Text == "Kenwood ТК-2107"
                || txB_model.Text == "Vertex - 261")
                {
                    txB_priceRemont.Text = "2535.97";
                }
                else
                {
                    txB_priceRemont.Text = "2559.75";
                }
            }
            if (cmB_сategory.Text == "6")
            {
                if (txB_model.Text == "Icom IC-F3GT"
                || txB_model.Text == "Icom IC-F11" || txB_model.Text == "Icom IC-F16" || txB_model.Text == "Icom IC-F3GS"
                || txB_model.Text == "Motorola P040" || txB_model.Text == "Motorola P080" || txB_model.Text == "Motorola GP-300"
                || txB_model.Text == "Motorola GP-320" || txB_model.Text == "Motorola GP-340" || txB_model.Text == "Motorola GP-360"
                || txB_model.Text == "Альтавия-301М" || txB_model.Text == "Comrade R5" || txB_model.Text == "Гранит Р33П-1"
                || txB_model.Text == "Гранит Р-43" || txB_model.Text == "Радий-301" || txB_model.Text == "Kenwood ТК-2107"
                || txB_model.Text == "Vertex - 261")
                {
                    txB_priceRemont.Text = "5071.94";
                }
                else
                {
                    txB_priceRemont.Text = "5119.51";
                }
            }
        }

        void RemontRSTForm_KeyUp(object sender, KeyEventArgs e)
        {
            TxB_completed_works();
            TxB_parts();
        }
        void Label_company_DoubleClick(object sender, EventArgs e)
        {
            cmb_remont_select.Visible = true;
        }

        #region лайфхак для ремонтов
        void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmB_сategory.Text == "6")
            {
                if (cmb_remont_select.Text == "1")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена батарейных контактов";
                        txB_parts_1.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_2.Text = "Замена уплотнителя О-кольца";
                        txB_parts_2.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена шлейфа";
                        txB_parts_4.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_6.Text = "Замена заглушки";
                        txB_parts_6.Text = "LN9820 Заглушка для GP-серии";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "50012013001 Динамик";

                        txB_сompleted_works_2.Text = "Замена накладки РТТ";
                        txB_parts_2.Text = "HN000696A01 Накладка РТТ";

                        txB_сompleted_works_3.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        txB_parts_3.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        txB_сompleted_works_4.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_4.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_5.Text = "Замена О кольца";
                        txB_parts_5.Text = "32012111001 О кольцо";

                        txB_сompleted_works_6.Text = "Замена контактов АКБ";
                        txB_parts_6.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_7.Text = "Замена верхнего уплотнителя";
                        txB_parts_7.Text = "32012089001 Верхний уплотнитель";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена антенного разъема";
                        txB_parts_1.Text = "Антенный разъем для F3G (8950005260)";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "K036NA500-66 Динамик для IC-44088 ";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена фильтра";
                        txB_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена заглушки";
                        txB_parts_6.Text = "Заглушка МР37";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена антенного разъема";
                        txB_parts_1.Text = "Антенный разъем для F3GT";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "K036NA500-66 Динамик для IC-44088 ";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена фильтра";
                        txB_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена заглушки";
                        txB_parts_6.Text = "Заглушка МР37";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена антенного разъема";
                        txB_parts_1.Text = "Антенный разъем для F16 (6910015910)";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "K036NA500-66 Динамик для IC-44088 ";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена фильтра";
                        txB_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена заглушки";
                        txB_parts_6.Text = "Заглушка МР37";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "2")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена держателя боковой клавиатуры";
                        txB_parts_1.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        txB_сompleted_works_2.Text = "Замена войлока GP-340";
                        txB_parts_2.Text = "3586057А02 Войлок GP340";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена шлейфа";
                        txB_parts_4.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии ";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_6.Text = "Замена батарейных контактов";
                        txB_parts_6.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "50012013001 Динамик";

                        txB_сompleted_works_2.Text = "Замена контактов АКБ";
                        txB_parts_2.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_3.Text = "Замена верхнего уплотнителя";
                        txB_parts_3.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_4.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        txB_parts_4.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        txB_сompleted_works_5.Text = "Замена ручки переключения каналов";
                        txB_parts_5.Text = "36012017001 Ручка переключения каналов";

                        txB_сompleted_works_6.Text = "Замена  ручки регулятора громкости";
                        txB_parts_6.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_7.Text = "Замена сетки динамика";
                        txB_parts_7.Text = "35012060001 Сетка динамика";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088 ";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена разъёма";
                        txB_parts_5.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_6.Text = "Замена регулятора громкости";
                        txB_parts_6.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        txB_сompleted_works_7.Text = "Замена ручки";
                        txB_parts_7.Text = "KNOB N-276 Ручка регулятора громкости";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3GT";

                        txB_сompleted_works_5.Text = "Замена разъёма";
                        txB_parts_5.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_6.Text = "Замена регулятора громкости";
                        txB_parts_6.Text = "Регулятор громкости для F3GT";

                        txB_сompleted_works_7.Text = "Замена ручки";
                        txB_parts_7.Text = "Ручка регулятора громкости для F3GT";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для IC-F16 (2260002840)";

                        txB_сompleted_works_5.Text = "Замена разъёма";
                        txB_parts_5.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_6.Text = "Замена регулятора громкости";
                        txB_parts_6.Text = "Регулятор громкости для F16";

                        txB_сompleted_works_7.Text = "Замена ручки";
                        txB_parts_7.Text = "Ручка регулятора громкости для F16";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                }

                if (cmb_remont_select.Text == "3")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена войлока GP-340";
                        txB_parts_1.Text = "3586057А02 Войлок GP340";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена ручки переключателя каналов";
                        txB_parts_4.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_6.Text = "Замена шлейфа";
                        txB_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_1.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_2.Text = "Замена верхнего уплотнителя";
                        txB_parts_2.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_3.Text = "Замена контактов АКБ";
                        txB_parts_3.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_4.Text = "Замена регулятора громкости";
                        txB_parts_4.Text = "1875103С04 Регулятор громкости";

                        txB_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        txB_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена кнопки РТТ";
                        txB_parts_6.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_7.Text = "Замена накладки РТТ";
                        txB_parts_7.Text = "HN000696A01 Накладка РТТ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена транзистора";
                        txB_parts_1.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена резины";
                        txB_parts_3.Text = "Резина МР12";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "Замена панели защелки";
                        txB_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_7.Text = "Замена контакта + ";
                        txB_parts_7.Text = "2251 PLUS TERMINAL Контакт  + ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена транзистора";
                        txB_parts_1.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена резины";
                        txB_parts_3.Text = "Резина МР12";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "Замена панели защелки";
                        txB_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_7.Text = "Замена контакта + ";
                        txB_parts_7.Text = "2251 PLUS TERMINAL Контакт  + ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена транзистора";
                        txB_parts_1.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена резины";
                        txB_parts_3.Text = "Резина МР12";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "Замена панели защелки";
                        txB_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_7.Text = "Замена контакта + ";
                        txB_parts_7.Text = "2251 PLUS TERMINAL Контакт  + ";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "4")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена фронтальной наклейки";
                        txB_parts_1.Text = "1364279В03 Фронтальная наклейка для GP-340";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена ручки регулятора громкости";
                        txB_parts_5.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_6.Text = "Замена шлейфа";
                        txB_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена верхнего уплотнителя";
                        txB_parts_1.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_2.Text = "Замена контактов АКБ";
                        txB_parts_2.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_3.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_3.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_4.Text = "Замена регулятора громкости";
                        txB_parts_4.Text = "1875103С04 Регулятор громкости";

                        txB_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        txB_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена динамика";
                        txB_parts_6.Text = "50012013001 Динамик";

                        txB_сompleted_works_7.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_7.Text = "32012110001 Уплотнитель контактов АКБ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена антенного разъема";
                        txB_parts_2.Text = "Антенный разъем для F3G (8950005260)";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "Замена регулятора громкости";
                        txB_parts_6.Text = "Регулятор громкости TP76NOON-15F-A103-2251 ";

                        txB_сompleted_works_7.Text = "Замена ручки";
                        txB_parts_7.Text = "KNOB N-276 Ручка регулятора громкости";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена антенного разъема";
                        txB_parts_2.Text = "Антенный разъем для F3G (8950005260)";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "Замена регулятора громкости";
                        txB_parts_6.Text = "Регулятор громкости TP76NOON-15F-A103-2251 ";

                        txB_сompleted_works_7.Text = "Замена ручки";
                        txB_parts_7.Text = "KNOB N-276 Ручка регулятора громкости";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена антенного разъема";
                        txB_parts_2.Text = "Антенный разъем для F3G (8950005260)";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "Замена регулятора громкости";
                        txB_parts_6.Text = "Регулятор громкости TP76NOON-15F-A103-2251 ";

                        txB_сompleted_works_7.Text = "Замена ручки";
                        txB_parts_7.Text = "KNOB N-276 Ручка регулятора громкости";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "5")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена герметика верхней панели";
                        txB_parts_1.Text = "3280533Z05 Герметик верхней панели";

                        txB_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        txB_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_5.Text = "Замена батарейных контактов";
                        txB_parts_5.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_6.Text = "Замена шлейфа";
                        txB_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена контактов АКБ";
                        txB_parts_1.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_2.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_2.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_3.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        txB_parts_3.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "50012013001 Динамик";

                        txB_сompleted_works_5.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_5.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_6.Text = "Замена  ручки регулятора громкости";
                        txB_parts_6.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_7.Text = "Замена кнопки РТТ";
                        txB_parts_7.Text = "4070354A01 Кнопка РТТ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2974";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_6.Text = "Замена контакта -";
                        txB_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_7.Text = "Замена клавиатуры";
                        txB_parts_7.Text = "Клавиатура 2251 MAIN SEAL";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2974";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_6.Text = "Замена контакта -";
                        txB_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_7.Text = "Замена клавиатуры";
                        txB_parts_7.Text = "Клавиатура 2251 MAIN SEAL";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2974";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_6.Text = "Замена контакта -";
                        txB_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_7.Text = "Замена клавиатуры";
                        txB_parts_7.Text = "Клавиатура 2251 MAIN SEAL";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "6")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена липучки интерфейсного разъёма";
                        txB_parts_1.Text = "1386058A01 Липучка интерфейсного разъема";

                        txB_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        txB_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_4.Text = "Замена ручки переключателя каналов";
                        txB_parts_4.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        txB_сompleted_works_5.Text = "Замена шлейфа";
                        txB_parts_5.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_6.Text = "Замена батарейных контактов";
                        txB_parts_6.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена  ручки регулятора громкости";
                        txB_parts_2.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_4.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        txB_parts_4.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "50012013001 Динамик";

                        txB_сompleted_works_6.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_6.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_7.Text = "Замена контактов АКБ";
                        txB_parts_7.Text = "0915184H01 Контакты АКБ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена клавиатуры";
                        txB_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_5.Text = "Замена кнопки РТТ";
                        txB_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        txB_сompleted_works_6.Text = "Замена корпуса";
                        txB_parts_6.Text = "Корпус IC-F3GS";

                        txB_сompleted_works_7.Text = "Замена прокладки";
                        txB_parts_7.Text = "2251 JACK PANEL Прокладка";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена клавиатуры";
                        txB_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_5.Text = "Замена кнопки РТТ";
                        txB_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        txB_сompleted_works_6.Text = "Замена корпуса";
                        txB_parts_6.Text = "Корпус IC-F3GT";

                        txB_сompleted_works_7.Text = "Замена прокладки";
                        txB_parts_7.Text = "2251 JACK PANEL Прокладка";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена клавиатуры";
                        txB_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_5.Text = "Замена кнопки РТТ";
                        txB_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        txB_сompleted_works_6.Text = "Замена корпуса";
                        txB_parts_6.Text = "Корпус для IC-F16 (с вклееным динамиком защелкой АКБ линзой)";

                        txB_сompleted_works_7.Text = "Замена прокладки";
                        txB_parts_7.Text = "2251 JACK PANEL Прокладка";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "7")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена микрофона";
                        txB_parts_3.Text = "5015027H01 Микрофон для GP-340";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена шлейфа";
                        txB_parts_5.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_6.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_6.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена микрофона";
                        txB_parts_2.Text = "5015027H01 Микрофон для DP2400";

                        txB_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        txB_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена контактов АКБ";
                        txB_parts_4.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_5.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_5.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_6.Text = "Замена О кольца";
                        txB_parts_6.Text = "32012111001 О кольцо";

                        txB_сompleted_works_7.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_7.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена прокладки";
                        txB_parts_1.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_4.Text = "Замена защелки АКБ";
                        txB_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_5.Text = "Замена микросхемы";
                        txB_parts_5.Text = "Микросхема TA31136FN8 EL IC";

                        txB_сompleted_works_6.Text = "Замена кнопки";
                        txB_parts_6.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_7.Text = "Замена динамика";
                        txB_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена прокладки";
                        txB_parts_1.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_4.Text = "Замена защелки АКБ";
                        txB_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_5.Text = "Замена микросхемы";
                        txB_parts_5.Text = "Микросхема TA31136FN8 EL IC";

                        txB_сompleted_works_6.Text = "Замена кнопки";
                        txB_parts_6.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_7.Text = "Замена динамика";
                        txB_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена прокладки";
                        txB_parts_1.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_4.Text = "Замена защелки АКБ";
                        txB_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_5.Text = "Замена микросхемы";
                        txB_parts_5.Text = "Микросхема TA31136FN8 EL IC";

                        txB_сompleted_works_6.Text = "Замена кнопки";
                        txB_parts_6.Text = "Кнопка РТТ для IC-F16 (2260002840)";

                        txB_сompleted_works_7.Text = "Замена динамика";
                        txB_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "8")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена передней панели";
                        txB_parts_3.Text = "1580666Z03 Передняя панель для GP-340";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена липучки интерфейсного разъёма";
                        txB_parts_5.Text = "1386058A01 Липучка интерфейсного разъема";

                        txB_сompleted_works_6.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_6.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена контактов АКБ";
                        txB_parts_2.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_3.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_3.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_4.Text = "Замена верхнего уплотнителя";
                        txB_parts_4.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_5.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_5.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_6.Text = "Замена  ручки регулятора громкости";
                        txB_parts_6.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_7.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_7.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена фильтра";
                        txB_parts_3.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена прокладки";
                        txB_parts_6.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_7.Text = "Замена заглушки";
                        txB_parts_7.Text = "Заглушка МР37";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена фильтра";
                        txB_parts_3.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена прокладки";
                        txB_parts_6.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_7.Text = "Замена заглушки";
                        txB_parts_7.Text = "Заглушка МР37";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена фильтра";
                        txB_parts_3.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена прокладки";
                        txB_parts_6.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_7.Text = "Замена заглушки";
                        txB_parts_7.Text = "Заглушка МР37";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "9")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена ручки переключателя каналов";
                        txB_parts_1.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_6.Text = "Замена герметика верхней панели";
                        txB_parts_6.Text = "3280533Z05 Герметик верхней панели";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_2.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_3.Text = "Замена контактов АКБ";
                        txB_parts_3.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "50012013001 Динамик";

                        txB_сompleted_works_5.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_5.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_6.Text = "Замена кнопки РТТ";
                        txB_parts_6.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_7.Text = "Замена верхнего уплотнителя";
                        txB_parts_7.Text = "32012089001 Верхний уплотнитель";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена фильтра";
                        txB_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_6.Text = "Замена антенного разъема";
                        txB_parts_6.Text = "Антенный разъем для F3G (8950005260)";

                        txB_сompleted_works_7.Text = "Замена заглушки";
                        txB_parts_7.Text = "Заглушка МР37";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена фильтра";
                        txB_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_6.Text = "Замена антенного разъема";
                        txB_parts_6.Text = "Антенный разъем для F3G (8950005260)";

                        txB_сompleted_works_7.Text = "Замена заглушки";
                        txB_parts_7.Text = "Заглушка МР37";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена фильтра";
                        txB_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_6.Text = "Замена антенного разъема";
                        txB_parts_6.Text = "Антенный разъем для F16";

                        txB_сompleted_works_7.Text = "Замена заглушки";
                        txB_parts_7.Text = "Заглушка МР37";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "10")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена войлока GP-340";
                        txB_parts_1.Text = "3586057А02 Войлок GP340";

                        txB_сompleted_works_2.Text = "Замена микросхемы";
                        txB_parts_2.Text = "5185963A27 Микросхема синтезатора WARIS";

                        txB_сompleted_works_3.Text = "Замена липучки интерфейсного разъёма";
                        txB_parts_3.Text = "1386058A01 Липучка интерфейсного разъема";

                        txB_сompleted_works_4.Text = "Замена держателя боковой клавиатуры";
                        txB_parts_4.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        txB_сompleted_works_5.Text = "Замена уплотнителя О-кольца";
                        txB_parts_5.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        txB_сompleted_works_6.Text = "Замена фронтальной наклейки";
                        txB_parts_6.Text = "1364279В03 Фронтальная наклейка для GP-340";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_2.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "50012013001 Динамик";

                        txB_сompleted_works_4.Text = "Замена  ручки регулятора громкости";
                        txB_parts_4.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "Замена регулятора громкости";
                        txB_parts_5.Text = "1875103С04 Регулятор громкости";

                        txB_сompleted_works_6.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_6.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_7.Text = "Замена контактов АКБ";
                        txB_parts_7.Text = "0915184H01 Контакты АКБ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена разъёма";
                        txB_parts_1.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_2.Text = "Замена регулятора громкости";
                        txB_parts_2.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "Замена ручки";
                        txB_parts_6.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_7.Text = "Замена динамика";
                        txB_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена разъёма";
                        txB_parts_1.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_2.Text = "Замена регулятора громкости";
                        txB_parts_2.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "Замена ручки";
                        txB_parts_6.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_7.Text = "Замена динамика";
                        txB_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена разъёма";
                        txB_parts_1.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_2.Text = "Замена регулятора громкости";
                        txB_parts_2.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F16";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "Замена ручки";
                        txB_parts_6.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_7.Text = "Замена динамика";
                        txB_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "11")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена войлока GP-340";
                        txB_parts_1.Text = "3586057А02 Войлок GP340";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_4.Text = "Замена шлейфа";
                        txB_parts_4.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_5.Text = "Замена микрофона";
                        txB_parts_5.Text = "5015027H01 Микрофон для GP-340";

                        txB_сompleted_works_6.Text = "Замена заглушки";
                        txB_parts_6.Text = "HLN9820 Заглушка для GP-серии";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        txB_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена О кольца";
                        txB_parts_4.Text = "32012111001 О кольцо";

                        txB_сompleted_works_4.Text = "Замена контактов АКБ";
                        txB_parts_4.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_5.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_5.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_6.Text = "Замена верхнего уплотнителя";
                        txB_parts_6.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_7.Text = "Замена микрофона";
                        txB_parts_7.Text = "5015027H01 Микрофон для DP2400";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена транзистора";
                        txB_parts_3.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_4.Text = "Замена прокладки";
                        txB_parts_4.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_5.Text = "Замена разъёма";
                        txB_parts_5.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_6.Text = "Замена контакта + ";
                        txB_parts_6.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_7.Text = "Замена панели защелки";
                        txB_parts_7.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена транзистора";
                        txB_parts_3.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_4.Text = "Замена прокладки";
                        txB_parts_4.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_5.Text = "Замена разъёма";
                        txB_parts_5.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_6.Text = "Замена контакта + ";
                        txB_parts_6.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_7.Text = "Замена панели защелки";
                        txB_parts_7.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена транзистора";
                        txB_parts_3.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_4.Text = "Замена прокладки";
                        txB_parts_4.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_5.Text = "Замена разъёма";
                        txB_parts_5.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_6.Text = "Замена контакта + ";
                        txB_parts_6.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_7.Text = "Замена панели защелки";
                        txB_parts_7.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                }

                if (cmb_remont_select.Text == "12")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        txB_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_4.Text = "Замена ручки переключателя каналов";
                        txB_parts_4.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        txB_сompleted_works_5.Text = "Замена батарейных контактов";
                        txB_parts_5.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_6.Text = "Замена шлейфа";
                        txB_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена контактов АКБ";
                        txB_parts_1.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_2.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_2.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_3.Text = "Замена верхнего уплотнителя";
                        txB_parts_3.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_4.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_4.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_5.Text = "Замена кнопки РТТ";
                        txB_parts_5.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_6.Text = "Замена  ручки регулятора громкости";
                        txB_parts_6.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_7.Text = "Замена О кольца";
                        txB_parts_7.Text = "32012111001 О кольцо";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена регулятора громкости";
                        txB_parts_1.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "Замена заглушки";
                        txB_parts_6.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_7.Text = "Замена антенного разъема";
                        txB_parts_7.Text = "Антенный разъем для F3G (8950005260)";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена регулятора громкости";
                        txB_parts_1.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "Замена заглушки";
                        txB_parts_6.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_7.Text = "Замена антенного разъема";
                        txB_parts_7.Text = "Антенный разъем для F3G (8950005260)";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена регулятора громкости";
                        txB_parts_1.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "Замена заглушки";
                        txB_parts_6.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_7.Text = "Замена антенного разъема";
                        txB_parts_7.Text = "Антенный разъем для F16";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "13")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена войлока GP-340";
                        txB_parts_1.Text = "3586057А02 Войлок GP340";

                        txB_сompleted_works_2.Text = "Замена ручки переключателя каналов";
                        txB_parts_2.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_4.Text = "Замена батарейных контактов";
                        txB_parts_4.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_5.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_5.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_6.Text = "Замена шлейфа";
                        txB_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_1.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "50012013001 Динамик";

                        txB_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_4.Text = "Замена контактов АКБ";
                        txB_parts_4.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_5.Text = "Замена кнопки РТТ";
                        txB_parts_5.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_6.Text = "Замена верхнего уплотнителя";
                        txB_parts_6.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_7.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_7.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена прокладки";
                        txB_parts_4.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2974";

                        txB_сompleted_works_6.Text = "Замена контакта -";
                        txB_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена прокладки";
                        txB_parts_4.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2974";

                        txB_сompleted_works_6.Text = "Замена контакта -";
                        txB_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена прокладки";
                        txB_parts_4.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2974";

                        txB_сompleted_works_6.Text = "Замена контакта -";
                        txB_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "14")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена держателя боковой клавиатуры";
                        txB_parts_1.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        txB_сompleted_works_2.Text = "Замена микросхемы";
                        txB_parts_2.Text = "5185963A27 Микросхема синтезатора WARIS";

                        txB_сompleted_works_3.Text = "Замена герметика верхней панели";
                        txB_parts_3.Text = "3280533Z05 Герметик верхней панели";

                        txB_сompleted_works_4.Text = "Замена клавиатуры РТТ";
                        txB_parts_4.Text = "7580532Z01 Клавиатура РТТ для GP-340/640";

                        txB_сompleted_works_5.Text = "Замена уплотнителя О-кольца";
                        txB_parts_5.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        txB_сompleted_works_6.Text = "Замена фронтальной наклейки";
                        txB_parts_6.Text = "1364279В03 Фронтальная наклейка для GP-340";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена контактов АКБ";
                        txB_parts_1.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "50012013001 Динамик";

                        txB_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_4.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_4.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        txB_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена ручки переключения каналов";
                        txB_parts_6.Text = "36012017001 Ручка переключения каналов";

                        txB_сompleted_works_7.Text = "Замена переключателя каналов 16 позиций";
                        txB_parts_7.Text = "40012029001 Переключатель каналов 16 позиций";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "Замена корпуса";
                        txB_parts_6.Text = "Корпус IC-F3GS";

                        txB_сompleted_works_7.Text = "Замена кнопки РТТ";
                        txB_parts_7.Text = "JPM1990-2711R Кнопка РТТ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "Замена корпуса";
                        txB_parts_6.Text = "Корпус IC-F3GS";

                        txB_сompleted_works_7.Text = "Замена кнопки РТТ";
                        txB_parts_7.Text = "JPM1990-2711R Кнопка РТТ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "Замена корпуса";
                        txB_parts_6.Text = "Корпус IC-F3GS";

                        txB_сompleted_works_7.Text = "Замена кнопки РТТ";
                        txB_parts_7.Text = "JPM1990-2711R Кнопка РТТ";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "15")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена микрофона";
                        txB_parts_1.Text = "5015027H01 Микрофон для GP-340";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "HLN9820 Заглушка для GP-серии";

                        txB_сompleted_works_6.Text = "Замена шлейфа";
                        txB_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        txB_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена О кольца";
                        txB_parts_4.Text = "32012111001 О кольцо";

                        txB_сompleted_works_4.Text = "Замена контактов АКБ";
                        txB_parts_4.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_5.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_5.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_6.Text = "Замена кнопки РТТ";
                        txB_parts_6.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_7.Text = "Замена динамика";
                        txB_parts_7.Text = "50012013001 Динамик";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_6.Text = "Замена фильтра";
                        txB_parts_6.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_6.Text = "Замена фильтра";
                        txB_parts_6.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_6.Text = "Замена фильтра";
                        txB_parts_6.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "16")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_2.Text = "Замена клавиатуры РТТ";
                        txB_parts_2.Text = "7580532Z01 Клавиатура РТТ для GP-340/640";

                        txB_сompleted_works_3.Text = "Замена держателя боковой клавиатуры";
                        txB_parts_3.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        txB_сompleted_works_4.Text = "Замена клавиши РТТ";
                        txB_parts_4.Text = "4080523Z02 Клавиша РТТ";

                        txB_сompleted_works_5.Text = "Замена герметика верхней панели";
                        txB_parts_5.Text = "3280533Z05 Герметик верхней панели ";

                        txB_сompleted_works_6.Text = "Замена фронтальной наклейки";
                        txB_parts_6.Text = "1364279В03 Фронтальная наклейка для GP-340";

                        txB_сompleted_works_7.Text = "Замена батарейных контактов";
                        txB_parts_7.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена кнопки РТТ";
                        txB_parts_1.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_2.Text = "Замена  ручки регулятора громкости";
                        txB_parts_2.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена О кольца";
                        txB_parts_3.Text = "32012111001 О кольцо";

                        txB_сompleted_works_4.Text = "Замена резиновой клавиши PTT";
                        txB_parts_4.Text = "75012081001 Резиновая клавиша РТТ";

                        txB_сompleted_works_5.Text = "Замена накладки РТТ";
                        txB_parts_5.Text = "38012011001 Накладка РТТ";

                        txB_сompleted_works_6.Text = "Замена контактов АКБ";
                        txB_parts_6.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_5.Text = "Замена фильтра";
                        txB_parts_5.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_5.Text = "Замена фильтра";
                        txB_parts_5.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_5.Text = "Замена фильтра";
                        txB_parts_5.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "17")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена передней панели";
                        txB_parts_1.Text = "1580666Z03 Передняя панель для GP-340";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_4.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_4.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_5.Text = "Замена клавиатуры РТТ";
                        txB_parts_5.Text = "7580532Z01 Клавиатура РТТ для GP-340/640";

                        txB_сompleted_works_6.Text = "Замена держателя боковой клавиатуры";
                        txB_parts_6.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        txB_сompleted_works_7.Text = "Замена клавиши РТТ";
                        txB_parts_7.Text = "4080523Z02 Клавиша РТТ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена кнопки РТТ";
                        txB_parts_1.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_2.Text = "Замена контактов АКБ";
                        txB_parts_2.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_3.Text = "Замена корпуса";
                        txB_parts_3.Text = "Корпус DP2400(e)";

                        txB_сompleted_works_4.Text = "Замена резиновой клавиши PTT";
                        txB_parts_4.Text = "75012081001 Резиновая клавиша РТТ";

                        txB_сompleted_works_5.Text = "Замена накладки РТТ";
                        txB_parts_5.Text = "38012011001 Накладка РТТ";

                        txB_сompleted_works_6.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_6.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = ")";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = ")";



                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = ")";


                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

            }

            if (cmB_сategory.Text == "5")
            {
                if (cmb_remont_select.Text == "1")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_2.Text = "Замена уплотнителя О-кольца";
                        txB_parts_2.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена шлейфа";
                        txB_parts_4.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_5.Text = "Замена микрофона";
                        txB_parts_5.Text = "5015027H01 Микрофон для GP-340";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        txB_parts_1.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "50012013001 Динамик";

                        txB_сompleted_works_3.Text = "Замена О кольца";
                        txB_parts_3.Text = "32012111001 О кольцо";

                        txB_сompleted_works_4.Text = "Замена накладки РТТ";
                        txB_parts_4.Text = "HN000696A01 Накладка РТТ";

                        txB_сompleted_works_5.Text = "Замена верхнего уплотнителя";
                        txB_parts_5.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "2")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена шлейфа";
                        txB_parts_1.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_2.Text = "Замена войлока GP-340";
                        txB_parts_2.Text = "3586057А02 Войлок GP340";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена сетки динамика";
                        txB_parts_1.Text = "35012060001 Сетка динамика";

                        txB_сompleted_works_2.Text = "Замена  ручки регулятора громкости";
                        txB_parts_2.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        txB_parts_3.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        txB_сompleted_works_4.Text = "Замена ручки переключения каналов";
                        txB_parts_4.Text = "36012017001 Ручка переключения каналов";

                        txB_сompleted_works_5.Text = "Замена верхнего уплотнителя";
                        txB_parts_5.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";
                    }

                }

                if (cmb_remont_select.Text == "3")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена войлока GP-340";
                        txB_parts_1.Text = "3586057А02 Войлок GP340";

                        txB_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        txB_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена ручки переключателя каналов";
                        txB_parts_4.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена кнопки РТТ";
                        txB_parts_1.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_2.Text = "Замена регулятора громкости";
                        txB_parts_2.Text = "1875103С04 Регулятор громкости";

                        txB_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        txB_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена верхнего уплотнителя";
                        txB_parts_5.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_5.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_5.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена клавиатуры";
                        txB_parts_1.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена контакта -";
                        txB_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена клавиатуры";
                        txB_parts_1.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена контакта -";
                        txB_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена клавиатуры";
                        txB_parts_1.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена контакта -";
                        txB_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "4")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена шлейфа";
                        txB_parts_3.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена ручки регулятора громкости";
                        txB_parts_5.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена верхнего уплотнителя";
                        txB_parts_1.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_2.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_2.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_3.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_3.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "50012013001 Динамик";

                        txB_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        txB_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена ручки";
                        txB_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена кнопки";
                        txB_parts_3.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена ручки";
                        txB_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена кнопки";
                        txB_parts_3.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена ручки";
                        txB_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена кнопки";
                        txB_parts_3.Text = "Кнопка РТТ для F16";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "5")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена шлейфа";
                        txB_parts_1.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        txB_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена батарейных контактов";
                        txB_parts_5.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_1.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "50012013001 Динамик";

                        txB_сompleted_works_3.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        txB_parts_3.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        txB_сompleted_works_4.Text = "Замена  ручки регулятора громкости";
                        txB_parts_4.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_5.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена контакта + ";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_6.Text = "Замена панели защелки";
                        txB_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена контакта + ";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_6.Text = "Замена панели защелки";
                        txB_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена контакта + ";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_6.Text = "Замена панели защелки";
                        txB_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "6")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_1.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        txB_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_3.Text = "Замена шлейфа";
                        txB_parts_3.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_4.Text = "Замена ручки переключателя каналов";
                        txB_parts_4.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        txB_parts_2.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        txB_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        txB_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_4.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "50012013001 Динамик";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "7")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_1.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_2.Text = "Замена микрофона";
                        txB_parts_2.Text = "5015027H01 Микрофон для GP-340";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена шлейфа";
                        txB_parts_5.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена О кольца";
                        txB_parts_2.Text = "32012111001 О кольцо";

                        txB_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        txB_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена микрофона";
                        txB_parts_4.Text = "5015027H01 Микрофон для DP2400";

                        txB_сompleted_works_5.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_5.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_6.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_6.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена разъёма";
                        txB_parts_1.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_2.Text = "Замена защелки АКБ";
                        txB_parts_2.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена разъёма";
                        txB_parts_1.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_2.Text = "Замена защелки АКБ";
                        txB_parts_2.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена разъёма";
                        txB_parts_1.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_2.Text = "Замена защелки АКБ";
                        txB_parts_2.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "8")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_1.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена передней панели";
                        txB_parts_3.Text = "1580666Z03 Передняя панель для GP-340";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена липучки интерфейсного разъёма";
                        txB_parts_5.Text = "1386058A01 Липучка интерфейсного разъема";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_2.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_3.Text = "Замена верхнего уплотнителя";
                        txB_parts_3.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_4.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_4.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        txB_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "9")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена герметика верхней панели";
                        txB_parts_1.Text = "3280533Z05 Герметик верхней панели";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "50012013001 Динамик";

                        txB_сompleted_works_3.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_3.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_4.Text = "Замена верхнего уплотнителя";
                        txB_parts_4.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_5.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_5.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена прокладки";
                        txB_parts_1.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_4.Text = "Замена защелки АКБ";
                        txB_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена прокладки";
                        txB_parts_1.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_4.Text = "Замена защелки АКБ";
                        txB_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена прокладки";
                        txB_parts_1.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_4.Text = "Замена защелки АКБ";
                        txB_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "10")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена ручки переключателя каналов";
                        txB_parts_1.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        txB_сompleted_works_2.Text = "Замена войлока GP-340";
                        txB_parts_2.Text = "3586057А02 Войлок GP340";

                        txB_сompleted_works_3.Text = "Замена заглушки HLN9820";
                        txB_parts_3.Text = "Заглушка для GP-серии";

                        txB_сompleted_works_4.Text = "Замена уплотнителя О-кольца";
                        txB_parts_4.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        txB_сompleted_works_5.Text = "Замена держателя боковой клавиатуры";
                        txB_parts_5.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "50012013001 Динамик";

                        txB_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_4.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_4.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        txB_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена регулятора громкости";
                        txB_parts_6.Text = "1875103С04 Регулятор громкости";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена клавиатуры";
                        txB_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена клавиатуры";
                        txB_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена клавиатуры";
                        txB_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "11")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_2.Text = "Замена уплотнителя О-кольца";
                        txB_parts_2.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        txB_сompleted_works_3.Text = "Замена микрофона";
                        txB_parts_3.Text = "5015027H01 Микрофон для GP-340";

                        txB_сompleted_works_4.Text = "Замена шлейфа";
                        txB_parts_4.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_5.Text = "Замена антенны";
                        txB_parts_5.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена  ручки регулятора громкости";
                        txB_parts_2.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена верхнего уплотнителя";
                        txB_parts_3.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_4.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_4.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_5.Text = "Замена О кольца";
                        txB_parts_5.Text = "32012111001 О кольцо";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена контакта -";
                        txB_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_5.Text = "Замена клавиатуры";
                        txB_parts_5.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена контакта -";
                        txB_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_5.Text = "Замена клавиатуры";
                        txB_parts_5.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена контакта -";
                        txB_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_5.Text = "Замена клавиатуры";
                        txB_parts_5.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "12")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_1.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена липучки интерфейсного разъёма";
                        txB_parts_3.Text = "1386058A01 Липучка интерфейсного разъема";

                        txB_сompleted_works_4.Text = "Замена антенны";
                        txB_parts_4.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена кнопки РТТ";
                        txB_parts_1.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_2.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_2.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_4.Text = "Замена верхнего уплотнителя";
                        txB_parts_4.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_5.Text = "Замена О кольца";
                        txB_parts_5.Text = "32012111001 О кольцо";

                        txB_сompleted_works_6.Text = "Замена  ручки регулятора громкости";
                        txB_parts_6.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_3.Text = "Замена кнопки";
                        txB_parts_3.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "Заглушка МР37";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_3.Text = "Замена кнопки";
                        txB_parts_3.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "Заглушка МР37";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_3.Text = "Замена кнопки";
                        txB_parts_3.Text = "Кнопка РТТ для F16";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "Заглушка МР37";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "Ручка регулятора громкости F-16";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "13")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_2.Text = "Замена шлейфа";
                        txB_parts_2.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_3.Text = "Замена ручки регулятора громкости";
                        txB_parts_3.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена антенны";
                        txB_parts_5.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена верхнего уплотнителя";
                        txB_parts_1.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_2.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_2.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "50012013001 Динамик";

                        txB_сompleted_works_5.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_5.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена ручки";
                        txB_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_2.Text = "Замена резины";
                        txB_parts_2.Text = "Резина МР12";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_4.Text = "Замена панели защелки";
                        txB_parts_4.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_5.Text = "Замена контакта + ";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена ручки";
                        txB_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_2.Text = "Замена резины";
                        txB_parts_2.Text = "Резина МР12";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_4.Text = "Замена панели защелки";
                        txB_parts_4.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_5.Text = "Замена контакта + ";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена ручки";
                        txB_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_2.Text = "Замена резины";
                        txB_parts_2.Text = "Резина МР12";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_4.Text = "Замена панели защелки";
                        txB_parts_4.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_5.Text = "Замена контакта + ";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "14")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена шлейфа";
                        txB_parts_1.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_2.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_2.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_5.Text = "Замена антенны";
                        txB_parts_5.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_1.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "50012013001 Динамик";

                        txB_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        txB_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_4.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_5.Text = "Замена ручки переключения каналов";
                        txB_parts_5.Text = "36012017001 Ручка переключения каналов";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "15")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена шлейфа";
                        txB_parts_1.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_3.Text = "Замена ручки регулятора громкости";
                        txB_parts_3.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена антенны";
                        txB_parts_5.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена О кольца";
                        txB_parts_2.Text = "32012111001 О кольцо";

                        txB_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        txB_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "50012013001 Динамик";

                        txB_сompleted_works_5.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_5.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_6.Text = "Замена кнопки РТТ";
                        txB_parts_6.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "Заглушка МР37";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "Заглушка МР37";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "Заглушка МР37";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "16")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_2.Text = "Замена клавиатуры РТТ";
                        txB_parts_2.Text = "7580532Z01 Клавиатура РТТ для GP-340/640";

                        txB_сompleted_works_3.Text = "Замена держателя боковой клавиатуры";
                        txB_parts_3.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        txB_сompleted_works_4.Text = "Замена клавиши РТТ";
                        txB_parts_4.Text = "4080523Z02 Клавиша РТТ";

                        txB_сompleted_works_5.Text = "Замена герметика верхней панели";
                        txB_parts_5.Text = "3280533Z05 Герметик верхней панели ";

                        txB_сompleted_works_6.Text = "Замена фронтальной наклейки";
                        txB_parts_6.Text = "1364279В03 Фронтальная наклейка для GP-340";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена кнопки РТТ";
                        txB_parts_1.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_2.Text = "Замена  ручки регулятора громкости";
                        txB_parts_2.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена О кольца";
                        txB_parts_3.Text = "32012111001 О кольцо";

                        txB_сompleted_works_4.Text = "Замена резиновой клавиши PTT";
                        txB_parts_4.Text = "75012081001 Резиновая клавиша РТТ";

                        txB_сompleted_works_5.Text = "Замена накладки РТТ";
                        txB_parts_5.Text = "38012011001 Накладка РТТ";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "Замена кнопки РТТ";
                        txB_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "Замена кнопки РТТ";
                        txB_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "Замена кнопки РТТ";
                        txB_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "17")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена передней панели";
                        txB_parts_1.Text = "1580666Z03 Передняя панель для GP-340";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена клавиатуры РТТ";
                        txB_parts_4.Text = "7580532Z01 Клавиатура РТТ для GP-340/640";

                        txB_сompleted_works_5.Text = "Замена клавиши РТТ";
                        txB_parts_5.Text = "4080523Z02 Клавиша РТТ";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена кнопки РТТ";
                        txB_parts_1.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_2.Text = "Замена корпуса";
                        txB_parts_2.Text = "Корпус DP2400(e)";

                        txB_сompleted_works_3.Text = "Замена резиновой клавиши PTT";
                        txB_parts_3.Text = "75012081001 Резиновая клавиша РТТ";

                        txB_сompleted_works_4.Text = "Замена накладки РТТ";
                        txB_parts_4.Text = "38012011001 Накладка РТТ";

                        txB_сompleted_works_5.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_5.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = ")";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = ")";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = ")";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }
            }

        }



        #endregion

        void TxB_parts()
        {
            foreach (Control control in panel2.Controls)
            {
                if (control is TextBox && control.ContainsFocus)
                {
                    if (control.Text.Length > 0)
                    {
                        for (int i = 0; i < this.listBox2.Items.Count; i++)
                            if (!this.listBox2.Items[i].ToString().Contains(control.Text))
                            {
                                this.listBox2.Items.RemoveAt(i);
                                i--;
                            }
                    }
                    else
                    {
                        this.listBox2.Items.Clear();
                        for (int i = 0; i < list2.Count; i++)
                            this.listBox2.Items.Add(list2[i]);

                    }
                }
            }
        }

        void TxB_completed_works()
        {
            foreach (Control control in panel1.Controls)
            {
                if (control is TextBox && control.ContainsFocus)
                {
                    if (control.Text.Length > 0)
                    {
                        for (int i = 0; i < this.listBox1.Items.Count; i++)
                            if (!this.listBox1.Items[i].ToString().Contains(control.Text))
                            {
                                this.listBox1.Items.RemoveAt(i);
                                i--;
                            }
                    }
                    else
                    {
                        this.listBox1.Items.Clear();
                        for (int i = 0; i < list.Count; i++)
                            this.listBox1.Items.Add(list[i]);

                    }
                    //control.SelectionStart = txtBox.Text.Length;
                }
            }
        }

        void ListBox1_Click(object sender, EventArgs e)
        {
            if (focusedTB != null)
            {
                if (listBox1.SelectedIndex != -1)
                {
                    focusedTB.Focus(); //возвращаем фокус в текстбокс

                    foreach (Control control in panel1.Controls)
                    {
                        if (control is TextBox && control.ContainsFocus)
                        {
                            control.Text = listBox1.SelectedItem.ToString();

                            string value = "";

                            if (txB_model.Text == "Motorola GP-340" || txB_model.Text == "Motorola GP-360")
                            {
                                if (txB_сompleted_works_1.ContainsFocus)
                                {
                                    if (dictMotorolaGP340.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_1.Focus();
                                        txB_parts_1.Text = value;
                                        txB_parts_1.SelectionStart = txB_parts_1.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_2.ContainsFocus)
                                {
                                    if (dictMotorolaGP340.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_2.Focus();
                                        txB_parts_2.Text = value;
                                        txB_parts_2.SelectionStart = txB_parts_2.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_3.ContainsFocus)
                                {
                                    if (dictMotorolaGP340.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_3.Focus();
                                        txB_parts_3.Text = value;
                                        txB_parts_3.SelectionStart = txB_parts_3.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_4.ContainsFocus)
                                {
                                    if (dictMotorolaGP340.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_4.Focus();
                                        txB_parts_4.Text = value;
                                        txB_parts_4.SelectionStart = txB_parts_4.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_5.ContainsFocus)
                                {
                                    if (dictMotorolaGP340.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_5.Focus();
                                        txB_parts_5.Text = value;
                                        txB_parts_5.SelectionStart = txB_parts_5.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_6.ContainsFocus)
                                {
                                    if (dictMotorolaGP340.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_6.Focus();
                                        txB_parts_6.Text = value;
                                        txB_parts_6.SelectionStart = txB_parts_6.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_7.ContainsFocus)
                                {
                                    if (dictMotorolaGP340.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_7.Focus();
                                        txB_parts_7.Text = value;
                                        txB_parts_7.SelectionStart = txB_parts_7.Text.Length;
                                    }
                                }
                            }

                            else if (txB_model.Text == "Motorola DP-2400е" || txB_model.Text == "Motorola DP-2400")
                            {
                                if (txB_сompleted_works_1.ContainsFocus)
                                {
                                    if (dictMotorolaDP2400.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_1.Focus();
                                        txB_parts_1.Text = value;
                                        txB_parts_1.SelectionStart = txB_parts_1.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_2.ContainsFocus)
                                {
                                    if (dictMotorolaDP2400.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_2.Focus();
                                        txB_parts_2.Text = value;
                                        txB_parts_2.SelectionStart = txB_parts_2.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_3.ContainsFocus)
                                {
                                    if (dictMotorolaDP2400.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_3.Focus();
                                        txB_parts_3.Text = value;
                                        txB_parts_3.SelectionStart = txB_parts_3.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_4.ContainsFocus)
                                {
                                    if (dictMotorolaDP2400.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_4.Focus();
                                        txB_parts_4.Text = value;
                                        txB_parts_4.SelectionStart = txB_parts_4.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_5.ContainsFocus)
                                {
                                    if (dictMotorolaDP2400.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_5.Focus();
                                        txB_parts_5.Text = value;
                                        txB_parts_5.SelectionStart = txB_parts_5.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_6.ContainsFocus)
                                {
                                    if (dictMotorolaDP2400.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_6.Focus();
                                        txB_parts_6.Text = value;
                                        txB_parts_6.SelectionStart = txB_parts_6.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_7.ContainsFocus)
                                {
                                    if (dictMotorolaDP2400.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_7.Focus();
                                        txB_parts_7.Text = value;
                                        txB_parts_7.SelectionStart = txB_parts_7.Text.Length;
                                    }
                                }
                            }

                            else if (txB_model.Text == "Icom IC-F3GS" || txB_model.Text == "Icom IC-F3GT"
                            || txB_model.Text == "Icom IC-F16" || txB_model.Text == "Icom IC-F11")
                            {
                                if (txB_сompleted_works_1.ContainsFocus)
                                {
                                    if (dictICOM.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_1.Focus();
                                        txB_parts_1.Text = value;
                                        txB_parts_1.SelectionStart = txB_parts_1.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_2.ContainsFocus)
                                {
                                    if (dictICOM.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_2.Focus();
                                        txB_parts_2.Text = value;
                                        txB_parts_2.SelectionStart = txB_parts_2.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_3.ContainsFocus)
                                {
                                    if (dictICOM.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_3.Focus();
                                        txB_parts_3.Text = value;
                                        txB_parts_3.SelectionStart = txB_parts_3.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_4.ContainsFocus)
                                {
                                    if (dictICOM.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_4.Focus();
                                        txB_parts_4.Text = value;
                                        txB_parts_4.SelectionStart = txB_parts_4.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_5.ContainsFocus)
                                {
                                    if (dictICOM.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_5.Focus();
                                        txB_parts_5.Text = value;
                                        txB_parts_5.SelectionStart = txB_parts_5.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_6.ContainsFocus)
                                {
                                    if (dictICOM.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_6.Focus();
                                        txB_parts_6.Text = value;
                                        txB_parts_6.SelectionStart = txB_parts_6.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_7.ContainsFocus)
                                {
                                    if (dictICOM.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_7.Focus();
                                        txB_parts_7.Text = value;
                                        txB_parts_7.SelectionStart = txB_parts_7.Text.Length;
                                    }
                                }
                            }

                            else if (txB_model.Text == "Motorola GP-300")
                            {
                                if (txB_сompleted_works_1.ContainsFocus)
                                {
                                    if (dictMotorolaGP300.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_1.Focus();
                                        txB_parts_1.Text = value;
                                        txB_parts_1.SelectionStart = txB_parts_1.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_2.ContainsFocus)
                                {
                                    if (dictMotorolaGP300.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_2.Focus();
                                        txB_parts_2.Text = value;
                                        txB_parts_2.SelectionStart = txB_parts_2.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_3.ContainsFocus)
                                {
                                    if (dictMotorolaGP300.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_3.Focus();
                                        txB_parts_3.Text = value;
                                        txB_parts_3.SelectionStart = txB_parts_3.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_4.ContainsFocus)
                                {
                                    if (dictMotorolaGP300.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_4.Focus();
                                        txB_parts_4.Text = value;
                                        txB_parts_4.SelectionStart = txB_parts_4.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_5.ContainsFocus)
                                {
                                    if (dictMotorolaGP300.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_5.Focus();
                                        txB_parts_5.Text = value;
                                        txB_parts_5.SelectionStart = txB_parts_5.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_6.ContainsFocus)
                                {
                                    if (dictMotorolaGP300.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_6.Focus();
                                        txB_parts_6.Text = value;
                                        txB_parts_6.SelectionStart = txB_parts_6.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_7.ContainsFocus)
                                {
                                    if (dictMotorolaGP300.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_7.Focus();
                                        txB_parts_7.Text = value;
                                        txB_parts_7.SelectionStart = txB_parts_7.Text.Length;
                                    }
                                }
                            }

                            else if (txB_model.Text == "Альтавия-301М")
                            {
                                if (txB_сompleted_works_1.ContainsFocus)
                                {
                                    if (dictAltavia.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_1.Focus();
                                        txB_parts_1.Text = value;
                                        txB_parts_1.SelectionStart = txB_parts_1.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_2.ContainsFocus)
                                {
                                    if (dictAltavia.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_2.Focus();
                                        txB_parts_2.Text = value;
                                        txB_parts_2.SelectionStart = txB_parts_2.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_3.ContainsFocus)
                                {
                                    if (dictAltavia.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_3.Focus();
                                        txB_parts_3.Text = value;
                                        txB_parts_3.SelectionStart = txB_parts_3.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_4.ContainsFocus)
                                {
                                    if (dictAltavia.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_4.Focus();
                                        txB_parts_4.Text = value;
                                        txB_parts_4.SelectionStart = txB_parts_4.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_5.ContainsFocus)
                                {
                                    if (dictAltavia.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_5.Focus();
                                        txB_parts_5.Text = value;
                                        txB_parts_5.SelectionStart = txB_parts_5.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_6.ContainsFocus)
                                {
                                    if (dictAltavia.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_6.Focus();
                                        txB_parts_6.Text = value;
                                        txB_parts_6.SelectionStart = txB_parts_6.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_7.ContainsFocus)
                                {
                                    if (dictAltavia.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_7.Focus();
                                        txB_parts_7.Text = value;
                                        txB_parts_7.SelectionStart = txB_parts_7.Text.Length;
                                    }
                                }
                            }

                            else if (txB_model.Text == "Comrade R5")
                            {
                                if (txB_сompleted_works_1.ContainsFocus)
                                {
                                    if (dictComradeR5.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_1.Focus();
                                        txB_parts_1.Text = value;
                                        txB_parts_1.SelectionStart = txB_parts_1.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_2.ContainsFocus)
                                {
                                    if (dictComradeR5.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_2.Focus();
                                        txB_parts_2.Text = value;
                                        txB_parts_2.SelectionStart = txB_parts_2.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_3.ContainsFocus)
                                {
                                    if (dictComradeR5.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_3.Focus();
                                        txB_parts_3.Text = value;
                                        txB_parts_3.SelectionStart = txB_parts_3.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_4.ContainsFocus)
                                {
                                    if (dictComradeR5.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_4.Focus();
                                        txB_parts_4.Text = value;
                                        txB_parts_4.SelectionStart = txB_parts_4.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_5.ContainsFocus)
                                {
                                    if (dictComradeR5.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_5.Focus();
                                        txB_parts_5.Text = value;
                                        txB_parts_5.SelectionStart = txB_parts_5.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_6.ContainsFocus)
                                {
                                    if (dictComradeR5.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_6.Focus();
                                        txB_parts_6.Text = value;
                                        txB_parts_6.SelectionStart = txB_parts_6.Text.Length;
                                    }
                                }
                                else if (txB_сompleted_works_7.ContainsFocus)
                                {
                                    if (dictComradeR5.TryGetValue(control.Text, out value))
                                    {
                                        txB_parts_7.Focus();
                                        txB_parts_7.Text = value;
                                        txB_parts_7.SelectionStart = txB_parts_7.Text.Length;
                                    }
                                }
                            }
                        }
                    }
                    this.listBox1.Items.Clear();
                    for (int i = 0; i < list.Count; i++)
                        this.listBox1.Items.Add(list[i]);
                }
            }

        }

        private void ListBox2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                if (focusedTB2 != null)
                    focusedTB2.Focus(); //возвращаем фокус в текстбокс

                foreach (Control control in panel2.Controls)
                {
                    if (control is TextBox && control.ContainsFocus)
                    {
                        control.Text = listBox2.SelectedItem.ToString();
                    }
                }
                this.listBox2.Items.Clear();
                for (int i = 0; i < list2.Count; i++)
                    this.listBox2.Items.Add(list2[i]);
            }
        }

        #region изменение в тест боксах для словаря

        void TxB_parts_1_TextChanged(object sender, EventArgs e)
        {
            if (txB_parts_1.Text.Length > 0)
            {
                for (int i = 0; i < this.listBox2.Items.Count; i++)
                    if (!this.listBox2.Items[i].ToString().Contains(txB_parts_1.Text))
                    {
                        this.listBox2.Items.RemoveAt(i);
                        i--;
                    }
            }
            else
            {
                this.listBox2.Items.Clear();
                for (int i = 0; i < list2.Count; i++)
                    this.listBox2.Items.Add(list2[i]);

            }
        }

        private void TxB_parts_2_TextChanged(object sender, EventArgs e)
        {
            if (txB_parts_2.Text.Length > 0)
            {
                for (int i = 0; i < this.listBox2.Items.Count; i++)
                    if (!this.listBox2.Items[i].ToString().Contains(txB_parts_2.Text))
                    {
                        this.listBox2.Items.RemoveAt(i);
                        i--;
                    }
            }
            else
            {
                this.listBox2.Items.Clear();
                for (int i = 0; i < list2.Count; i++)
                    this.listBox2.Items.Add(list2[i]);

            }
        }

        private void TxB_parts_3_TextChanged(object sender, EventArgs e)
        {
            if (txB_parts_3.Text.Length > 0)
            {
                for (int i = 0; i < this.listBox2.Items.Count; i++)
                    if (!this.listBox2.Items[i].ToString().Contains(txB_parts_3.Text))
                    {
                        this.listBox2.Items.RemoveAt(i);
                        i--;
                    }
            }
            else
            {
                this.listBox2.Items.Clear();
                for (int i = 0; i < list2.Count; i++)
                    this.listBox2.Items.Add(list2[i]);

            }
        }

        private void TxB_parts_4_TextChanged(object sender, EventArgs e)
        {
            if (txB_parts_4.Text.Length > 0)
            {
                for (int i = 0; i < this.listBox2.Items.Count; i++)
                    if (!this.listBox2.Items[i].ToString().Contains(txB_parts_4.Text))
                    {
                        this.listBox2.Items.RemoveAt(i);
                        i--;
                    }
            }
            else
            {
                this.listBox2.Items.Clear();
                for (int i = 0; i < list2.Count; i++)
                    this.listBox2.Items.Add(list2[i]);

            }
        }

        private void TxB_parts_5_TextChanged(object sender, EventArgs e)
        {
            if (txB_parts_5.Text.Length > 0)
            {
                for (int i = 0; i < this.listBox2.Items.Count; i++)
                    if (!this.listBox2.Items[i].ToString().Contains(txB_parts_5.Text))
                    {
                        this.listBox2.Items.RemoveAt(i);
                        i--;
                    }
            }
            else
            {
                this.listBox2.Items.Clear();
                for (int i = 0; i < list2.Count; i++)
                    this.listBox2.Items.Add(list2[i]);

            }
        }

        private void TxB_parts_6_TextChanged(object sender, EventArgs e)
        {
            if (txB_parts_6.Text.Length > 0)
            {
                for (int i = 0; i < this.listBox2.Items.Count; i++)
                    if (!this.listBox2.Items[i].ToString().Contains(txB_parts_6.Text))
                    {
                        this.listBox2.Items.RemoveAt(i);
                        i--;
                    }
            }
            else
            {
                this.listBox2.Items.Clear();
                for (int i = 0; i < list2.Count; i++)
                    this.listBox2.Items.Add(list2[i]);

            }
        }

        private void TxB_parts_7_TextChanged(object sender, EventArgs e)
        {
            if (txB_parts_7.Text.Length > 0)
            {
                for (int i = 0; i < this.listBox2.Items.Count; i++)
                    if (!this.listBox2.Items[i].ToString().Contains(txB_parts_7.Text))
                    {
                        this.listBox2.Items.RemoveAt(i);
                        i--;
                    }
            }
            else
            {
                this.listBox2.Items.Clear();
                for (int i = 0; i < list2.Count; i++)
                    this.listBox2.Items.Add(list2[i]);

            }
        }

        #endregion


    }
}

